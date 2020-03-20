using System;
using System.Collections;
using System.IO;
using BaseDonneeConversion;

namespace WindowsFormsApp1
{
    class BankersConversion
    {
        public static void convertBankers(Excel sheet, Excel sheetEn)
        {
            int rowCount = sheet.getRowCount();
            int firstSheetCols = sheet.getColumnCount();
            sheet.changeSheet(2);
            int secondSheetCols = sheet.getColumnCount();
            ArrayList bankerItems = new ArrayList();
            CatConversionList categoriesList = new CatConversionList();
            for(int i = 1; i < rowCount; i++)
            {
                BankersItem item = new BankersItem();
                sheet.changeSheet(1);
                item.sku=sheet.readCell(i, 0);
                item.productName = sheet.readCell(i, 2);
                item.productNameEn = sheetEn.readCell(i, 2);
                item.description = sheet.readCell(i, 3);
                item.descriptionEn = sheetEn.readCell(i, 3);
                item.lineName = sheet.readCell(i,4);
                item.categories = sheet.readCell(i, 6).Split('|');
                item.searchKeywords = sheet.readCell(i, 7).Split('|');
                item.defaultImage = sheet.readCell(i, 8);
                item.colors = sheet.readCell(i,9).Split('|');
                item.size = sheet.readCell(i, 10);
                sheet.changeSheet(2);
                int index = 5;
                while(!sheet.readCell(i, index).Equals("")&&index<sheet.getColumnCount())
                {
                    int quantity = Int32.Parse(sheet.readDoubleCell(i, index) + "");
                    Double price = Double.Parse(sheet.readDoubleCell(i, index + 1) + "");
                    item.quantities.Add(quantity);
                    item.prices.Add(price);
                    index += 3;
                }
                bankerItems.Add(item);
            }
            ArrayList dbItems = new ArrayList();
            for(int i = 0; i < bankerItems.Count; i++)
            {
                BankersItem item = (BankersItem) bankerItems[i];
                if (item.colors.Length > 0)
                {
                    ProduitDB itemGroup= new ProduitDB();
                    itemGroup.code = item.sku + "_Group";
                    itemGroup.descriptionEn = ProduitDB.setDescription(item.descriptionEn, item.quantities, item.prices, false);
                    itemGroup.descriptionFr = ProduitDB.setDescription(item.description, item.quantities, item.prices, true);
                    itemGroup.imageURL = item.defaultImage;
                    ArrayList categories = new ArrayList();
                    for(int k = 0; k < item.categories.Length; k++)
                    {
                        categoriesList.checkCategories(item.categories[k], categories);
                    }
                    while (categories.Count<9)
                    {
                        categories.Add("");
                    }
                    itemGroup.categories = categories;
                    itemGroup.attributeContext = CatConversionList.getCategoryContext((String)itemGroup.categories[0], categoriesList);
                    itemGroup.notes = "Bankers";
                    if(item.prices.Count <= 1)
                    {
                        itemGroup.status = "Disable";
                    }
                    dbItems.Add(itemGroup);
                    for(int j = 0; j < item.colors.Length; j++)
                    {
                        ProduitDB itemDB = new ProduitDB();
                        ArrayList itemCategories = new ArrayList();
                        itemDB.code = item.sku + "_" + item.colors[j];
                        itemDB.code = TextManager.setItemName(itemDB.code);
                        ProduitDB.setChildInfo(itemGroup, itemDB);
                        if (item.prices.Count > 0)
                        {
                            itemDB.price = (double)item.prices[0];
                        }
                        while(itemCategories.Count < 9)
                        {
                            itemCategories.Add("");
                        }
                        if (item.quantities.Count > 0)
                        {
                            itemGroup.minQty = (int)item.quantities[0];
                        }
                        itemDB.attributeCouleur = item.colors[j];
                        itemDB.notes = "Bankers";
                        dbItems.Add(itemDB);
                    }
                }
            }
            Excel output = Excel.createAndUseFile(sheet.getPath());
            ProduitDB.writeDBHeader(output);
            for(var i = 0; i < dbItems.Count; i++)
            {
                ProduitDB item = (ProduitDB)dbItems[i];
                ProduitDB.writeDBItem(output, (i + 1), item);
            }
            output.save();
        }

        public static void uploadPictures(Excel sheet)
        {
            FTP ftpClient = new FTP();
            String directory = Path.GetDirectoryName(sheet.getPath());
            String imageDirectory = directory + "\\images\\";
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
            }
            FTP.checkIfFilesExist(imageDirectory+"Debug\\", "ImagesManquantes.txt");
            StreamWriter file = new StreamWriter(imageDirectory + "\\Debug\\ImagesManquantes.txt", true);
            int rowCount = sheet.getRowCount();
                ArrayList bankerItems = new ArrayList();
                for (int i = 1; i < rowCount; i++)
                {
                BankersItem item = new BankersItem();
                item.sku = sheet.readCell(i, 0);
                item.defaultImage = sheet.readCell(i, 8);
                item.colors = sheet.readCell(i, 9).Split('|');
                bankerItems.Add(item);
            }
            ArrayList dbItems = new ArrayList();
            for (int i = 0; i < bankerItems.Count; i++)
            {
                BankersItem item = (BankersItem)bankerItems[i];
                if (item.colors.Length > 0)
                {
                    ProduitDB itemGroup = new ProduitDB();
                    itemGroup.code = item.sku + "_Group";
                    itemGroup.imageURL = item.defaultImage;
                    ftpClient.downloadFile(itemGroup, imageDirectory, file);
                    dbItems.Add(itemGroup);
                    for (int j = 0; j < item.colors.Length; j++)
                    {
                        ProduitDB itemDB = new ProduitDB();
                        itemDB.code = item.sku + "_" + item.colors[j];
                        itemDB.code = itemDB.code.Replace("/", "_");
                        itemDB.code = itemDB.code.Replace(" ", "_");
                        itemDB.code = itemDB.code.Replace("\\", "_");
                        itemDB.imageURL = item.defaultImage;
                        ftpClient.downloadFile(itemDB, imageDirectory, file);
                        dbItems.Add(itemDB);
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
    }
}
