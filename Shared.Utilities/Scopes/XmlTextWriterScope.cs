using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Shared.Utilities.Scopes
{   
    /// <summary>
    /// Scope to provide WriteStart/WriteEnd scopes for XmlTextWriter objects.
    /// </summary>
    public class XmlTextWriterScope : Scope<XmlTextWriter>
    {
        #region Constructors

        /// <summary>
        /// Create new XmlTextWriterScope wrapping the supplied XmlTextWriter
        /// </summary>
        public XmlTextWriterScope(XmlTextWriter xmlWriter) : base(xmlWriter){}

        #endregion

        #region Public Methods

        /// <summary>
        /// Get disposer to provide WriteStartElement/WriteEndElement semantics
        /// </summary>
        public Disposer<XmlTextWriter> WriteElement(string localName)
        {
            return Create(v => v.WriteStartElement(localName), v => v.WriteEndElement());
        }

        /// <summary>
        /// Get disposer to provide WriteStartElement/WriteEndElement semantics
        /// </summary>
        public Disposer<XmlTextWriter> WriteElement(string localName, string ns)
        {
            return Create(v => v.WriteStartElement(localName, ns), v => v.WriteEndElement());
        }

        /// <summary>
        /// Get disposer to provide WriteStartElement/WriteEndElement semantics
        /// </summary>
        public Disposer<XmlTextWriter> WriteElement(string prefix, string localName, string ns)
        {
            return Create(v => v.WriteStartElement(prefix, localName, ns), v => v.WriteEndElement());
        }

        /// <summary>
        /// Get disposer to provide WriteStartAttribute/WriteEndElement semantics
        /// </summary>
        public Disposer<XmlTextWriter> WriteAttribute(string localName)
        {
            return Create(v => v.WriteStartAttribute(localName), v => v.WriteEndAttribute());
        }

        /// <summary>
        /// Get disposer to provide WriteStartAttribute/WriteEndElement semantics
        /// </summary>
        public Disposer<XmlTextWriter> WriteAttribute(string localName, string ns)
        {
            return Create(v => v.WriteStartAttribute(localName, ns), v => v.WriteEndAttribute());
        }

        /// <summary>
        /// Get disposer to provide WriteStartAttribute/WriteEndElement semantics
        /// </summary>
        public Disposer<XmlTextWriter> WriteAttribute(string prefix, string localName, string ns)
        {
            return Create(v => v.WriteStartAttribute(prefix, localName, ns), v => v.WriteEndElement());
        }

        /// <summary>
        /// Get disposer to provide WriteStartDocument/WriteEndDocument semantics
        /// </summary>
        public Disposer<XmlTextWriter> WriteDocument()
        {
            return Create(v => v.WriteStartDocument(), v => v.WriteEndDocument());
        }

        /// <summary>
        /// Get disposer to provide WriteStartDocument/WriteEndDocument semantics
        /// </summary>
        public Disposer<XmlTextWriter> WriteDocument(bool standalone)
        {
            return Create(v => v.WriteStartDocument(standalone), v => v.WriteEndDocument());
        }

        #endregion
    }
}
