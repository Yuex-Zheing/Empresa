using Empresa.Services;

namespace EmpresaWebTest.Models
{
    public class ProcessReturn
    {
        public Object ObjectReturn { get; set; }
        public List<Error> error { get; set; }

        public ProcessReturn()
        {            
            error = new List<Error>();
        }
    }
}
