using System.ComponentModel.DataAnnotations;

namespace VenusOnCrisis.DTOs
{
    public class RegisterDto
    {
        [Required] // class-validator
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}