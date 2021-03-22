using System.ComponentModel.DataAnnotations;

namespace VueBookLibrary.ServerApp.Models
{
    public class Credentials
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
