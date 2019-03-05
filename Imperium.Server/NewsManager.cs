using System;
using System.Collections.Generic;

namespace Imperium.Server
{
    public class NewsManager<T>
    {
        private readonly Dictionary<T, List<News>> _registeredNews 
            = new Dictionary<T, List<News>>();

        

        internal void BeginNewsRegistration(T t)
        {
            lock (_registeredNews) _registeredNews[t] = new List<News>();
        }

        internal void EndNewsRegistration(T t)
        {
            lock (_registeredNews) _registeredNews.Remove(t);
        }



        public void AddNews(T t, string type, Dictionary<string, dynamic> info, bool unical = false)
        {
            lock (_registeredNews)
            {
                if (_registeredNews.ContainsKey(t))
                {
                    if (unical)
                    {
                        _registeredNews[t].RemoveAll(n => n.Type == type);
                    }
                
                    _registeredNews[t].Add(new News {Type = type, Info = info,});
                }
            }
        }

        public News[] GetNews(T t)
        {
            lock (_registeredNews)
            {
                if (_registeredNews.ContainsKey(t))
                {
                    var result = _registeredNews[t].ToArray();
                    _registeredNews[t].Clear();
                    return result;
                }
            }

            throw new InvalidOperationException();
        }
    }
}