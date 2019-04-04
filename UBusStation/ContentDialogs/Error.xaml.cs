using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace UBusStation.ContentDialogs
{
    public sealed partial class Error : ContentDialog
    {
        public Error()
        {
            this.InitializeComponent();
        }

        public Error(string msg)
        {
            this.InitializeComponent();
            TbMessageError.Text = msg;
        }

        public Error(string msg, string hint)
        {
            this.InitializeComponent();
            TbMessageError.Text = msg;
            TbHint.Text = hint;

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        
    }
}
