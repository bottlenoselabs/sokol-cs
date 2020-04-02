// Copyright (c) Lucas Girouard-Stranks. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace Sokol.Graphics
{
    public readonly partial struct Context
    {
        /// <summary>
        ///     Creates a <see cref="Context" />. Must be called once after a GL context has been created and made
        ///     active.
        /// </summary>
        /// <returns>A <see cref="Context" />.</returns>
        [DllImport(Sg.LibraryName, EntryPoint = "sg_setup_context")]
        public static extern Context Create();

        [DllImport(Sg.LibraryName, EntryPoint = "sg_activate_context")]
        private static extern void Activate(Context context);

        [DllImport(Sg.LibraryName, EntryPoint = "sg_discard_context")]
        private static extern void Destroy(Context context);
    }
}
