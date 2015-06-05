using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IConverter_XML;
using CommonPlugin;

namespace Films
{
    sealed class PluginAdapter:IPlugin
    {
        private ITransform __adaptee;

        public string pluginName
        {
            get { return __adaptee.transformName; }
        }

        public ITransform SetPlugin { set { this.__adaptee = value; } }

        public void PluginFunction()
        {
            __adaptee.transform("file.xml", "newFile.html");
        }
    }
}
