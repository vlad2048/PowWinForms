# PowWinForms

## Table of content

- [Features](#features)
- [Usage](#usage)
- [License](#license)

## Features

- Persist windows position and other objects (https://github.com/anakic/Jot)
- Binds controls to reactive variables (https://github.com/vlad2048/PowRxVar)
- Ability to edit trees (https://github.com/vlad2048/PowTrees & ObjectListView)

## Usage

### Use RX for a control/form
This ties the reactive logic to the lifetime of the control (using the HandleCreated / HandleDestroyed events)
```c#
partial class MainWindow : Form
{
	public MainWindow()
	{
		InitializeComponent();

		this.InitRX(d =>
		{
			MyLogic.Init(this).D(d);
		});
	}
}
```

### Persist windows
```c#
// Add a call to Track() when creating a window
var win = new MainWindow().Track();
```

### Persist custom objects
```c#
class UserPrefs
{
	public string? LastFolder { get; set; }


	public void Save() => Saving?.Invoke(null, EventArgs.Empty);

	public event EventHandler? Saving;
	static UserPrefs()
	{
		WinFormsUtils.Tracker.Configure<UserPrefs>()
			.Properties(e => new
			{
				e.LastFolder
			})
			.PersistOn(nameof(UserPrefs.Saving));
	}
}
```
Usage:
```c#
var userPrefs = new UserPrefs().Track();
// ...
userPrefs.Save();
```

### Create reactive variables
```c#
public partial class MainWin : Form
{
	public MainWin()
	{
		InitializeComponent();

		// Create a disposable tied to the lifetime of this form
		var d = this.MakeD();

		this.Events().HandleCreated.Subscribe(_ =>
		{
			// Create and bind reactive variables and use d to destroy them when the form is closed
			var name = Var.Make("name").D(d);

			// bind name
		});
	}
}
```


## License

MIT
