// <copyright file="IMobileBackupApi.cs" company="Quamotion">
// Copyright (c) 2016-2017 Quamotion. All rights reserved.
// </copyright>

namespace iMobileDevice.MobileBackup
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using iMobileDevice.iDevice;
    using iMobileDevice.Lockdown;
    using iMobileDevice.Afc;
    using iMobileDevice.Plist;
    
    
    public partial interface IMobileBackupApi
    {
        
        /// <summary>
        /// Gets or sets the <see cref="ILibiMobileDeviceApi"/> which owns this <see cref="MobileBackup"/>.
        /// </summary>
        ILibiMobileDevice Parent
        {
            get;
        }
        
        /// <summary>
        /// Connects to the mobilebackup service on the specified device.
        /// </summary>
        /// <param name="device">
        /// The device to connect to.
        /// </param>
        /// <param name="service">
        /// The service descriptor returned by lockdownd_start_service.
        /// </param>
        /// <param name="client">
        /// Pointer that will be set to a newly allocated
        /// mobilebackup_client_t upon successful return.
        /// </param>
        /// <returns>
        /// MOBILEBACKUP_E_SUCCESS on success, MOBILEBACKUP_E_INVALID ARG if one
        /// or more parameters are invalid, or DEVICE_LINK_SERVICE_E_BAD_VERSION if
        /// the mobilebackup version on the device is newer.
        /// </returns>
        MobileBackupError mobilebackup_client_new(iDeviceHandle device, LockdownServiceDescriptorHandle service, out MobileBackupClientHandle client);
        
        /// <summary>
        /// Starts a new mobilebackup service on the specified device and connects to it.
        /// </summary>
        /// <param name="device">
        /// The device to connect to.
        /// </param>
        /// <param name="client">
        /// Pointer that will point to a newly allocated
        /// mobilebackup_client_t upon successful return. Must be freed using
        /// mobilebackup_client_free() after use.
        /// </param>
        /// <param name="label">
        /// The label to use for communication. Usually the program name.
        /// Pass NULL to disable sending the label in requests to lockdownd.
        /// </param>
        /// <returns>
        /// MOBILEBACKUP_E_SUCCESS on success, or an MOBILEBACKUP_E_* error
        /// code otherwise.
        /// </returns>
        MobileBackupError mobilebackup_client_start_service(iDeviceHandle device, out MobileBackupClientHandle client, string label);
        
        /// <summary>
        /// Disconnects a mobilebackup client from the device and frees up the
        /// mobilebackup client data.
        /// </summary>
        /// <param name="client">
        /// The mobilebackup client to disconnect and free.
        /// </param>
        /// <returns>
        /// MOBILEBACKUP_E_SUCCESS on success, or MOBILEBACKUP_E_INVALID_ARG
        /// if client is NULL.
        /// </returns>
        MobileBackupError mobilebackup_client_free(System.IntPtr client);
        
        /// <summary>
        /// Polls the device for mobilebackup data.
        /// </summary>
        /// <param name="client">
        /// The mobilebackup client
        /// </param>
        /// <param name="plist">
        /// A pointer to the location where the plist should be stored
        /// </param>
        /// <returns>
        /// an error code
        /// </returns>
        MobileBackupError mobilebackup_receive(MobileBackupClientHandle client, out PlistHandle plist);
        
        /// <summary>
        /// Sends mobilebackup data to the device
        /// </summary>
        /// <param name="client">
        /// The mobilebackup client
        /// </param>
        /// <param name="plist">
        /// The location of the plist to send
        /// </param>
        /// <returns>
        /// an error code
        /// </returns>
        /// <remarks>
        /// This function is low-level and should only be used if you need to send
        /// a new type of message.
        /// </remarks>
        MobileBackupError mobilebackup_send(MobileBackupClientHandle client, PlistHandle plist);
        
        /// <summary>
        /// Request a backup from the connected device.
        /// </summary>
        /// <param name="client">
        /// The connected MobileBackup client to use.
        /// </param>
        /// <param name="backup_manifest">
        /// The backup manifest, a plist_t of type PLIST_DICT
        /// containing the backup state of the last backup. For a first-time backup
        /// set this parameter to NULL.
        /// </param>
        /// <param name="base_path">
        /// The base path on the device to use for the backup
        /// operation, usually "/".
        /// </param>
        /// <param name="proto_version">
        /// A string denoting the version of the backup protocol
        /// to use. Latest known version is "1.6"
        /// </param>
        /// <returns>
        /// MOBILEBACKUP_E_SUCCESS on success, MOBILEBACKUP_E_INVALID_ARG if
        /// one of the parameters is invalid, MOBILEBACKUP_E_PLIST_ERROR if
        /// backup_manifest is not of type PLIST_DICT, MOBILEBACKUP_E_MUX_ERROR
        /// if a communication error occurs, MOBILEBACKUP_E_REPLY_NOT_OK
        /// </returns>
        MobileBackupError mobilebackup_request_backup(MobileBackupClientHandle client, PlistHandle backupManifest, string basePath, string protoVersion);
        
        /// <summary>
        /// Sends a confirmation to the device that a backup file has been received.
        /// </summary>
        /// <param name="client">
        /// The connected MobileBackup client to use.
        /// </param>
        /// <returns>
        /// MOBILEBACKUP_E_SUCCESS on success, MOBILEBACKUP_E_INVALID_ARG if
        /// client is invalid, or MOBILEBACKUP_E_MUX_ERROR if a communication error
        /// occurs.
        /// </returns>
        MobileBackupError mobilebackup_send_backup_file_received(MobileBackupClientHandle client);
        
        /// <summary>
        /// Request that a backup should be restored to the connected device.
        /// </summary>
        /// <param name="client">
        /// The connected MobileBackup client to use.
        /// </param>
        /// <param name="backup_manifest">
        /// The backup manifest, a plist_t of type PLIST_DICT
        /// containing the backup state to be restored.
        /// </param>
        /// <param name="flags">
        /// Flags to send with the request. Currently this is a combination
        /// of the following mobilebackup_flags_t:
        /// MB_RESTORE_NOTIFY_SPRINGBOARD - let SpringBoard show a 'Restore' screen
        /// MB_RESTORE_PRESERVE_SETTINGS - do not overwrite any settings
        /// MB_RESTORE_PRESERVE_CAMERA_ROLL - preserve the photos of the camera roll
        /// </param>
        /// <param name="proto_version">
        /// A string denoting the version of the backup protocol
        /// to use. Latest known version is "1.6". Ideally this value should be
        /// extracted from the given manifest plist.
        /// </param>
        /// <returns>
        /// MOBILEBACKUP_E_SUCCESS on success, MOBILEBACKUP_E_INVALID_ARG if
        /// one of the parameters is invalid, MOBILEBACKUP_E_PLIST_ERROR if
        /// backup_manifest is not of type PLIST_DICT, MOBILEBACKUP_E_MUX_ERROR
        /// if a communication error occurs, or MOBILEBACKUP_E_REPLY_NOT_OK
        /// if the device did not accept the request.
        /// </returns>
        MobileBackupError mobilebackup_request_restore(MobileBackupClientHandle client, PlistHandle backupManifest, MobileBackupFlags flags, string protoVersion);
        
        /// <summary>
        /// Receive a confirmation from the device that it successfully received
        /// a restore file.
        /// </summary>
        /// <param name="client">
        /// The connected MobileBackup client to use.
        /// </param>
        /// <param name="result">
        /// Pointer to a plist_t that will be set to the received plist
        /// for further processing. The caller has to free it using plist_free().
        /// Note that it will be set to NULL if the operation itself fails due to
        /// a communication or plist error.
        /// If this parameter is NULL, it will be ignored.
        /// </param>
        /// <returns>
        /// MOBILEBACKUP_E_SUCCESS on success, MOBILEBACKUP_E_INVALID_ARG if
        /// client is invalid, MOBILEBACKUP_E_REPLY_NOT_OK if the expected
        /// 'BackupMessageRestoreFileReceived' message could not be received,
        /// MOBILEBACKUP_E_PLIST_ERROR if the received message is not a valid backup
        /// message plist, or MOBILEBACKUP_E_MUX_ERROR if a communication error
        /// occurs.
        /// </returns>
        MobileBackupError mobilebackup_receive_restore_file_received(MobileBackupClientHandle client, out PlistHandle result);
        
        /// <summary>
        /// Receive a confirmation from the device that it successfully received
        /// application data file.
        /// </summary>
        /// <param name="client">
        /// The connected MobileBackup client to use.
        /// </param>
        /// <param name="result">
        /// Pointer to a plist_t that will be set to the received plist
        /// for further processing. The caller has to free it using plist_free().
        /// Note that it will be set to NULL if the operation itself fails due to
        /// a communication or plist error.
        /// If this parameter is NULL, it will be ignored.
        /// </param>
        /// <returns>
        /// MOBILEBACKUP_E_SUCCESS on success, MOBILEBACKUP_E_INVALID_ARG if
        /// client is invalid, MOBILEBACKUP_E_REPLY_NOT_OK if the expected
        /// 'BackupMessageRestoreApplicationReceived' message could not be received,
        /// MOBILEBACKUP_E_PLIST_ERROR if the received message is not a valid backup
        /// message plist, or MOBILEBACKUP_E_MUX_ERROR if a communication error
        /// occurs.
        /// </returns>
        MobileBackupError mobilebackup_receive_restore_application_received(MobileBackupClientHandle client, out PlistHandle result);
        
        /// <summary>
        /// Tells the device that the restore process is complete and waits for the
        /// device to close the connection. After that, the device should reboot.
        /// </summary>
        /// <param name="client">
        /// The connected MobileBackup client to use.
        /// </param>
        /// <returns>
        /// MOBILEBACKUP_E_SUCCESS on success, MOBILEBACKUP_E_INVALID_ARG if
        /// client is invalid, MOBILEBACKUP_E_PLIST_ERROR if the received disconnect
        /// message plist is invalid, or MOBILEBACKUP_E_MUX_ERROR if a communication
        /// error occurs.
        /// </returns>
        MobileBackupError mobilebackup_send_restore_complete(MobileBackupClientHandle client);
        
        /// <summary>
        /// Sends a backup error message to the device.
        /// </summary>
        /// <param name="client">
        /// The connected MobileBackup client to use.
        /// </param>
        /// <param name="reason">
        /// A string describing the reason for the error message.
        /// </param>
        /// <returns>
        /// MOBILEBACKUP_E_SUCCESS on success, MOBILEBACKUP_E_INVALID_ARG if
        /// one of the parameters is invalid, or MOBILEBACKUP_E_MUX_ERROR if a
        /// communication error occurs.
        /// </returns>
        MobileBackupError mobilebackup_send_error(MobileBackupClientHandle client, string reason);
    }
}
