using System.ComponentModel.DataAnnotations;

namespace MasterChef.Domain.Entities
{
	public abstract class BaseEntity
	{
		[Key]
		public int Id { get; set; }
	}
}
