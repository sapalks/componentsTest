﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole {
    class Program {
        static void Main(string[] args) {
            System.Reflection.Assembly assembly =
                System.Reflection.Assembly.LoadFile
                (@"D:\Project\ComponentGradient\ComponentsLibrary\bin\Debug\ComponentsLibrary.dll");
            Type[] type = assembly.GetTypes();
            foreach (Type item in type) {
                Console.WriteLine(item.FullName);
            }
            Console.ReadKey();
        }
    }
}
