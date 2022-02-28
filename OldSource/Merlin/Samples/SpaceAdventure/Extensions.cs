using System.Diagnostics.CodeAnalysis;

namespace SpaceAdventure
{
    public static class Utils
    {
        public static bool IsNull<T>([NotNullWhen(false)]T? obj) where T : class
            => obj == null;
    }
}
