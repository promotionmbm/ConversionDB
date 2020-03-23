using BaseDonneeConversion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Universal_Info.Utils;

namespace WindowsFormsApp1.Sanmar
{
    class SanmarConversion
    {
        public static void convertSanmar(Excel sheet, Excel sheetFr, String descTextFile)
        {
            String[] descText = System.IO.File.ReadAllLines(descTextFile);
            String directory = Path.GetDirectoryName(sheet.getPath());
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Console.WriteLine("Created " + directory);
            }
            if (!File.Exists(directory + "/debug.txt"))
            {
                FileStream fs = File.Create(directory + "/debug.txt");
                fs.Close();
            }
            CatConversionList catConversionList = new CatConversionList();
            List<OutsideItem> sanmarItems = new List<OutsideItem>();
            ArrayList sanmarItemsFr = new ArrayList();
            ArrayList sanmarDescFr = loadDescriptionsFrItems(descText);
            String itemId = "";
            SanmarItem item = new SanmarItem();
            for (int i = 1; i < sheet.getRowCount()+1; i++)
            {
                if (itemId.Equals(sheet.readCell(i, 2)))
                {
                    item.colorSizes.Add(new ColorSize(sheet.readCell(i, 3), sheet.readCell(i, 4)));
                }
                else
                {
                    if (!itemId.Equals(""))
                    {
                        sanmarItems.Add(item);
                        item = new SanmarItem();
                    }
                    itemId=sheet.readCell(i, 2);
                    item.itemID = itemId;
                    item.colorSizes.Add(new ColorSize(sheet.readCell(i, 3), sheet.readCell(i, 4)));
                    item.imageURL = sheet.readCell(i, 11);
                    item.categories = sheet.readCell(i, 12).Split(new String[] { "; " }, StringSplitOptions.None);
                    item.name = sheet.readCell(i, 13);
                    if (sheet.readCell(i, 16).Contains("Call For Pricing"))
                    {
                        item.price = getPrice(sheet.readCell(i, 2));
                    }
                    else
                    {
                        item.price = Math.Round(sheet.readDoubleCell(i, 17) / 0.46, 2);
                    }
                    item.description = sheet.readCell(i, 14).Replace("\n", "<br>") + "<br/><b><font style=\"font-size:20px\">Discounts on volume may be applicable.<br/><br/>Contact us for a price with an embroidery or a serigraphy.</b></font><br/>";
                }
            }
            for(int i = 0; i < sheetFr.getRowCount(); i++)
            {
                if(!sheetFr.readCell(i, 0).Equals(""))
                {
                    item = new SanmarItem();
                    item.name = sheetFr.readCell(i, 1);
                    item.itemID = sheetFr.readCell(i+1, 1);
                    sanmarItemsFr.Add(item);
                }
            }
            List<ProduitDB> itemsDB = ConversionUtils.getProducts(sanmarItems);
            Excel output = Excel.createAndUseFile(sheet.getPath());
            ProduitDB.writeDBHeader(output);
            for(int i=0; i < itemsDB.Count; i++)
            {
                ProduitDB.writeDBItem(output, (i + 1), (ProduitDB)itemsDB[i]);
            }
            output.save();
        }

        public static String getDescriptionFr(ArrayList list, String code)
        {
            Console.WriteLine(code);
            foreach(SanmarDescFr desc in list)
            {
                if (desc.getManCode().Equals(code) || desc.getWomanCode().Equals(code) || desc.getYouthCode().Equals(code) || desc.getLargeCode().Equals(code) || desc.getOtherCode().Equals(code) || desc.getScndManCode().Equals(code) || desc.getScndWomanCode().Equals(code) || desc.getThrdWomanCode().Equals(code))
                {
                    return desc.getDescription();
                }
            }
            return "";
        }

        public static ArrayList loadDescriptionsFrItems(String[] descFile)
        {
            ArrayList descriptions = new ArrayList();
            Boolean inDesc = false;
            String description = "";
            SanmarDescFr desc = new SanmarDescFr();
            for(int i=0;i < descFile.Length;i++){
                String line = descFile[i];
                if (inDesc)
                {
                    if (line.Equals(""))
                    {
                        inDesc = false;
                    }
                    else
                    {
                        description += line + "<br/>";
                    }
                }
                else
                {
                    if (line.Contains("<") || line.Contains(">"))
                    {
                        inDesc = true;
                        description += "<br/><b><font style=\"font-size:20px\">Rabais de volume peut s’appliquer<br/><br/>Contactez-nous pour obtenir un prix avec une broderie ou une sérigraphie.</b></font><br/>";
                        desc.setDescription(description);
                        descriptions.Add(desc);
                        desc = new SanmarDescFr();
                        description = "";
                    }
                    else if (line.StartsWith("Dimensions"))
                    {
                        inDesc = true;
                        description += "<br/>"+ line + "<br/>";
                    }
                    else if (line.Equals("*92/8 POLYESTER/SPANDEX"))
                    {
                        description += "<br/><b><font style=\"font-size:20px\">Rabais de volume peut s’appliquer<br/><br/>Contactez-nous pour obtenir un prix avec une broderie ou une sérigraphie.</b></font><br/>";
                        desc.setDescription(description);
                        descriptions.Add(desc);
                        desc = new SanmarDescFr();
                        description = "92/8";
                    }
                    else if (description.Equals("92/8") && line.Equals("ICÔNES"))
                    {
                        inDesc = true;
                        description = "";
                    }
                    else if (line.Equals("A100"))
                    {
                        desc.setManCode(line);
                    }
                    else if (line.Equals("ATC0820") || line.Equals("ATC110F") || line.Equals("ATC110P") || line.Equals("ATC6089M") || line.Equals("ATC6245CM") || line.Equals("C140") || line.Equals("C105") || line.Equals("C112") || line.Equals("C1008") || line.Equals("C100") || line.Equals("A101") || line.Equals("B1036") || line.Equals("B120") || line.Equals("B110") || line.Equals("C1314") || line.Equals("C1312") || line.Equals("C1313") || line.Equals("NE205") || line.Equals("NE400") || line.Equals("NE200") || line.Equals("C1318") || line.Equals("C1317") || line.Equals("C1202") || line.Equals("F1010") || line.Equals("F1101"))
                    {
                        desc.setWomanCode(descFile[i]);
                    }
                    else if (line.StartsWith("GRAND GL") || line.StartsWith("GRAND TG"))
                    {
                        desc.setLargeCode(descFile[i + 1].Replace(" ", ""));
                    }
                    else if (line.StartsWith("ADULT"))
                    {
                        i++;
                        if (!desc.getManCode().Equals(""))
                        {
                            if (desc.getScndManCode().Equals(""))
                                desc.setScndManCode(descFile[i]);
                        }
                        else {
                            while (desc.getManCode().Equals(""))
                            {
                                if (descFile[i].Contains("à"))
                                {
                                    i++;
                                }
                                else
                                {
                                    desc.setManCode(descFile[i].Replace(" ", ""));
                                }
                            }
                        }
                    }
                    else if (line.StartsWith("FEMME"))
                    {
                        if (desc.getWomanCode().Equals(""))
                            desc.setWomanCode(descFile[i + 1].Replace(" ", ""));
                        else if (desc.getScndWomanCode().Equals(""))
                            desc.setScndWomanCode(descFile[i + 1].Replace(" ", ""));
                        else if (desc.getThrdWomanCode().Equals(""))
                            desc.setThrdWomanCode(descFile[i + 1].Replace(" ", ""));
                    }
                    else if (line.StartsWith("JEUNE") ||line.StartsWith("TOUT-PETIT"))
                    {
                        i++;
                        desc.setYouthCode("");
                        while (desc.getYouthCode().Equals(""))
                        {
                            if (descFile[i].Contains("à"))
                            {
                                i++;
                            }
                            else
                            {
                                desc.setYouthCode(descFile[i].Replace(" ", ""));
                            }
                        }
                    }
                    else if (line.Replace(" ","").Equals("$") && !desc.getYouthCode().Equals(descFile[i - 1].Replace(" ", "")) && !desc.getLargeCode().Equals(descFile[i - 1].Replace(" ", "")) && !desc.getManCode().Equals(descFile[i - 1].Replace(" ", "")) && !desc.getWomanCode().Equals(descFile[i - 1].Replace(" ", "")) && desc.getOtherCode().Equals(""))
                    {
                        desc.setOtherCode(descFile[i - 1].Replace(" ", ""));
                    }
                }
            }
            descriptions.Add(desc);
            return descriptions;
        }

        public static void checkItemsReallyThere(String itemsFile, String descFile)
        {
            String[] descText = System.IO.File.ReadAllLines(descFile);
            String[] items = System.IO.File.ReadAllLines(itemsFile);
            String itemsThereFile = itemsFile.Replace(".txt", "_there.txt");
            if (!File.Exists(itemsThereFile))
            {
                FileStream fs = File.Create(itemsThereFile);
                fs.Close();
            }
            StreamWriter writer = new StreamWriter(itemsThereFile, true);
            foreach(String desc in descText)
            {
                foreach(String item in items)
                {
                    if (item.Equals(desc))
                    {
                        writer.WriteLine(item);
                    }
                }
            }
            writer.Close();
        }

        public static String getNameFr(String id, ArrayList products)
        {
            foreach(SanmarItem item in products)
            {
                if (item.itemID.ToUpper().Equals(id.ToUpper()))
                {
                    return item.name;
                }
            }
            return "";
        }

        public static double getPrice(String product)
        {
            if (product.Equals("NF0A3LGX"))
            {
                return (119.99 * 1.4);
            }
            else if(product.Equals("NF0A3LGY"))
            {
                return (119.99 * 1.4);
            }
            else if (product.Equals("NF0A3LH2"))
            {
                return (219.99 * 1.4);
            }
            else if (product.Equals("NF0A3LH4"))
            {
                return (139.99 * 1.4);
            }
            else if (product.Equals("NF0A3LH5"))
            {
                return (139.99 * 1.4);
            }
            else if (product.Equals("NF0A3LHD"))
            {
                return (169.99 * 1.4);
            }
            else if (product.Equals("NF0A3LHK"))
            {
                return (219.99 * 1.4);
            }
            else if (product.Equals("NF0A3LHL"))
            {
                return (169.99 * 1.4);
            }
            return 0;
        }



        public static String changeCategories(String item, String category)
        {
            
                if (item.Equals("A100_group"))
                {
                    return "catApron";
                }
                else if (item.Equals("A101_group")|| item.Equals("C1009_group"))
                {
                    return "catGloves";
                }
                else if (item.Equals("F1010_group"))
                {
                    return "catScarf";
                }
                else if (item.Equals("F1101_group"))
                {
                    return "catBlanket";
                }
                else if (item.Equals("PT20_group"))
                {
                    return "catPants";
                }
                else if (item.Equals("PT88_group"))
                {
                    return "catPants";
                }
                else if (item.Equals("SP14_group"))
                {
                    return "catShirt";
                }
                else if (item.Equals("SP24_group"))
                {
                    return "catShirt";
                }
                else if (item.Equals("SY20_group"))
                {
                    return "catShirt";
                }
            return category;
        }
    }
}
