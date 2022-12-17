using System;
using System.Collections.Generic;

namespace MasterChef.Web.Models
{
    public class IngredienteModel
    {
        public int? id { get; set; }
        public string? Nome { get; set; }
        public string? descricao { get; set; }
        public decimal? peso { get; set; }
        public int? quantidade { get; set; }
        public int receitaId { get; set; }
        public DateTime? dataCadastro { get; set; }
        public List<IngredienteModel>? ingredientes { get; set; }
    }
}
