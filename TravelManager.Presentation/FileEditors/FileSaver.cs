using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using TravelManager.Domain.Entities;
using TravelManager.Domain.Interfaces;
using TravelManager.Presentation.ViewModels;

namespace TravelManager.Presentation.FileEditors
{
    class FileSaver : IFileSaver<TripViewModel>
    {
        private const string FILEPATH = "trips";
        public SpecificNotifications Save(IEnumerable<TripViewModel> collection)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var finalPath = Path.Combine(basePath, FILEPATH + ".csv");
            DataTable dataTable = ToDataTable(collection);

            ToCSV(dataTable, finalPath);
            return SpecificNotifications.ExportInfo;

        }

        private void ToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, true);
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }

            sw.Write(sw.NewLine);

            foreach (DataRow dr in dtDataTable.Rows)
            {

                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {

                    if (!Convert.IsDBNull(dr[i]))
                    {

                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }

                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }

                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }

                sw.Write(sw.NewLine);
            }

            sw.Close();
        }

        private DataTable ToDataTable(IEnumerable<TripViewModel> collection)
        {
            DataTable dataTable = new DataTable(typeof(TripViewModel).Name);
            PropertyInfo[] Props = typeof(TripViewModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }

            foreach (TripViewModel item in collection)
            {
                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}

