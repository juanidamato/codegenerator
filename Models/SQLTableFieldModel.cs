using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codegenerator.Models
{
    public class SQLTableFieldModel
    {
        public string name { get; set; }
        public string fieldType { get; set; }
        public int isnullable { get; set; }
    }
}

