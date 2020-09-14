namespace Application.DTO.DTO
{
    public class ProdutoDTO
    {
        public int? CodigoProduto { get; set; }
        public string Nome { get; set; }
        public int Fabricacao { get; set; }
        public string Tamanho { get; set; }
        public decimal Valor { get; set; }
    }
}