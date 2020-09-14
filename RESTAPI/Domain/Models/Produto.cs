using Domain.Models.Enum;

namespace Domain.Models
{
    public class Produto : Base
    {
        public string Nome { get; set; }
        public FabricacaoEnum Fabricacao { get; set; }
        public string Tamanho { get; set; }
        public decimal Valor { get; set; }
        
        public int? IdPedido { get; set; }
    }
}