namespace BackEndDawa.Models
{
    public class Reserve
    {

        public int Id { get; set; }
        public DateTime PickupDate { get; set; }

        public DateTime DropoffDate { get; set; }
        public double Price { get; set; }
        public bool status { get; set; }
        
        //Client relation
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        //Vehicle relation
        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }


    }
}
