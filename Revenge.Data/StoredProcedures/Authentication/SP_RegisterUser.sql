CREATE OR ALTER PROCEDURE dbo.sp_RegisterUser
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @Email NVARCHAR(255),
    @Password NVARCHAR(255),
    @Cellphone NVARCHAR(20) = NULL,
    @Birthdate DATE = NULL,
    @Directions NVARCHAR(500) = NULL,
    @NumIdentification INT = NULL,
    @CreatedAt DATETIME2,
    @Success BIT OUTPUT,
    @ErrorMessage NVARCHAR(500) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION
        
        -- Verificar si el email ya existe
        IF EXISTS (SELECT 1 FROM Users WHERE email = @Email)
        BEGIN
            SET @Success = 0
            SET @ErrorMessage = 'El email ya está registrado en el sistema'
            ROLLBACK TRANSACTION
            RETURN
        END
        
        
        INSERT INTO Users (
            id, 
            name, 
            email, 
            password, 
            cellphone, 
            birthdate, 
            directions, 
            numIdentification, 
            createdAt, 
            updatedAt
        )
        VALUES (
            @Id,
            @Name,
            @Email,
            @Password,
            @Cellphone,
            @Birthdate,
            @Directions,
            @NumIdentification,
            @CreatedAt,
            @CreatedAt
        )
        
        -- Mensaje de error
        SET @Success = 1
        SET @ErrorMessage = NULL
        
        COMMIT TRANSACTION
        
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        
        SET @Success = 0
        SET @ErrorMessage = ERROR_MESSAGE()
        
    END CATCH
END
GO