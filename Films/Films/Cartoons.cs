using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Films
{
    public class Cartoons : Childrens
    {
        [XmlElement("duration")]
        public string duration { get; set; }
        public Cartoons()
        {
            name = "Гадкий я";
            year = 2010;
            director = "Пьер Коффин, Крис Рено";
            producer = "Джон Коэн, Крис Меледандри, Джанет Хили";
            feature = "Мультфильм";
            voiceOfTheFilm = "Стив Карелл, Джейсон Сигел, Джули Эндрюс";
            duration = "95 мин";
        }
    }
}
