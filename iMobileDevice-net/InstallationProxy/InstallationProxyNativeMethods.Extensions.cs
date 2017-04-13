// <copyright file="InstallationProxyNativeMethods.cs" company="Quamotion">
// Copyright (c) 2016-2017 Quamotion. All rights reserved.
// </copyright>

namespace iMobileDevice.InstallationProxy
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using iMobileDevice.iDevice;
    using iMobileDevice.Lockdown;
    using iMobileDevice.Afc;
    using iMobileDevice.Plist;
    
    
    public partial class InstallationProxyNativeMethods
    {
        
        public static InstallationProxyError instproxy_lookup(InstallationProxyClientHandle client, System.Collections.ObjectModel.ReadOnlyCollection<string> appids, PlistHandle clientOptions, out PlistHandle result)
        {
            System.Runtime.InteropServices.ICustomMarshaler appidsMarshaler = NativeStringArrayMarshaler.GetInstance(null);
            System.IntPtr appidsNative = appidsMarshaler.MarshalManagedToNative(appids);
            InstallationProxyError returnValue = InstallationProxyNativeMethods.instproxy_lookup(client, appidsNative, clientOptions, out result);
            return returnValue;
        }
        
        public static InstallationProxyError instproxy_check_capabilities_match(InstallationProxyClientHandle client, out string capabilities, PlistHandle clientOptions, out PlistHandle result)
        {
            System.Runtime.InteropServices.ICustomMarshaler capabilitiesMarshaler = NativeStringMarshaler.GetInstance(null);
            System.IntPtr capabilitiesNative = System.IntPtr.Zero;
            InstallationProxyError returnValue = InstallationProxyNativeMethods.instproxy_check_capabilities_match(client, out capabilitiesNative, clientOptions, out result);
            capabilities = ((string)capabilitiesMarshaler.MarshalNativeToManaged(capabilitiesNative));
            capabilitiesMarshaler.CleanUpNativeData(capabilitiesNative);
            return returnValue;
        }
        
        public static void instproxy_command_get_name(PlistHandle command, out string name)
        {
            System.Runtime.InteropServices.ICustomMarshaler nameMarshaler = NativeStringMarshaler.GetInstance(null);
            System.IntPtr nameNative = System.IntPtr.Zero;
            InstallationProxyNativeMethods.instproxy_command_get_name(command, out nameNative);
            name = ((string)nameMarshaler.MarshalNativeToManaged(nameNative));
            nameMarshaler.CleanUpNativeData(nameNative);
        }
        
        public static void instproxy_status_get_name(PlistHandle status, out string name)
        {
            System.Runtime.InteropServices.ICustomMarshaler nameMarshaler = NativeStringMarshaler.GetInstance(null);
            System.IntPtr nameNative = System.IntPtr.Zero;
            InstallationProxyNativeMethods.instproxy_status_get_name(status, out nameNative);
            name = ((string)nameMarshaler.MarshalNativeToManaged(nameNative));
            nameMarshaler.CleanUpNativeData(nameNative);
        }
        
        public static InstallationProxyError instproxy_status_get_error(PlistHandle status, out string name, out string description, ref ulong code)
        {
            System.Runtime.InteropServices.ICustomMarshaler descriptionMarshaler = NativeStringMarshaler.GetInstance(null);
            System.IntPtr descriptionNative = System.IntPtr.Zero;
            System.Runtime.InteropServices.ICustomMarshaler nameMarshaler = NativeStringMarshaler.GetInstance(null);
            System.IntPtr nameNative = System.IntPtr.Zero;
            InstallationProxyError returnValue = InstallationProxyNativeMethods.instproxy_status_get_error(status, out nameNative, out descriptionNative, ref code);
            name = ((string)nameMarshaler.MarshalNativeToManaged(nameNative));
            nameMarshaler.CleanUpNativeData(nameNative);
            description = ((string)descriptionMarshaler.MarshalNativeToManaged(descriptionNative));
            descriptionMarshaler.CleanUpNativeData(descriptionNative);
            return returnValue;
        }
        
        public static InstallationProxyError instproxy_client_get_path_for_bundle_identifier(InstallationProxyClientHandle client, string bundleId, out string path)
        {
            System.Runtime.InteropServices.ICustomMarshaler pathMarshaler = NativeStringMarshaler.GetInstance(null);
            System.IntPtr pathNative = System.IntPtr.Zero;
            InstallationProxyError returnValue = InstallationProxyNativeMethods.instproxy_client_get_path_for_bundle_identifier(client, bundleId, out pathNative);
            path = ((string)pathMarshaler.MarshalNativeToManaged(pathNative));
            pathMarshaler.CleanUpNativeData(pathNative);
            return returnValue;
        }
    }
}
