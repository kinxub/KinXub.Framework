using System.Configuration;

namespace KinXub.Framework.RepositoryDal.Base
{
    public class DbList
    {
        //.Net Framework
        public static readonly string MSdb = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

        //.Net Core
        //public static readonly string MSdb = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["ConnectionStrings:sqlconnection"];
    }
}
