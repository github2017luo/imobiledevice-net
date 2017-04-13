// <copyright file="DebugServerNativeMethods.cs" company="Quamotion">
// Copyright (c) 2016-2017 Quamotion. All rights reserved.
// </copyright>

namespace iMobileDevice.DebugServer
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using iMobileDevice.iDevice;
    using iMobileDevice.Lockdown;
    using iMobileDevice.Afc;
    using iMobileDevice.Plist;
    
    
    public partial class DebugServerNativeMethods
    {
        
        const string libraryName = "imobiledevice";
        
        /// <summary>
        /// Connects to the debugserver service on the specified device.
        /// </summary>
        /// <param name="device">
        /// The device to connect to.
        /// </param>
        /// <param name="service">
        /// The service descriptor returned by lockdownd_start_service.
        /// </param>
        /// <param name="client">
        /// Pointer that will point to a newly allocated
        /// debugserver_client_t upon successful return. Must be freed using
        /// debugserver_client_free() after use.
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success, DEBUGSERVER_E_INVALID_ARG when
        /// client is NULL, or an DEBUGSERVER_E_* error code otherwise.
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_new", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_new(iDeviceHandle device, LockdownServiceDescriptorHandle service, out DebugServerClientHandle client);
        
        /// <summary>
        /// Starts a new debugserver service on the specified device and connects to it.
        /// </summary>
        /// <param name="device">
        /// The device to connect to.
        /// </param>
        /// <param name="client">
        /// Pointer that will point to a newly allocated
        /// debugserver_client_t upon successful return. Must be freed using
        /// debugserver_client_free() after use.
        /// </param>
        /// <param name="label">
        /// The label to use for communication. Usually the program name.
        /// Pass NULL to disable sending the label in requests to lockdownd.
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success, or an DEBUGSERVER_E_* error
        /// code otherwise.
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_start_service", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_start_service(iDeviceHandle device, out DebugServerClientHandle client, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string label);
        
        /// <summary>
        /// Disconnects a debugserver client from the device and frees up the
        /// debugserver client data.
        /// </summary>
        /// <param name="client">
        /// The debugserver client to disconnect and free.
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success, DEBUGSERVER_E_INVALID_ARG when
        /// client is NULL, or an DEBUGSERVER_E_* error code otherwise.
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_free", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_free(System.IntPtr client);
        
        /// <summary>
        /// Sends raw data using the given debugserver service client.
        /// </summary>
        /// <param name="client">
        /// The debugserver client to use for sending
        /// </param>
        /// <param name="data">
        /// Data to send
        /// </param>
        /// <param name="size">
        /// Size of the data to send
        /// </param>
        /// <param name="sent">
        /// Number of bytes sent (can be NULL to ignore)
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success,
        /// DEBUGSERVER_E_INVALID_ARG when one or more parameters are
        /// invalid, or DEBUGSERVER_E_UNKNOWN_ERROR when an unspecified
        /// error occurs.
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_send", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_send(DebugServerClientHandle client, byte[] data, uint size, ref uint sent);
        
        /// <summary>
        /// Receives raw data using the given debugserver client with specified timeout.
        /// </summary>
        /// <param name="client">
        /// The debugserver client to use for receiving
        /// </param>
        /// <param name="data">
        /// Buffer that will be filled with the data received
        /// </param>
        /// <param name="size">
        /// Number of bytes to receive
        /// </param>
        /// <param name="received">
        /// Number of bytes received (can be NULL to ignore)
        /// </param>
        /// <param name="timeout">
        /// Maximum time in milliseconds to wait for data.
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success,
        /// DEBUGSERVER_E_INVALID_ARG when one or more parameters are
        /// invalid, DEBUGSERVER_E_MUX_ERROR when a communication error
        /// occurs, or DEBUGSERVER_E_UNKNOWN_ERROR when an unspecified
        /// error occurs.
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_receive_with_timeout", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_receive_with_timeout(DebugServerClientHandle client, byte[] data, uint size, ref uint received, uint timeout);
        
        /// <summary>
        /// Receives raw data from the debugserver service.
        /// </summary>
        /// <param name="client">
        /// The debugserver client
        /// </param>
        /// <param name="data">
        /// Buffer that will be filled with the data received
        /// </param>
        /// <param name="size">
        /// Number of bytes to receive
        /// </param>
        /// <param name="received">
        /// Number of bytes received (can be NULL to ignore)
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success,
        /// DEBUGSERVER_E_INVALID_ARG when client or plist is NULL
        /// </returns>
        /// <remarks>
        /// The default read timeout is 10 seconds.
        /// </remarks>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_receive", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_receive(DebugServerClientHandle client, byte[] data, uint size, ref uint received);
        
        /// <summary>
        /// Sends a command to the debugserver service.
        /// </summary>
        /// <param name="client">
        /// The debugserver client
        /// </param>
        /// <param name="command">
        /// Command to process and send
        /// </param>
        /// <param name="response">
        /// Response received for the command (can be NULL to ignore)
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success,
        /// DEBUGSERVER_E_INVALID_ARG when client or command is NULL
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_send_command", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_send_command(DebugServerClientHandle client, DebugServerCommandHandle command, out System.IntPtr response);
        
        /// <summary>
        /// Receives and parses response of debugserver service.
        /// </summary>
        /// <param name="client">
        /// The debugserver client
        /// </param>
        /// <param name="response">
        /// Response received for last command (can be NULL to ignore)
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success,
        /// DEBUGSERVER_E_INVALID_ARG when client is NULL
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_receive_response", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_receive_response(DebugServerClientHandle client, out System.IntPtr response);
        
        /// <summary>
        /// Controls status of ACK mode when sending commands or receiving responses.
        /// </summary>
        /// <param name="client">
        /// The debugserver client
        /// </param>
        /// <param name="enabled">
        /// A boolean flag indicating whether the internal ACK mode
        /// handling should be enabled or disabled.
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success, or an DEBUGSERVER_E_* error
        /// code otherwise.
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_set_ack_mode", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_set_ack_mode(DebugServerClientHandle client, int enabled);
        
        /// <summary>
        /// Sets the argv which launches an app.
        /// </summary>
        /// <param name="client">
        /// The debugserver client
        /// </param>
        /// <param name="argc">
        /// Number of arguments
        /// </param>
        /// <param name="argv">
        /// Array starting with the executable to be run followed by it's arguments
        /// </param>
        /// <param name="response">
        /// Response received for the command (can be NULL to ignore)
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success,
        /// DEBUGSERVER_E_INVALID_ARG when client is NULL
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_set_argv", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_set_argv(DebugServerClientHandle client, int argc, System.IntPtr argv, out System.IntPtr response);
        
        /// <summary>
        /// Adds or sets an environment variable.
        /// </summary>
        /// <param name="client">
        /// The debugserver client
        /// </param>
        /// <param name="env">
        /// The environment variable in "KEY=VALUE" notation
        /// </param>
        /// <param name="response">
        /// Response received for the command (can be NULL to ignore)
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success,
        /// DEBUGSERVER_E_INVALID_ARG when client is NULL
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_client_set_environment_hex_encoded", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_client_set_environment_hex_encoded(DebugServerClientHandle client, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string env, out System.IntPtr response);
        
        /// <summary>
        /// Creates and initializes a new command object.
        /// </summary>
        /// <param name="name">
        /// The name of the command which is sent in plain text
        /// </param>
        /// <param name="argv">
        /// Array of tokens for the command ment to be encoded
        /// </param>
        /// <param name="argc">
        /// Number of items in the token array
        /// </param>
        /// <param name="command">
        /// New command object
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success,
        /// DEBUGSERVER_E_INVALID_ARG when name or command is NULL
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_command_new", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_command_new([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string name, int argc, System.IntPtr argv, out DebugServerCommandHandle command);
        
        /// <summary>
        /// Frees memory of command object.
        /// </summary>
        /// <param name="command">
        /// The command object
        /// </param>
        /// <returns>
        /// DEBUGSERVER_E_SUCCESS on success,
        /// DEBUGSERVER_E_INVALID_ARG when command is NULL
        /// </returns>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_command_free", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern DebugServerError debugserver_command_free(System.IntPtr command);
        
        /// <summary>
        /// Encodes a string into hex notation.
        /// </summary>
        /// <param name="buffer">
        /// String to encode into hex notiation
        /// </param>
        /// <param name="encoded_buffer">
        /// The buffer receives a hex encoded string
        /// </param>
        /// <param name="encoded_length">
        /// Length of the hex encoded string
        /// </param>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_encode_string", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void debugserver_encode_string([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string buffer, out System.IntPtr encodedBuffer, ref uint encodedLength);
        
        /// <summary>
        /// Decodes a hex encoded string.
        /// </summary>
        /// <param name="encoded_buffer">
        /// The buffer with a hex encoded string
        /// </param>
        /// <param name="encoded_length">
        /// Length of the encoded buffer
        /// </param>
        /// <param name="buffer">
        /// Decoded string to be freed by the caller
        /// </param>
        [System.Runtime.InteropServices.DllImportAttribute(DebugServerNativeMethods.libraryName, EntryPoint="debugserver_decode_string", CallingConvention=System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public static extern void debugserver_decode_string([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string encodedBuffer, uint encodedLength, out System.IntPtr buffer);
    }
}
