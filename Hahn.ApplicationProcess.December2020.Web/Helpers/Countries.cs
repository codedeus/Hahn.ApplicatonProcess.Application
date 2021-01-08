using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Web.Helpers
{
    public static class Countries
    {
        public static async Task<bool> IsKnownCountry(string name)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = string.Format(CultureInfo.InvariantCulture, "https://restcountries.eu/rest/v2/name/{0}?fullText=true", name);
                    var uri = new Uri(url);
                    var response = await client.GetStringAsync(uri).ConfigureAwait(false);

                    using (HttpResponseMessage res = await client.GetAsync(uri))
                    {
                        using (HttpContent content = res.Content)
                        {
                            return res.IsSuccessStatusCode;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
