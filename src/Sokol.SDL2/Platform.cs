// ReSharper disable InconsistentNaming

namespace Sokol
{
    public enum Platform
    {
        Unknown,
        Windows,
        macOS,
        Linux,
        iOS,
        Android
    }

    internal static class PlatformHelper
    {
        internal static Platform GetPlatformFrom(string @string)
        {
            return @string switch
            {
                "Windows" => Platform.Windows,
                "Mac OS X" => Platform.macOS,
                "Linux" => Platform.Linux,
                "iOS" => Platform.iOS,
                "Android" => Platform.Android,
                _ => Platform.Unknown
            };
        }
    }
}