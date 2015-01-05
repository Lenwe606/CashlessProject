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

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class KlantVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Klant pagina"; }
        }

        public ICommand AccountCommand
        {
            get { return new RelayCommand(ChangeToAccount); }
        }

        private void ChangeToAccount()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new AccountVM());
            }
        }

        public ICommand LogoutCommand
        {
            get { return new RelayCommand(Logout); }
        }

        private void Logout()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            ApplicationVM.token = null;
            appvm.ChangePage(new LoginVM());
        }

        public KlantVM()
        {
            if (ApplicationVM.token != null)
            {
                GetKlanten();
            }
        }

        public ICommand ProductCommand
        {
            get { return new RelayCommand(ChangeToProduct); }
        }

        public ICommand MedewerkerCommand
        {
            get { return new RelayCommand(ChangeToMedewerker); }
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

        private ObservableCollection<Klant> _klanten;
        public ObservableCollection<Klant> Klanten
        {
            get { return _klanten; }
            set { _klanten = value; OnPropertyChanged("Klanten"); }
        }

        private void ChangeToProduct()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new ProductVM());
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


        private async void GetKlanten()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/klant");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Klanten = JsonConvert.DeserializeObject<ObservableCollection<Klant>>(json);
                }
            }
        }

        private Klant _selectedKlant;
        public Klant SelectedKlant
        {
            get { return _selectedKlant; }
            set { _selectedKlant = value; OnPropertyChanged("SelectedKlant"); }
        }

        public ICommand SaveCommand
        {
            get { return new RelayCommand(SaveKlant); }
        }

        public ICommand DeleteCommand
        {
            get { return new RelayCommand(DeleteKlant); }
        }


        private async void SaveKlant()
        {
            string input = JsonConvert.SerializeObject(SelectedKlant);

                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PutAsync("http://localhost:7535/api/klant", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("error");
                    }
                }
            
        }

        private async void DeleteKlant()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:7535/api/klant/" + SelectedKlant.Id);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("error");
                }
                else
                {
                    Klanten.Remove(SelectedKlant);
                }
            }
        }
    }
}
