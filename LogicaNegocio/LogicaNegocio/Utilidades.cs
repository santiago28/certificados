using Datos.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using Datos.DTO;
using System.Linq;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace LogicaNegocio.LogicaNegocio
{
    public static class Utilidades
    {
        public static DataTable ToDataTable(List<Row> data)
        {

            //PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            //foreach (PropertyDescriptor prop in properties)
            //    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var items in data)
            {
                DataRow row = table.NewRow();
                //var i = 0;
                //foreach (var value in items)

                //    row[i] = value;
                //    table.Rows.Add(row);
                //    i++;
            }
            return table;

        }
    }
}
