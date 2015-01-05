using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class Product
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _productNaam;
        public string ProductNaam
        {
            get { return _productNaam; }
            set { _productNaam = value; }
        }

        private double _prijs;
        public double Prijs
        {
            get { return _prijs; }
            set { _prijs = value; }
        }

        private int _categorieId;
        public int CategorieId
        {
            get { return _categorieId; }
            set { _categorieId = value; }
        }

        public Product()
        {

        }
        
        
    }
}
