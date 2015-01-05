using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel.Kassasysteem
{
    class DoorgevoerdVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Doorgevoerd"; }
        }

        private double _totaal;
        public double Totaal
        {
            get { return _totaal; }
            set { _totaal = value; OnPropertyChanged("Totaal"); }
        }

        private double _nieuwSaldo;
        public double NieuwSaldo
        {
            get { return _nieuwSaldo; }
            set { _nieuwSaldo = value; OnPropertyChanged("NieuwSaldo"); }
        }

        public ICommand NewCommand
        {
            get { return new RelayCommand(New); }
        }

        private void New()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.CurrentKlant = null;
            appvm.Bestelling = null;
            appvm.Totaal = 0;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new KlantenKaartScannenVM());
            }
        }
        
        public DoorgevoerdVM()
        {
            GegevensLaden();
        }

        private async void GegevensLaden()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/plaatsorder?id="+appvm.CurrentKlant.Id);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    NieuwSaldo = JsonConvert.DeserializeObject<double>(json);
                    Totaal = appvm.Totaal;
                }
            }
        }
        

    }
}
