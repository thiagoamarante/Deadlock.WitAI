using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Deadlock.WitAI
{
    public class ConverseResponse
    {
        /// <summary>
        /// The type of the bot response. Either merge (first bot action after a user message), msg (the bot has something to say), action (the bot has something to do) or stop (the bot is waiting to proceed).
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ConverseType Type { get; set; }

        /// <summary>
        /// The answer of your bot, when applicable.
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// The action to execute, when applicable.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Object of entities, when applicable. Each entity is an array of values (even when there is only one value).
        /// </summary>
        public JObject Entities { get; set; }

        /// <summary>
        /// Represents the confidence level of the next step, between 0 (low) and 1 (high).
        /// </summary>
        public decimal Confidence { get; set; }
    }
}
