using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainClass;

namespace MyNewClasses
{
    public class Historical:Film
    {
        public string country { get; set; }
        public Historical()
        {
            name = "Коко до Шанель";
            year = 2009;
            producer = "Кэролайн Бенджо, Филипп Каркассон, Кэрол Скотта";
            director = "Анн Фонтен";
            feature = "исторический";
            country = "Франция, Бельгия";
        }
    }
}
