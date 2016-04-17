using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deadlock.WitAI.Test
{
    public class ChatTest
    {
        private WitAIClient _Client;     
        private int _MaxSteps = 3;
        private int _CurrentStep = 0;
        private string _SessionId = Guid.NewGuid().ToString();
        private JObject _Context = new JObject();

        public ChatTest(string accessToken)
        {           
            this._Client = new WitAIClient(accessToken);
        }

        public void Start()
        {
            Console.WriteLine("Send a message...");
            this.WaitMessage();
        }

        private void WaitMessage()
        {
            this._CurrentStep = 0;
            string message = Console.ReadLine();                        
            var result = this._Client.Converse(this._SessionId, message, this._Context).Result;
            this.HandleResult(result);
        }

        private void HandleResult(Result<ConverseResponse> result)
        {
            if(result.Data.Type == ConverseType.Msg)
            {
                Console.WriteLine($"bot: {result.Data.Msg}");
                this.WaitMessage();
            }
            else if(result.Data.Type == ConverseType.Stop)
            {
                Console.WriteLine($"bot: estou sem entender nada!!!");
                this._Context = new JObject();
                this.WaitMessage();
            }
            else if (result.Data.Type == ConverseType.Action)
            {
                this.HandleAction(result);
                this.HandleResult(this._Client.Converse(this._SessionId, null, this._Context).Result);
            }
            else if (result.Data.Type == ConverseType.Merge)
            {
                this._CurrentStep++;
                if(this._CurrentStep < this._MaxSteps)
                {
                    if(result.Data.Entities.HasValues)
                    {
                        foreach(var property in result.Data.Entities.Properties())
                        {
                            if(this._Context.Property(property.Name) == null)
                                this._Context.Add(property.Name, (property.Value as JArray)[0]["value"]);
                        }
                    }
                    this.HandleResult(this._Client.Converse(this._SessionId, null, this._Context).Result);
                }
                else
                {
                    Console.WriteLine($"bot: estou sem entender nada!!!");
                    this.WaitMessage();
                }                
            }
        }

        private void HandleAction(Result<ConverseResponse> result)
        {
            if(result.Data.Action == "getPiada")
            {
                this._Context.Add("piada", "O garoto apanhou da vizinha, e a mãe furiosa foi tomar satisfação: Por que a senhora bateu no meu filho? Ele foi mal-educado, e me chamou de gorda. E a senhora acha que vai emagrecer batendo nele?");
            }
        }
    }
}
