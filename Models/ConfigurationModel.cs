using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codegenerator.Models
{
    public  class GeneratorConfigurationModel
    {
        public  string ConnectionString { get; set; }
        public  string Author { get; set; }
        public  string DomainNamespace { get; set; }
        public string EntitiesSuffixNamespace { get; set; }
        public string EntitiesInheritsFrom { get; set; }
        public  string ApplicationNamespace { get; set; }

      
    }
    public  static class GeneratorConfigurationManager
    {
        public static GeneratorConfigurationModel _gConfig { get; set; } = new();

        public static void ReadFromFile()
        {
            string curr;
            string Json;
            curr = Path.GetDirectoryName(Application.ExecutablePath);
            if (File.Exists(Path.Combine(curr, "config.txt")))
            {
                Json = File.ReadAllText(Path.Combine(curr, "config.txt"));
                try
                {
                    _gConfig=System.Text.Json.JsonSerializer.Deserialize<GeneratorConfigurationModel>(Json);
                }
                catch
                {
                    //ignore exception
                }
            }
           
        }

        public static void SaveToFile()
        {
            string curr;
            curr = Path.GetDirectoryName(Application.ExecutablePath);
            File.WriteAllText(Path.Combine(curr, "config.txt"), System.Text.Json.JsonSerializer.Serialize(GeneratorConfigurationManager._gConfig));
        }
    }
}
