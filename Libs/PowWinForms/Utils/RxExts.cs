using System.Reactive.Linq;
using PowRxVar;

namespace PowWinForms.Utils;

public static class RxExts
{
	public static IObservable<T> ObserveOnWinFormsUIThread<T>(this IObservable<T> obs) => obs.ObserveOn(SynchronizationContext.Current!);

	// TODO: Move to PowRxVar
	public static IDisposable PipeInto<T>(this IObservable<T> obs, IRwVar<T> rwVar)
	{
		var d = new Disp();
		obs.Subscribe(v => rwVar.V = v).D(d);
		return d;
	}
}