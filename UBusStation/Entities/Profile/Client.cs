using System.Collections.Generic;

namespace UBusStation.Entities.Profile
{
    public class Client
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
        /// Почта, она же логин
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Номер телефона, он же логин
        /// </summary>
        public string PhoneNumber { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }

        public Client()
        {
            Ticket = new List<Ticket>();
        }

    }
}
