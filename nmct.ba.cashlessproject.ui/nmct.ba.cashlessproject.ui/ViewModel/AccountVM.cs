using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.api.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class AccountVM:ObservableObject, IPage
    {
        public string Name
        {
            get { return "Account pagina"; }
        }

        private string _gebruiker;
        public string Gebruiker
        {
            get { return _gebruiker; }
            set { _gebruiker = value; OnPropertyChanged("Gebruiker"); }
        }

        private string _oldPass;
        public string OldPass
        {
            get { return _oldPass; }
            set { _oldPass = value; OnPropertyChanged("OldPass"); }
        }

        private string _newPass;
        public string NewPass
        {
            get { return _newPass; }
            set { _newPass = value; OnPropertyChanged("NewPass"); }
        }

        private string _newPassConfirm;
        public string NewPassConfirm
        {
            get { return _newPassConfirm; }
            set { _newPassConfirm = value; OnPropertyChanged("NewPassConfirm"); }
        }

        private string _tssPass;
        public string TssPass
        {
            get { return _tssPass; }
            set { _tssPass = value; }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }
        
        
        

        public ICommand LogoutCommand
        {
            get { return new RelayCommand(Logout); }
        }

        public ICommand MedewerkerCommand
        {
            get { return new RelayCommand(ChangeToMedewerker); }
        }

        public ICommand ProductCommand
        {
            get { return new RelayCommand(ChangeToProduct); }
        }

        public ICommand KlantCommand
        {
            get { return new RelayCommand(ChangeToKlant); }
        }

        public ICommand KassaCommand
        {
            get { return new RelayCommand(ChangeToKassa); }
        }

        private void ChangeToKassa()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new KassaVM());
            }
        }

        private void ChangeToMedewerker()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new MedewerkerVM());
            }
        }

        private void ChangeToProduct()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new ProductVM());
            }
        }

        private void ChangeToKlant()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new KlantVM());
            }
        }

        private void Logout()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            ApplicationVM.token = null;
            appvm.ChangePage(new LoginVM());
        }

        public AccountVM()
        {
            GetInfo();
        }

        private async void GetInfo()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            string user = Cryptography.Encrypt(appvm.Username);

            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/password?user="+user);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    List<string> info = JsonConvert.DeserializeObject<List<string>>(json);

                    Gebruiker = Cryptography.Decrypt(info[0].ToString());
                    TssPass = Cryptography.Decrypt(info[1].ToString());
                }
            }
        }

        public ICommand SaveCommand
        {
            get { return new RelayCommand(Save); }
        }

        public ICommand ResetCommand
        {
            get { return new RelayCommand(Reset); }
        }

        private void Save()
        { 
            if(OldPass == TssPass)
            {
                if(NewPass == NewPassConfirm)
                {
                    SavePassword();
                }
                else
                {
                    Error = "Het nieuwe wachtwoord is niet gelijk in de 2 velden!";
                }
            }
            else
            {
                Error = "Wachtwoord klopt niet!";
            }
        }

        private async void SavePassword()
        {
            List<string> lijst = new List<string>();
            lijst.Add(Cryptography.Encrypt(NewPass));
            lijst.Add(Cryptography.Encrypt(Gebruiker));

            string input = JsonConvert.SerializeObject(lijst);

            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.PutAsync("http://localhost:7535/api/password", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string output = await response.Content.ReadAsStringAsync();
                    Error = "Wachtwoord opgeslagen!";
                    Reset();
                }
                else
                {
                    Console.WriteLine("error");
                }
            }
        }

        private void Reset()
        {
            OldPass = "";
            NewPass = "";
            NewPassConfirm = "";
        }
    }
}
