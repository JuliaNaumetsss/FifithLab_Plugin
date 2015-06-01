using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonPlugin;
using System.Xml;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.IO;



namespace FromXMLtoJSON
{
    public class TransformXML : IPlugin
    {
        public string pluginName
        {
            get { return "TransformXML"; }
        }

            public void PluginFunction()
            {
            FileStream fs = new FileStream("file.xml", FileMode.Open);
            XmlDocument doc = new XmlDocument();
            doc.Load(fs);
            fs.Close();
            string json = JsonConvert.SerializeXmlNode(doc);

            File.WriteAllText("Data.json", json);
            }
    }
}
