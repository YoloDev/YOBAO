namespace Yobao.Models {
	using System.ComponentModel.DataAnnotations;
	public class Item {
		[Key]
		public int Id { get; set; }
		public string SKU { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
	}
}