using GoedBezigWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Tests.Data
{
    public class DummyGoedBezigDbContext:DbContext
    {
        public Group Test123 { get; }

        public DummyGoedBezigDbContext()
        {
            Test123 = new Group("test123", true);
        }
    }
}
