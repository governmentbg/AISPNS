using AISTN.CommercialRegIntegrator.XMLClasses;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AISTN.CommercialRegIntegrator.Helpers
{
    internal class XmlManipulator
    {
        private XmlDocument _xmlDoc;
        private XmlNamespaceManager _namespaceManager;
        private string xmlNamespace;
        private string namespacePrefix;
        private string originalXMLContent;

        public XmlManipulator(string xmlContent, string xmlNamespace, string namespacePrefix)
        {
            this.originalXMLContent = xmlContent;
            this.namespacePrefix = namespacePrefix;
            this.xmlNamespace = xmlNamespace;

            _xmlDoc = new XmlDocument();
            _xmlDoc.LoadXml(this.originalXMLContent);

            _namespaceManager = new XmlNamespaceManager(_xmlDoc.NameTable);
            _namespaceManager.AddNamespace(namespacePrefix, xmlNamespace);
        }

       
        public XmlManipulator(XmlNode xmlNode, string xmlNamespace, string namespacePrefix) : this(xmlNode.OuterXml,  xmlNamespace, namespacePrefix)
        {
            
        }

        public void AddNameSpace(string namespacePrefix, string xmlNamespace)
        {
            _namespaceManager.AddNamespace(namespacePrefix, xmlNamespace);
        }


        public async Task<XmlNodeList?> extractXMLNodesAsync(string mainSelector)
        { 
            return _xmlDoc.SelectNodes(mainSelector, this._namespaceManager);
        }
        protected void NormalizeNamespace()
        {

            XmlElement root = _xmlDoc.DocumentElement;
            if (root == null)
                throw new InvalidOperationException("The XML document must have a root element.");

            // Recursively process each node to set the namespace prefix
            SetNamespacePrefix(root, this.namespacePrefix, this.xmlNamespace, this._xmlDoc);

        }
        private void SetNamespacePrefix(XmlNode node, string prefix, string ns, XmlDocument doc)
        {
            // Ignore nodes that are not elements (e.g., comments, text nodes, etc.)
            if (node.NodeType != XmlNodeType.Element)
                return;

            // Set the prefix for the current element
            if (node.Prefix != prefix)
            {
                XmlElement element = (XmlElement)node;
                XmlElement newElement = doc.CreateElement(prefix, element.LocalName, ns);
                while (element.HasAttributes)
                {
                    newElement.SetAttributeNode(element.RemoveAttributeNode(element.Attributes[0]));
                }
                foreach (XmlNode child in element.ChildNodes)
                {
                    newElement.AppendChild(element.OwnerDocument.ImportNode(child, true));
                }
                element.ParentNode.ReplaceChild(newElement, element);
            }

            // Recursively process child nodes
            foreach (XmlNode child in node.ChildNodes)
            {
                SetNamespacePrefix(child, prefix, ns, doc);
            }
        }

    }
}
