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
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class MedewerkerVM: ObservableObject,IPage
    {
        public string Name
        {
            get { return "Medewerker pagina"; }
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

        public MedewerkerVM()
        {
            if (ApplicationVM.token != null)
            {
                GetMedewerkers();
            }
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

        private ObservableCollection<Medewerker> _medewerkers;
        public ObservableCollection<Medewerker> Medewerkers
        {
            get { return _medewerkers; }
            set { _medewerkers = value; OnPropertyChanged("Medewerkers"); }
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


        private async void GetMedewerkers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/medewerker");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Medewerkers = JsonConvert.DeserializeObject<ObservableCollection<Medewerker>>(json);
                }
            }
        }

        private Medewerker _selectedMedewerker;
        public Medewerker SelectedMedewerker
        {
            get { return _selectedMedewerker; }
            set { _selectedMedewerker = value; OnPropertyChanged("SelectedMedewerker"); }
        }

        public ICommand NewCommand
        {
            get { return new RelayCommand(NewCustomer); }
        }

        public ICommand SaveCommand
        {
            get { return new RelayCommand(SaveCustomer); }
        }

        public ICommand DeleteCommand
        {
            get { return new RelayCommand(DeleteMedewerker); }
        }

        private void NewCustomer()
        {
            Medewerker c = new Medewerker();
            Medewerkers.Add(c);
            SelectedMedewerker = c;
        }

        private async void SaveCustomer()
        {
            string input = JsonConvert.SerializeObject(SelectedMedewerker);

            // check insert (no ID assigned) or update (already an ID assigned)
            if (SelectedMedewerker.Id == 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PostAsync("http://localhost:7535/api/medewerker", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        string output = await response.Content.ReadAsStringAsync();
                        SelectedMedewerker.Id = Int32.Parse(output);
                    }
                    else
                    {
                        Console.WriteLine("error");
                    }
                }
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PutAsync("http://localhost:7535/api/medewerker", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("error");
                    }
                }
            }
        }

        private async void DeleteMedewerker()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:7535/api/medewerker/" + SelectedMedewerker.Id);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("error");
                }
                else
                {
                    Medewerkers.Remove(SelectedMedewerker);
                }
            }
        }



    }
}
