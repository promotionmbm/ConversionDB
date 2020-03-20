using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class TextManager
    {
        public static String francisation(string description)
        {
            String descriptionFr = description.Replace("DEFAULT BRANDING METHOD", "MÉTHODE DE MARQUAGE PAR DÉFAUT");
            descriptionFr = descriptionFr.Replace("PACKAGING", "EMBALLAGE");
            descriptionFr = descriptionFr.Replace("Setup", "Frais de montage");
            descriptionFr = descriptionFr.Replace("Quantity", "Quantité");
            descriptionFr = descriptionFr.Replace("Price", "Prix");
            return descriptionFr;
        }

        public static String convertDoubleText(Double price)
        {
            int priceInt = Convert.ToInt32(price * 100);
            if (priceInt % 100 == 0)
            {
                return price + ",00";
            }
            else if (priceInt % 10 == 0)
            {
                return price + "0";
            }
            return price + "";
        }

        public static String setItemName(String item)
        {
            String finalItem = item;
            finalItem = finalItem.Replace("/", "_");
            finalItem = finalItem.Replace(" ", "_");
            finalItem = finalItem.Replace("\\", "_");
            finalItem = finalItem.Replace("(", "");
            finalItem = finalItem.Replace(")", "");
            finalItem = finalItem.Replace(".", "");
            return finalItem;


        }

        public static String convertColor(String color)
        {
            if (color.Equals("Gray"))
            {
                return "Grey";
            }
            else if(color.Equals("Gray "))
            {
                return "Grey";
            }
            else if (color.Equals("Brown"))
            {
                return "Boysenberry";
            }
            else if (color.Equals("PP"))
            {
                return "Purple";
            }
            else if(color.Equals("Multicolor"))
            {
                return "Multi-couleur";
            }
            else if (color.Equals("Camo"))
            {
                return "ArmyGreen";
            }
            return color;
        }
    }
}
