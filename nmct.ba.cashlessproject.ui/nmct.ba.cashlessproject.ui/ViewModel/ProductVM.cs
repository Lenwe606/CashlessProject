using GalaSoft.MvvmLight.Command;
using Lib;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.api.helper;
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
    class ProductVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Product pagina"; }
        }
        public ICommand LogoutCommand
        {
            get { return new RelayCommand(Logout); }
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

        private void Logout()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            ApplicationVM.token = null;
            appvm.ChangePage(new LoginVM());
        }

        public ProductVM()
        {
            if (ApplicationVM.token != null)
            {
                GetProducten();
            }
        }

        public ICommand MedewerkerCommand
        {
            get { return new RelayCommand(ChangeToMedewerker); }
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

        private void ChangeToKlant()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new KlantVM());
            }
        }

        private ObservableCollection<Product> _producten;
        public ObservableCollection<Product> Producten
        {
            get { return _producten; }
            set { _producten = value; OnPropertyChanged("Producten"); }
        }

        private async void GetProducten()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:7535/api/product");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Producten = JsonConvert.DeserializeObject<ObservableCollection<Product>>(json);
                }
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
        }

        public ICommand NewCommand
        {
            get { return new RelayCommand(NewProduct); }
        }

        public ICommand SaveCommand
        {
            get { return new RelayCommand(SaveProduct); }
        }

        public ICommand DeleteCommand
        {
            get { return new RelayCommand(DeleteProduct); }
        }

        private void NewProduct()
        {
            Product c = new Product();
            Producten.Add(c);
            SelectedProduct = c;
        }

        private async void SaveProduct()
        {
            string input = JsonConvert.SerializeObject(SelectedProduct);

            // check insert (no ID assigned) or update (already an ID assigned)
            if (SelectedProduct.Id == 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PostAsync("http://localhost:7535/api/product", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        string output = await response.Content.ReadAsStringAsync();
                        SelectedProduct.Id = Int32.Parse(output);
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
                    HttpResponseMessage response = await client.PutAsync("http://localhost:7535/api/product", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("error");
                    }
                }
            }
        }

        private async void DeleteProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync("http://localhost:7535/api/product/" + SelectedProduct.Id);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("error");
                }
                else
                {
                    Producten.Remove(SelectedProduct);
                }
            }
        }
    }
}
