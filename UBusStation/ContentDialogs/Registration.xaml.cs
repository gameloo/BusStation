using System;
using UBusStation.ContentDialogs.src;
using UBusStation.Entities.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace UBusStation.ContentDialogs
{
    public sealed partial class Registration : ContentDialog
    {
        public Client Client { get; private set; }

        public Registration()
        {
            this.InitializeComponent();
            IsPrimaryButtonEnabled = false;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if ((PbPassword1.Password != PbPassword2.Password) || PbPassword1.Password.Equals(String.Empty) || (TbMail.Text.Equals(String.Empty) && TbPhoneNumber.Text.Equals(String.Empty)))
            {
                args.Cancel = true;
                return;
            }

            if (!TbMail.Text.Equals(String.Empty))
            {
                if (!CheckForm.IsMail(TbMail.Text))
                {
                    args.Cancel = true;
                    return;
                }
            }

            if (!CheckForm.IsPasspordId(TbPassportID.Text))
            {
                args.Cancel = true;
                return;
            }

            Client = new Client()
            {
                FirstName = TbFirstName.Text,
                LastName = TbLastName.Text,
                Patronymic = TbPatronymic.Text,
                PassportID = TbPassportID.Text,
                Mail = TbMail.Text == String.Empty ? null : TbMail.Text,
                PhoneNumber = TbPhoneNumber.Text == String.Empty ? null : TbPhoneNumber.Text,
                Password = PbPassword1.Password
            };
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Client = null;
        }

        private void CbAgree_Checked(object sender, RoutedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }

        private void CbAgree_Unchecked(object sender, RoutedEventArgs e)
        {
            IsPrimaryButtonEnabled = false;
        }
    }
}
