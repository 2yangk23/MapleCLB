using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;

namespace MapleCLB.Tools {
    public class ItemParse {
        public static Dictionary<int, string> Parsing_Data(String which)
        {
            Dictionary<int, string> data = new Dictionary<int, string>();
            string line;
            string[] stringSeparators = new string[] {"-"};
            string[] result;
            //int counter = 0;
            Assembly assembly = Assembly.GetExecutingAssembly();
            System.IO.StreamReader file = new System.IO.StreamReader(assembly.GetManifestResourceStream("MapleCLB.Resources."+which+".txt"));
            while ((line = file.ReadLine()) != null){
            /*    line = Regex.Replace(line, @"[^\u0000-\u007F]", "?");
                if (Array.Exists(result, element => element == line)){
                    Console.WriteLine("Ignoring Line @ " + counter);
                }
                else{
                    using (System.IO.StreamWriter stuff = new System.IO.StreamWriter(@"c:\\test2.txt", true)){
                        stuff.WriteLine(line);
                    }
                }
                result[counter] = line;
                counter ++;
              */  
                result = line.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                data.Add(Int32.Parse(result[0].Trim()),result[1].Trim());
            }
            file.Close();
            return data;
        }
    }
}