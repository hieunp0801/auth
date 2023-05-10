using System.ComponentModel.DataAnnotations;

namespace auth.Models
{
    public class Role
    {
        [Key]
        public int id {get;set;}
        public string role_name {get;set;}
    }
}