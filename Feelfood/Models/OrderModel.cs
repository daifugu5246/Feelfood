using Feelfood.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Feelfood.Models
{
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Canteen { get; set; }
        [Required]
        public string? Restaurent { get; set; }
        [Required]
        public string? Menu { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public string? UserId { get; set; }
        public FeelfoodUser? User { get; set; }
        public int? JobId { get; set; }
        public JobModel? Job { get; set; }

        public OrderModel() 
        { 
            Status = ORDERSTATUS.WAITING.ToString();

        }
    }
}
enum ORDERSTATUS
{
    WAITING,
    ON_BUYING,
    ON_GOING
};