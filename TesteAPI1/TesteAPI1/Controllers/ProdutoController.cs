using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteAPI1.Data;
using TesteAPI1.Models;

namespace TesteAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly TesteApiContext _context;

        public ProdutoController(ILogger<ProdutoController> logger, TesteApiContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("Enviar")]
        public async Task<IActionResult> Enviar()
        {
            try
            {
                _logger.LogInformation("Iniciou envio.");

                Guid id = Guid.NewGuid();

                _context.Produtos
                    .Add(new Produto()
                    {
                        Id = id,
                        Nome = $"Teste_{id}",
                        DataCadastro = DateTime.Now
                    });

                // simula demora no processamento
                Thread.Sleep(TimeSpan.FromSeconds(30));

                await _context.SaveChangesAsync();

                _logger.LogInformation(message: $"Produto {id}, salvou no banco.");

                return Ok(id);

            }
            catch (Exception ex)
            {
                string mensagem = $"Erro ao enviar produto. Exeption: {ex.Message}";
                _logger.LogError(message: mensagem);
                return BadRequest(error: mensagem);
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                _logger.LogInformation("Iniciou busca.");

                var produtos = await _context.Produtos.ToListAsync();

                if (produtos.Count > 0)
                    _logger.LogInformation("Encontrou produtos.");
                else
                    _logger.LogInformation("Não encontrou produtos.");

                return Ok(new
                {
                    Total = produtos.Count,
                    Produtos = produtos
                });
            }
            catch (Exception ex)
            {
                string mensagem = $"Erro ao listar produtos. Exeption: {ex.Message}";
                _logger.LogError(message: mensagem);
                return BadRequest(error: mensagem);
            }
        }
    }
}