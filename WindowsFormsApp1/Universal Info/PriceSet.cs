using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Universal_Info
{
    public class PriceSet
    {
        public int id;
        public string priceSetName;
        public int priceBreak;
        public double price;
        public double secondPrice;
        public double thirdPrice;
        public int displayQty;
        public int hide;
        public int flatRate;

        public PriceSet(int id, string name, int priceBreak, double price)
        {
            this.id = id;
            this.priceSetName = name;
            this.priceBreak = priceBreak;
            this.price = price;
            this.secondPrice = 0;
            this.thirdPrice = 0;
            this.displayQty = 0;
            this.hide = 0;
            this.flatRate = 0;
        }
    }
}
