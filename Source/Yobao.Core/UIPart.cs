namespace Yobao {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Text;
	using System.Threading.Tasks;

	public class UIPart<TModel> {
		readonly Expression<Func<TModel, object>> _MemberExpression;
		public Lazy<Dictionary<object, object>> Values { get; private set; }
		public Lazy<object> Label { get; private set; }

		public UIPart(Expression<Func<TModel, object>> memberExpression) {
			_MemberExpression = memberExpression;
		}
		public UIPart<TModel> AsLookup<TLookup>(IQueryable<TLookup> items,
			Expression<Func<TLookup, object>> idField,
			Expression<Func<TLookup, object>> labelField) {

			Values = new Lazy<Dictionary<object, object>>(() => {
				return items.ToDictionary<TLookup, object, object>((k) =>
					k.GetType().GetProperty(idField.Name),
					(v) => v.GetType().GetProperty(labelField.Name
				));
			});

			return this;
		}

		public void Hide() {
			throw new NotImplementedException();
		}

		/// <summary>
		///  I want to somehow be able to, from configuration, say that
		///  I want to select from the current item equaling the TLookup's property, then
		///  select the property on TLookup to display.
		/// </summary>
		public UIPart<TModel> DisplayAs<TLookup>(IQueryable<TLookup> items, Func<TLookup, bool> query) {
			Label = new Lazy<object>(() => {
				return items.FirstOrDefault(i => query.Invoke(i));
			});
			return this;
		}
	}
}