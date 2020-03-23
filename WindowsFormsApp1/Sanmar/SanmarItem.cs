using BaseDonneeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Universal_Info.Utils;

namespace WindowsFormsApp1.Sanmar
{
    class SanmarItem : OutsideItem
    {
        public String itemID { get; set; }
        public List<ColorSize> colorSizes {get; set;}
        public String name { get; set; }
        public String imageURL { get; set; }
        public String[] categories { get; set; }
        public String description { get; set; }
        public double price { get; set; }

        public SanmarItem()
        {
            this.itemID = "";
            this.colorSizes = new List<ColorSize>();
            this.name = "";
            this.imageURL = "";
            this.categories = new String[0];
            this.description = "";
            this.price = 0;
        }

        public ProduitDB getProduit()
        {
            ProduitDB produit = new ProduitDB();
            produit.productName = this.name;
            produit.sku = this.itemID;
            produit.description = this.description;
            produit.imageURL = this.imageURL;
            return produit;
        }

        public ProduitDB[] getChildProducts()
        {
            ProduitDB[] childProducts = new ProduitDB[this.colorSizes.Count];
            for(int i=0; i < this.colorSizes.Count; i++)
            {
                ProduitDB produit = this.getProduit();
                produit.sku = this.itemID + "_" + this.colorSizes[i].color + "_" + this.colorSizes[i].size;
                childProducts[i] = produit;
            }
            return childProducts;
        }

        public static String fixSize(String size)
        {
            if (size.Equals("2XL") || size.Equals("2XLT"))
            {
                return "XXL";
            }
            else if (size.Equals("3XLT"))
            {
                return "XXXL";
            }
            else if (size.Equals("XLT"))
            {
                return "XL";
            }
            else if (size.Equals("LT"))
            {
                return "L";
            }
            else if (size.Equals("4XLT"))
            {
                return "XXXXL";
            }
            else if (size.Equals("OSFA"))
            {
                return "";
            }
            return size;
        }
    }
}
