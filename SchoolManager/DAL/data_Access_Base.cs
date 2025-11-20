using System.Configuration;

namespace SchoolManager.DAL
{
    public abstract class data_Access_Base
    {
        protected string connection_String;
        public data_Access_Base()
        {
            connection_String = ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString;
        }
    }
}