# Exemplo_ProgramacaoAssincrona_DotNET8
Exemplo de Programação Assíncrona baseada em tarefas utilizando o .NET 8

Neste exemplo a aplicação "TesteConsole1" fará chamadas na API "TesteAPI1" para simular o envio de requests assíncronas.

No console foi utilizado a classe SemaphoreSlim exemplificar um limite de quantidades de requests por vez na API.

A API foi desenvolvida em .NET 8. E foi utilizado o Entity Framework Core In-Memory para persistir os dados em memória.
Ao persistir o produto no banco de dados em memória foi adicionado um delay de 30 segundos para simular a demora de um processamento.

O exemplo visa demonstrar o ganho de tempo no processamento. 
Conforme aprensetado aqui são 4 requests em que cada uma demora 30 segundos para terminar.
Se fosse executado de forma síncrona, uma por vez, iria demorar 2 minutos. Mas como mostrado aqui esse tempo pode cair para 30 segundos no total, para todas tarefas.
No caso esta demorando 1 minuto pois utilizei a classe SemaphoreSlim para limitar requests em 2 por vez.
