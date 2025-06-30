namespace BackEndDawa.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set ; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string RucNumber { get; set; } = string.Empty;
        public DateTime RegisterDate{ get; set; }
        public Boolean Status { get; set; }

    }
}
