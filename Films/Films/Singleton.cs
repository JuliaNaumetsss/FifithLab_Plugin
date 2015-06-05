using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films
{
    public class Singleton
    {
        private List<object> myList = null;
        private static Singleton instance = null;

        private Singleton()
        {
            myList = new List<object>();
        }

        public static Singleton getInstance()
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }

        public void add(object newObject)
        {
            myList.Add(newObject);
        }

        public object getByIndex(int index)
        {
            return myList[index];
        }

        public void deleteByIndex(int index)
        {
            myList.RemoveAt(index);
        }

        public void setMyList(List<object> newList)
        {
            myList = newList;
        }

        public List<object> getMyList()
        {
            return myList;
        }

        public void clearMyList()
        {
            myList.Clear();
        }
    }
}