using AutoService.Domain.Data;

namespace AutoService.Tests.Fixtures;

public class AutoServiceFixture
{
    public AutoServiceContext Context { get; }


    public AutoServiceFixture()
    {
        Context = DataSeeder.Seed();
    }
}