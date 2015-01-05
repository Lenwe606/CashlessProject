using GalaSoft.MvvmLight.Command;
using Lib;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.ViewModel.Kassasysteem
{
    class LoginVM: ObservableObject, IPage
    {
        private string usr = "ver1";
        private string pass = "test";

        public string Name
        {
            get { return "Login"; }
        }

        private Medewerker _selectedUser;
        public Medewerker SelectedUser
        {
            get { return _selectedUser; }
            set { _selectedUser = value; OnPropertyChanged("SelectedUser"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }

        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        private ObservableCollection<Medewerker> _gebruikers;
        public ObservableCollection<Medewerker> Gebruikers
        {
            get { return _gebruikers; }
            set { _gebruikers = value; OnPropertyChanged("Gebruikers"); }
        }

        private void Login()
        {
            if(Password == SelectedUser.Wachtwoord)
            {
                ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                appvm.CurrentMedewerker = SelectedUser;
                appvm.ChangePage(new KlantenKaartScannenVM());
            }
            else
            {
                Error = "Gebruikersnaam en wachtwoord kloppen niet";
            }
        }
        

        public LoginVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            ApplicationVM.token = GetToken();

            if (!ApplicationVM.token.IsError)
            {
                getGebruikers();
            }
            else
            {
                Error = "Token error";
            }
        }

        private async void getGebruikers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/medewerker");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Gebruikers = JsonConvert.DeserializeObject<ObservableCollection<Medewerker>>(json);
                }
            }
        }

        private TokenResponse GetToken()
        {
            OAuth2Client client = new OAuth2Client(new Uri("http://localhost:7535/token"));
            return client.RequestResourceOwnerPasswordAsync(usr, pass).Result;
        }
    }
}
