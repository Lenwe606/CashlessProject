using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem
{
    class SaldoOpladenVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Profiel"; }
        }

        private double _huidigSaldo;
        public double HuidigSaldo
        {
            get { return _huidigSaldo; }
            set { _huidigSaldo = value; OnPropertyChanged("HuidigSald"); }
        }

        private int _bedrag;
        public int Bedrag
        {
            get { return _bedrag; }
            set { _bedrag = value; OnPropertyChanged("Bedrag"); }
        }

        private double _nieuwSaldo;
        public double NieuwSaldo
        {
            get { return _nieuwSaldo; }
            set { _nieuwSaldo = value; OnPropertyChanged("NieuwSaldo"); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        public ICommand SaldoUpCommand
        {
            get { return new RelayCommand(SaldoUp); }
        }

        public ICommand SaldoDownCommand
        {
            get { return new RelayCommand(SaldoDown); }
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

            appvm.ChangePage(new ProfielVM());
        }

        private void SaldoUp()
        {
            if (Bedrag <= 95)
            {
                Bedrag += 5;
                NieuwSaldo = HuidigSaldo + Bedrag;
            }
        }

        private void SaldoDown()
        {
            if (Bedrag >= 10)
            {
                Bedrag -= 5;
                NieuwSaldo = HuidigSaldo + Bedrag;
            }
        }

        private void Next()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.Bedrag = Bedrag;

            appvm.ChangePage(new BetalingUitvoerenVM());
        } 

        public SaldoOpladenVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if(appvm.CurrentKlant.Id != 0 || appvm.CurrentKlant != null)
            {
                if (appvm.Geannuleerd == true)
                {
                    HuidigSaldo = appvm.CurrentKlant.Saldo;
                    Bedrag = appvm.Bedrag;
                    NieuwSaldo = HuidigSaldo + Bedrag;
                    Error = "Betaling geannuleerd!";
                }
                else
                {
                    HuidigSaldo = appvm.CurrentKlant.Saldo;
                    Bedrag = 5;
                    NieuwSaldo = HuidigSaldo + Bedrag;
                }
            }
        }
        
        
    }
}
