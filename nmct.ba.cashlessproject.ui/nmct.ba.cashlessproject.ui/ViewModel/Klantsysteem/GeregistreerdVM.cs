using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem
{
    class GeregistreerdVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Geregistreerd"; }
        }

        private string _userText;
        public string UserText
        {
            get { return _userText; }
            set { _userText = value; }
        }

        private DispatcherTimer mtimer = new DispatcherTimer();

        public GeregistreerdVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            UserText = appvm.CurrentKlant.KlantNaam + ", U bent nu geregistreerd";

            mtimer.Interval = TimeSpan.FromSeconds(5);
            mtimer.Tick += ChangePage;
            mtimer.Start();
        }

        private void ChangePage(Object sender, System.EventArgs e)
        {
            mtimer.Stop();

            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.CurrentKlant = null;
            appvm.kaartCode = 0;

            appvm.ChangePage(new LoginVM());
        }
        
    }
}
