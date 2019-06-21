using System.ComponentModel.DataAnnotations;

namespace DataTransferObjects.Entities.Identity
{
    public class RegisterUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmedPassword { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
