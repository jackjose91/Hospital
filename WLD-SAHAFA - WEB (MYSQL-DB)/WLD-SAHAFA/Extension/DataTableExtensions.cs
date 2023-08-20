using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;
namespace WLD_SAHAFA.Extension
{
    public static class DataTableExtensions
    {
        public static IList<T> ToList<T>(this DataTable table)
        {
            if (table == null)
                return null ;
            var result=JsonConvert.SerializeObject(table);
            return JsonConvert.DeserializeObject<IList<T>>(result.ToString());
        }
    }
}