namespace Microsoft.RestServices.Exchange
{
    /// <summary>
    /// Preference.
    /// </summary>
    public class Preference
    {
        /// <summary>
        /// Create new instance of <see cref="Preference"/>
        /// </summary>
        /// <param name="prefer"></param>
        public Preference(string prefer)
        {
            ArgumentValidator.ThrowIfNullOrEmpty(prefer, nameof(prefer));
            this.Prefer = prefer;
        }

        /// <summary>
        /// Prefer value.
        /// </summary>
        public string Prefer { get; }

        /// <summary>
        /// To string impl.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Prefer;
        }

        /// <summary>
        /// Equals impl.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Preference preference))
            {
                return false;
            }

            return this.Prefer.Equals(preference.Prefer);
        }
    }
}
