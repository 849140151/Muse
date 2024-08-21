using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Muse.DB.Configuration;
using Muse.UI.View;
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

        string slnFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\.."));
        string dbPath = Path.Combine(slnFolder, "Muse.DB", "Muse.sqlite");
        services.AddDbContext<MyDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));


        services.AddSingleton<SongListViewModel>();
        services.AddSingleton<SongList>();

        services.AddSingleton<PlayBarViewModel>();
        services.AddSingleton<PlayBar>();

        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();

        return services.BuildServiceProvider();
    }

    private void Application_Startup(object sender, Win.StartupEventArgs e)
    {
        var mainWindow = Services.GetService<MainWindow>();
        mainWindow!.Show();
    }


}