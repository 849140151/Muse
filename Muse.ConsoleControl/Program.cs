using Muse.DB.Configuration;
using Muse.DB.Model;

namespace Muse.ConsoleControl;

class Program
{
    static void Main()
    {
        using ( MyDbContext context = new MyDbContext())
        {
            SongBasic songBasic = new SongBasic
            {
                Performers = "tuki",
                Title = "Bansanka",
                Duration = "03:35"
            };
            context.SongBasic.Add(songBasic);
            context.SaveChanges();
        }
    }
}