using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Films
{
    public class Fiction: Childrens
    {
        [XmlElement("boxOffice")]
        public string boxOffice { get; set; }
         public Fiction()
        {
            name = "Интерстеллар";
            year = 2014;
            director = "Кристофер Нолан";
            producer = "Кристофер Нолан, Эмма Томас, Линда Обст";
            feature = "Фантастика";
            voiceOfTheFilm = "Мэттью Макконахи, Энн Хэтэуэй";
            boxOffice = "672 млн";
        }
    }
}
