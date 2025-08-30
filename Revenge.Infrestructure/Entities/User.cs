using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Revenge.Infrestructure.Entities
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; } = null!;
        public string cedula { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public PhoneAttribute? PhoneAttribute { get; set; }
        public DateOnly? birthDate { get; set; }
        public JsonObject? directions { get; set; }
        public TimestampAttribute? createdAt { get; set; }
        public TimestampAttribute? updatedAt { get; set; }
    }
}