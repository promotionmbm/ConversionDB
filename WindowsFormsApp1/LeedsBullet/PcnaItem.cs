using BaseDonneeConversion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Universal_Info.Utils;

namespace WindowsFormsApp1
{
    class PcnaItem : OutsideItem
    {
        public String code { get; set; }
        public String[] codes { get; set; }
        public String division { get; set; }
        public String description { get; set; }
        public String name { get; set; }
        public String category { get; set; }
        public Boolean apparel { get; set; }
        public Boolean ecoFriendly { get; set; }
        public Boolean giftSet { get; set; }
        public List<String> colors { get; set; }
        public List<String> sizes { get; set; }

        public ProduitDB getProduit()
        {
            ProduitDB produit = new ProduitDB();
            produit.productName = this.name;
            produit.sku = this.code;
            produit.description = this.description;
            return produit;
        }

        public ProduitDB[] getChildProducts()
        {
            List<ProduitDB> childProducts = new List<ProduitDB>();
            for(int i = 0; i < this.colors.Count; i++)
            {
                if (!this.colors[i].Equals(""))
                {
                    for(int j = 0; j < this.sizes.Count; j++)
                    {
                        ProduitDB produit = this.getProduit();
                        produit.sku = this.code + "_" + this.colors[i] + "_" + this.sizes[j];
                        childProducts.Add(produit);
                    }
                }
            }
            return childProducts.ToArray<ProduitDB>();
        }
    }
}
