using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp1.Sanmar;
using WindowsFormsApp1.Universal_Info;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (comboBox1.Text.Equals("Leeds et Bullet") &&comboBox2.Text.Equals("Conversion"))
            {
                openFileDialog1.Filter = "Extensible Markup Language files (.xml)|*.xml";
            }
            else if (comboBox1.Text.Equals("Sanmar") && comboBox2.Text.Equals("Items manquants"))
            {
                openFileDialog1.Filter = "Text file (.txt)|*.txt";
            }
            else
            {
                openFileDialog1.Filter = "Microsoft Excel Spreadsheets (.xlsb, .xlsm, .xlsx)|*.xlsb;*.xlsm;*.xlsx";
            }

            if (openFileDialog1.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text.Equals("Nettoyage"))
            {
                if (comboBox1.Text.Equals("Sanmar"))
                {

                }
                else{
                    Excel productExcel = new Excel(textBox1.Text, 1);
                    Excel disabledExcel = Excel.createAndUseFile(productExcel.getPath());
                    int maxRow = 1;
                    disabledExcel.writeBoldCell(0, 0, "Code");
                    disabledExcel.writeBoldCell(0, 1, "Status");
                    for (int i = 1; i < productExcel.getRowCount(); i++)
                    {
                        if (productExcel.readCell(i, 0).EndsWith("_"))
                        {
                            disabledExcel.writeCell(maxRow, 0, productExcel.readCell(i, 0));
                            disabledExcel.writeCell(maxRow, 1, "Disable");
                            maxRow++;
                        }
                    }
                    disabledExcel.save();
                    MessageBox.Show("Nettoyage complété!");
                }
            }
            else if (comboBox1.Text.Equals("Bankers"))
            {
                Excel bankersExcel = new Excel(textBox1.Text, 1);
                if (comboBox2.Text.Equals("Conversion"))
                {
                    Excel bankersExcelEn = new Excel(textBox2.Text, 1);
                    BankersConversion.convertBankers(bankersExcel, bankersExcelEn);
                    MessageBox.Show("Conversion terminée avec succès!");
                }
                else if (comboBox2.Text.Equals("Ajouter images"))
                {
                    BankersConversion.uploadPictures(bankersExcel);
                    MessageBox.Show("Les images ont été ajoutées avec succès!");
                }
                else if (comboBox2.Text.Equals("Categories"))
                {
                    CatConversionList catConversionList = new CatConversionList();
                    String directory = "C:\\Users\\Caroline\\Documents\\Database\\Bankers\\debug\\";
                    FTP.checkIfFilesExist(directory, "CatManquantes.txt");
                    StreamWriter file = new StreamWriter(directory + "CatManquantes.txt", true);
                    ArrayList categoriesList = new ArrayList();
                    int maxRows = bankersExcel.getRowCount();
                    for (int i = 1; i < maxRows; i++)
                    {
                        String[] categories = bankersExcel.readCell(i, 6).Split('|');
                        for (int j = 0; j < categories.Length; j++)
                        {
                            catConversionList.debugCategories(categories[j], file, categoriesList);
                        }
                    }
                    file.Close();
                    MessageBox.Show("Vérification terminée!");
                }
                else if (comboBox2.Text.Equals("Couleurs"))
                {
                    Excel attributeExcel = new Excel(textBox3.Text, 1);
                    DebcoConversion.addColors(bankersExcel, attributeExcel);
                    MessageBox.Show("Les couleurs ont été ajoutées avec succès!");
                }
                else if (comboBox2.Text.Equals("Merge"))
                {

                }
            }
            else if (comboBox1.Text.Equals("Debco"))
            {
                Excel debcoExcel = new Excel(textBox1.Text, 1);

                if (comboBox2.Text.Equals("Categories"))
                {
                    String directory = "C:\\Users\\Caroline\\Documents\\Database\\Debco\\debug\\";
                    CatConversionList catConversionList = new CatConversionList();
                    FTP.checkIfFilesExist(directory, "CatManquantes.txt");
                    StreamWriter file = new StreamWriter(directory + "CatManquantes.txt", true);
                    ArrayList categoriesList = new ArrayList();
                    int maxRows = debcoExcel.getRowCount();
                    for (int i = 1; i < maxRows; i++)
                    {
                        String categorie = debcoExcel.readCell(i, 104);
                        catConversionList.debugCategories(categorie, file, categoriesList);
                    }
                    file.Close();
                    MessageBox.Show("Vérification terminée!");
                }
                else if (comboBox2.Text.Equals("Conversion"))
                {
                    DebcoConversion.convertDebco(debcoExcel);
                    MessageBox.Show("Conversion terminée avec succès!");
                }
                else if (comboBox2.Text.Equals("Ajouter images"))
                {
                    DebcoConversion.uploadPictures(debcoExcel);
                    MessageBox.Show("Les images ont été ajoutées avec succès!");
                }
                else if (comboBox2.Text.Equals("Couleurs"))
                {
                    Excel attributeExcel = new Excel(textBox2.Text, 1);
                    DebcoConversion.addColors(debcoExcel, attributeExcel);
                    MessageBox.Show("Les couleurs ont été ajoutées avec succès!");
                }
            }
            else if (comboBox1.Text.Equals("Leeds et Bullet"))
            {
                if (comboBox2.Text.Equals("Conversion"))
                {
                    Console.WriteLine("Creating file");
                    XMLFile infoFile = new XMLFile(textBox1.Text);
                    XMLFile pricesFile = new XMLFile(textBox2.Text);
                    XMLFile imagesFiles = new XMLFile(textBox3.Text);
                    Console.WriteLine("Done");
                    PcnaConversion.convertPcna(infoFile, pricesFile, imagesFiles);
                    MessageBox.Show("Conversion complétée");
                }
                else if (comboBox2.Text.Equals("Ajouter images"))
                {
                    Excel sheet = new Excel(textBox1.Text, 1);
                    FTP.uploadFromConvertedExcel(sheet);
                    MessageBox.Show("Images mises en ligne!");
                }
                else if (comboBox2.Text.Equals("Couleurs"))
                {
                    Excel convertedSheet = new Excel(textBox1.Text, 1);
                    Excel attributesSheet = new Excel(textBox2.Text, 1);
                    TasksWithConvertedExcel.ajouterCouleurs(convertedSheet, attributesSheet);
                    MessageBox.Show("Couleurs ajoutées!");
                }
            }
            else if (comboBox1.Text.Equals("Sanmar"))
            {
                if (comboBox2.Text.Equals("Conversion"))
                {
                    Excel sanmarExcel = new Excel(textBox1.Text, 1);
                    Excel sanmarExcelFr = new Excel(textBox2.Text, 1);
                    SanmarConversion.convertSanmar(sanmarExcel, sanmarExcelFr, textBox3.Text);
                    MessageBox.Show("Conversion terminée");
                }
                else if(comboBox2.Text.Equals("Items manquants"))
                {
                    String itemFile = textBox1.Text;
                    String descFile = textBox2.Text;
                    SanmarConversion.checkItemsReallyThere(itemFile, descFile);
                    MessageBox.Show("Vérification terminée");
                }
                else if(comboBox2.Text.Equals("Ajouter images"))
                {
                    Excel sheet = new Excel(textBox1.Text, 1);
                    FTP.uploadFromConvertedExcel(sheet);
                    MessageBox.Show("Images mises en ligne!");
                }
                else if (comboBox2.Text.Equals("Couleurs"))
                {
                    Excel convertedSheet = new Excel(textBox1.Text, 1);
                    Excel attributesSheet = new Excel(textBox2.Text, 1);
                    TasksWithConvertedExcel.ajouterCouleurs(convertedSheet, attributesSheet);
                    MessageBox.Show("Couleurs ajoutées!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (comboBox1.Text.Equals("Leeds et Bullet") && comboBox2.Text.Equals("Conversion"))
            {
                openFileDialog1.Filter = "Extensible Markup Language files (.xml)|*.xml";
            }
            else if (comboBox1.Text.Equals("Sanmar") && comboBox2.Text.Equals("Items manquants"))
            {
                openFileDialog1.Filter = "Text file (.txt)|*.txt";
            }
            else
            {
                openFileDialog1.Filter = "Microsoft Excel Spreadsheets (.xls, .xlsb, .xlsm, .xlsx)|*.xls;*.xlsb;*.xlsm;*.xlsx";
            }

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (comboBox1.Text.Equals("Leeds et Bullet"))
            {
                openFileDialog1.Filter = "Extensible Markup Language files (.xml)|*.xml";
            }
            else if (comboBox1.Text.Equals("Sanmar") && comboBox2.Text.Equals("Conversion"))
            {
                openFileDialog1.Filter = "Text file (.txt)|*.txt";
            }
            else
            {
                openFileDialog1.Filter = "Microsoft Excel Spreadsheets (.xls, .xlsb, .xlsm, .xlsx)|*.xls;*.xlsb;*.xlsm;*.xlsx";
            }
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox3.Text = openFileDialog1.FileName;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals("Bankers"))
            {
                setBankersUI();
            }
            else if (comboBox1.Text.Equals("Debco"))
            {
                setDebcoUI();
            }
            else if(comboBox1.Text.Equals("Leeds et Bullet"))
            {
                setPcnaUI();
            }
            else if (comboBox1.Text.Equals("Sanmar"))
            {
                setSanmarUI();
            }
        }

        private void setPcnaUI()
        {
            if (comboBox2.Text.Equals("Conversion"))
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = true;
                label2.Text = "Fichier XML Info";
                label3.Text = "Fichier XML Prix";
                label5.Text = "Fichier XML Images";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
            }
            else if (comboBox2.Text.Equals("Ajouter images"))
            {
                textBox1.Visible = true;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = false;
                label5.Visible = false;
                label2.Text = "Fichier Excel Converti";
                label3.Text = "";
                label5.Text = "";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
            }
            else if(comboBox2.Text.Equals("Couleurs"))
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = false;
                label2.Text = "Fichier Excel Converti";
                label3.Text = "Fichier Attributs";
                label5.Text = "";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = false;
            }
            else
            {
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = false;
                label3.Visible = false;
                label5.Visible = false;
                label2.Text = "";
                label3.Text = "";
                label5.Text = "";
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
            }
        }

        private void setDebcoUI()
        {
            if (comboBox2.Text.Equals("Couleurs"))
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = false;
                label2.Text = "Fichier Excel";
                label3.Text = "Fichier Attributs";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = false;
            }
            else
            {
                textBox1.Visible = true;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = false;
                label5.Visible = false;
                label2.Text = "Fichier Excel";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
            }
        }

        private void setSanmarUI()
        {
            if (comboBox2.Text.Equals("Conversion"))
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = true;
                label2.Text = "Fichier Excel";
                label3.Text = "Fichier Excel Français";
                label5.Text = "Fichier Txt desc Français";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
            }
            else if (comboBox2.Text.Equals("Couleurs"))
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = false;
                label2.Text = "Fichier Excel Converti";
                label3.Text = "Fichier Attributs";
                label5.Text = "";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = false;
            }
            else if(comboBox2.Text.Equals("Items manquants"))
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = false;
                label2.Text = "Fichier txt items";
                label3.Text = "Fichier txt desc";
                label5.Text = "";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = false;
            }
            else if(comboBox2.Text.Equals("Ajouter images"))
            {
                textBox1.Visible = true;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                label2.Visible = true;
                label3.Visible = false;
                label5.Visible = false;
                label2.Text = "Fichier excel converti";
                label3.Text = "";
                label5.Text = "";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
            }
            else
            {
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = false;
                label3.Visible = false;
                label5.Visible = false;
                label2.Text = "";
                label3.Text = "";
                label5.Text = "";
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
            }
        }

        private void setBankersUI()
        {
            if (comboBox2.Text.Equals("Conversion"))
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = false;
                label2.Text = "Fichier Excel Français";
                label3.Text = "Fichier Excel Anglais";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = false;
            }
            else if (comboBox2.Text.Equals("Ajouter images"))
            {
                textBox1.Visible = true;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = false;
                label5.Visible = false;
                label2.Text = "Fichier Excel Original";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
            }
            else if (comboBox2.Text.Equals("Categories"))
            {
                textBox1.Visible = true;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = false;
                label5.Visible = false;
                label2.Text = "Fichier Excel Français";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
            }
            else if (comboBox2.Text.Equals("Couleurs"))
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = true;
                label2.Text = "Fichier Excel Français";
                label3.Text = "Fichier Excel Anglais";
                label5.Text = "Fichier Attributs";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
            }
            else if (comboBox2.Text.Equals("Merge"))
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                label2.Visible = true;
                label3.Visible = true;
                label5.Visible = false;
                label2.Text = "Fichier Excel Converti";
                label3.Text = "Fichier Attributs";
                label5.Text = "";
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBox2.Text.Equals("Nettoyage")) { 
                label1.Visible = true;
                comboBox1.Visible = true;
                if (comboBox1.Text.Equals("Bankers"))
                {
                    setBankersUI();
                }
                else if(comboBox1.Text.Equals("Debco"))
                {
                    setDebcoUI();
                }
                else if (comboBox1.Text.Equals("Leeds et Bullet"))
                {
                    setPcnaUI();
                }
                else if (comboBox1.Text.Equals("Sanmar"))
                {
                    setSanmarUI();
                }
                else
                {
                    label2.Visible = false;
                    textBox1.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;
                }
            }
            else
            {
                label1.Visible = false;
                comboBox1.Visible = false;
                label2.Visible = true;
                label2.Text = "Fichier Produits Export";
                textBox1.Visible = true;
                button1.Visible = true;
                button2.Visible = true;
            }
        }
        
    }
}
