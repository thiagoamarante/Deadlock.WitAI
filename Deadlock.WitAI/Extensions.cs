using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadlock.WitAI
{
    public static class Extensions
    {
        public static NameValueCollection AddQuery(this NameValueCollection collection, string name, string value, Func<bool> condition = null)
        {
            if(condition == null || condition())
                collection.Add(name, value);
            return collection;
        }
    }
}
