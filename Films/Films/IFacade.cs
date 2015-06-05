using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Films
{
    class IFacade
    {
        public void serialize(List<object> myList, Type[] extraTypes)
        {
            XmlSerializer xs = new XmlSerializer(typeof(MyListCollection), extraTypes);
            var myCollection = new MyListCollection();
            myCollection.myList = myList;
            TextWriter writer = new StreamWriter("file.xml");
            xs.Serialize(writer, myCollection);
            writer.Close();
        }

        public List<object> deserialize(Type[] extraTypes)
        {
            List<object> myList = new List<object>();
            XmlSerializer mySerializer = new XmlSerializer(typeof(MyListCollection), extraTypes);
            FileStream fs = new FileStream("file.xml", FileMode.Open);
            try
            {
                MyListCollection myCollection = (MyListCollection)mySerializer.Deserialize(fs);
                myList = myCollection.myList;
            }
            catch { }
            fs.Close();
            return myList;
        }

        public Type[] getTypesArrary(Type[] types, Type[] newTypes, bool flag )
        {
            if (flag == true)
            {
                types = new Type[newTypes.Length + 3];
                int count = 3;
                foreach (Type tempType in newTypes)
                {
                    types[count] = tempType;
                    ++count;
                }
            }
            else
            {
                types = new Type[3];
            }
            types[0] = typeof(Cartoons);
            types[1] = typeof(Fiction);
            types[2] = typeof(Melodrama);
            return types;
        }
    }
}
