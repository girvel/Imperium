using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Province.Log
{
    public class Log
    {
        public readonly List<TextWriter> Writers;



        public Log(params TextWriter[] writers)
        {
            Writers = writers.ToList();
        }

        public virtual void Message(string text)
        {
            var str = $"{DateTime.Now:hh:mm:ss}\t {text}\n\n";
            foreach (var w in Writers)
            {
                w.Write(str);
            }
        }
    }
}