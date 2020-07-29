namespace ABM.DTO
{
    public class Address
    {
        public int Id { get; set; }
        public AddressType AddressType { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public int floor { get; set; }
        public City City { get; set; }
    }
}