namespace Feelfood.Models
{
    public class AddorderModel
    {
        public JobModel Job { get; set; }
        public OrderModel Order { get; set; }

        public AddorderModel ()
        {
            Job = new JobModel ();
            Order = new OrderModel ();
        }
        public AddorderModel(JobModel job)
        {
            Job = job;
            Order = new OrderModel();
        }
    }
}
