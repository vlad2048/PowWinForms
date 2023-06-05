using System.ComponentModel;
using System.Reactive.Linq;
using Jot;
using PowRxVar;

namespace PowWinForms;

public static class WinFormsUtils
{
	public static Tracker Tracker { get; } = new();

	static WinFormsUtils()
	{
		TrackerSetup.Init(Tracker);
	}


	/// <summary>
	/// Setup the reactive code for a control<br/>
	/// Use this in the control's constructor after the call to InitializeComponent<br/>
	/// It will call the initAction on HandleCreated and dispose the IRoDispBase given to it in HandleDestroyed
	/// </summary>
	/// <param name="ctrl">the control (use this)</param>
	/// <param name="initAction">initAction</param>
	public static void InitRX(this Control ctrl, Action<IRoDispBase> initAction)
	{
		var d = new Disp().DisposeOnHandleDestroyed(ctrl);
		ctrl.Events().HandleCreated.Subscribe(_ => initAction(d)).D(d);
	}


	/// <summary>
	/// Persist Forms position, size, ... <br/>
	/// Tracking for other object types can be setup by using WinFormsUtils.Tracker <br/>
	/// See JOT library: https://github.com/anakic/Jot
	/// </summary>
	public static T Track<T>(this T obj) where T : class
	{
		switch (obj)
		{
			case Form form:
				form.Events().Load.Subscribe(_ => Tracker.Track(obj)).DisposeOnHandleDestroyed(form);
				break;

			default:
				Tracker.Track(obj);
				break;
		}

		return obj;
	}


	/// <summary>
	/// Check if we are in design mode
	/// </summary>
	public static bool IsDesignMode => LicenseManager.UsageMode == LicenseUsageMode.Designtime;


	private static D DisposeOnHandleDestroyed<D>(this D dispDst, Control ctrl) where D : IDisposable
	{
		ctrl.Events().HandleDestroyed.Take(1).Subscribe(_ => dispDst.Dispose());
		return dispDst;
	}
}