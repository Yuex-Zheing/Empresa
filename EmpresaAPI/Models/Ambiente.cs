using System.Text;

namespace EmpresaAPI.Models
{
    public class Ambiente
    {
        public string db_host { get; set; }
        public string db_port { get; set; }
        public string db_name { get; set; }
        public string db_user { get; set; }
        public string db_clave { get; set; }

        public string getConexionString() {
            StringBuilder ctx = new StringBuilder();
            ctx.AppendFormat("Server = {0}; Database = {1}; User Id = {2}; Password = {3};",
                                db_host, db_name, db_user, db_clave);
            return  ctx.ToString();
        }
    }
}
