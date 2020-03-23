using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using BaseDonneeConversion;
using WindowsFormsApp1.Universal_Info.Utils;

namespace WindowsFormsApp1
{
    class DebcoConversion
    {
        public static void convertDebco(Excel sheet)
        {
            int rowCount = sheet.getRowCount();
            int colCount = sheet.getColumnCount();
            List<OutsideItem> debcoItems = new List<OutsideItem>();
            CatConversionList categoriesList = new CatConversionList();
            for (int i = 1; i < rowCount; i++)
            {
                DebcoItem item = new DebcoItem();
                item.itemID = sheet.readCell(i, 0);
                Console.WriteLine(item.itemID);
                item.nameEn = sheet.readCell(i, 1).ToLower();
                item.nameFr = sheet.readCell(i, 2).ToLower();
                item.descriptionEn = sheet.readCell(i, 3);
                item.descriptionFr = sheet.readCell(i, 5);
                item.quantities = new ArrayList();
                item.imageURL = sheet.readCell(i, 57);
                item.prices = new ArrayList();
                for (int j = 0; j < 5; j++)
                {
                    if(sheet.readCell(i, 8+(2*j)).Equals(""))
                    {
                        break;
                    }
                    int quantity = Int32.Parse(sheet.readDoubleCell(i, 8 + (2 * j)) + "");
                    Double price = Double.Parse(sheet.readDoubleCell(i, 9 + (2 * j)) + "");
                    item.quantities.Add(quantity);
                    item.prices.Add(price);
                }
                item.colors = new String[] { sheet.readCell(i, 33), sheet.readCell(i, 34), sheet.readCell(i, 35), sheet.readCell(i, 36), sheet.readCell(i, 37), sheet.readCell(i, 38), sheet.readCell(i, 39), sheet.readCell(i, 40), sheet.readCell(i, 41), sheet.readCell(i, 42), sheet.readCell(i, 43), sheet.readCell(i, 44), sheet.readCell(i, 45), sheet.readCell(i, 46), sheet.readCell(i, 47)};
                item.category = sheet.readCell(i, 104);
                item.majorCategory = sheet.readCell(i, 105);
                debcoItems.Add(item);
            }
            List<ProduitDB> dbItems = ConversionUtils.getProducts(debcoItems);
            Excel output = Excel.createAndUseFile(sheet.getPath());
            ProduitDB.writeDBHeader(output);
            for (int i = 0; i < dbItems.Count; i++)
            {
                ProduitDB item = (ProduitDB)dbItems[i];
                ProduitDB.writeDBItem(output, (i+1), item);
            }
            output.save();
        }

        public static void uploadPictures(Excel sheet)
        {
            FTP ftpClient = new FTP();
            String directory = Path.GetDirectoryName(sheet.getPath());
            String imageDirectory = directory + "\\images\\Debug\\";
            FTP.checkIfFilesExist(imageDirectory, "ImagesManquantes.txt");
            StreamWriter file = new StreamWriter(imageDirectory + "ImagesManquantes.txt", true);
            int rowCount = sheet.getRowCount();
            ArrayList debcoItems = new ArrayList();
            for (int i = 1; i < rowCount; i++)
            {
                DebcoItem item = new DebcoItem();
                item.itemID = sheet.readCell(i, 0);
                item.imageURL = sheet.readCell(i, 57);
                item.colors = new String[] { sheet.readCell(i, 33), sheet.readCell(i, 34), sheet.readCell(i, 35), sheet.readCell(i, 36), sheet.readCell(i, 37), sheet.readCell(i, 38), sheet.readCell(i, 39), sheet.readCell(i, 40), sheet.readCell(i, 41), sheet.readCell(i, 42), sheet.readCell(i, 43), sheet.readCell(i, 44), sheet.readCell(i, 45), sheet.readCell(i, 46), sheet.readCell(i, 47) };
                debcoItems.Add(item);
            }
            ArrayList dbItems = new ArrayList();
            for (int i = 0; i < debcoItems.Count; i++)
            {
                DebcoItem item = (DebcoItem)debcoItems[i];
                if (item.colors.Length > 0)
                {
                    ProduitDB itemGroup = new ProduitDB();
                    itemGroup.sku = item.itemID + "_Group";
                    itemGroup.imageURL = item.imageURL;
                    ftpClient.downloadFile(itemGroup, imageDirectory, file);
                    dbItems.Add(itemGroup);
                    for (int j = 0; j < item.colors.Length; j++)
                    {
                        if (!item.colors[j].Equals(""))
                        {
                            ProduitDB itemDB = new ProduitDB();
                            itemDB.sku = item.itemID + "_" + item.colors[j];
                            itemDB.sku = itemDB.sku.Replace("/", "_");
                            itemDB.sku = itemDB.sku.Replace(" ", "_");
                            itemDB.sku = itemDB.sku.Replace("\\", "_");
                            itemDB.imageURL = item.imageURL;
                            ftpClient.downloadFile(itemDB, imageDirectory, file);
                            dbItems.Add(itemDB);
                        }
                    }
                }
            }
            file.Close();
            Console.WriteLine("Terminé");
            ftpClient.setCredentials("MBMPromotion", "M8mProm071on#2019!");
            for (var i = 0; i < dbItems.Count; i++)
            {
                ProduitDB item = (ProduitDB)dbItems[i];
                ftpClient.uploadFile(item, imageDirectory);
            }
        }

        public static void addColors(Excel debcoSheet, Excel attributeSheet)
        {
            int maxCol = attributeSheet.getColumnCount();
            int maxRow = attributeSheet.getRowCount();
            int debcoRows = debcoSheet.getRowCount();
            int colorRow = 0;
            for(int i = 0; i < maxRow; i++)
            {
                if (attributeSheet.readCell(i, 0).Equals("Couleur"))
                {
                    colorRow = i;
                    break;
                }
            }
            ArrayList colors = new ArrayList();
            int color = 6;
            while (color < maxCol)
            {
                colors.Add(attributeSheet.readCell(colorRow, color).ToUpper());
                if(attributeSheet.readCell(colorRow, color).Equals(""))
                {
                    Console.WriteLine(color + " est vide.");
                }
                if (attributeSheet.readCell(colorRow, (color+1)).Equals(""))
                {
                    Console.WriteLine((color + 1) + " est vide.");
                }
                if (attributeSheet.readCell(colorRow, (color + 2)).Equals(""))
                {
                    Console.WriteLine((color + 2) + " est vide.");
                }
                color += 3;
            }
            for(int i = 1; i < debcoRows; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    Boolean exists = false;
                    Boolean noTranslation = false;
                    if(!debcoSheet.readCell(i, 18 + j).Equals(""))
                    {
                        if (debcoSheet.readCell(i, 33 + j).Equals(""))
                        {
                            noTranslation = true;
                        }
                        for (int k = 0; k < colors.Count; k++)
                        {
                            if (debcoSheet.readCell(i, 33 + j).ToUpper().Equals((String)colors[k]))
                            {
                                exists = true;
                                break;
                            }
                        }
                        if (!exists)
                        {
                            if (noTranslation)
                                Console.WriteLine("En train d'écrire " + debcoSheet.readCell(i, 18 + j) + " qui n'a pas de traduction à la rangée " + (i + 1) + "!");
                            colors.Add(debcoSheet.readCell(i, 33 + j).ToUpper());
                            attributeSheet.writeBoldCell(0, maxCol, "Predefined Value " + colors.Count + " Value ");
                            attributeSheet.writeBoldCell(0, maxCol + 1, "Predefined Value " + colors.Count + " Description en ");
                            attributeSheet.writeBoldCell(0, maxCol + 2, "Predefined Value " + colors.Count + " Description fr ");
                            attributeSheet.writeCell(colorRow, maxCol, debcoSheet.readCell(i, 33 + j));
                            attributeSheet.writeCell(colorRow, maxCol + 1, debcoSheet.readCell(i, 18 + j));
                            attributeSheet.writeCell(colorRow, maxCol + 2, debcoSheet.readCell(i, 33 + j));
                            maxCol += 3;
                        }
                    }
                }
            }
            attributeSheet.save();
        }
    }
}
