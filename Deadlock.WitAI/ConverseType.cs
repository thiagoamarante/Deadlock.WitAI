using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadlock.WitAI
{
    public enum ConverseType
    {
        /// <summary>
        /// Merge means that you need to execute the merge action;
        /// </summary>
        Merge,
        /// <summary>
        /// Msg means that you need to execute the say action;
        /// </summary>
        Msg,
        /// <summary>
        /// Action means that you need to execute the action whose name is specified in the action field;
        /// </summary>
        Action,
        /// <summary>
        /// Stop means that the bot is done: there is nothing left to do until the next user message.
        /// </summary>
        Stop
    }
}
