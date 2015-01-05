using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem
{
    class BevestigingVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Bevestiging"; }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; OnPropertyChanged("FullName"); }
        }

        private string _adres;
        public string Adres
        {
            get { return _adres; }
            set { _adres = value; OnPropertyChanged("Adres"); }
        }

        private string _adres2;
        public string Adres2
        {
            get { return _adres2; }
            set { _adres2 = value; OnPropertyChanged("Adres2"); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }
        
        public ICommand NextCommand
        {
            get { return new RelayCommand(Next); }
        }

        public ICommand BackCommand
        {
            get { return new RelayCommand(Back); }
        }

        private void Back()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new RegistratieVM());
        }

        public BevestigingVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if(appvm.CurrentKlant != null)
            {
                FullName = appvm.CurrentKlant.KlantNaam;
                string[] delen = appvm.CurrentKlant.Adres.Split(',');
                Adres = delen[0];
                Adres2 = delen[1];
                
            }
            else
            {
                appvm.ChangePage(new RegistratieVM());
            }
        }

        private async void Next()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            string input = JsonConvert.SerializeObject(appvm.CurrentKlant);

            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.PostAsync("http://localhost:7535/api/klantverkrijgen", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    ChangePage();
                }
                else
                {
                    Console.WriteLine("error");
                    Error = "Registratie mislukt! Probeer het opnieuw.";
                }
            }
        }

        private void ChangePage()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            appvm.ChangePage(new GeregistreerdVM());
        }
    }
}
