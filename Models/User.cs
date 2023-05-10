using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BCrypt.Net;

namespace auth.Models
{
    // [Table("users")]
    public class User
    {
        [Key]
        public int id {get;set;}
        public string username {get;set;}
        public string passwordHash {get;set;}
        
    }
}