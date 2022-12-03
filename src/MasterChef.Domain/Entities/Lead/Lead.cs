using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MasterChef.Domain.Entities
{
    public partial class Lead : BaseEntity
    {
        [DisplayName("Data de cadastro")]
        public DateTime DataCadastro { get; set; }
        public DateTime LastUpdate { get; set; }

        [Required(ErrorMessage = "Inserir o nome")]
        [MaxLength(100, ErrorMessage = "Tamanho do nome inv�lido, favor inserir um valor at� 100 caracteres.")]
        public string Nome { get; set; }

        public bool AceitaPropaganda { get; set; }

        [MaxLength(50, ErrorMessage = "Tamanho do E-mail inv�lido!")]
        [EmailAddress(ErrorMessage = "Por favor, insira um e-mail v�lido")]
        [DisplayName("E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Favor inserir um n�mero de telefone v�lido")]
        [MaxLength(14)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [MaxLength(7)]
        public string Cupom { get; set; }
        
        public static ILeadBuilder Create() => new Builder();
        public static ILeadBuilder Create(Lead instance) => new Builder(instance);
    }
}