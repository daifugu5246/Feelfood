using Feelfood.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace Feelfood.Models
{

    public class JobModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Canteen { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? OrderId { get; set; }
        public OrderModel? Order { get; set; }
        public FeelfoodUser? User { get; set; }
        public string? UserId { get; set; }

        public JobModel()
        {
            Status = JOBSTATUS.NO_ORDER.ToString();
        }
    }
}
enum JOBSTATUS
{
    NO_ORDER,
    ON_BUYING,
    ON_GOING
};