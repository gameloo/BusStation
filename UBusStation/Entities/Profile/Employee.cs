using System.Collections.Generic;

namespace UBusStation.Entities.Profile
{
    public class Employee
    {
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Паспортные данные
        /// </summary>
        public string PassportID { get; set; }
        /// <summary>
        /// Пароль для входа в систему
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Имя учетной записи для входа в систему
        /// </summary>
        public string Username { get; set; }
        
        public int? PositionId { get; set; }
        public Position Position { get; set; }

        public virtual ICollection<Trip> Trip { get; set; }
        public Employee()
        {
            Trip = new List<Trip>();
        }
        
    }
}
