using System.Globalization;

namespace BackEndDawa.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Seats { get; set; }
        public string Transmission { get; set; } = string.Empty;
        public string FueType { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public double PricePerDay { get; set; }
        public string Poster { get; set; } = string.Empty;
        public bool Status { get; set; }

        //Company relation
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
