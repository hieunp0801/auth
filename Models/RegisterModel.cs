using System.ComponentModel.DataAnnotations;

namespace auth.Models
{
    public class RegisterModel
    {
        [Required]
        public string username {get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string password {get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string confirmPassword {get;set;}

    }
}