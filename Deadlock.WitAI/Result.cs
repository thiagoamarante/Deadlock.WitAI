using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Deadlock.WitAI
{
    public class Result<T>
    {
        public string Content { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSuccessStatusCode { get; set; }

        public T Data { get; set; }
    }
}
