using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainClass;

namespace MyNewClasses
{
    public class Thriller:Film
    {
        public string actors { get; set; }
        public Thriller()
        {
            name = "Начало";
            year = 2010;
            producer = "Кристофер Нолан, Эмма Томас";
            director = "Кристофер Нолан";
            feature = "Триллер";
            actors = "Леонардо Ди Каприо, Эллен Пейдж, Джозеф Гордон-Левитт";

        }
    }
}
