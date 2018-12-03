using UnityEngine;
using System.Collections;
using OfficeOpenXml;
using System.IO;

public class ExcelHelper
{

    public static ExcelBase LoadExcel(string path)
    {
        FileInfo file = new FileInfo(path);
        ExcelPackage ep = new ExcelPackage(file);
		ExcelBase xls = new ExcelBase(ep.Workbook);
        return xls;
    }

	public static ExcelBase CreateExcel(string path) {
		ExcelPackage ep = new ExcelPackage ();
		ep.Workbook.Worksheets.Add ("sheet");
		ExcelBase xls = new ExcelBase(ep.Workbook);
		SaveExcel (xls, path);
		return xls;
	}

    public static void SaveExcel(ExcelBase xls, string path)
    {
        FileInfo output = new FileInfo(path);
        ExcelPackage ep = new ExcelPackage();
        for (int i = 0; i < xls.Tables.Count; i++)
        {
            ExcelTable table = xls.Tables[i];
            ExcelWorksheet sheet = ep.Workbook.Worksheets.Add(table.TableName);
            for (int row = 1; row <= table.NumberOfRows; row++) {
                for (int column = 1; column <= table.NumberOfColumns; column++) {
                    sheet.Cells[row, column].Value = table.GetValue(row, column);
                }
            }
        }
        ep.SaveAs(output);
    }
}
