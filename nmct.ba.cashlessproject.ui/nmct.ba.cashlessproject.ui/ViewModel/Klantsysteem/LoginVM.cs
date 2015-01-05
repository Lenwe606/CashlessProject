using GalaSoft.MvvmLight.Command;
using Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem
{
    class LoginVM: ObservableObject, IPage
    {
        private string usr = "ver1";
        private string pass = "test";

        public string Name
        {
            get { return "Login"; }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; OnPropertyChanged("Status"); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        private int _kaartCode;
        public int Kaartcode
        {
            get { return _kaartCode; }
            set { _kaartCode = value; OnPropertyChanged("KaartCode"); }
        }

        public ICommand LoadCommand
        {
            get { return new RelayCommand(Load); }
        }

        public LoginVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            ApplicationVM.token = GetToken();

            if (ApplicationVM.token.IsError)
            {
                Error = "Token error";
            }
        }
        
        private void Load()
        {
            KlantGegevensOphalen();
            Status = "Bezig met laden...";
        }

        private async void KlantGegevensOphalen()
        {
            if (Kaartcode >= 10000 && Kaartcode <= 99999)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/klantverkrijgen?code=" + Kaartcode);
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        Klant nieuw = JsonConvert.DeserializeObject<Klant>(json);

                        if (nieuw.Id != 0)
                        {
                            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                            appvm.CurrentKlant = nieuw;

                            Console.WriteLine("Klant ingelogd");

                            appvm.ChangePage(new ProfielVM());
                        }
                        else
                        {
                            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                            appvm.kaartCode = Kaartcode;
                            Console.WriteLine("Nieuwe klant");
                            appvm.ChangePage(new RegistratieVM());
                        }

                    }
                    
                }
            }
            else
            {
                Status = "";
                Error = "Kaart niet herkend";
            }
        }

        private TokenResponse GetToken()
        {
            OAuth2Client client = new OAuth2Client(new Uri("http://localhost:7535/token"));
            return client.RequestResourceOwnerPasswordAsync(usr, pass).Result;
        }
        
    }
}
