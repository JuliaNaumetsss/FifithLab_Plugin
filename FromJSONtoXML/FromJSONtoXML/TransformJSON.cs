using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonPlugin;
using System.IO;
using Newtonsoft.Json;
using System.Xml;

namespace FromJSONtoXML
{
    public class TransformJSON : IPlugin
    {
        public string pluginName
        {
            get { return "TransformJSON"; }
        }
        public void PluginFunction()
        {
            string json = null;

            using (StreamReader sr = new StreamReader("Data.json"))
            {
                json = sr.ReadToEnd();
            }
            XmlDocument doc = (XmlDocument)JsonConvert.DeserializeXmlNode(json);
            doc.Save("file.xml");
        }
    }
}
