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
    class KassaVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Kassa pagina"; }
        }

        public KassaVM()
        {
            if (ApplicationVM.token != null)
            {
                GetKassas();
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

        public ICommand KlantCommand
        {
            get { return new RelayCommand(ChangeToKlant); }
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

        private ObservableCollection<Kassa> _kassas;
        public ObservableCollection<Kassa> Kassas
        {
            get { return _kassas; }
            set { _kassas = value; OnPropertyChanged("Kassas"); }
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

        private void ChangeToMedewerker()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new MedewerkerVM());
            }
        }


        private async void GetKassas()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/kassa");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Kassas = JsonConvert.DeserializeObject<ObservableCollection<Kassa>>(json);
                }
            }
        }

        private Kassa _selectedKassa;
        public Kassa SelectedKassa
        {
            get { return _selectedKassa; }
            set { _selectedKassa = value; OnPropertyChanged("SelectedKassa"); }
        }

        public ICommand NewCommand
        {
            get { return new RelayCommand(NewKassa); }
        }

        public ICommand SaveCommand
        {
            get { return new RelayCommand(SaveKassa); }
        }

        public ICommand DeleteCommand
        {
            get { return new RelayCommand(DeleteKassa); }
        }

        private void NewKassa()
        {
            Kassa c = new Kassa();
            Kassas.Add(c);
            SelectedKassa = c;
        }

        private async void SaveKassa()
        {
            string input = JsonConvert.SerializeObject(SelectedKassa);

            // check insert (no ID assigned) or update (already an ID assigned)
            if (SelectedKassa.Id == 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PostAsync("http://localhost:7535/api/kassa", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        string output = await response.Content.ReadAsStringAsync();
                        SelectedKassa.Id = Int32.Parse(output);
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
                    HttpResponseMessage response = await client.PutAsync("http://localhost:7535/api/kassa", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("error");
                    }
                }
            }
        }

        private async void DeleteKassa()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:7535/api/kassa/" + SelectedKassa.Id);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("error");
                }
                else
                {
                    Kassas.Remove(SelectedKassa);
                }
            }
        }
    }
}
