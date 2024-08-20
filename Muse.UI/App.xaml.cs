using Microsoft.Extensions.DependencyInjection;
using Muse.UI.ViewModel;
using Win = System.Windows;


namespace Muse.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Win.Application
{
    public App()
    {
        Services = ConfigureServices();
    }

    public new static App Current => (App)Win.Application.Current;

    public IServiceProvider Services { get; }

    public static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>(sp => new MainWindow{ DataContext = sp.GetService<MainWindowViewModel>() });

        return services.BuildServiceProvider();
    }

    private void Application_Startup(object sender, Win.StartupEventArgs e)
    {
        var mainWindow = Services.GetService<MainWindow>();
        mainWindow!.Show();
    }


}