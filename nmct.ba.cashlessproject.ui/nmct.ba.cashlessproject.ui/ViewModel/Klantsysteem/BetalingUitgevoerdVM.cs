using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem
{
    class BetalingUitgevoerdVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Betaling uitgevoerd"; }
        }

        private double _nieuwSaldo;
        public double NieuwSaldo
        {
            get { return _nieuwSaldo; }
            set { _nieuwSaldo = value; OnPropertyChanged("NieuwSaldo"); }
        }

        private DispatcherTimer mtimer = new DispatcherTimer();

        public BetalingUitgevoerdVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            NieuwSaldo = appvm.CurrentKlant.Saldo + appvm.Bedrag;

            mtimer.Interval = TimeSpan.FromSeconds(5);
            mtimer.Tick += ChangePage;
            mtimer.Start();


        }

        private void ChangePage(Object sender, System.EventArgs e)
        {
            mtimer.Stop();

            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.Bedrag = 0;
            appvm.CurrentKlant = null;
            appvm.Geannuleerd = false;
            appvm.Username = null;

            appvm.ChangePage(new LoginVM());

        }
        
    }
}
