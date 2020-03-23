using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using BaseDonneeConversion;

namespace WindowsFormsApp1
{
    class PcnaConversion
    {
        public static void convertPcna(XMLFile infoFile, XMLFile pricesFile, XMLFile imagesFile)
        {
            List<ProduitDB> produits=XMLFile.readPcnaXML(infoFile, pricesFile, imagesFile);
            Excel excelFinal = Excel.createAndUseFile(pricesFile.getPath().Replace(".xml", ".xlsx"));
            ProduitDB.writeDBHeader(excelFinal);
            Console.WriteLine("Finished creating items");
            for(int i = 0; i < produits.Count; i++)
            {
                ProduitDB produit = (ProduitDB)produits[i];
                ProduitDB.writeDBItem(excelFinal, i + 1, produit);
            }
            excelFinal.save();
            //XMLFile.writeFile(imagesFile);
        }
    }
}
