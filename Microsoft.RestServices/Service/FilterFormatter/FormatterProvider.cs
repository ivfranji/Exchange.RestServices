namespace Microsoft.RestServices.Exchange
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// List of formatters.
    /// </summary>
    internal class FormatterProvider
    {
        /// <summary>
        /// String formatter name.
        /// </summary>
        private const string StringFormatterName = "System.String";

        /// <summary>
        /// Formatters supported.
        /// </summary>
        private Dictionary<string, IFilterFormatter> formatters;

        /// <summary>
        /// Create new instance of <see cref="FormatProvider"/>
        /// </summary>
        internal FormatterProvider()
        {
            this.formatters = new Dictionary<string, IFilterFormatter>();

            Type baseFilterFormatterType = typeof(BaseFilterFormatter);

            foreach (Type type in Assembly.GetAssembly(baseFilterFormatterType).GetTypes())
            {
                if (type.IsClass && 
                    !type.IsAbstract && 
                    type.IsSubclassOf(baseFilterFormatterType))
                {
                    IFilterFormatter formatter = (BaseFilterFormatter) Activator.CreateInstance(type);
                    this.formatters.Add(formatter.Type, formatter);
                }
            }
        }

        /// <summary>
        /// Returns correct formatter for a type. Defaults to string.
        /// </summary>
        /// <param name="type">Type full name.</param>
        /// <returns></returns>
        public IFilterFormatter this[string type]
        {
            get
            {
                if (this.formatters.ContainsKey(type))
                {
                    return this.formatters[type];
                }

                return this.formatters[FormatterProvider.StringFormatterName];
            }
        }
    }
}