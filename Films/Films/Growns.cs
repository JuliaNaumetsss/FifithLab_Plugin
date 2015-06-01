using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainClass;
using System.Xml;
using System.Xml.Serialization;

namespace Films
{
    public abstract class Growns : Film
    {
        [XmlElement("actors")]
        public string actors { get; set; }
    }
}
