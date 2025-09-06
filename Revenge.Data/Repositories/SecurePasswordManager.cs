using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

//Hecho por Yoeri Pichardo
public sealed class SecurePasswordManager
{
    #region Constantes de Configuración

    private const int MIN_WORK_FACTOR = 12;
    private const int MAX_WORK_FACTOR = 15;
    private const int DEFAULT_WORK_FACTOR = 13;
    private const int MIN_PASSWORD_LENGTH = 8;
    private const int MAX_PASSWORD_LENGTH = 64;

    #endregion

    #region Propiedades Públicas

    public int WorkFactor { get; }
    public bool ValidatePasswordStrength { get; }

    #endregion

    #region Constructores

    public SecurePasswordManager() : this(DEFAULT_WORK_FACTOR, true)
    {
    }

    public SecurePasswordManager(int workFactor, bool validatePasswordStrength = true)
    {
        if (workFactor < MIN_WORK_FACTOR || workFactor > MAX_WORK_FACTOR)
        {
            throw new ArgumentOutOfRangeException(
                nameof(workFactor),
                $"El factor de trabajo debe estar entre {MIN_WORK_FACTOR} y {MAX_WORK_FACTOR}");
        }

        WorkFactor = workFactor;
        ValidatePasswordStrength = validatePasswordStrength;
    }

    #endregion

    #region Métodos Públicos de Hashing

    public string HashPassword(string password)
    {
        try
        {
            ValidatePasswordInput(password);

            if (ValidatePasswordStrength)
            {
                ValidatePasswordComplexity(password);
            }

            var normalizedPassword = NormalizePassword(password);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(normalizedPassword, WorkFactor);

            ClearSensitiveData(ref normalizedPassword);

            return hashedPassword;
        }
        catch (Exception ex) when (!(ex is ArgumentException))
        {
            throw new InvalidOperationException("Error al generar el hash de la contraseña", ex);
        }
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("La contraseña no puede estar vacía", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                throw new ArgumentException("El hash no puede estar vacío", nameof(hashedPassword));
            }

            if (!IsValidBCryptHash(hashedPassword))
            {
                return false;
            }

            var normalizedPassword = NormalizePassword(password);
            bool isValid = BCrypt.Net.BCrypt.Verify(normalizedPassword, hashedPassword);

            ClearSensitiveData(ref normalizedPassword);

            return isValid;
        }
        catch (Exception ex) when (!(ex is ArgumentException))
        {
            return false;
        }
    }

    #endregion

    #region Métodos Públicos de Utilidad

    public bool NeedsRehash(string hashedPassword)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(hashedPassword) || !IsValidBCryptHash(hashedPassword))
            {
                return true;
            }

            var currentWorkFactor = ExtractWorkFactor(hashedPassword);
            return currentWorkFactor < WorkFactor;
        }
        catch
        {
            return true;
        }
    }

    public PasswordStrengthResult ValidatePasswordStrengthDetailed(string password)
    {
        var result = new PasswordStrengthResult();

        if (string.IsNullOrEmpty(password))
        {
            result.AddError("La contraseña no puede estar vacía");
            return result;
        }

        if (password.Length < MIN_PASSWORD_LENGTH)
        {
            result.AddError($"La contraseña debe tener al menos {MIN_PASSWORD_LENGTH} caracteres");
        }

        if (password.Length > MAX_PASSWORD_LENGTH)
        {
            result.AddError($"La contraseña no puede tener más de {MAX_PASSWORD_LENGTH} caracteres");
        }

        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            result.AddError("La contraseña debe contener al menos una letra minúscula");
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            result.AddError("La contraseña debe contener al menos una letra mayúscula");
        }

        if (!Regex.IsMatch(password, @"[0-9]"))
        {
            result.AddError("La contraseña debe contener al menos un número");
        }

        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>?]"))
        {
            result.AddError("La contraseña debe contener al menos un carácter especial");
        }

        if (ContainsCommonWeakPatterns(password))
        {
            result.AddError("La contraseña contiene patrones comunes débiles");
        }

        return result;
    }

    public string GenerateSecurePassword(int length = 16)
    {
        if (length < 12 || length > MAX_PASSWORD_LENGTH)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "La longitud debe estar entre 12 y 64");
        }

        const string lowercase = "abcdefghijklmnopqrstuvwxyz";
        const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string digits = "0123456789";
        const string special = "!@#$%^&*()_+-=[]{}|;:,.<>?";
        const string allChars = lowercase + uppercase + digits + special;

        using var rng = RandomNumberGenerator.Create();
        var password = new StringBuilder(length);
        var buffer = new byte[4];

        password.Append(GetRandomChar(lowercase, rng, buffer));
        password.Append(GetRandomChar(uppercase, rng, buffer));
        password.Append(GetRandomChar(digits, rng, buffer));
        password.Append(GetRandomChar(special, rng, buffer));

        for (int i = 4; i < length; i++)
        {
            password.Append(GetRandomChar(allChars, rng, buffer));
        }

        return ShuffleString(password.ToString(), rng, buffer);
    }

    #endregion

    #region Métodos Privados de Validación

    private void ValidatePasswordInput(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("La contraseña no puede estar vacía", nameof(password));
        }

        if (password.Length > MAX_PASSWORD_LENGTH)
        {
            throw new ArgumentException($"La contraseña no puede tener más de {MAX_PASSWORD_LENGTH} caracteres",
                nameof(password));
        }
    }

    private void ValidatePasswordComplexity(string password)
    {
        var result = ValidatePasswordStrengthDetailed(password);
        if (!result.IsValid)
        {
            throw new ArgumentException($"Contraseña no cumple requisitos de seguridad: {string.Join(", ", result.Errors)}");
        }
    }

    private bool IsValidBCryptHash(string hash)
    {
        return !string.IsNullOrWhiteSpace(hash) &&
               hash.Length == 60 &&
               hash.StartsWith("$2") &&
               Regex.IsMatch(hash, @"^\$2[abyxy]?\$\d{2}\$[A-Za-z0-9./]{53}$");
    }

    private int ExtractWorkFactor(string hash)
    {
        var parts = hash.Split('$');
        if (parts.Length >= 3 && int.TryParse(parts[2], out int workFactor))
        {
            return workFactor;
        }
        return 0;
    }

    private bool ContainsCommonWeakPatterns(string password)
    {
        var lowerPassword = password.ToLowerInvariant();
        var sequentialPatterns = new[] { "123", "abc", "qwe", "asd", "zxc" };
        var repetitivePatterns = new[] { "111", "aaa", "000" };

        foreach (var pattern in sequentialPatterns.Concat(repetitivePatterns))
        {
            if (lowerPassword.Contains(pattern))
            {
                return true;
            }
        }

        for (int i = 0; i < password.Length - 2; i++)
        {
            if (password[i] == password[i + 1] && password[i + 1] == password[i + 2])
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region Métodos Privados de Utilidad

    private string NormalizePassword(string password)
    {
        return Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(password));
    }

    private void ClearSensitiveData(ref string sensitiveData)
    {
        if (sensitiveData != null)
        {
            sensitiveData = null;
            GC.Collect();
        }
    }

    private char GetRandomChar(string charset, RandomNumberGenerator rng, byte[] buffer)
    {
        rng.GetBytes(buffer);
        var randomValue = BitConverter.ToUInt32(buffer, 0);
        return charset[(int)(randomValue % charset.Length)];
    }

    private string ShuffleString(string input, RandomNumberGenerator rng, byte[] buffer)
    {
        var array = input.ToCharArray();

        for (int i = array.Length - 1; i > 0; i--)
        {
            rng.GetBytes(buffer);
            var randomValue = BitConverter.ToUInt32(buffer, 0);
            int j = (int)(randomValue % (i + 1));

            (array[i], array[j]) = (array[j], array[i]);
        }

        return new string(array);
    }

    #endregion
}

public class PasswordStrengthResult
{
    private readonly List<string> _errors = new List<string>();
    public bool IsValid => _errors.Count == 0;
    public IReadOnlyList<string> Errors => _errors.AsReadOnly();

    internal void AddError(string error)
    {
        _errors.Add(error);
    }
}