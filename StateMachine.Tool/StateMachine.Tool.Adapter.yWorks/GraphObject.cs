﻿using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace StateMachine.Tool.Adapter.yWorks
{
    public abstract class GraphObject
    {
        public string Identifier { get; private set; }

        //public string Text { get; protected set; }
        public string Description { get; protected set; }

        public virtual void Load(XElement element, KeyMapping keyMapping)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));
            if (keyMapping == null)
                throw new ArgumentNullException(nameof(keyMapping));

            Identifier = (string) element.Attribute("id");
            if (Identifier != null)
                Identifier = Identifier.Trim();

            var dataElements = element
                .Elements(XName.Get("data", element.GetDefaultNamespace().NamespaceName));

            var descriptionContent = dataElements
                .Where(x => (string) x.Attribute("key") == keyMapping.NodeDescrionId ||
                            (string) x.Attribute("key") == keyMapping.GraphDescrionId ||
                            (string) x.Attribute("key") == keyMapping.EdgeDescrionId)
                .FirstOrDefault();

            if (descriptionContent != null && descriptionContent.FirstNode != null)
                if (descriptionContent.FirstNode.NodeType == XmlNodeType.CDATA)
                    Description = ((XCData) descriptionContent.FirstNode).Value;
                else if (descriptionContent.FirstNode.NodeType == XmlNodeType.Text)
                    Description = ((XText) descriptionContent.FirstNode).Value;

            if (Description != null)
                Description = Description.Trim();
        }

        protected void CheckIdProperty(IXmlLineInfo element)
        {
            var hasId = string.IsNullOrWhiteSpace(Identifier) == false;
            var hasDescription = string.IsNullOrWhiteSpace(Description) == false;

            if (hasId == false)
            {
                var info = element;
                var msg = string.Format(
                    "The node [description: {0}] at line {1} position {2} is incomplete.",
                    hasDescription ? Description : "<missing>",
                    element.LineNumber,
                    element.LinePosition);
                throw new FormatException(msg);
            }
        }

        protected void CheckDescriptionProperty(IXmlLineInfo element)
        {
            var hasId = string.IsNullOrWhiteSpace(Identifier) == false;
            var hasDescription = string.IsNullOrWhiteSpace(Description) == false;

            if (hasDescription == false)
            {
                var msg = string.Format(
                    "The node [id: {0}] at line {1} position {2} is incomplete.",
                    hasId ? Identifier : "<missing>",
                    element.LineNumber,
                    element.LinePosition);
                throw new FormatException(msg);
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}", Identifier, Description);
        }
    }
}