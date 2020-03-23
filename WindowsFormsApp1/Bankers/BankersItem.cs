using System;
using System.Collections;
using WindowsFormsApp1.Universal_Info;
using WindowsFormsApp1.Universal_Info.Utils;

namespace BaseDonneeConversion
{
    class BankersItem : OutsideItem
    {
        public String sku { get; set; }
        public String productName { get; set; }
        public String productNameEn { get; set; }
        public String description { get; set; }
        public String descriptionEn { get; set; }
        public String lineName { get; set; }
        public String[] categories { get; set; }
        public String[] searchKeywords { get; set; }
        public String defaultImage { get; set; }
        public String[] colors { get; set; }
        public String size { get; set; }
        public ArrayList quantities { get; set; }
        public ArrayList prices { get; set; }

        public BankersItem()
        {
            colors = new string[0];
            quantities = new ArrayList();
            prices = new ArrayList();
        }

        public ProduitDB getProduit()
        {
            ProduitDB produit = new ProduitDB();
            produit.productName = this.productName;
            produit.sku = this.sku;
            produit.description = this.description;
            produit.imageURL = this.defaultImage;
            produit.productType = "master static";
            for (int i = 0; i < this.quantities.Count; i++)
            {
                produit.priceSets.Add(new PriceSet(0, this.productName, (int)this.quantities[i], (double)this.prices[i]));
            }
            return produit;
        }

        public ProduitDB[] getChildProducts()
        {
            ProduitDB[] produits = new ProduitDB[this.colors.Length];
            for (int i = 0; i < this.colors.Length; i++)
            {
                ProduitDB itemDB = this.getProduit();
                itemDB.sku = this.sku + "_" + this.colors[i];
                itemDB.productType = "static";
                produits[i] = itemDB;
            }
            return produits;
        }
    }
}
