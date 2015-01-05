using GalaSoft.MvvmLight.Command;
using Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.ViewModel.Klantsysteem
{
    class ApplicationVM: ObservableObject
    {
        public static TokenResponse token = null;
        public bool Geannuleerd = false;


        private Klant _currentKlant;
        public Klant CurrentKlant
        {
            get { return _currentKlant; }
            set { _currentKlant = value; }
        }

        private int _bedrag;
        public int Bedrag
        {
            get { return _bedrag; }
            set { _bedrag = value; }
        }

        public ApplicationVM()
        {
            CurrentPage = new LoginVM();
        }

        public string Username = null;
        public int kaartCode = 0;

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
