using System;
using System.Collections;
using System.Collections.Generic;
using WindowsFormsApp1;
using WindowsFormsApp1.Universal_Info;

namespace BaseDonneeConversion
{
    public class ProduitDB
    {
        public String productName { get; set; }
        public String sku { get; set; }
        public String productType { get; set; }
        public String description { get; set; }
        public double setupPrice { get; set; }
        public int priceSetId { get; set; }
        public bool active { get; set; }
        public String imageURL { get; set; }
        public List<PriceSet> priceSets { get; set; }

        public ProduitDB()
        {
            this.productName = "";
            this.sku = "";
            this.description = "";
            this.setupPrice = 0;
            this.priceSetId = 0;
            this.active = true;
            this.imageURL = "";
            this.priceSets = new List<PriceSet>();
        }

        public static void writeDBItem(Excel output, int row, ProduitDB item)
        {
            output.writeCell(row, 0, item.productName);
            output.writeCell(row, 1, item.sku);
            output.writeCell(row, 2, item.productType);
            output.writeCell(row, 3, item.description);
            output.writeDoubleCell(row, 4, item.setupPrice);
            output.writeCell(row, 5, item.priceSetId+"");
            output.writeCell(row, 6, item.active.ToString());
        }

        public static void writeDBHeader(Excel sheet)
        {
            sheet.writeBoldCell(0, 0, "ProductName");
            sheet.writeBoldCell(0, 1, "SKU");
            sheet.writeBoldCell(0, 2, "ProductType");
            sheet.writeBoldCell(0, 3, "Description");
            sheet.writeBoldCell(0, 4, "SetupPrice");
            sheet.writeBoldCell(0, 5, "PriceSetID");
            sheet.writeBoldCell(0, 6, "Active");
        }

        public static String setDescription(String description, ArrayList quantities, ArrayList prices, Boolean isFrench)
        {
            String descriptionFinale = description+"<br /><br /><table style=\"border-collapse:collapse;border:1px solid black; text-align:center;\"><tr style=\"border-collapse:collapse;border:1px solid black;padding:5px;\"><th style=\"border-collapse:collapse;border:1px solid black;padding:5px 10px;\">Quantity</th>";
            descriptionFinale = descriptionFinale.Replace("</DIV>", "");
            for(int i = 0; i < quantities.Count; i++)
            {
                descriptionFinale=descriptionFinale + "<td style=\"border-collapse:collapse;border:1px solid black;padding:5px;\">" + quantities[i] + "</td>";
            }
            descriptionFinale = descriptionFinale + "</tr><tr style=\"border-collapse:collapse;border:1px solid black;padding:5px;\"><th style=\"border-collapse:collapse;border:1px solid black;\">Price</th>";
            for (int i = 0; i < prices.Count; i++)
            {
                if(isFrench)
                    descriptionFinale = descriptionFinale + "<td style=\"border-collapse:collapse;border:1px solid black;padding:5px;\">$" + TextManager.convertDoubleText((double)prices[i]) + "</td>";
                else
                    descriptionFinale = descriptionFinale + "<td style=\"border-collapse:collapse;border:1px solid black;padding:5px;\">" + TextManager.convertDoubleText((double)prices[i]) + "$</td>";
            }
            descriptionFinale = descriptionFinale + "</tr></table></div>";
            if (isFrench)
            {
                descriptionFinale=TextManager.francisation(descriptionFinale);
            }
            return descriptionFinale;
        }

        public static ArrayList fillCategories(ArrayList categories)
        {
            while (categories.Count < 9)
            {
                categories.Add("");
            }
            return categories;
        }
    }
}
