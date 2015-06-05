using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Films
{
    public static class Facade
    {
        static IFacade currValue = new IFacade();

        public static void OperationSerialize(List<object> list, Type[] types, Type[] newTypes, bool flag)
        {
            types = currValue.getTypesArrary(types, newTypes, flag);
            currValue.serialize(list, types);
        }

        public static List<object> OperationDeserialize(Type[] types, Type[] newTypes, bool flag)
        {
            types = currValue.getTypesArrary(types, newTypes, flag);
            return currValue.deserialize(types);
        }
        
    }
}
