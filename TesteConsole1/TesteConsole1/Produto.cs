namespace TesteConsole1
{
    public class Produto
    {
        private readonly HttpClient _httpClient;

        public Produto(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7254");
        }

        public async Task EnviarProdutos()
        {
            var response = await _httpClient.PostAsync("api/Produto/Enviar", null);

            if (response.IsSuccessStatusCode)
                Console.WriteLine($"OK. ProdutoId: {await response.Content.ReadAsStringAsync()}");
            else
                Console.WriteLine("ERRO.");

        }
    }
}