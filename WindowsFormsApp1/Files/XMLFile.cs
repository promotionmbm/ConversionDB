using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using BaseDonneeConversion;
using System.Collections;
using WindowsFormsApp1.Universal_Info.Utils;

namespace WindowsFormsApp1
{
    class XMLFile
    {
        String path;
        XDocument file;
        XNamespace ns;

        public XMLFile(String path)
        {
            this.path = path;
            this.file = XDocument.Load(path);
            this.ns = this.file.Root.Name.Namespace;
        }

        public String getPath()
        {
            return this.path;
        }

        public XDocument getFile()
        {
            return this.file;
        }

        public static List<ProduitDB> readPcnaXML(XMLFile infoFile, XMLFile pricesFile, XMLFile imagesFile)
        {
            double USDtoCAD = 1.35;
            ArrayList produits = new ArrayList();
            CatConversionList catConversionList = new CatConversionList();
            XElement xmlImage = imagesFile.file.Element(imagesFile.ns + "MediaContents");
            XElement xmlPrice = pricesFile.file.Element(pricesFile.ns + "ItemPrices");
            XElement xmlInfo = infoFile.file.Element(infoFile.ns + "Items");
            var items = from el in xmlInfo.Elements(infoFile.ns + "Item")
                        select new
                        {
                            name = (String)el.Element(infoFile.ns + "Description"),
                            division = (String)el.Element(infoFile.ns + "Division"),
                            apparel = (Boolean)el.Element(infoFile.ns + "Flags").Element(infoFile.ns + "Apparel"),
                            colors = from prop in el.Element(infoFile.ns + "VariantProperties").Elements(infoFile.ns + "VariantProperty")
                                     where (String)prop.Attribute("Type") == "AvailableColors"
                                     select (String)prop.Attribute("Value"),
                            description = (String)el.Element(infoFile.ns + "MarketingDescription"),
                            code = (String)el.Element(infoFile.ns + "Style"),
                            skus = from codes in el.Element(infoFile.ns + "Skus").Elements(infoFile.ns + "Sku")
                                   select new
                                   {
                                       sku = (String)codes.Attribute("Number"),
                                       color = (String)codes.Element(infoFile.ns + "Color").Attribute("MarketColor"),
                                       size = (String)codes.Element(infoFile.ns + "Size"),
                                       CANprices = (from item in xmlPrice.Elements(pricesFile.ns + "ItemPrice")
                                                    where (String)item.Element(pricesFile.ns + "SKU") == (String)codes.Attribute("Number")
                                                    select new
                                                    {
                                                        pricesQty = from price in item.Element(pricesFile.ns + "Prices").Elements(pricesFile.ns + "Price")
                                                                    where (String)price.Element(pricesFile.ns + "PriceType") == "Canadian"
                                                                    select new
                                                                    {
                                                                        price = (double)price.Element(pricesFile.ns + "PriceValue"),
                                                                        qty = (int)price.Element(pricesFile.ns + "FromQty")
                                                                    }
                                                    }),
                                       USprices = (from item in xmlPrice.Elements(pricesFile.ns + "ItemPrice")
                                                   where (String)item.Element(pricesFile.ns + "SKU") == (String)codes.Attribute("Number")
                                                   select new
                                                   {
                                                       pricesQty = from price in item.Element(pricesFile.ns + "Prices").Elements(pricesFile.ns + "Price")
                                                                   where (String)price.Element(pricesFile.ns + "PriceType") == "Canadian"
                                                                   select new
                                                                   {
                                                                       price = (double)price.Element(pricesFile.ns + "PriceValue"),
                                                                       qty = (int)price.Element(pricesFile.ns + "FromQty")
                                                                   }
                                                   }),
                                       imageURL = (from image in xmlImage.Elements(imagesFile.ns + "MediaContent")
                                                   where (String)image.Element(imagesFile.ns + "Sku") == (String)codes.Attribute("Number")
                                                   select new
                                                   {
                                                       sku = image.Element(imagesFile.ns + "Sku").Value,
                                                       url = (from img in image.Element(imagesFile.ns + "Media").Elements(imagesFile.ns + "Medium")
                                                                  where img.Element(imagesFile.ns + "MediumSize").Value == "Large"
                                                                  select new
                                                                  {
                                                                      size= img.Element(imagesFile.ns + "MediumSize").Value,
                                                                      img=(String)img.Element(imagesFile.ns + "Url").Value
                                                                  })
                                                   })
                                   },
                            category = (String)el.Element(infoFile.ns + "Category"),
                            giftSet = (Boolean)el.Element(infoFile.ns + "Flags").Element(infoFile.ns + "GiftSet"),
                            sizes = (from sku in el.Element(infoFile.ns + "Skus").Elements(infoFile.ns + "Sku")
                                     select sku.Element(infoFile.ns + "Size")).Distinct()
                        };
            List<OutsideItem> pcnaItems = new List<OutsideItem>();
            foreach (var item in items)
            {
                Boolean added = false;
                PcnaItem pcnaItem = new PcnaItem();
                pcnaItem.code = item.code;
                List<String> codeStrings= item.skus.Cast<String>().ToList();
                pcnaItem.codes = codeStrings.ToArray<String>();
                pcnaItem.division = item.division;
                pcnaItem.description = item.description;
                pcnaItem.name = item.name;
                pcnaItem.category = item.category;
                pcnaItem.apparel = item.apparel;
                pcnaItem.giftSet = item.giftSet;
                pcnaItem.colors = item.colors.Cast<String>().ToList();
                pcnaItem.sizes = item.sizes.Cast<String>().ToList();
                pcnaItems.Add(pcnaItem);
            }
            Console.WriteLine("Fini d'écrire les objets");
            return ConversionUtils.getProducts(pcnaItems);
        }

        public static void writeFile(XMLFile file)
        {
            StreamWriter fs = new StreamWriter(file.path);
            fs.WriteLine(file.file);
        }
    }
}
