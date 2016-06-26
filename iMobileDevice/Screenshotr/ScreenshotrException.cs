// <copyright file="ScreenshotrException.cs" company="Quamotion">
// Copyright (c) 2016 Quamotion. All rights reserved.
// </copyright>

namespace iMobileDevice.Screenshotr
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using iMobileDevice.iDevice;
    using iMobileDevice.Lockdown;
    using iMobileDevice.Afc;
    using iMobileDevice.Plist;
    
    
    /// Represents an exception that occurred when interacting with the Screenshotr API.
    [System.SerializableAttribute()]
    public class ScreenshotrException : System.Exception
    {
        
        /// <summary>
        /// Backing field for the <see cref="ErrorCode"/> property.
        /// </summary>
        private ScreenshotrError errorCode;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenshotrException"/> class.
        /// </summary>
        public ScreenshotrException()
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenshotrException"/> class with a specified error code.
        /// <summary>
        /// <param name="error">
        /// The error code of the error that occurred.
        /// </param>
        public ScreenshotrException(ScreenshotrError error) : 
                base(string.Format("An Screenshotr error occurred. The error code was {0}", error))
        {
            this.errorCode = error;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenshotrException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public ScreenshotrException(string message) : 
                base(message)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenshotrException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception, or <see langword="null"/> if no inner exception is specified.
        /// </param>
        public ScreenshotrException(string message, System.Exception inner) : 
                base(message, inner)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ScreenshotrException"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.
        /// </param>
        protected ScreenshotrException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                base(info, context)
        {
        }
        
        /// <summary>
        /// Gets the error code that represents the error.
        /// </summary>
        public virtual ScreenshotrError ErrorCode
        {
            get
            {
                return this.errorCode;
            }
        }
    }
}