using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MainClass
{
    [Serializable]
    [XmlRoot("Film")]
    public abstract class Film
    {
        public string name {  get; set; }
        public int year { get; set; }
        public string director { get; set; }
        public string producer { get; set; }
        public string feature { get; set; }
    }
}
