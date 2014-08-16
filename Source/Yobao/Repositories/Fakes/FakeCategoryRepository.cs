namespace Yobao.Repositories.Fakes {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using Yobao.Models;
	public class FakeCategoryRepository : FakeRepository<Category>, IRepository<Category> {
		public FakeCategoryRepository() : base() {
			SaveOrUpdate(new Category { Description = "Waders" });
			SaveOrUpdate(new Category { Description = "Boots" });
			SaveOrUpdate(new Category { Description = "Rods" });
			SaveOrUpdate(new Category { Description = "Reels" });
		}
		public override Category Get(object id) {
			return _Items.FirstOrDefault(x => x.Id.Equals(Convert.ToInt32(id)));
		}
		protected override Category FindByKey(Category item) {
			return _Items.FirstOrDefault(x => x.Id == item.Id);
		}
		protected override void SetKey(Category item) {
			item.Id = _Items.Any() ? _Items.Max(x => x.Id) + 1 : 1;
		}
	}
}