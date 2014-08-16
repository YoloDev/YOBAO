namespace Yobao.Models {
	using System.ComponentModel.DataAnnotations;
	public class Category {
		[Key]
		public int Id { get; set; }
		public string Description { get; set; }
	}
}