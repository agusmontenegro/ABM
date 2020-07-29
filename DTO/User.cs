using System.Collections.Generic;

namespace ABM.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int CountFailedAttempts { get; set; }
        public string Email { get; set; }
        public List<Phone> Phones { get; set; }
        public List<Address> Addresses { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public bool LoginSuccess { get; set; }
        public List<Rol> Roles { get; set; }
        public Rol RolActivo { get; set; }
    }
}