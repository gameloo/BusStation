using System;
using System.Collections.ObjectModel;
using System.Linq;
using UBusStation.ContentDialogs;
using UBusStation.Context;
using UBusStation.Entities;
using UBusStation.Entities.Profile;
using UBusStation.Service;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace UBusStation
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Trip> trips;
        private Client currentUser;

        public MainPage()
        {
            this.InitializeComponent();
            using (BusStationDatabase db = new BusStationDatabase())
            {
                trips = new ObservableCollection<Trip>(db.Trip.ToList());
            }

        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser != null)
            {
                currentUser = null;
                TbWelcome.Text = "";
                BtnLogin.Content = "Войти";
                return;
            }

            Login loginDialog = new Login();
            ContentDialogResult result = await loginDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    currentUser = AutorizationService.LoginClient(loginDialog.Username, loginDialog.Password);
                    TbWelcome.Text = String.Format("Добро пожаловать, {0} {1}", currentUser.FirstName, currentUser.LastName);
                    BtnLogin.Content = "Выйти";
                }
                catch (InvalidOperationException)
                {
                    Error errorDialog = new Error("Такого пользователя не существует или пароль введен неверно");
                    await errorDialog.ShowAsync();
                }
            }
        }

        private async void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {

            Registration registrationDialog = new Registration();
            ContentDialogResult result = await registrationDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                if (!AutorizationService.Registration(registrationDialog.Client))
                {
                    Error errorDialog = new Error("Одно из полей задано неверно");
                    await errorDialog.ShowAsync();
                }
            }

        }

        private async void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser == null) return;
            if (LvTrip.SelectedItem == null) return;
            Trip selectedTrip = LvTrip.SelectedItem as Trip;
            BuyTicket buyTicketDialog = new BuyTicket(currentUser, selectedTrip);
            var result = await buyTicketDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                ClientService.BuyTicket(buyTicketDialog.Ticket);
            }
        }

        private async void LoginEmployer_Click(object sender, RoutedEventArgs e)
        {
            Login loginDialog = new Login();
            ContentDialogResult result = await loginDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                try
                {
                    var userEmployee = AutorizationService.LoginEmployee(loginDialog.Username, loginDialog.Password);
                    Frame.Navigate(EmployeeService.LoadWorkspace(userEmployee), userEmployee);
                }
                catch (InvalidOperationException)
                {
                    Error errorDialog = new Error("Такого пользователя не существует или пароль введен неверно");
                    await errorDialog.ShowAsync();
                }
            }
        }
    }
}
