using System;
namespace EFAssignment1.Core.Entities
{
	public abstract class EntityBase
	{
		public long Id { get; set; }

		public DateTime CreatedDateTime { get; set; }

		public DateTime UpdatedDateTime { get; set; }

		public EntityBase()
		{
			
		}
	}
}

