﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonPlugin
{
    interface IPlugin
    {
        string pluginName { get; }

        void PluginFunction();
    }
}
