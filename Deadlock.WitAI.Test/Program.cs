using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Deadlock.WitAI.Test
{
    class Program
    {
        static Config _Config = null;
        

        static void Main(string[] args)
        {
            _Config = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "config.personal.json"));
            Test().Wait();
            Chat();
            Console.ReadLine();
        }

        static async Task Test()
        {
            //await Message();
            //await Converse1();
            //await Converse2();
            //await Converse();          
        }

        static async Task Message()
        {
            using (WitAIClient client = new WitAIClient(_Config.AccessToken))
            {
                var result = await client.Message("como está o tempo?");
                
            }
        }

        static void Chat()
        {
            ChatTest chat = new ChatTest(_Config.AccessToken);
            chat.Start();
        }      

        static async Task Converse1()
        {
            using (WitAIClient client = new WitAIClient(_Config.AccessToken))
            {
                string sessionId = "123456";
                JObject context = new JObject();
                Result<ConverseResponse> result = null;
                result = await client.Converse(sessionId, "onde você mora?", context);
                if(result.Data.Type == ConverseType.Merge)
                {
                    result = await client.Converse(sessionId, null, context);
                    if(result.Data.Type == ConverseType.Msg)
                    {
                        Console.WriteLine(result.Data.Msg);
                    }
                }
                
            }
        }

        static async Task Converse2()
        {
            using (WitAIClient client = new WitAIClient(_Config.AccessToken))
            {
                string sessionId = "1234567";
                JObject context = new JObject();
                Result<ConverseResponse> result = null;
                result = await client.Converse(sessionId, "conta uma piada?", context);
                if (result.Data.Type == ConverseType.Merge)
                {
                    result = await client.Converse(sessionId, null, context);
                    if (result.Data.Type == ConverseType.Action && result.Data.Action == "getPiada")
                    {
                        context.Add("piada", "O garoto apanhou da vizinha, e a mãe furiosa foi tomar satisfação: Por que a senhora bateu no meu filho? Ele foi mal-educado, e me chamou de gorda. E a senhora acha que vai emagrecer batendo nele?");
                        result = await client.Converse(sessionId, null, context);
                        Console.WriteLine(result.Data.Msg);
                    }
                }

            }
        }

        static async Task Converse()
        {
            using (WitAIClient client = new WitAIClient(_Config.AccessToken))
            {
                string sessionId = "123456";
                JObject context = new JObject();

                var result1 = await client.Converse(sessionId, "oi bot, tudo bem com você?", context);
                if (result1.Data.Type == ConverseType.Merge)
                {
                    var result2 = await client.Converse(sessionId, null, context);

                    var result3 = await client.Converse(sessionId, "como esta o tempo?", context);

                    var result4 = await client.Converse(sessionId, null, context);

                    var result5 = await client.Converse(sessionId, "em Fortaleza", context);
                    context.Add("location", "Fortaleza");

                    var result6 = await client.Converse(sessionId, null, context);

                    var result7 = await client.Converse(sessionId, "em?", context);
                }
            }
        }
    }
}
