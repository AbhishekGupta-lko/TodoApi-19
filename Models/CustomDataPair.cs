using System.Data.SqlClient;
using System.Data;

namespace TodoApi.Models
{
    public class CustomDataPair
    {
        public string Key { get; set; }
        public object Obj { get; set; }
    }
}
