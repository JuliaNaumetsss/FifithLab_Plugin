using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainClass;

namespace MyNewClasses
{
   public class Horror:Film
    {
        public string filmCompany { get; set; }
        public Horror()
        {
            name = "Сердце ангела";
            year = 1987;
            producer = "Алан Маршал";
            director = "Алан Паркер";
            feature = "Ужасы";
            filmCompany = "TriStar Pictures";
        }
    }
}
