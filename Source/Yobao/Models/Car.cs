namespace Yobao.Models {
	using System.ComponentModel.DataAnnotations;
	public class Car {
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}