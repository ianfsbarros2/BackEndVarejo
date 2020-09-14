using Domain.Models.Enum;

namespace Domain.Models
{
    public class Cliente : Base
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public SexoEnum Sexo { get; set; }
        public string Email { get; set; }
    }
}