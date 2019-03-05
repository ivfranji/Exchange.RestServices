namespace Exchange.RestServices
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Contains map with method names with method info.
    /// </summary>
    internal class ActionMapper
    {
        /// <summary>
        /// Move message action.
        /// </summary>
        internal const string MoveMessageAction = "Move~Message";

        /// <summary>
        /// Move message action.
        /// </summary>
        internal const string MoveMailFolderAction = "Move~MailFolder";

        /// <summary>
        /// Copy message action.
        /// </summary>
        internal const string CopyMessageAction = "Copy~Message";

        /// <summary>
        /// Send message action.
        /// </summary>
        internal const string SendMessageAction = "Send~Message";

        /// <summary>
        /// Reply message action.
        /// </summary>
        internal const string ReplyMessageAction = "Reply~Message";

        /// <summary>
        /// Reply message action.
        /// </summary>
        internal const string ForwardMessageAction = "Forward~Message";

        /// <summary>
        /// Decline event action.
        /// </summary>
        internal const string DeclineEventAction = "Decline~Event";

        /// <summary>
        /// Decline event action.
        /// </summary>
        internal const string CompleteTaskAction = "Complete~Task";

        /// <summary>
        /// Method mapper.
        /// </summary>
        private Dictionary<string, MethodInfo> mapper;

        /// <summary>
        /// Create new instance of <see cref="ActionMapper"/>.
        /// </summary>
        /// <param name="type">Type.</param>
        internal ActionMapper(Type type)
        {
            ArgumentValidator.ThrowIfNull(type, nameof(type));
            this.mapper = new Dictionary<string, MethodInfo>();
            foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                object[] attributes = methodInfo.GetCustomAttributes(typeof(ActionAttribute), false);
                if (attributes != null && attributes.Length == 1)
                {
                    if (attributes[0] is ActionAttribute actionAttribute)
                    {
                        this.mapper.Add(actionAttribute.Name, methodInfo);
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves method if registered, otherwise throws.
        /// </summary>
        /// <param name="key">Method name.</param>
        /// <returns></returns>
        public MethodInfo this[string key]
        {
            get
            {
                if (this.ContainsAction(key))
                {
                    return this.mapper[key];
                }

                throw new NotSupportedException($"Action '{key}' not supported.");
            }
        }

        /// <summary>
        /// Validate if 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsAction(string name)
        {
            return this.mapper.ContainsKey(name);
        }
    }
}