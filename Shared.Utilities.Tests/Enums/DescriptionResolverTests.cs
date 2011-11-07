using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E = Shared.Utilities.Enums;
using Shared.Utilities.Enums;
using NUnit.Framework;

namespace Shared.Utilities.Tests.Enums
{
    [TestFixture]
    public class DescriptionResolverTests
    {
        #region Test structs/enums

        public enum TestStruct { }

        public enum TestEnum
        {
            Item1 = 1,

            [E.Description("Second Item")]
            Item2 = 2
        }

        #endregion

        #region GetDescription

        /// <summary>
        /// Test to make sure that an exception is thrown if a null Enum is
        /// passed to the resolver.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDescription_ValueNull_ThrowsException()
        {
            string description = DescriptionResolver.GetDescription(null);
        }

        /// <summary>
        /// Test to make sure that an exception is thrown if using a enum value
        /// that isn't defined for that enum
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDescription_ValueNotDefined_ThrowsException()
        {
            //Force a value that isn't defined
            TestEnum value = (TestEnum)0;

            string description = DescriptionResolver.GetDescription(value);
        }

        /// <summary>
        /// Test to make sure that an exception is thrown if a non-enum type
        /// is supplied
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDescription_TypeIsNotAnEnum_ThrowsException()
        {
            //C# won't allow generic parameters to be constrainted to enums
            //so we have to allow structs.  We don't have to be happy about it
            //though so we'll throw an exception if the supplied type is not
            //an enum.
            TestStruct value = new TestStruct();

            string description = DescriptionResolver.GetDescription(value);
        }

        /// <summary>
        /// Test to make sure that the description for a value without a DescriptionAttribute
        /// is the same as Value.ToString()
        /// </summary>
        [Test]
        public void GetDescription_ValueDoesNotHaveDescription()
        {
            string description = DescriptionResolver.GetDescription(TestEnum.Item1);

            Assert.AreEqual(description, TestEnum.Item1.ToString());
        }

        /// <summary>
        /// Test to make sure that the correct description is returned.
        /// </summary>
        [Test]
        public void GetDescription_ValueHasDescription()
        {
            string description = DescriptionResolver.GetDescription(TestEnum.Item2);

            Assert.AreEqual(description, "Second Item");
        }

        #endregion

        #region TryGetDescription

        /// <summary>
        /// Test to make sure that no exception is thrown if a null Enum is
        /// passed to the resolver but an empty string is returned instead.
        /// </summary>
        [Test]
        public void GetDescription_ValueNull()
        {
            string description = DescriptionResolver.TryGetDescription(null);

            Assert.AreEqual(description, string.Empty);
        }

        /// <summary>
        /// Test to make sure that no exception is thrown if using a enum value
        /// that isn't defined for that enum but value.ToString() is returned instead.
        /// </summary>
        [Test]
        public void TryGetDescription_ValueNotDefined()
        {
            //Force a value that isn't defined
            TestEnum value = (TestEnum)0;
            
            string description = DescriptionResolver.TryGetDescription(value);

            Assert.AreEqual(description, value.ToString());
        }

        /// <summary>
        /// Test to make sure that no exception is thrown if a non-enum type
        /// is supplied but value.ToString() is returned instead.
        /// </summary>
        [Test]
        public void TryGetDescription_TypeIsNotAnEnum()
        {
            //C# won't allow generic parameters to be constrainted to enums
            //so we have to allow structs.  We don't have to be happy about it
            //though so we'll throw an exception if the supplied type is not
            //an enum.
            TestStruct value = new TestStruct();

            string description = DescriptionResolver.TryGetDescription(value);

            Assert.AreEqual(description, value.ToString());
        }

        /// <summary>
        /// Test to make sure that the description for a value without a DescriptionAttribute
        /// is the same as Value.ToString()
        /// </summary>
        [Test]
        public void TryGetDescription_ValueDoesNotHaveDescription()
        {
            string description = DescriptionResolver.TryGetDescription(TestEnum.Item1);

            Assert.AreEqual(description, TestEnum.Item1.ToString());
        }

        /// <summary>
        /// Test to make sure that the correct description is returned.
        /// </summary>
        [Test]
        public void TryGetDescription_ValueHasDescription()
        {
            string description = DescriptionResolver.TryGetDescription(TestEnum.Item2);

            Assert.AreEqual(description, "Second Item");
        }

        #endregion
    }
}
