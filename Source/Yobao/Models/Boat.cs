namespace Yobao.Models {
	using System.ComponentModel.DataAnnotations;
	public class Boat {
		[Key]
		public int Id { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		public decimal Price { get; set; }
	}
}