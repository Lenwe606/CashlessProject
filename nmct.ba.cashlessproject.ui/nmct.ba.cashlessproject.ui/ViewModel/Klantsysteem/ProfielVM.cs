using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem
{
    class ProfielVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Profiel"; }
        }

        private string _fullname;
        public string FullName
        {
            get { return _fullname; }
            set { _fullname = value; OnPropertyChanged("FullName"); }
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

        private byte[] _foto;
        public byte[] Foto
        {
            get { return _foto; }
            set { _foto = value; OnPropertyChanged("Foto"); }
        }

        private double _saldo;
        public double Saldo
        {
            get { return _saldo; }
            set { _saldo = value; OnPropertyChanged("Saldo"); }
        }
        
        public ICommand LogoutCommand
        {
            get { return new RelayCommand(Logout); }
        }

        public ICommand SaldoCommand
        {
            get { return new RelayCommand(SaldoVeranderen); }
        }

        private void Logout()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.CurrentKlant = null;

            appvm.ChangePage(new LoginVM());
        }

        private void SaldoVeranderen()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new SaldoOpladenVM());
        }

        public ProfielVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if(appvm.CurrentKlant != null || appvm.CurrentKlant.Id != 0)
            {
                FullName = appvm.CurrentKlant.KlantNaam;
                string[] delen = appvm.CurrentKlant.Adres.Split(',');
                Adres = delen[0];
                Adres2 = delen[1];
                Foto = appvm.CurrentKlant.Foto;
                Saldo = appvm.CurrentKlant.Saldo;
            }
            else
            {
                appvm.ChangePage(new LoginVM());
            }
        }
    }

}
