// <copyright file="PinvokeApi.cs" company="Quamotion">
// Copyright (c) 2016-2017 Quamotion. All rights reserved.
// </copyright>

namespace iMobileDevice.Pinvoke
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using iMobileDevice.iDevice;
    using iMobileDevice.Lockdown;
    using iMobileDevice.Afc;
    using iMobileDevice.Plist;
    
    
    public partial class PinvokeApi : IPinvokeApi
    {
        
        /// <summary>
        /// Backing field for the <see cref="Parent"/> property
        /// </summary>
        private ILibiMobileDevice parent;
        
        /// <summary>
        /// Initializes a new instance of the <see cref"PinvokeApi"/> class
        /// </summary>
        /// <param name="parent">
        /// The <see cref="ILibiMobileDeviceApi"/> which owns this <see cref="Pinvoke"/>.
        /// </summary>
        public PinvokeApi(ILibiMobileDevice parent)
        {
            this.parent = parent;
        }
        
        /// <inheritdoc/>
        public ILibiMobileDevice Parent
        {
            get
            {
                return this.parent;
            }
        }
        
        /// <summary>
        /// Frees a string that was previously allocated by libimobiledevice.
        /// </summary>
        /// <param name="string">
        /// The string to free.
        /// </param>
        /// <returns>
        /// Always returns PINVOKE_E_SUCCESS.
        /// </returns>
        public virtual PinvokeError pinvoke_free_string(System.IntPtr @string)
        {
            return PinvokeNativeMethods.pinvoke_free_string(@string);
        }
        
        /// <summary>
        /// Gets the size of a string that was previously allocated by libimobiledevice.
        /// </summary>
        /// <param name="string">
        /// The string of which to get its size.
        /// </param>
        /// <param name="length">
        /// The length of the string, in bytes.
        /// </param>
        /// <returns>
        /// Always returns PINVOKE_E_SUCCESS.
        /// </returns>
        public virtual PinvokeError pinvoke_get_string_length(System.IntPtr @string, out ulong length)
        {
            return PinvokeNativeMethods.pinvoke_get_string_length(@string, out length);
        }
    }
}
