// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using Sokol.Graphics;
using static Sokol.App.PInvoke;

namespace Sokol.App
{
    /// <summary>
    ///     The region of memory that holds the color data for the image displayed on screen.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Public API.")]
    public static class Framebuffer
    {
        /// <summary>
        ///     Gets the current width of the <see cref="Framebuffer" />. May change from frame-to-frame.
        /// </summary>
        /// <value>The current width of the <see cref="Framebuffer" />.</value>
        public static int Width => sapp_width();

        /// <summary>
        ///     Gets the current height of the <see cref="Framebuffer" />. May change from frame-to-frame.
        /// </summary>
        /// <value>The current height of the <see cref="Framebuffer" />.</value>
        public static int Height => sapp_height();

        /// <summary>
        ///     Gets a value indicating whether the <see cref="Framebuffer" /> currently is using
        ///     full-resolution for displays with increased pixel density (dots-per-inch).
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         When <see cref="IsHighDpi" /> is <c>false</c>, <see cref="Width" /> and <see cref="Height" />
        ///         will not up-scaled, but the rendered content will be up-scaled by the rendering system composer.
        ///     </para>
        ///     <para>
        ///         When <see cref="IsHighDpi" /> is <c>true></c>, <see cref="Width" /> and <see cref="Height" />
        ///         will be up-scaled according to <see cref="DpiScale" />.
        ///     </para>
        /// </remarks>
        /// <value>
        ///     A value indicating whether the <see cref="Framebuffer" /> currently is using full-resolution for displays with
        ///     increased pixel density.
        /// </value>
        public static bool IsHighDpi => sapp_high_dpi();

        /// <summary>
        ///     Gets the scaling factor for increased pixel density (dots-per-inch).
        /// </summary>
        /// <value>The scaling factor for the increased pixel density (dots-per-inch).</value>
        /// <remarks>
        ///     <para>
        ///         Use <see cref="DpiScale" /> to convert window dimensions to <see cref="Framebuffer" /> dimensions, or
        ///         vice-versa.
        ///     </para>
        /// </remarks>
        public static float DpiScale => sapp_dpi_scale();

        /// <summary>
        ///     Gets the color <see cref="PixelFormat" /> of the <see cref="Framebuffer" />.
        /// </summary>
        /// <value>The color <see cref="PixelFormat" /> of the <see cref="Framebuffer" />.</value>
        public static PixelFormat ColorFormat => (PixelFormat)sapp_color_format();

        /// <summary>
        ///     Gets the depth <see cref="PixelFormat" /> of the <see cref="Framebuffer" />.
        /// </summary>
        /// <value>The depth <see cref="PixelFormat" /> of the <see cref="Framebuffer" />.</value>
        public static PixelFormat DepthFormat => (PixelFormat)sapp_depth_format();

        /// <summary>
        ///     Gets the multi-sample anti-aliasing (MSAA) sample count of the <see cref="Framebuffer" />.
        /// </summary>
        /// <value>The multi-sample anti-aliasing (MSAA) sample count.</value>
        public static int SampleCount => sapp_sample_count();
    }
}
