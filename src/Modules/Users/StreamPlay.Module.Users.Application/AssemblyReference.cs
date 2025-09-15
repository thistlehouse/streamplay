using System.Reflection;

namespace StreamPlay.Module.Users.Application;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}