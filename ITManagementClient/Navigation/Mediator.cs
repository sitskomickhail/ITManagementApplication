using System;
using System.Collections.Generic;

namespace ITManagementClient.Navigation
{
    public static class Mediator
    {
        private static IDictionary<string, Action<object>> pl_dict = new Dictionary<string, Action<object>>();

        public static void Subscribe(string token, Action<object> callback)
        {
            if (!pl_dict.ContainsKey(token))
            {
                pl_dict.Add(token, callback);
            }
            else
            {
                pl_dict[token] = callback;
            }
        }

        public static void Notify(string token, object args = null)
        {
            if (pl_dict.ContainsKey(token))
            {
                var callback = pl_dict[token];
                callback(args);
            }
        }
    }
}