using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterChef.Domain.Entities
{
	public class BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

        [Display(Name = "Data de Criação")]
		
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Data de Atualização")]
        public DateTime? LastChange { get; set; }

        public bool Active { get; set; }

        public BaseEntity()
        {
            LastChange = DateTime.Now;
            Active = true;
        }
	}
}
