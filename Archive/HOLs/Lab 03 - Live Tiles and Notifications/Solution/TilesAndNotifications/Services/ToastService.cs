using System.Xml.Linq;
using Windows.Data.Xml.Dom;

namespace TilesAndNotifications.Services
{
    public static class ToastService
    {
        public static XmlDocument CreateToast()
        {
            var xDoc = new XDocument(
                new XElement("toast",
                    new XElement("visual",
                        new XElement("binding", new XAttribute("template", "ToastGeneric"),
                            new XElement("text", "To Do List"),
                            new XElement("text", "Is the task complete?")
                            ) // binding
                        ), // visual
                    new XElement("actions",
                        new XElement("action", new XAttribute("activationType", "background"),
                            new XAttribute("content", "Yes"), new XAttribute("arguments", "yes")),
                        new XElement("action", new XAttribute("activationType", "background"),
                            new XAttribute("content", "No"), new XAttribute("arguments", "no"))
                        ) // actions
                    )
                );

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xDoc.ToString());
            return xmlDoc;
        }
    }
}