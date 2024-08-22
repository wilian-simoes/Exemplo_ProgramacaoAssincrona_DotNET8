namespace TesteAPI1.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}