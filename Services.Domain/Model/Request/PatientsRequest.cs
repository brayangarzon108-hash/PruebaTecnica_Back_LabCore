namespace StoreSample.Domain.Model.Dto
{
    public class PatientsRequest
    {
        public PatientsRequest()
        {
        }
        public int Id { get; set; }
        public int TypeDocument { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string? UserId { get; set; }       
        public bool? Enabled { get; set; }
    }
}
