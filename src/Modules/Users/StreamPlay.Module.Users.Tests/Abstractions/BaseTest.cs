using Bogus;

namespace StreamPlay.Module.Users.Tests.Abstractions;

public abstract class BaseTest
{
    protected static readonly Faker Faker = new();
}