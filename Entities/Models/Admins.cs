﻿using CapstoneIdeaGenerator.Server.Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneIdeaGenerator.Server.Entities.AuthenticationModels
{
    public class Admins
    {
        [Key]
        public int AdminId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public int Age { get; set; }

        public DateTime? DateJoined { get; set; } = DateTime.UtcNow;

        public string Email { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string? PasswordResetToken { get; set; }

        public DateTime? ResetTokenExpiry { get; set; } = DateTime.UtcNow;
    }
}
