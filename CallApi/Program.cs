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
        static void Main(string[] args)
        {
            var qtd = Convert.ToInt32(Console.ReadLine());

            RunAsync(qtd).GetAwaiter().GetResult();

            Console.WriteLine("Processo Finalizado");
            Console.ReadLine();

        }

        static async Task RunAsync(int n)
        {           
            try
            {
                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                    using (var client = new HttpClient(httpClientHandler))
                    {
                        client.BaseAddress = new Uri("http://localhost:5000/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));
                        
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

                        foreach (var f in flags)
                        {
                            await CreateFeatureToogle(f, client);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        
        static async Task<Uri> CreateFeatureToogle(FeatureFlag flag, HttpClient client)
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
