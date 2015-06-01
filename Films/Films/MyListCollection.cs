using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Films
{
    [Serializable]
    public class MyListCollection
    {
        [XmlArray]
        [XmlArrayItem("Films")]
        public List<object> myList;
    }
}
