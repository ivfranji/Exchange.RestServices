namespace Microsoft.RestServices.Exchange
{
    using System.Collections.Generic;

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
        /// String formatter name.
        /// </summary>
        private const string BooleanFormatterName = "System.Boolean";

        /// <summary>
        /// Microsoft graph recipient formatter name.
        /// </summary>
        private const string MicrosoftGraphRecipientFormatterName = "Microsoft.Graph.Recipient";

        /// <summary>
        /// Int formatter name.
        /// </summary>
        private const string IntFormatterName = "System.Int32";

        /// <summary>
        /// Date time offset formatter name.
        /// </summary>
        private const string DateTimeOffsetFormatterName = "System.DateTimeOffset";

        /// <summary>
        /// Date time formatter name.
        /// </summary>
        private const string DateTimeFormatterName = "System.DateTime";

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

            this.formatters.Add(
                FormatterProvider.BooleanFormatterName, 
                new BoolFilterFormatter());

            this.formatters.Add(
                FormatterProvider.StringFormatterName, 
                new StringFilterFormatter());

            this.formatters.Add(
                FormatterProvider.MicrosoftGraphRecipientFormatterName, 
                new RecipientFilterFormatter());

            this.formatters.Add(
                FormatterProvider.IntFormatterName, 
                new IntFilterFormatter());

            this.formatters.Add(
                FormatterProvider.DateTimeOffsetFormatterName, 
                new DateTimeOffsetFilterFormatter());

            this.formatters.Add(
                FormatterProvider.DateTimeFormatterName, 
                new DateTimetFilterFormatter());
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