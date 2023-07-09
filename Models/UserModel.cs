using System.ComponentModel.DataAnnotations;
namespace MessageAppBack.Models
{
    public class UserModel
    {   
        
        
        [Key]
        public int UserId { get; set; }

     
        [Required]
        public string Username { get; set; }=String.Empty;

        [Required]
        public string Email { get; set; }=String.Empty;

      
}}
