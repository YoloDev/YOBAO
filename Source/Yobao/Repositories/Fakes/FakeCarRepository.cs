namespace Yobao.Repositories.Fakes {
	using System.Linq;
	using Yobao.Models;
	public class FakeCarRepository : FakeRepository<Car>, IRepository<Car> {
		public FakeCarRepository()
			: base() {
			SaveOrUpdate(new Car { Name = "Corolla" });
			SaveOrUpdate(new Car { Name = "Tarago" });
		}
		protected override Car FindByKey(Car item) {
			return _Items.FirstOrDefault(x => x.Id == item.Id);
		}
		protected override void SetKey(Car item) {
			item.Id = _Items.Any() ? _Items.Max(x => x.Id) + 1 : 1;
		}
	}
}