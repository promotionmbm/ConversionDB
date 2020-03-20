using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Universal_Info
{
    class TasksWithConvertedExcel
    {
        public static void ajouterCouleurs(Excel convertedExcel, Excel attributesExcel)
        {
            int maxCol = attributesExcel.getColumnCount();
            int maxRow = attributesExcel.getRowCount();
            int convertedRows = convertedExcel.getRowCount();
            int colorRow = 0;
            for (int i = 0; i < maxRow; i++)
            {
                if (attributesExcel.readCell(i, 0).Equals("Couleur"))
                {
                    colorRow = i;
                    break;
                }
            }
            ArrayList colors = new ArrayList();
            int color = 8;
            while (color < maxCol)
            {
                colors.Add(attributesExcel.readCell(colorRow, color).ToUpper());
                if (attributesExcel.readCell(colorRow, color).Equals(""))
                {
                    Console.WriteLine(color + " est vide.");
                }
                if (attributesExcel.readCell(colorRow, (color + 1)).Equals(""))
                {
                    Console.WriteLine((color + 1) + " est vide.");
                }
                if (attributesExcel.readCell(colorRow, (color + 2)).Equals(""))
                {
                    Console.WriteLine((color + 2) + " est vide.");
                }
                color += 3;
            }
            for (int i = 1; i < convertedRows; i++)
            {
                Boolean exists = false;
                if (!convertedExcel.readCell(i, 25).Equals(""))
                {
                    for (int k = 0; k < colors.Count; k++)
                    {
                        if (convertedExcel.readCell(i, 25).ToUpper().Equals((String)colors[k]))
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        colors.Add(convertedExcel.readCell(i, 25).ToUpper());
                        attributesExcel.writeBoldCell(0, maxCol, "Predefined Value " + colors.Count + " Value ");
                        attributesExcel.writeBoldCell(0, maxCol + 1, "Predefined Value " + colors.Count + " Description en ");
                        attributesExcel.writeBoldCell(0, maxCol + 2, "Predefined Value " + colors.Count + " Description fr ");
                        attributesExcel.writeCell(colorRow, maxCol, convertedExcel.readCell(i, 25));
                        attributesExcel.writeCell(colorRow, maxCol + 1, convertedExcel.readCell(i, 25));
                        attributesExcel.writeCell(colorRow, maxCol + 2, convertedExcel.readCell(i, 25));
                        maxCol += 3;
                    }
                }
            }
            attributesExcel.save();
        } 
    }
}
