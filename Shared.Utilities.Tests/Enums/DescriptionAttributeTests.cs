using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E = Shared.Utilities.Enums;
using System.Reflection;
using NUnit.Framework;

namespace Shared.Utilities.Tests.Enums
{
    /// <summary>
    /// A set of tests for the DescriptionAttribute class.
    /// </summary>
    [TestFixture]
    public class DescriptionAttributeTests
    {
        #region Test Enums

        private enum EnumWithEmptyStringDescription
        {
            [E.Description("")]
            Item1
        }

        private enum EnumWithNullStringDescription
        {
            [E.Description(null)]
            Item1
        }

        #endregion

        #region Constructor Tests

        /// <summary>
        /// Use an enum that has a description of an empty string.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EnumHasEmptyStringDescription_ThrowsException()
        {
            ForceCustomAttributeInstantiation(typeof(EnumWithEmptyStringDescription));
        }

        /// <summary>
        /// Use an enum that has a description of a null string.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_EnumHasNullStringDescription_ThrowsException()
        {
            ForceCustomAttributeInstantiation(typeof(EnumWithNullStringDescription));
        }

        #endregion

        #region private methods

        /// <summary>
        /// Custom attributes are only instatiated when inspecting them via
        /// reflection.  Inspect them here to force them to get created.
        /// </summary>
        private void ForceCustomAttributeInstantiation(Type type)
        {
           FieldInfo[] memInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

           foreach (FieldInfo info in memInfos)
           {
               object[] attrs = info.GetCustomAttributes(typeof(E.DescriptionAttribute), false);
               string desc = ((E.DescriptionAttribute)attrs[0]).Description;
           }
        }

        #endregion
    }
}
