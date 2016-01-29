using System;

namespace Trinity
{
    /// <summary>
    /// Doesn't do anything special apart from simplified syntax in order to use string.format
    /// (see static methods)
    /// </summary>
    /// <remarks>
    /// The pure static throw was not being handled by exception management properly
    /// change to the thread static singleton fixed this
    /// </remarks>
    [Serializable]
    public class TrinityException : Exception
    {
        #region Constructors
        /// <summary>
        /// Use the static constructors
        /// </summary>
        protected TrinityException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Use the static constructors
        /// </summary>
        protected TrinityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Create a new Exception
        /// </summary>
        /// <remarks>
        /// Only use this exception if no suitable alternative exists - eg: use InvalidArgumentException if it is a better fit
        /// </remarks>
        public static TrinityException Create(string format, params object[] args)
        {
            string message = string.Format(format, args);
            TrinityException exception = new TrinityException(message);
            return exception;
        }

        /// <summary>
        /// Create a new Exception
        /// </summary>
        /// <remarks>
        /// Only use this exception if no suitable alternative exists - eg: use InvalidArgumentException if it is a better fit
        /// </remarks>
        public static TrinityException Create(Exception innerException, string format, params object[] args)
        {
            string message = string.Format(format, args);
            TrinityException exception = new TrinityException(message, innerException);
            return exception;
        }
        #endregion
    }
}