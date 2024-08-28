using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using TesteConsole1;


// Registrar as dependências
var services = new ServiceCollection();
services.AddHttpClient<Produto>();
var serviceProvider = services.BuildServiceProvider();

var produto = serviceProvider.GetRequiredService<Produto>();


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
                    await produto.EnviarProdutos();
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
    Console.WriteLine($"Tempo de processamento: {stopwatch.Elapsed}");
}
catch (Exception ex)
{

    Console.WriteLine($"ERRO: {ex.Message}");
}