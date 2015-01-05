using GalaSoft.MvvmLight.Command;
using Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel.Kassasysteem
{
    class BestellingVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Kaart scannen"; }
        }

        private string _klantNaam;
        public string KlantNaam
        {
            get { return _klantNaam; }
            set { _klantNaam = value; }
        }

        private double _klantSaldo;
        public double KlantSaldo
        {
            get { return _klantSaldo; }
            set { _klantSaldo = value; }
        }

        private string _medewerkerNaam;
        public string MedewerkerNaam
        {
            get { return _medewerkerNaam; }
            set { _medewerkerNaam = value; }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
        }

        private string _resterendSaldo;
        public string ResterendSaldo
        {
            get { return _resterendSaldo; }
            set { _resterendSaldo = value; OnPropertyChanged("ResterendSaldo"); }
        }
        
        

        private ObservableCollection<Product> _producten;
        public ObservableCollection<Product> Producten
        {
            get { return _producten; }
            set { _producten = value; OnPropertyChanged("Producten"); }
        }

        private ObservableCollection<Product> _order;
        public ObservableCollection<Product> Order
        {
            get { return _order; }
            set { _order = value; OnPropertyChanged("Order"); }
        }
        

        public ICommand DrankenCommand
        {
            get { return new RelayCommand(GetDranken); }
        }

        public ICommand EtenCommand
        {
            get { return new RelayCommand(GetEten); }
        }

        public ICommand AndereCommand
        {
            get { return new RelayCommand(GetAndere); }
        }

        public ICommand AddCommand
        {
            get { return new RelayCommand(Add); }
        }

        public ICommand RemoveCommand
        {
            get { return new RelayCommand(Remove); }
        }

        public ICommand BackCommand
        {
            get { return new RelayCommand(Back); }
        }

        public ICommand NextCommand
        {
            get { return new RelayCommand(Next); }
        }

        private void Back()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.CurrentKlant = null;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new KlantenKaartScannenVM());
            }
        }

        public BestellingVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            Klant currentKlant = appvm.CurrentKlant;
            Medewerker currentMedewerker = appvm.CurrentMedewerker;

            KlantNaam = currentKlant.KlantNaam;
            KlantSaldo = currentKlant.Saldo;
            MedewerkerNaam = currentMedewerker.MedewerkerNaam;
            ResterendSaldo = "0,00";

            if(appvm.Bestelling != null && appvm.Totaal != 0)
            {
                Order = appvm.Bestelling;
                ResterendSaldo = appvm.Totaal.ToString();
            }

            VulLijst();
        }

        private async void VulLijst()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/dranken");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Producten = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                }
            }
        }

        private void Next()
        {
            if(ResterendSaldo != "Onvoldoende saldo" || Order.Count <1)
            {
                ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                appvm.Bestelling = Order;
                appvm.Totaal = Convert.ToDouble(ResterendSaldo);

                appvm.ChangePage(new BevestigingVM());
            }
        }

        private async void GetDranken()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/dranken");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Producten = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                }
            }
        }

        private async void GetEten()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/eten");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Producten = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                }
            }
        }

        private async void GetAndere()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/andere");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Producten = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                }
            }
        }

        private void Add()
        {
            if (ResterendSaldo != "Onvoldoende saldo")
            {
                if (Order == null)
                {
                    Order = new ObservableCollection<Product>();
                    Order.Add(SelectedProduct);
                    ResterendSaldo = (Convert.ToDouble(ResterendSaldo) + SelectedProduct.Prijs).ToString();
                    CheckSaldo();
                }
                else
                {
                    Order.Add(SelectedProduct);
                    ResterendSaldo = (Convert.ToDouble(ResterendSaldo) + SelectedProduct.Prijs).ToString();
                    CheckSaldo();
                }
            }
        }

        private void CheckSaldo()
        {
            if (Convert.ToDouble(ResterendSaldo) > KlantSaldo)
            {
                ResterendSaldo = "Onvoldoende saldo";
            }
        }

        private void Remove()
        {
            if(Order.Count != 0)
            {
                if (ResterendSaldo == "Onvoldoende saldo")
                {
                    Order.RemoveAt(Order.Count - 1);

                    double tss = 0;
                    foreach(Product c in Order)
                    {
                        tss += c.Prijs;
                    }
                    ResterendSaldo = tss.ToString();
                }
                else
                {
                    ResterendSaldo = (Convert.ToDouble(ResterendSaldo) - Order[Order.Count - 1].Prijs).ToString();
                    Order.RemoveAt(Order.Count - 1);
                }
            }
        }
    }
}
