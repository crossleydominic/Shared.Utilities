using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NUnit.Framework;
using Shared.Utilities.Scopes;

namespace Shared.Utilities.Tests.Scopes
{
    [TestFixture]
    public class XmlTextWriterScopeTests
    {
        #region WriteElement Tests

        [Test]
        public void WriteElement_ElementWritten()
        {
            StringWriter stringWriter = new StringWriter();

            XmlTextWriterScope subject = new XmlTextWriterScope(new XmlTextWriter(stringWriter));

            using (subject.WriteElement("testTag"))
            {
                subject.Value.WriteString("testContent");
            }

            int tagStartIndex = stringWriter.ToString().IndexOf("<testTag>");
            int contentIndex = stringWriter.ToString().IndexOf("testContent");
            int tagEndIndex = stringWriter.ToString().IndexOf("</testTag>");

            Assert.Greater(tagStartIndex, -1);
            Assert.Greater(contentIndex, -1);
            Assert.Greater(tagEndIndex, -1);

            Assert.Greater(contentIndex, tagStartIndex);
            Assert.Greater(tagEndIndex, contentIndex);
        }

        #endregion

        #region WriteDocument Tests

        [Test]
        public void WriteDocument_DocumentWritten()
        {
            StringWriter stringWriter = new StringWriter();

            XmlTextWriterScope subject = new XmlTextWriterScope(new XmlTextWriter(stringWriter));

            using (subject.WriteDocument())
            using (subject.WriteElement("testTag")) {}

            Assert.IsTrue(stringWriter.ToString().Contains("<?xml"));
        }

        #endregion

        #region WriteAttribute Tests

        [Test]
        public void WriteAttribute_AttributeWritten()
        {
            StringWriter stringWriter = new StringWriter();

            XmlTextWriterScope subject = new XmlTextWriterScope(new XmlTextWriter(stringWriter));

            using (subject.WriteDocument())
            using (subject.WriteElement("testTag")) 
            using (subject.WriteAttribute("testAttribute"))
            {
                subject.Value.WriteString("attributeValue");
            }

            Assert.IsTrue(stringWriter.ToString().Contains("<?xml"));
        }

        #endregion

    }
}
