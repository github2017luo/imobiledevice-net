// <copyright file="SpringBoardServicesApi.cs" company="Quamotion">
// Copyright (c) 2016-2017 Quamotion. All rights reserved.
// </copyright>

namespace iMobileDevice.SpringBoardServices
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using iMobileDevice.iDevice;
    using iMobileDevice.Lockdown;
    using iMobileDevice.Afc;
    using iMobileDevice.Plist;
    
    
    public partial class SpringBoardServicesApi : ISpringBoardServicesApi
    {
        
        /// <summary>
        /// Backing field for the <see cref="Parent"/> property
        /// </summary>
        private ILibiMobileDevice parent;
        
        /// <summary>
        /// Initializes a new instance of the <see cref"SpringBoardServicesApi"/> class
        /// </summary>
        /// <param name="parent">
        /// The <see cref="ILibiMobileDeviceApi"/> which owns this <see cref="SpringBoardServices"/>.
        /// </summary>
        public SpringBoardServicesApi(ILibiMobileDevice parent)
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
        /// Connects to the springboardservices service on the specified device.
        /// </summary>
        /// <param name="device">
        /// The device to connect to.
        /// </param>
        /// <param name="service">
        /// The service descriptor returned by lockdownd_start_service.
        /// </param>
        /// <param name="client">
        /// Pointer that will point to a newly allocated
        /// sbservices_client_t upon successful return.
        /// </param>
        /// <returns>
        /// SBSERVICES_E_SUCCESS on success, SBSERVICES_E_INVALID_ARG when
        /// client is NULL, or an SBSERVICES_E_* error code otherwise.
        /// </returns>
        public virtual SpringBoardServicesError sbservices_client_new(iDeviceHandle device, LockdownServiceDescriptorHandle service, out SpringBoardServicesClientHandle client)
        {
            SpringBoardServicesError returnValue;
            returnValue = SpringBoardServicesNativeMethods.sbservices_client_new(device, service, out client);
            client.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Starts a new sbservices service on the specified device and connects to it.
        /// </summary>
        /// <param name="device">
        /// The device to connect to.
        /// </param>
        /// <param name="client">
        /// Pointer that will point to a newly allocated
        /// sbservices_client_t upon successful return. Must be freed using
        /// sbservices_client_free() after use.
        /// </param>
        /// <param name="label">
        /// The label to use for communication. Usually the program name.
        /// Pass NULL to disable sending the label in requests to lockdownd.
        /// </param>
        /// <returns>
        /// SBSERVICES_E_SUCCESS on success, or an SBSERVICES_E_* error
        /// code otherwise.
        /// </returns>
        public virtual SpringBoardServicesError sbservices_client_start_service(iDeviceHandle device, out SpringBoardServicesClientHandle client, string label)
        {
            SpringBoardServicesError returnValue;
            returnValue = SpringBoardServicesNativeMethods.sbservices_client_start_service(device, out client, label);
            client.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Disconnects an sbservices client from the device and frees up the
        /// sbservices client data.
        /// </summary>
        /// <param name="client">
        /// The sbservices client to disconnect and free.
        /// </param>
        /// <returns>
        /// SBSERVICES_E_SUCCESS on success, SBSERVICES_E_INVALID_ARG when
        /// client is NULL, or an SBSERVICES_E_* error code otherwise.
        /// </returns>
        public virtual SpringBoardServicesError sbservices_client_free(System.IntPtr client)
        {
            return SpringBoardServicesNativeMethods.sbservices_client_free(client);
        }
        
        /// <summary>
        /// Gets the icon state of the connected device.
        /// </summary>
        /// <param name="client">
        /// The connected sbservices client to use.
        /// </param>
        /// <param name="state">
        /// Pointer that will point to a newly allocated plist containing
        /// the current icon state. It is up to the caller to free the memory.
        /// </param>
        /// <param name="format_version">
        /// A string to be passed as formatVersion along with
        /// the request, or NULL if no formatVersion should be passed. This is only
        /// supported since iOS 4.0 so for older firmware versions this must be set
        /// to NULL.
        /// </param>
        /// <returns>
        /// SBSERVICES_E_SUCCESS on success, SBSERVICES_E_INVALID_ARG when
        /// client or state is invalid, or an SBSERVICES_E_* error code otherwise.
        /// </returns>
        public virtual SpringBoardServicesError sbservices_get_icon_state(SpringBoardServicesClientHandle client, out PlistHandle state, string formatVersion)
        {
            SpringBoardServicesError returnValue;
            returnValue = SpringBoardServicesNativeMethods.sbservices_get_icon_state(client, out state, formatVersion);
            state.Api = this.Parent;
            return returnValue;
        }
        
        /// <summary>
        /// Sets the icon state of the connected device.
        /// </summary>
        /// <param name="client">
        /// The connected sbservices client to use.
        /// </param>
        /// <param name="newstate">
        /// A plist containing the new iconstate.
        /// </param>
        /// <returns>
        /// SBSERVICES_E_SUCCESS on success, SBSERVICES_E_INVALID_ARG when
        /// client or newstate is NULL, or an SBSERVICES_E_* error code otherwise.
        /// </returns>
        public virtual SpringBoardServicesError sbservices_set_icon_state(SpringBoardServicesClientHandle client, PlistHandle newstate)
        {
            return SpringBoardServicesNativeMethods.sbservices_set_icon_state(client, newstate);
        }
        
        /// <summary>
        /// Get the icon of the specified app as PNG data.
        /// </summary>
        /// <param name="client">
        /// The connected sbservices client to use.
        /// </param>
        /// <param name="bundleId">
        /// The bundle identifier of the app to retrieve the icon for.
        /// </param>
        /// <param name="pngdata">
        /// Pointer that will point to a newly allocated buffer
        /// containing the PNG data upon successful return. It is up to the caller
        /// to free the memory.
        /// </param>
        /// <param name="pngsize">
        /// Pointer to a uint64_t that will be set to the size of the
        /// buffer pngdata points to upon successful return.
        /// </param>
        /// <returns>
        /// SBSERVICES_E_SUCCESS on success, SBSERVICES_E_INVALID_ARG when
        /// client, bundleId, or pngdata are invalid, or an SBSERVICES_E_* error
        /// code otherwise.
        /// </returns>
        public virtual SpringBoardServicesError sbservices_get_icon_pngdata(SpringBoardServicesClientHandle client, string bundleid, ref System.IntPtr pngdata, ref ulong pngsize)
        {
            return SpringBoardServicesNativeMethods.sbservices_get_icon_pngdata(client, bundleid, ref pngdata, ref pngsize);
        }
        
        /// <summary>
        /// Gets the interface orientation of the device.
        /// </summary>
        /// <param name="client">
        /// The connected sbservices client to use.
        /// </param>
        /// <param name="interface_orientation">
        /// The interface orientation upon successful return.
        /// </param>
        /// <returns>
        /// SBSERVICES_E_SUCCESS on success, SBSERVICES_E_INVALID_ARG when
        /// client or state is invalid, or an SBSERVICES_E_* error code otherwise.
        /// </returns>
        public virtual SpringBoardServicesError sbservices_get_interface_orientation(SpringBoardServicesClientHandle client, ref SpringBoardServicesInterfaceOrientation interfaceOrientation)
        {
            return SpringBoardServicesNativeMethods.sbservices_get_interface_orientation(client, ref interfaceOrientation);
        }
        
        /// <summary>
        /// Get the home screen wallpaper as PNG data.
        /// </summary>
        /// <param name="client">
        /// The connected sbservices client to use.
        /// </param>
        /// <param name="pngdata">
        /// Pointer that will point to a newly allocated buffer
        /// containing the PNG data upon successful return. It is up to the caller
        /// to free the memory.
        /// </param>
        /// <param name="pngsize">
        /// Pointer to a uint64_t that will be set to the size of the
        /// buffer pngdata points to upon successful return.
        /// </param>
        /// <returns>
        /// SBSERVICES_E_SUCCESS on success, SBSERVICES_E_INVALID_ARG when
        /// client or pngdata are invalid, or an SBSERVICES_E_* error
        /// code otherwise.
        /// </returns>
        public virtual SpringBoardServicesError sbservices_get_home_screen_wallpaper_pngdata(SpringBoardServicesClientHandle client, ref System.IntPtr pngdata, ref ulong pngsize)
        {
            return SpringBoardServicesNativeMethods.sbservices_get_home_screen_wallpaper_pngdata(client, ref pngdata, ref pngsize);
        }
    }
}
