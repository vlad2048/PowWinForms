# PowWinForms

## Table of content

- [Features](#features)
- [Usage](#usage)
- [License](#license)

## Features

- Persist windows position, size, ... (https://github.com/anakic/Jot)
- Binds controls to reactive variables (https://github.com/vlad2048/PowRxVar)
- Ability to edit trees (https://github.com/vlad2048/PowTrees & ObjectListView)

## Usage

### Persist windows
```c#
// Add a call to Track() when creating a window
var win = new MainWindow().Track();
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
