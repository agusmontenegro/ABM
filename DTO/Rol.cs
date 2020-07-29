using System.Collections.Generic;

namespace ABM.DTO
{
    public class Rol
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public List<Funcionality> Funcionalities { get; set; }
    }
}