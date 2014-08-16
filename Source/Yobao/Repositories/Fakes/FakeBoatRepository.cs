﻿namespace Yobao.Repositories.Fakes {
	using System.Linq;
	using Yobao.Models;
	public class FakeBoatRepository : FakeRepository<Boat>, IRepository<Boat> {
		public FakeBoatRepository()
			: base() {
			SaveOrUpdate(new Boat { Make = "Action Craft", Model = "Flatsmaster", Price = 999 });
			SaveOrUpdate(new Boat { Make = "Action Craft", Model = "Flyfisher", Price = 999 });
			SaveOrUpdate(new Boat { Make = "Action Craft", Model = "FlatsPro", Price = 999 });
		}
		protected override Boat FindByKey(Boat item) {
			return _Items.FirstOrDefault(x => x.Id == item.Id);
		}
		protected override void SetKey(Boat item) {
			item.Id = _Items.Any() ? _Items.Max(x => x.Id) + 1 : 1;
		}
	}
}