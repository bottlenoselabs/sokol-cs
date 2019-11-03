using static SDL2.SDL;

// ReSharper disable InconsistentNaming

namespace Sokol.Samples
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
        internal static Platform? _runtimePlatform;
        
        internal static Platform RuntimePlatform
        {
            get
            {
                if (_runtimePlatform != null) return _runtimePlatform.Value;
                
                var platformString = SDL_GetPlatform();
                _runtimePlatform = GetPlatformFrom(platformString);
                return _runtimePlatform.Value;
            }
        }
        
        private static Platform GetPlatformFrom(string @string)
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