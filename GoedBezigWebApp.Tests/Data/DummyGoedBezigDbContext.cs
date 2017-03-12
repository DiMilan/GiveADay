using System;
using GoedBezigWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GoedBezigWebApp.Tests.Data
{
    public class DummyGoedBezigDbContext:DbContext
    {
        public Group Test123 { get; }

        public DummyGoedBezigDbContext()
        {
            Test123 = new Group()
            {
                GroupName = "Test123",
                Timestamp = new DateTime(2017,2,15,19,04,07),
                ClosedGroup = true

            };
        }
    }
}
