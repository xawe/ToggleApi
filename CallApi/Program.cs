using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ToogleApi.Models;
using System.Text.Encodings;
using System.Collections.Generic;

namespace CallApi
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            var qtd = Convert.ToInt32(Console.ReadLine());

            RunAsync(qtd).GetAwaiter().GetResult();

        }

        static async Task RunAsync(int n)
        {            
            client.BaseAddress = new Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {

                var r = await GetFeatureToggleAsync("api/FeatureToogle/4ba9a9c4-e1be-4db6-b2a3-ae0f0d2c52ef");

                List<FeatureFlag> flags = new List<FeatureFlag>();
                for (int i = 0; i < n; i++)
                {
                    var id = Guid.NewGuid().ToString();
                    flags.Add(new FeatureFlag
                    {
                        CreatedOn = DateTime.Now,
                        Description = "...",
                        Flag = "Flag." + i.ToString(),
                        Id = id,
                        Value = "S"
                    });
                }


                flags.ForEach( async f  =>  await CreateFeatureToogle(f));

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        static async Task<FeatureFlag> GetFeatureToggleAsync(string path)
        {
            FeatureFlag ff = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                ff = await response.Content.ReadAsAsync<FeatureFlag>();
            }
            return ff;
        }
        static async Task<Uri> CreateFeatureToogle(FeatureFlag flag)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(flag);
                HttpResponseMessage response = await client.PostAsync(
                    "api/FeatureToogle", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                response.EnsureSuccessStatusCode();

                // return URI of the created resource.
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                var x = ex;
                throw;
            }
            
        }

    }
}
