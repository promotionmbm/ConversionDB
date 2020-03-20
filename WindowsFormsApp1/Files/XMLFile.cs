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

        public static ArrayList readPcnaXML(XMLFile infoFile, XMLFile pricesFile, XMLFile imagesFile)
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
            foreach (var item in items)
            {
                Boolean added = false;
                ProduitDB parent = new ProduitDB();
                parent.code = item.code + "_Group";
                parent.titleEn = item.name;
                parent.titleFr = item.name;
                parent.descriptionEn = item.description;
                parent.descriptionFr = item.description;
                parent.notes = item.division;
                catConversionList.checkCategories(item.category, parent.categories);
                parent.categories=ProduitDB.fillCategories(parent.categories);
                parent.minQty = -1;
                if (item.apparel)
                {
                    parent.attributeContext = "Garnment";
                }
                else
                {
                    parent.attributeContext = "Article publicitaire";
                }
                foreach (var code in item.skus)
                {
                    ProduitDB child = new ProduitDB();
                    ProduitDB.setChildInfo(parent, child);
                    child.code = item.code;
                    if (!code.color.Equals(""))
                    {
                        child.code = child.code +"_"+ TextManager.setItemName(code.color);
                    }
                    if (code.size != null)
                    {
                        child.code = child.code + "_" + code.size;
                    }
                    foreach(var image in code.imageURL)
                    {
                        foreach(var link in image.url)
                        {
                            if (!link.img.Equals(""))
                            {
                                child.imageURL = link.img;
                            }
                        }
                    }
                    if (!child.imageURL.Equals("")) {
                        child.attributeCouleur = TextManager.convertColor(code.color);
                        child.attributeSize = code.size;
                        child.minQty = -1;
                        child.price = 0;
                        ArrayList quantities = new ArrayList();
                        ArrayList prices = new ArrayList();
                        if (!child.notes.Equals("Trimark")) {
                            foreach (var price in code.CANprices)
                            {
                                foreach (var priceQty in price.pricesQty)
                                {
                                    quantities.Add(priceQty.qty);
                                    prices.Add(priceQty.price);
                                    if (child.minQty == -1)
                                    {
                                        child.minQty = priceQty.qty;
                                    }
                                    else if (child.minQty > priceQty.qty)
                                    {
                                        child.minQty = priceQty.qty;
                                    }
                                    if (child.price < priceQty.price)
                                    {
                                        child.price = priceQty.price;
                                    }
                                }
                                parent.minQty = child.minQty;
                                parent.price = child.price;
                                parent.imageURL = child.imageURL;
                                if (!added)
                                {
                                    parent.descriptionEn = ProduitDB.setDescription(parent.descriptionEn, quantities, prices, false);
                                    parent.descriptionFr = ProduitDB.setDescription(parent.descriptionFr, quantities, prices, true);
                                    if (parent.price == 0)
                                    {
                                        break;
                                    }
                                    produits.Add(parent);
                                    added = true;
                                    child.descriptionEn = parent.descriptionEn;
                                    child.descriptionFr = parent.descriptionFr;
                                }
                                Console.WriteLine(child.code);
                                Console.WriteLine(item.code);
                                if (!child.code.Equals(item.code))
                                {
                                    produits.Add(child);
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Fini d'écrire les objets");
            return produits;
        }

        public static void writeFile(XMLFile file)
        {
            StreamWriter fs = new StreamWriter(file.path);
            fs.WriteLine(file.file);
        }
    }
}
