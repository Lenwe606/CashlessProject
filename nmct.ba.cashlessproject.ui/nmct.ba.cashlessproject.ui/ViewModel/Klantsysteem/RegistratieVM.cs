using GalaSoft.MvvmLight.Command;
using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem
{
    class RegistratieVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Registratie"; }
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
            appvm.kaartCode = 0;
            appvm.ChangePage(new LoginVM());
        }


        private int nr;
        private Random mrandom = new Random();
        private DispatcherTimer mtimer = new DispatcherTimer();

        public RegistratieVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (appvm.CurrentKlant == null || appvm.CurrentKlant.Id == 0)
            {
                nr = mrandom.Next(1, 4);
                mtimer.Interval = TimeSpan.FromSeconds(4);
                mtimer.Tick += LoadKlant;
                mtimer.Start();
            }
            else
            {
                FullName = appvm.CurrentKlant.KlantNaam;
                string[] delen = appvm.CurrentKlant.Adres.Split(',');
                Adres = delen[0];
                Adres2 = delen[1];
            }
        }

        private void LoadKlant(object sender, System.EventArgs e)
        {
            mtimer.Stop();

            if(nr == 1)
            {
                FullName = "Jan Janssens";
                Adres = "Fortstraat 47";
                Adres2 = "9700 Oudenaarde";
            }
            else if(nr == 2)
            {
                FullName = "Pol Peeters";
                Adres = "Graaf Karel de Goedelaan 5";
                Adres2 = "8500 Kortrijk";
            }
            else
            {
                FullName = "Bart De Bakker";
                Adres = "Veldstraat 25";
                Adres2 = "9000 Gent";
            }
        }

        private void Next()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            Klant nieuw = new Klant();
            nieuw.KlantNaam = FullName;
            nieuw.KaartCode = appvm.kaartCode;
            nieuw.Adres = Adres + ", " + Adres2;
            nieuw.Foto = null;
            nieuw.Saldo = 0;
            nieuw.Id = 1;

            appvm.CurrentKlant = nieuw;

            appvm.ChangePage(new BevestigingVM());
        }
        
        
    }
}
