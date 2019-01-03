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

        ~Log()
        {
            Writers.ForEach(w => w.Dispose());
        }

        public virtual void Message(string text)
        {
            var str = $"{DateTime.Now:hh:mm:ss}\t {text}\n";
            foreach (var w in Writers)
            {
                w.Write(str);
            }
        }
    }
}