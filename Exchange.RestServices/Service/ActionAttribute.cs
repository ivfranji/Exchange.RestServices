namespace Exchange.RestServices
{
    using System;

    /// <summary>
    /// Action attribute containing action name.
    /// </summary>
    internal class ActionAttribute : Attribute
    {
        /// <summary>
        /// Create new instance of <see cref="ActionAttribute"/>
        /// </summary>
        /// <param name="name">Name of the action.</param>
        public ActionAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Name of the action.
        /// </summary>
        public string Name { get; }
    }
}