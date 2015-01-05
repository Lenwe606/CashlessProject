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
    class BevestigingVM:ObservableObject,IPage
    {
        public string Name
        {
            get { return "Bevestiging"; }
        }

        private ObservableCollection<Product> _order;
        public ObservableCollection<Product> Order
        {
            get { return _order; }
            set { _order = value; OnPropertyChanged("Order"); }
        }

        private double _totaal;
        public double Totaal
        {
            get { return _totaal; }
            set { _totaal = value; OnPropertyChanged("Totaal"); }
        }

        public ICommand NextCommand
        {
            get { return new RelayCommand(Next); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }
        

        public ICommand BackCommand
        {
            get { return new RelayCommand(Back); }
        }

        private async void Next()
        {
            bool test = false;
            int i = 0;
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            foreach (Product c in Order)
            {
                if (test == false)
                {
                    using (HttpClient client = new HttpClient())
                    {
                       

                        List<double> lijst = new List<double>();
                        lijst.Add(appvm.CurrentKlant.Id);
                        lijst.Add(1);
                        lijst.Add(c.Id);
                        lijst.Add(1);
                        lijst.Add(c.Prijs);

                        string input = JsonConvert.SerializeObject(lijst);

                        client.SetBearerToken(ApplicationVM.token.AccessToken);
                        HttpResponseMessage response = await client.PostAsync("http://localhost:7535/api/plaatsorder", new StringContent(input, Encoding.UTF8, "application/json"));
                        if (response.IsSuccessStatusCode)
                        {
                            i++;
                            if (i == Order.Count)
                            {
                                test = true;
                            }
                        }
                        else
                        {
                            Error = "Kon bestelling niet plaatsen. Probeer het opnieuw";
                        }
                    }
                }            
            
            }

            if(test == true)
                {
                        using (HttpClient client = new HttpClient())
                        {
                            List<double> lijst = new List<double>();
                            lijst.Add(appvm.CurrentKlant.Id);
                            lijst.Add(Totaal);

                            string input = JsonConvert.SerializeObject(lijst);
                            client.SetBearerToken(ApplicationVM.token.AccessToken);
                            HttpResponseMessage response = await client.PutAsync("http://localhost:7535/api/plaatsorder", new StringContent(input, Encoding.UTF8, "application/json"));
                            if (response.IsSuccessStatusCode)
                            {
                                appvm.ChangePage(new DoorgevoerdVM());
                            }
                            else
                            {
                                Error = "Kon bestelling niet plaatsen. Probeer het opnieuw";
                            }
                        }
                }
        }

        private void Back()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new BestellingVM());
            }
        }

        public BevestigingVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if(appvm.Bestelling.Count != 0 && appvm.Totaal != 0)
            {
                Order = appvm.Bestelling;
                Totaal = appvm.Totaal;
            }
        }
        
        
    }
}
