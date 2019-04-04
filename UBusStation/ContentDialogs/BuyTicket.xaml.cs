using System;
using System.Collections.Generic;
using System.Linq;
using UBusStation.Context;
using UBusStation.Entities;
using UBusStation.Entities.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace UBusStation.ContentDialogs
{
    public sealed partial class BuyTicket : ContentDialog
    {
        public Ticket Ticket { get; private set; }
        private Client client;
        private Trip trip;

        public BuyTicket()
        {
            this.InitializeComponent();
            IsPrimaryButtonEnabled = false;
        }

        public BuyTicket(Client client, Trip trip)
        {
            this.InitializeComponent();
            this.client = client;
            this.trip = trip;
            IsPrimaryButtonEnabled = false;
            TbBuyer.Text = String.Format("{0} {1}", client.FirstName, client.LastName);
            TbTripNumber.Text = trip.TripNumber;
            TbTime.Text = trip.DateDep;
            TbCoast.Text = new Random().Next(2000).ToString();

            using (BusStationDatabase db = new BusStationDatabase())
            {
                var occupiedSeat = from i in db.Ticket
                                   where i.TripId == trip.Id
                                   select i.Seat;
                var allSeat = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
                var freeSeat = from i in allSeat
                              where !(occupiedSeat.Any(u => u == i))
                              select i;
                CbSeat.ItemsSource = freeSeat;
                if (freeSeat.Count() == 0) CbAgree.IsEnabled = false;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Ticket = new Ticket()
            {
                ClientId = client.Id,
                TripId = trip.Id,
                Coast = Double.Parse(TbCoast.Text),
                DateBuy = DateTime.UtcNow.ToString("yyyy - MM - ddTHH:mm:ssZ"),
                Seat = (int)CbSeat.SelectedItem
            };
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
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
