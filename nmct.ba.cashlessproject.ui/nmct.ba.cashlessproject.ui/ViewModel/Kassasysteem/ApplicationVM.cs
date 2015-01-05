using GalaSoft.MvvmLight.CommandWpf;
using Lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.ViewModel.Kassasysteem
{
    class ApplicationVM: ObservableObject
    {
        public static TokenResponse token = null;

        private Klant _currentKlant;
        public Klant CurrentKlant
        {
            get { return _currentKlant; }
            set { _currentKlant = value; }
        }

        private Medewerker _currentMedewerker;
        public Medewerker CurrentMedewerker
        {
            get { return _currentMedewerker; }
            set { _currentMedewerker = value; }
        }

        private ObservableCollection<Product> _bestelling;
        public ObservableCollection<Product> Bestelling
        {
            get { return _bestelling; }
            set { _bestelling = value; }
        }

        private double _totaal;
        public double Totaal
        {
            get { return _totaal; }
            set { _totaal = value; }
        }

        public ApplicationVM()
        {
            CurrentPage = new LoginVM();
        }

        private object currentPage;
        public object CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        private List<IPage> pages;
        public List<IPage> Pages
        {
            get
            {
                if (pages == null)
                    pages = new List<IPage>();
                return pages;
            }
        }

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        public void ChangePage(IPage page)
        {
            CurrentPage = page;
        }
    }
}
