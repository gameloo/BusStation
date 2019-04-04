using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UBusStation.ContentDialogs;
using UBusStation.Context;
using UBusStation.Entities.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace UBusStation.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class HRWorkspace : Page
    {
        private Employee user;
        private ObservableCollection<Employee> employees;
        private List<Position> positions;

        public HRWorkspace()
        {
            this.InitializeComponent();

            using (BusStationDatabase db = new BusStationDatabase())
            {
                employees = new ObservableCollection<Employee>(db.Employee);
                positions = db.Position.ToList();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                user = (Employee)e.Parameter;
            }
        }

        private async void AddEmpButton_Click(object sender, RoutedEventArgs e)
        {
            var addnewEmployeeDialog = new EmployeeProfile();
            var result = await addnewEmployeeDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (AddNewEmployee(addnewEmployeeDialog.Employee))
                {
                    addnewEmployeeDialog.Employee.Position = positions.First(p => p.Id == addnewEmployeeDialog.Employee.PositionId);
                    employees.Add(addnewEmployeeDialog.Employee);
                }
                else
                {
                    var errorDialog = new Error("Не удалось добавить сотрудника");
                    await errorDialog.ShowAsync();
                }
            }
        }

        private async void EditEmpButton_Click(object sender, RoutedEventArgs e)
        {

            if (LvEmployees.SelectedItem == null) return;
            var editEmployeeDialog = new EmployeeProfile((Employee)LvEmployees.SelectedItem);
            var result = await editEmployeeDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (EditEmployee(editEmployeeDialog.Employee))
                {
                    editEmployeeDialog.Employee.Position = positions.First(p => p.Id == editEmployeeDialog.Employee.PositionId);
                    LvEmployees.ItemsSource = null;
                    LvEmployees.ItemsSource = employees;
                }
                else
                {
                    var errorDialog = new Error("Не удалось обновить профиль сотрудника");
                    await errorDialog.ShowAsync();
                }
            }
        }

        private void DelEmpButton_Click(object sender, RoutedEventArgs e)
        {
            if (LvEmployees.SelectedItem == null) return;
            RemoveEmployeeFromDB((Employee)LvEmployees.SelectedItem);
            employees.Remove((Employee)LvEmployees.SelectedItem);
        }

        private bool AddNewEmployee(Employee employee)
        {
            using (BusStationDatabase db = new BusStationDatabase())
            {
                try
                {
                    db.Employee.Add(employee);
                    db.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }

        private bool EditEmployee(Employee employee)
        {
            using (BusStationDatabase db = new BusStationDatabase())
            {
                try
                {
                    db.Employee.Update(employee);
                    db.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }

        }

        private void RemoveEmployeeFromDB(Employee employee)
        {
            using (BusStationDatabase db = new BusStationDatabase())
            {
                db.Employee.Remove(employee);
                db.SaveChanges();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
