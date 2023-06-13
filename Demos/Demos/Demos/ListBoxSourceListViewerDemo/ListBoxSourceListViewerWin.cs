using System.Reactive.Linq;
using DynamicData;
using PowMaybe;
using PowRxVar;
using PowWinForms;
using PowWinForms.ListBoxSourceListViewing;

namespace Demos.Demos.ListBoxSourceListViewerDemo;

sealed partial class ListBoxSourceListViewerWin : Form
{
	private static int cnt;

	public ListBoxSourceListViewerWin()
	{
		InitializeComponent();

		this.InitRX(d =>
		{

			var source = new SourceList<Rec>().D(d);
			var obs = source.Connect();

			Observable.Timer(TimeSpan.FromSeconds(3)).Subscribe(_ =>
			{
				source.AddRange(recs);
			}).D(d);

			ListBoxSourceListViewer.View(out var selectedRec, obs, listBox);

			selectedRec.Subscribe(mayRec => selectedLabel.Text = mayRec.IsSome(out var rec) switch
			{
				true => $"{rec}",
				false => "_"
			}).D(d);


			addBtn.Events().Click.Subscribe(_ =>
			{
				var rec = new Rec($"Name_{cnt}", cnt);
				source.Add(rec);
				cnt++;
			}).D(d);

			delBtn.Events().Click.Subscribe(_ =>
			{
				if (selectedRec.V.IsNone(out var rec)) return;
				source.Remove(rec);
			}).D(d);

			clearBtn.Events().Click.Subscribe(_ =>
			{
				source.Clear();
			}).D(d);
		});
	}

	private static readonly Rec recVlad = new("Vlad", 12);
	private static readonly Rec recErik = new("Erik", 34);
	private static readonly Rec recMilou = new("GoncaloMilou", 56);
	private static readonly Rec recGoncalo = new("Goncalo", 78);
	private static readonly Rec[] recs = { recVlad, recErik, recMilou, recGoncalo };

	private record Rec(string Name, int Age)
	{
		public override string ToString() => $"{Name} / {Age}";
	}
}
