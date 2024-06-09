using System;
namespace EFAssignment1.Core.Entities
{
	public class Post : EntityBase
	{
		public string Title { get; set; }

		public string Content { get; set; }

		public long BlogId { get; set; }

		public long PostTypeId { get; set; }

		public long UserId { get; set; }

		public Blog Blog { get; set; }

		public PostType PostType { get; set; }

		public User User { get; set; }

		public Post()
		{
		}
	}
}

