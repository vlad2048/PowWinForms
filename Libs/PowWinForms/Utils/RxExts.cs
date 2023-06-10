using System.Reactive.Linq;

namespace PowWinForms.Utils;

public static class RxExts
{
	public static IObservable<T> ObserveOnWinFormsUIThread<T>(this IObservable<T> obs) => obs.ObserveOn(SynchronizationContext.Current!);
}