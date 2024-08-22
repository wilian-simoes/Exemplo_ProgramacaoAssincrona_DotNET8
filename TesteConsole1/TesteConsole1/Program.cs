using System.Diagnostics;

Console.WriteLine("Iniciou o processamento.");

Stopwatch stopwatch = new();
stopwatch.Start();

try
{
    List<Task> tasks = [];
    SemaphoreSlim semaphoreSlim = new(initialCount: 2);

    for (int i = 0; i < 4; i++)
    {
        await semaphoreSlim.WaitAsync();

        tasks.Add(
            Task.Run(async() =>
            {
                try
                {
                    await EnviarProdutos();
                }
                finally
                {
                    semaphoreSlim.Release();
                }
            }));
    }

    await Task.WhenAll(tasks);

    Console.WriteLine("Fim do processamento.");
    stopwatch.Stop();
    Console.WriteLine(stopwatch.Elapsed);
}
catch (Exception ex)
{

    Console.WriteLine($"ERRO: {ex.Message}");
}

async Task EnviarProdutos()
{
    using (var client = new HttpClient())
    {
        var response = await client.PostAsync("https://localhost:7254/api/Produto/Enviar", null);

        if (response.IsSuccessStatusCode)
            Console.WriteLine($"OK. ProdutoId: {await response.Content.ReadAsStringAsync()}");
        else
            Console.WriteLine("ERRO.");
    }
}