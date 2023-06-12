using System.Reactive.Linq;
using System.Reactive.Subjects;
using PowRxVar;

namespace PowWinForms.TreeEditing.Structs;

// @formatter:off
public interface ITreeEvt<T> { }
public record TreeLoadedEvt  <T>(TNod<T> Root                        ) : ITreeEvt<T>;
public record TreeUnloadedEvt<T>                                       : ITreeEvt<T>;
public record NodeAddedEvt   <T>(TNod<T> Parent, T NodeContent       ) : ITreeEvt<T>;
public record NodeRemovedEvt <T>(TNod<T> Node                        ) : ITreeEvt<T>;
public record NodeChangedEvt <T>(TNod<T> Node, T NodeContent         ) : ITreeEvt<T>;

public static class TreeEvtExts
{
	public static IObservable<TreeLoadedEvt  <T>> WhenTreeLoaded  <T>(this ITreeEvtObs<T> obs) => obs.WhenChanged.OfType<TreeLoadedEvt  <T>>();
	public static IObservable<TreeUnloadedEvt<T>> WhenTreeUnloaded<T>(this ITreeEvtObs<T> obs) => obs.WhenChanged.OfType<TreeUnloadedEvt<T>>();
	public static IObservable<NodeAddedEvt   <T>> WhenNodeAdded   <T>(this ITreeEvtObs<T> obs) => obs.WhenChanged.OfType<NodeAddedEvt   <T>>();
	public static IObservable<NodeRemovedEvt <T>> WhenNodeRemoved <T>(this ITreeEvtObs<T> obs) => obs.WhenChanged.OfType<NodeRemovedEvt <T>>();
	public static IObservable<NodeChangedEvt <T>> WhenNodeChanged <T>(this ITreeEvtObs<T> obs) => obs.WhenChanged.OfType<NodeChangedEvt <T>>();

	public static void SignalTreeLoaded  <T>(this ITreeEvtSig<T> sig, TNod<T> root                 ) => sig.SignalChanged(new TreeLoadedEvt  <T>(root)               );
	public static void SignalTreeUnloaded<T>(this ITreeEvtSig<T> sig                               ) => sig.SignalChanged(new TreeUnloadedEvt<T>()                   );
	public static void SignalNodeAdded   <T>(this ITreeEvtSig<T> sig, TNod<T> parent, T nodeContent) => sig.SignalChanged(new NodeAddedEvt   <T>(parent, nodeContent));
	public static void SignalNodeRemoved <T>(this ITreeEvtSig<T> sig, TNod<T> node                 ) => sig.SignalChanged(new NodeRemovedEvt <T>(node)               );
	public static void SignalNodeChanged <T>(this ITreeEvtSig<T> sig, TNod<T> node, T nodeContent  ) => sig.SignalChanged(new NodeChangedEvt <T>(node, nodeContent)  );
}
// @formatter:on

public interface ITreeEvtObs<T>
{
	IObservable<ITreeEvt<T>> WhenChanged { get; }
}

public interface ITreeEvtSig<T>
{
	void SignalChanged(ITreeEvt<T> evt);
}

public sealed class TreeEvt<T> : ITreeEvtSig<T>, ITreeEvtObs<T>, IDisposable
{
	private readonly Disp d = new();
	public void Dispose() => d.Dispose();

	private readonly ISubject<ITreeEvt<T>> whenChanged;
	public IObservable<ITreeEvt<T>> WhenChanged => whenChanged.AsObservable();
	public void SignalChanged(ITreeEvt<T> root) => whenChanged.OnNext(root);

	private TreeEvt()
	{
		whenChanged = new Subject<ITreeEvt<T>>().D(d);
	}

	public static (ITreeEvtSig<T>, ITreeEvtObs<T>, IDisposable) Make()
	{
		var evt = new TreeEvt<T>();
		return (evt, evt, evt);
	}
}