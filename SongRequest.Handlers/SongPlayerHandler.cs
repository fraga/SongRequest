using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SongRequest.Handlers
{
    public class SongPlayerHandler: BaseHandler
    {
        public override void Process(System.Net.HttpListenerRequest request, System.Net.HttpListenerResponse response)
        {
            string[] actionPath = request.RawUrl.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            string action = actionPath[1];
        }
    }
}
