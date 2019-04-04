using System.Collections.Generic;

namespace UBusStation.Entities.Profile
{
    public class Position
    {
        public int Id { get; set; }
        public string Vocation { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }

        public Position()
        {
            Employee = new List<Employee>();
        }
    }
}
