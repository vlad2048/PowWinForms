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
	/// Persist Forms position, size, ... <br/>
	/// Tracking for other object types can be setup by using WinFormsUtils.Tracker <br/>
	/// See JOT library: https://github.com/anakic/Jot
	/// </summary>
	public static T Track<T>(this T obj) where T : class
	{
		switch (obj)
		{
			case Form form:
				form.Events().Load.Subscribe(_ => Tracker.Track(obj)).DD(form);
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


	/// <summary>
	/// Create a disposable based on the lifetime of the control
	/// </summary>
	public static IRoDispBase MakeD(this Control ctrl) => new Disp().DD(ctrl);



	private static D DD<D>(this D dispDst, Control ctrl) where D : IDisposable
	{
		ctrl.Events().HandleDestroyed.Take(1).Subscribe(_ => dispDst.Dispose());
		return dispDst;
	}
}