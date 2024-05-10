using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api_Estudo.Configuration
{
    public class Base
    {
        public static string GetConnectionString()
        {

            return System.Configuration.ConfigurationManager.ConnectionStrings["consultorio"].ConnectionString;

        }

    }


}
