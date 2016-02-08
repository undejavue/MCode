using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MClassLib
{
    class CommManager
    {
        public static string ConnectionString
        {
            get
            {
                //return WebConfigurationManager.AppSettings["storeDbConnectionString"];
                return "Data Source=WINCC-PROJECT\\WINCC;Initial Catalog=MCODE_DB;User ID=scada;Password=qazwsx";
            }
        }
    }
}
