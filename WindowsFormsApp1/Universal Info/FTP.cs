using BaseDonneeConversion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class FTP
    {
        static WebClient client;

        public FTP()
        {
            client = new WebClient();
        }

        public void setCredentials(String user, String password)
        {
            client.Credentials = new NetworkCredential(user, password);
        }

        public void downloadFile(ProduitDB item, String directory, StreamWriter writer)
        {
            if (!item.imageURL.Equals(""))
            {
                client.DownloadFile(item.imageURL, directory + item.code + ".jpg");
                Console.WriteLine("Downloading" + item.code + "");
            }
            else
            {
                Console.WriteLine("Writing" + item.code + "");
                writer.WriteLine(item.code);
            }
        }

        public void uploadFile(ProduitDB item, String directory)
        {
            String filePath = directory + item.code + ".jpg";
            Console.WriteLine("Uploading from " + directory + item.code + ".jpg");
            if (File.Exists(filePath)) { 
                client.UploadFile("ftp://www.mbmpromotion.com/PhotoImport/" + item.code + ".jpg", filePath);
            }
        }

        public static void checkIfFilesExist(String directory, String file)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(directory +file))
            {
                FileStream fs= File.Create(directory + file);
                fs.Close();
            }
        }

        public static void uploadFromConvertedExcel(Excel sheet)
        {
            FTP ftpClient = new FTP();
            String directory = Path.GetDirectoryName(sheet.getPath());
            String imageDirectory = directory + "\\images\\";
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
                Console.WriteLine("Created " + imageDirectory);
            }
            FTP.checkIfFilesExist(imageDirectory + "Debug\\", "ImagesManquantes.txt");
            StreamWriter file = new StreamWriter(imageDirectory + "\\Debug\\ImagesManquantes.txt", true);
            int rowCount = sheet.getRowCount();
            ArrayList items = new ArrayList();
            for (int i = 1; i < rowCount; i++)
            {
                ProduitDB item = new ProduitDB();
                item.code = sheet.readCell(i, 0);
                item.imageURL = sheet.readCell(i, 28);
                ftpClient.downloadFile(item, imageDirectory, file);
                items.Add(item);
            }
            file.Close();
            Console.WriteLine("Terminé");
            ftpClient.setCredentials("MBMPromotion", "M8mProm071on#2019!");
            for (var i = 0; i < items.Count; i++)
            {
                ProduitDB item = (ProduitDB)items[i];
                ftpClient.uploadFile(item, imageDirectory);
            }
        }
    }
}
