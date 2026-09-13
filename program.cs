using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsumerAdviceApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            string url = "https://api.adviceslip.com/advice";

            Console.WriteLine("Iniciando requisição para obter dados de um conselho:");
            Console.WriteLine();
            Console.WriteLine(url);
            Console.WriteLine();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Faz a requisição GET para a API
                    string responseBody = await client.GetStringAsync(url);

                    // Desserializa a resposta JSON para o objeto C#
                    AdviceResponse result = JsonSerializer.Deserialize<AdviceResponse>(responseBody);

                    if (result?.Slip != null)
                    {
                        Console.WriteLine("Conselho de Hoje:");
                        Console.WriteLine(result.Slip.Advice);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao obter conselho: {ex.Message}");
                }
            }

            // Mantém a janela aberta no Visual Studio
            Console.ReadKey();
        }
    }

    // Classes para mapear a resposta JSON da API: {"slip": { "id": 214, "advice": "..." }}
    public class AdviceResponse
    {
        [JsonPropertyName("slip")]
        public Slip Slip { get; set; }
    }

    public class Slip
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("advice")]
        public string Advice { get; set; }
    }
}
