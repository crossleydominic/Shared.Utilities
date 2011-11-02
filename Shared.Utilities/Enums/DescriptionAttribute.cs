using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Enums
{
    /// <summary>
    /// An attribute that can be applied to members of an 
    /// enum to give them nicer descriptions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple=false, Inherited=true)]
    public class DescriptionAttribute : Attribute
    {
        #region Private members

        /// <summary>
        /// The description that has been applied to an enum members
        /// </summary>
        private string _description;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new attribute with the specified description
        /// </summary>
        /// <param name="description">
        /// The description that will be applied to the enum members
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if description is null or empty.
        /// </exception>
        public DescriptionAttribute(string description)
        {
            #region Input validation

            Insist.IsNotNullOrEmpty(description, "description");

            #endregion

            _description = description;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the description appied to an enum members
        /// </summary>
        public string Description
        {
            get { return _description; }
        }

        #endregion
    }
}
