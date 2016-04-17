using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadlock.WitAI
{
    public static class UrlManager
    {
        public static string Base { get; } = "https://api.wit.ai/";

        public static string Message(string message) => $"message?{System.Web.HttpUtility.ParseQueryString("").AddQuery("q", message).ToString()}";

        public static string Converse(string sessionId, string message = null) => $"converse?{System.Web.HttpUtility.ParseQueryString("").AddQuery("session_id", sessionId).AddQuery("q", message, () => !string.IsNullOrEmpty(message)).ToString()}";
    }
}
