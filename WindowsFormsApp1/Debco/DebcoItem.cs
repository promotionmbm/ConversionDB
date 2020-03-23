using BaseDonneeConversion;
using System;
using System.Collections;
using WindowsFormsApp1.Universal_Info;
using WindowsFormsApp1.Universal_Info.Utils;

namespace WindowsFormsApp1
{
    class DebcoItem : OutsideItem
    {
        public String itemID { get; set; }
        public String nameEn { get; set; }
        public String nameFr { get; set; }
        public String descriptionEn { get; set; }
        public String descriptionFr { get; set; }
        public ArrayList quantities { get; set; }
        public ArrayList prices { get; set; }
        public String[] colors { get; set; }
        public String category { get; set; }
        public String majorCategory { get; set; }
        public String imageURL { get; set; }

        public ProduitDB getProduit()
        {
            ProduitDB produit = new ProduitDB();
            produit.productName = this.nameEn;
            produit.sku = this.itemID;
            produit.description = this.descriptionEn;
            produit.imageURL = imageURL;
            for(int i = 0; i < this.quantities.Count; i++)
            {
                produit.priceSets.Add(new PriceSet(0, this.nameEn, (int)this.quantities[i], (double)this.prices[i]));
            }
            return produit;
        }

        public ProduitDB[] getChildProducts()
        {
            ProduitDB[] childProducts = new ProduitDB[this.colors.Length];
            for(int i = 0; i < this.colors.Length; i++)
            {
                if (!this.colors[i].Equals(""))
                {
                    ProduitDB produit = this.getProduit();
                    produit.sku = this.itemID + "_" + this.colors[i];
                    produit.sku = TextManager.setItemName(produit.sku);
                    childProducts[i] = produit;
                }
            }
            return childProducts;
        }
    }
}
