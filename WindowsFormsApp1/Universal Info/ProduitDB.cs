using System;
using System.Collections;
using WindowsFormsApp1;

namespace BaseDonneeConversion
{
    class ProduitDB
    {
        //Code original+_Group si plusieurs couleurs
        //Aussi le code original+_nom couleur pour chaque couleur
        //
        public String code { get; set; }
        //Le code original+_Group la couleur appartient
        //
        public String codeParent { get; set; }
        //enabled/disabled
        //
        public String status { get; set; }
        //Garder vide
        //
        public String brand { get; set; }
        //kCentricStandard (Taxes régulières)
        //
        public String taxgroup { get; set; }
        //Nom de produit Anglais
        //
        public String titleEn { get; set; }
        //Nom de produit Français
        public String titleFr { get; set; }
        //
        public String descriptionEn { get; set; }
        public String descriptionFr { get; set; }
        //Prix vendu
        //Pas inclure à Group
        //Toujours le prix le plus cher
        //
        public double price { get; set; }
        //Proposal seulement
        //
        public String type { get; set; }
        //Catégories fournies
        //
        public int minQty { get; set; }
        //
        public String notes { get; set; }
        public ArrayList categories { get; set; }
        //Article publicitaire ou Garnment
        //
        public String attributeContext { get; set; }
        //Taille d'article (Article spécifique seulement)
        //
        public String attributeSize { get; set; }
        //Couleur d'article
        //
        public String attributeCouleur { get; set; }
        //Standard seulement
        //
        public String catalog { get; set; }
        //Dans le fichier excel, la colonne est Description Small en/Description Small fr
        public String imageURL { get; set; }

        public ProduitDB()
        {
            this.codeParent = "";
            this.status = "Enable";
            this.brand = "";
            this.taxgroup = "kCentricStandard";
            this.categories = new ArrayList();
            this.price = 0;
            this.minQty = 1;
            this.type = "Proposal";
            this.attributeSize = "";
            this.attributeCouleur = "";
            this.catalog = "Standard";
            this.imageURL = "";
        }

        public static void writeDBItem(Excel output, int row, ProduitDB item)
        {
            output.writeCell(row, 0, item.code);
            output.writeCell(row, 1, item.codeParent);
            output.writeCell(row, 2, item.status);
            output.writeCell(row, 3, item.brand);
            output.writeCell(row, 4, item.taxgroup);
            output.writeCell(row, 5, item.minQty + "");
            output.writeCell(row, 6, item.titleEn);
            output.writeCell(row, 7, item.titleFr);
            output.writeCell(row, 8, item.descriptionEn);
            output.writeCell(row, 9, item.descriptionFr);
            output.writeCell(row, 10, "");
            output.writeCell(row, 11, "");
            output.writeDoubleCell(row, 12, item.price);
            output.writeCell(row, 13, item.type);
            output.writeCell(row, 14, item.categories[0] + "");
            output.writeCell(row, 15, (String)item.categories[1]);
            output.writeCell(row, 16, (String)item.categories[2]);
            output.writeCell(row, 17, (String)item.categories[3]);
            output.writeCell(row, 18, (String)item.categories[4]);
            output.writeCell(row, 19, (String)item.categories[5]);
            output.writeCell(row, 20, (String)item.categories[6]);
            output.writeCell(row, 21, (String)item.categories[7]);
            output.writeCell(row, 22, (String)item.categories[8]);
            output.writeCell(row, 23, item.attributeContext);
            output.writeCell(row, 24, item.attributeSize);
            output.writeCell(row, 25, item.attributeCouleur);
            output.writeCell(row, 26, item.catalog);
            output.writeCell(row, 27, item.notes);
            output.writeCell(row, 28, item.imageURL);
        }

        public static void writeDBHeader(Excel sheet)
        {
            sheet.writeBoldCell(0, 0, "Code");
            sheet.writeBoldCell(0, 1, "Base Code");
            sheet.writeBoldCell(0, 2, "Status");
            sheet.writeBoldCell(0, 3, "Brand");
            sheet.writeBoldCell(0, 4, "Taxgroup");
            sheet.writeBoldCell(0, 5, "Quantity minimum");
            sheet.writeBoldCell(0, 6, "Title en");
            sheet.writeBoldCell(0, 7, "Title fr");
            sheet.writeBoldCell(0, 8, "Description Default en");
            sheet.writeBoldCell(0, 9, "Description Default fr");
            sheet.writeBoldCell(0, 10, "Description Small en");
            sheet.writeBoldCell(0, 11, "Description Small fr");
            sheet.writeBoldCell(0, 12, "Price Standard");
            sheet.writeBoldCell(0, 13, "Type");
            sheet.writeBoldCell(0, 14, "Category base code");
            sheet.writeBoldCell(0, 15, "Category 1");
            sheet.writeBoldCell(0, 16, "Category 2");
            sheet.writeBoldCell(0, 17, "Category 3");
            sheet.writeBoldCell(0, 18, "Category 4");
            sheet.writeBoldCell(0, 19, "Category 5");
            sheet.writeBoldCell(0, 20, "Category 6");
            sheet.writeBoldCell(0, 21, "Category 7");
            sheet.writeBoldCell(0, 22, "Category 8");
            sheet.writeBoldCell(0, 23, "AttributeContext");
            sheet.writeBoldCell(0, 24, "Attribute Size");
            sheet.writeBoldCell(0, 25, "Attribute Couleur");
            sheet.writeBoldCell(0, 26, "Catalog");
            sheet.writeBoldCell(0, 27, "Notes");
            sheet.writeBoldCell(0, 28, "Picture 1 Small");
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

        public static void setChildInfo(ProduitDB parent, ProduitDB child)
        {
            ArrayList itemCategories = new ArrayList();
            child.codeParent = parent.code;
            child.status = parent.status;
            child.brand = "";
            child.taxgroup= "kCentricStandard";
            child.titleEn = parent.titleEn;
            child.titleFr = parent.titleFr;
            child.descriptionEn = parent.descriptionEn;
            child.descriptionFr = parent.descriptionFr;
            child.type = "Proposal";
            while (itemCategories.Count < 9)
            {
                itemCategories.Add("");
            }
            child.categories = itemCategories;
            child.categories = fillCategories(child.categories);
            child.catalog = "Standard";
            child.attributeContext = parent.attributeContext;
            child.imageURL = parent.imageURL;
            child.minQty = parent.minQty;
            child.attributeSize = "N/D";
            child.notes = parent.notes;
        }
    }
}
