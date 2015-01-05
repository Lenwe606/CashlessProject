using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic;
using System.Threading;
using System.Windows.Threading;
using System.Net.Http;
using Newtonsoft.Json;

namespace nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem
{
    class BetalingUitvoerenVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Betaling"; }
        }

        private int _teBetalen;
        public int TeBetalen
        {
            get { return _teBetalen; }
            set { _teBetalen = value; OnPropertyChanged("TeBetalen"); }
        }

        private int _ingevoerd;
        public int Ingevoerd
        {
            get { return _ingevoerd; }
            set { _ingevoerd = value; OnPropertyChanged("Ingevoerd"); }
        }

        private DispatcherTimer mTimer = new DispatcherTimer();

        public ICommand BackCommand
        {
            get { return new RelayCommand(Back); }
        }

        private void Back()
        {
            mTimer.Stop();
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.Geannuleerd = true;
            appvm.ChangePage(new ProfielVM());
        }

        public BetalingUitvoerenVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if(appvm.Bedrag != 0)
            {
                TeBetalen = appvm.Bedrag;

                mTimer.Interval = TimeSpan.FromSeconds(4);
                mTimer.Tick += ShowPrompt;
                mTimer.Start();
            }
        }

        private void ShowPrompt(Object sender, System.EventArgs e)
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            mTimer.Stop();
            Ingevoerd = TeBetalen;

          SaldoOpladen();

                appvm.ChangePage(new BetalingUitgevoerdVM());

        }

        private async void SaldoOpladen()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            List<double> lijst = new List<double>();
            lijst.Add(appvm.CurrentKlant.Id);
            double bedrag = appvm.Bedrag + appvm.CurrentKlant.Saldo;
            lijst.Add(bedrag);

            string input = JsonConvert.SerializeObject(lijst);

            using (HttpClient client = new HttpClient())
                {
                    client.SetBearerToken(ApplicationVM.token.AccessToken);
                    HttpResponseMessage response = await client.PutAsync("http://localhost:7535/api/klantverkrijgen", new StringContent(input, Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("error");
                    }
                }
        }
        
        
    }
}
