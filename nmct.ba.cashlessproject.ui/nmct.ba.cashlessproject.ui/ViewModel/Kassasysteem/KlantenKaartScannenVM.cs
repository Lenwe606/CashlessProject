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
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel.Kassasysteem
{
    class KlantenKaartScannenVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Kaart scannen"; }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        private string _klantNaam;
        public string KlantNaam
        {
            get { return _klantNaam; }
            set { _klantNaam = value; OnPropertyChanged("Klant"); }
        }

        private double _saldo;
        public double Saldo
        {
            get { return _saldo; }
            set { _saldo = value; OnPropertyChanged("Saldo"); }
        }

        private int _kaartCode;
        public int KaartCode
        {
            get { return _kaartCode; }
            set { _kaartCode = value; OnPropertyChanged("KaartCode"); }
        }

        public ICommand LoadCommand
        {
            get { return new RelayCommand(Load); }
        }

        public ICommand LogoutCommand
        {
            get { return new RelayCommand(Logout); }
        }

        private void Logout()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.CurrentMedewerker = null;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new LoginVM());
            }
        }

        private bool test;
        
        private void Load()
        {
            GetKlant();

            if (test != false)
            {
                SetInfo();
                ChangePage();
            }
        }
        
        private async void GetKlant()
        {
            int code = KaartCode;

            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/klantverkrijgen?code="+code);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Klant selected = JsonConvert.DeserializeObject<Klant>(json);

                    if(selected.Id != 0)
                    {
                        ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                        appvm.CurrentKlant = selected;
                        test = true;
                    }
                    else
                    {
                        test = false;
                        Error = "Kaart niet herkend!";
                    }
                }

            }
        }

        private void SetInfo()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;


            KlantNaam = appvm.CurrentKlant.KlantNaam;
            Saldo = appvm.CurrentKlant.Saldo;
            Error = "u wordt doorgestuurd!";
        }

        private void ChangePage()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new BestellingVM());
            }
        }
        
    }
}
