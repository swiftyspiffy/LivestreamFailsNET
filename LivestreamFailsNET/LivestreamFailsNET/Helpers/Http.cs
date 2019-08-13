using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LivestreamFailsNET.Helpers
{
    internal class Http
    {
        internal static async Task<string> GetRequestAsync(string endpoint, List<KeyValuePair<string, string>> getParameters)
        {
            if(getParameters != null && getParameters.Count > 0)
            {
                for(var i = 0; i < getParameters.Count; i++)
                {
                    if (i == 0)
                        endpoint += $"?{queryParam(getParameters[i])}";
                    else
                        endpoint += $"&{queryParam(getParameters[i])}";
                }
            }

            using (HttpClient client = new HttpClient())
                return await client.GetStringAsync(endpoint);
        }

        private static string queryParam(KeyValuePair<string, string> kp)
        {
            return $"{HttpUtility.UrlEncode(kp.Key)}={HttpUtility.UrlEncode(kp.Value)}";
        }
    }
}
