using BaseDonneeConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Universal_Info.Utils
{
    class ConversionUtils
    {
        public static List<ProduitDB> getProducts(List<OutsideItem> outsideProducts)
        {
            List<ProduitDB> products = new List<ProduitDB>();
            for(int i = 0; i < outsideProducts.Count;i++) {
                ProduitDB parentProduct = outsideProducts[i].getProduit();
                products.Add(parentProduct);
                ProduitDB[] childrenProducts = outsideProducts[i].getChildProducts();
                foreach(ProduitDB produitDB in childrenProducts)
                {
                    products.Add(produitDB);
                }
            }
            return products;
        }
    }
}
