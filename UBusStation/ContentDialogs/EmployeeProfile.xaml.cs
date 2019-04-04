using System;
using System.Collections.Generic;
using System.Linq;
using UBusStation.ContentDialogs.src;
using UBusStation.Context;
using UBusStation.Entities.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace UBusStation.ContentDialogs
{
    public sealed partial class EmployeeProfile : ContentDialog
    {
        public Employee Employee { get; private set; }
        private static List<Position> positions;

        public EmployeeProfile()
        {
            this.InitializeComponent();
            LoadPositions();
        }

        public EmployeeProfile(Employee employee)
        {
            this.InitializeComponent();
            LoadPositions();
            Employee = employee;
            TbFirstName.Text = Employee.FirstName;
            TbLastName.Text = Employee.LastName;
            TbPatronymic.Text = Employee.Patronymic;
            TbPassportID.Text = Employee.PassportID.ToString();
            TbUsername.Text = Employee.Username;
            CbPostion.PlaceholderText = positions.First(p => p.Id == Employee.PositionId).Vocation;
            CbPostion.SelectedItem = CbPostion.Items.First(p => ((Position)p).Id == Employee.PositionId);
            PbPassword.Password = Employee.Password;
            PbPassword.Visibility = Visibility.Collapsed;
        }

        private void LoadPositions()
        {
            if (positions == null)
            {
                using (BusStationDatabase db = new BusStationDatabase())
                {
                    positions = db.Position.ToList();
                }
            }
            CbPostion.ItemsSource = positions;
            CbPostion.DisplayMemberPath = "Vocation";

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (
                TbFirstName.Text.Equals(String.Empty) ||
                TbLastName.Text.Equals(String.Empty) ||
                TbPassportID.Text.Equals(String.Empty) ||
                TbUsername.Text.Equals(String.Empty) ||
                CbPostion.SelectedItem == null ||
                PbPassword.Password.Equals(String.Empty)
                )
            {
                args.Cancel = true;
                return;
            }

            if (!CheckForm.IsPasspordId(TbPassportID.Text))
            {
                args.Cancel = true;
                return;
            }

            if (Employee == null) Employee = new Employee();
            Employee.FirstName = TbFirstName.Text;
            Employee.LastName = TbLastName.Text;
            Employee.Patronymic = TbPatronymic.Text;
            Employee.PassportID = TbPassportID.Text;
            Employee.Username = TbUsername.Text;
            Employee.PositionId = ((Position)CbPostion.SelectedItem).Id;
            Employee.Position = null;
            Employee.Password = PbPassword.Password;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
