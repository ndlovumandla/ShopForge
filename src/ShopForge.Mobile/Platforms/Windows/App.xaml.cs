using Microsoft.UI.Xaml;
using System.Text;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ShopForge.Mobile.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
	/// <summary>
	/// Initializes the singleton application object.  This is the first line of authored code
	/// executed, and as such is the logical equivalent of main() or WinMain().
	/// </summary>
	public App()
	{
		this.InitializeComponent();
		UnhandledException += OnUnhandledException;
		AppDomain.CurrentDomain.UnhandledException += OnDomainUnhandledException;
		TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

	private static void OnUnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
	{
		LogUnhandled("WinUI", e.Exception);
		e.Handled = true;
	}

	private static void OnDomainUnhandledException(object? sender, System.UnhandledExceptionEventArgs e)
	{
		if (e.ExceptionObject is Exception ex)
		{
			LogUnhandled("AppDomain", ex);
		}
	}

	private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
	{
		LogUnhandled("TaskScheduler", e.Exception);
		e.SetObserved();
	}

	private static void LogUnhandled(string source, Exception ex)
	{
		try
		{
			var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShopForge");
			Directory.CreateDirectory(folder);
			var filePath = Path.Combine(folder, "mobile-crash.log");
			var payload = new StringBuilder()
				.AppendLine($"[{DateTime.UtcNow:O}] {source} unhandled exception")
				.AppendLine(ex.ToString())
				.AppendLine(new string('-', 80))
				.ToString();
			File.AppendAllText(filePath, payload);
		}
		catch
		{
			// Avoid secondary failures inside the exception handler.
		}
	}
}

