using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace CommonPlugin
{
    public class PluginManager
    {
        public List<IPlugin> Plugins = new List<IPlugin>();

        public void ScanPlugins(string directory)
        {
            //перебираем все файлы dll
            foreach (var file in Directory.EnumerateFiles(directory, "*.dll", SearchOption.AllDirectories))
                try
                {
                    var ass = Assembly.LoadFile(file);
                    foreach (var type in ass.GetTypes())
                    {
                        //проверяем наличие интерфейса IPlugin
                        var i = type.GetInterface("IPlugin");
                        if (i != null)
                            //создаем экземпляр плагина 
                            Plugins.Add(ass.CreateInstance(type.FullName) as IPlugin);
                    }
                }
                catch {/*is not .NET assembly*/}
        }

    }
}
