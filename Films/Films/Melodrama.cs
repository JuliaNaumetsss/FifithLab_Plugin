using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Films
{
    public class Melodrama : Growns
    {
        [XmlElement("countOfPart")]
        public int countOfPart { get; set; }
        public Melodrama()
        {
            name = "Дневники вампира";
            year = 2009;
            director = "Джули Плек";
            producer = "Майкл Саби";
            feature = "Мелодрама";
            actors = "Нина Добрев, Пол Уэсли, Йен Сомерхолдер";
            countOfPart = 6;
        }
    }
}
