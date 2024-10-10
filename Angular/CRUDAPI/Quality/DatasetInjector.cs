using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CRUDAPI.Models;
using Newtonsoft.Json;

namespace CRUDAPI.Services
{
    public class DatasetInjector
    {
        private readonly HttpClient _httpClient;

        public DatasetInjector(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RunMockScript()
        {
            // Carregar dados dos arquivos JSON
            var usuariosJson = await File.ReadAllTextAsync("mock-usuarios.json");
            var modalidadesJson = await File.ReadAllTextAsync("mock-modalidades.json");
            var categoriasJson = await File.ReadAllTextAsync("mock-categorias.json");
            var timesJson = await File.ReadAllTextAsync("mock-times.json");
            var inscricoesJson = await File.ReadAllTextAsync("mock-inscricoes.json");
            var confrontosJson = await File.ReadAllTextAsync("mock-confrontos.json");
            var confrontoInscricoesJson = await File.ReadAllTextAsync("mock-confrontoInscricoes.json");
            // Adicionar mais arquivos JSON conforme necessário...

            // Converter JSON para objetos C#
            var usuarios = JsonConvert.DeserializeObject<Usuario[]>(usuariosJson);
            var categorias = JsonConvert.DeserializeObject<Categoria[]>(categoriasJson);
            var modalidades = JsonConvert.DeserializeObject<Modalidade[]>(modalidadesJson);
            var times = JsonConvert.DeserializeObject<Time[]>(timesJson);
            var inscricoes = JsonConvert.DeserializeObject<Inscricao[]>(inscricoesJson);
            var confrontos = JsonConvert.DeserializeObject<Confronto[]>(confrontosJson);
            var confrontoInscricoes = JsonConvert.DeserializeObject<ConfrontoInscricao[]>(confrontoInscricoesJson);
            // Converter mais arquivos JSON conforme necessário...

            // Simular solicitações HTTP
            foreach (var usuario in usuarios)
            {
                var response = await _httpClient.PostAsJsonAsync("api/usuario", usuario);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao criar usuário: {response.ReasonPhrase}");
                }
            }

            foreach (var modalidade in modalidades)
            {
                var response = await _httpClient.PostAsJsonAsync("api/modalidade", modalidade);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao criar modalidade: {response.ReasonPhrase}");
                }
            }

           foreach (var categoria in categorias)
            {
                var response = await _httpClient.PostAsJsonAsync("api/categoria", categoria);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao criar categoria: {response.ReasonPhrase}");
                }
            }

            foreach (var time in times)
            {
                var response = await _httpClient.PostAsJsonAsync("api/time", time);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao criar time: {response.ReasonPhrase}");
                }
            }

            foreach (var inscricao in inscricoes)
            {
                var response = await _httpClient.PostAsJsonAsync("api/inscricao", inscricao);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao criar inscricao: {response.ReasonPhrase}");
                }
            }

            foreach (var confronto in confrontos)
            {
                var response = await _httpClient.PostAsJsonAsync("api/confronto", confronto);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao criar confronto: {response.ReasonPhrase}");
                }
            }

            foreach (var confrontoInscricao in confrontoInscricoes)
            {
                var response = await _httpClient.PostAsJsonAsync("api/confrontoInscricao", confrontoInscricao);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao criar confrontoInscricao: {response.ReasonPhrase}");
                }
            }
        }
    }
}
