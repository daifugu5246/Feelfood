using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Feelfood.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Fname { get; set; }
        [Required]
        public string? Lname { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [NotMapped]
        public string? Comfirm_Password { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? Tel { get; set; }

    }
}
