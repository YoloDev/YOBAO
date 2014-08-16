namespace Yobao.Repositories.Fakes {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using Yobao.Models;
	public class FakeItemRepository : FakeRepository<Item>, IRepository<Item> {
		public FakeItemRepository() : base() {
			SaveOrUpdate(new Item { Name = "Product 1", CategoryId = 1, Price = 9, SKU = "xxyyzz" });
			SaveOrUpdate(new Item { Name = "Product 2", CategoryId = 1, Price = 9, SKU = "xxyyzz" });
			SaveOrUpdate(new Item { Name = "Product 3", CategoryId = 2, Price = 9, SKU = "xxyyzz" });
		}
		public override Item Get(object id) {
			return _Items.FirstOrDefault(x => x.Id.Equals(Convert.ToInt32(id)));
		}
		protected override Item FindByKey(Item item) {
			return _Items.FirstOrDefault(x => x.Id == item.Id);
		}
		protected override void SetKey(Item item) {
			item.Id = _Items.Any() ? _Items.Max(x => x.Id) + 1 : 1;
		}
	}
}