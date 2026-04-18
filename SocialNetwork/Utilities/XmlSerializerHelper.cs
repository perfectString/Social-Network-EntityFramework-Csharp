using System.Text;
using System.Xml.Serialization;

namespace SocialNetwork.Utilities;

public static class XmlSerializerHelper
{
    public static T? Deserialize<T>(string inputXml, string rootAttributeName)

    {
        XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootAttributeName);
        XmlSerializer xmlSerializer
            = new XmlSerializer(typeof(T), xmlRootAttribute);

        using StringReader stringReader = new StringReader(inputXml);
        T? importUserDtos = (T?)xmlSerializer.Deserialize(stringReader);

        return importUserDtos;
    }

    public static T? Deserialize<T>(Stream inputStream, string rootAttributeName)
    {
        XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootAttributeName);
        XmlSerializer xmlSerializer
            = new XmlSerializer(typeof(T), xmlRootAttribute);

       
        T? importUserDtos = (T?)xmlSerializer.Deserialize(inputStream);

        return importUserDtos;
    }

    public static string Serialize<T>(T objectToSerialize, string rootAttributeName, IDictionary<string, string> namespaces = null)
    {
        var result = new StringBuilder();

        XmlSerializerNamespaces XmlNamespace = new XmlSerializerNamespaces();

        if (namespaces == null)
        {
            XmlNamespace.Add(string.Empty, string.Empty);
        }
        else
        {

            foreach (var nsKVP in namespaces)
            {
                XmlNamespace.Add(nsKVP.Key, nsKVP.Value);
            }
        }

        XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(rootAttributeName);
        XmlSerializer xmlSerializer
            = new XmlSerializer(typeof(T), xmlRootAttribute);

        using StringWriter sw = new StringWriter(result);
        xmlSerializer.Serialize(sw, objectToSerialize, XmlNamespace);

        return result.ToString();
    }

    public static void Serialize<T>(T objectToSerialize, string rootAttributeName,
        Stream serializationStream, IDictionary<string, string>? namespaces = null)
    {
        XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
        if (namespaces == null)
        {
            xmlNamespaces.Add(string.Empty, string.Empty);
        }
        else
        {
            foreach (KeyValuePair<string, string> nsKvp in namespaces)
            {
                xmlNamespaces.Add(nsKvp.Key, nsKvp.Value);
            }
        }

        XmlRootAttribute xmlRootAttribute
            = new XmlRootAttribute(rootAttributeName);
        XmlSerializer xmlSerializer
            = new XmlSerializer(typeof(T), xmlRootAttribute);

        xmlSerializer.Serialize(serializationStream, objectToSerialize, xmlNamespaces);
    }
}
