using DynamicData;
using PowMaybe;
using PowRxVar;
using PowWinForms.Utils;

namespace PowWinForms.ListBoxSourceListViewing;

public static class ListBoxSourceListViewer
{
	public static IDisposable View<T>(
		out IRwMayBndVar<T> selectedItem,
		IObservable<IChangeSet<T>> source,
		ListBox listbox
	)
	{
		var d = new Disp();

		var selectedItemRw = VarMay.MakeBnd<T>().D(d);
		selectedItem = selectedItemRw.ToRwMayBndVar();

		listbox.Events().SelectedIndexChanged.Subscribe(_ =>
		{
			var idx = listbox.SelectedIndex;
			selectedItemRw.V = (idx >= 0 && idx < listbox.Items.Count) switch
			{
				true => May.Some((T)listbox.Items[idx]),
				false => May.None<T>()
			};
		}).D(d);


		void Add(T elt)
		{
			listbox.Items.Add(elt!);
			listbox.SelectedIndex = 0;
		}

		/*var autoSelectDone = false;

		void Add(T elt)
		{
			listbox.Items.Add(elt!);
			if (!autoSelectDone)
			{
				autoSelectDone = true;
				listbox.SelectedIndex = 0;
			}
		}*/

		void Del(T elt)
		{
			listbox.Items.Remove(elt!);
			if (selectedItemRw.V.IsSome(out var val) && val.Equals(elt))
				selectedItemRw.V = May.None<T>();
		}

		void Clear()
		{
			listbox.Items.Clear();
			selectedItemRw.V = May.None<T>();
		}

		source
			.ObserveOnWinFormsUIThread()
			.Subscribe(cs =>
			{
				foreach (var c in cs)
				{
					switch (c.Reason)
					{
						case ListChangeReason.Add:
							Add(c.Item.Current);
							break;
						case ListChangeReason.AddRange:
							foreach (var elt in c.Range)
								Add(elt!);
							break;
						case ListChangeReason.Remove:
							Del(c.Item.Current);
							break;
						case ListChangeReason.RemoveRange:
							foreach (var elt in c.Range)
								Del(elt);
							break;
						case ListChangeReason.Clear:
							Clear();
							break;
						case ListChangeReason.Moved:
						case ListChangeReason.Refresh:
						case ListChangeReason.Replace:
							throw new ArgumentException($"ListChangeReason not handled: {c.Reason}");
						default:
							throw new ArgumentException($"Unknown ListChangeReason: {c.Reason}");
					}
				}
			}).D(d);

		return d;
	}
}