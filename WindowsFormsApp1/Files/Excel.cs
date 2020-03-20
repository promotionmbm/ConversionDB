using System;
using ClosedXML.Excel;

namespace WindowsFormsApp1
{
    class Excel
    {
        String path = "";
        XLWorkbook wb;
        IXLWorksheet ws;

        public Excel(String path)
        {
            this.path = path;
            wb= new XLWorkbook();
            ws = wb.AddWorksheet("Products");
        }

        public Excel(String path, int sheet)
        {
            this.path = path;
            this.wb = new XLWorkbook(path);
            this.ws = wb.Worksheet(sheet);
        }

        public String getPath()
        {
            return this.path;
        }

        public static Excel createAndUseFile(String path)
        {
            String converted = path.Replace(".xls", "_converted.xls");
            Excel newFile = new Excel(converted);
            newFile.saveAs(converted);
            return newFile;
        }

        public double readDoubleCell(int i, int j)
        {
            i++;
            j++;
            if (!ws.Row(i).Cell(j).GetString().Equals(""))
                return ws.Row(i).Cell(j).GetDouble();
            else
                return 0;
        }

        public String readCell(int i, int j)
        {
            i++;
            j++;
            if (ws.Row(i).Cell(j).Value != null)
                return ws.Row(i).Cell(j).GetString();
            else
                return "";
        }

        public void writeBoldCell(int i, int j, String s)
        {
            i++;
            j++;
            ws.Row(i).Cell(j).Value = s;
            ws.Row(i).Cell(j).Style.Font.Bold = true;
        }

        public void writeCell(int i, int j, String s)
        {
            i++;
            j++;
            ws.Row(i).Cell(j).Value = s;
        }

        public void writeDoubleCell(int i, int j, double s)
        {
            i++;
            j++;
            if (s==0)
            {
                ws.Row(i).Cell(j).Value = "";
            }
            else
            {
                ws.Row(i).Cell(j).Style.NumberFormat.NumberFormatId = 2;
                ws.Row(i).Cell(j).Value = s;
            }
        }

        public void save()
        {
            wb.Save();
        }

        public void saveAs(String path)
        {
            wb.SaveAs(path);
        }

        public void changeSheet(int sheet)
        {
            this.ws = wb.Worksheet(sheet);
        }

        public int getRowCount()
        {
            return ws.LastRowUsed().RowNumber();
        }

        public int getColumnCount()
        {
            return ws.LastColumnUsed().ColumnNumber();
        }
    }
}
