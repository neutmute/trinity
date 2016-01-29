using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trinity
{
    static class Guard
    {
        /// <summary>
        /// Throw an exception in the argument is null
        /// </summary>
        [Obsolete("Uses old parameter ordering, Use Null()")]
        public static void NullArgument(string name, object argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void NullOrEmpty(string value, string messageFormat = null, params object[] messageArgs)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw TrinityException.Create(GetMessage("String cannot be null or empty", messageFormat, messageArgs));
            }
        }

        public static void EnumIsZero(Enum value)
        {
            if (Convert.ToInt32(value) == 0)
            {
                throw TrinityException.Create("Cannot be zero");
            }
        }

        [Obsolete("More terse method name used instead")]
        public static void AgainstNull(object value, string messageFormat, params object[] messageArgs)
        {
            Null(value, messageFormat, messageArgs);
        }

        public static void Against(bool conditionThatShouldBeFalse, string messageFormat = null, params object[] messageArgs)
        {
            if (conditionThatShouldBeFalse)
            {
                throw TrinityException.Create(messageFormat, messageArgs);
            }
        }

         public static void That(bool conditionThatShouldBeTrue, string messageFormat = null, params object[] messageArgs)
        {
            if (!conditionThatShouldBeTrue)
            {
                throw TrinityException.Create(messageFormat, messageArgs);
            }
        }


        public static void Null(object value, string messageFormat = null, params object[] messageArgs)
        {
            if (value == null)
            {
                throw TrinityException.Create(GetMessage("object cannot be null", messageFormat, messageArgs));
            }
        }

        private static string GetMessage(string defaultMessage, string messageFormat, params object[] messageArgs)
        {
            if (messageFormat != null)
            {
                defaultMessage = string.Format(messageFormat, messageArgs);
            }
            return defaultMessage;
        }
    }
}
