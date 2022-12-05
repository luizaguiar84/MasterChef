using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterChef.Domain.Entities
{
	public abstract class BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

        [Display(Name = "Data de Criação")]
		
        public DateTimeOffset CreateDate { get; set; }

        [Display(Name = "Data de Atualização")]
        public DateTimeOffset LastChange { get; set; }
	}
}
