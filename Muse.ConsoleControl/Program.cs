using Muse.DB.Configuration;
using Muse.DB.Model;

namespace Muse.ConsoleControl;

class Program
{
    static void Main()
    {
        using ( MyDbContext context = new MyDbContext())
        {
        }
    }
}