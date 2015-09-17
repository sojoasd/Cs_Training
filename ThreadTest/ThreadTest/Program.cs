﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Program that = new Program();

            //string PathStr = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).ToString();

            //PathStr = PathStr.Replace("bin","");

            Assembly oAss = Assembly.LoadFile(@"E:\Workspaces\ThreadTest\dllroot\Cars.dll");

            while (true)
            {
                Thread.Sleep(500);

                foreach (var type in oAss.GetExportedTypes())
                {
                    if (!type.IsInterface)
                    {
                        Object obj = oAss.CreateInstance(type.FullName);

                        MethodInfo method = oAss.GetType(type.FullName).GetMethod("ShowBrand");

                        Thread oth = new Thread(() => that.DoWork(method, obj, new Object[] { }));

                        oth.Start();

                        Thread.Sleep(500);
                    }
                }
                Thread.Sleep(500);
                Console.WriteLine("====================================================");
            }
        }

        public void DoWork(MethodInfo method, object obj, params object[] parameters)
        {
            Object res = method.Invoke(obj, parameters);
            Console.WriteLine("the Method returned {0}.", res);
        }
    }
}