using System;
using UBusStation.Entities.Profile;
using UBusStation.Pages;

namespace UBusStation.Service
{
    public class EmployeeService
    {
        public static Type LoadWorkspace(Employee employee)
        {
            if (employee.PositionId == 7) return typeof (HRWorkspace);
            return typeof(MainPage);
        }
    }
}
