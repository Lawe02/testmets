using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;

[assembly: InternalsVisibleTo("Eplicta.Mets.Tests")]

namespace Eplicta.Mets.Helpers
{ 
internal class Resource
{
    public static string Get(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"Eplicta.Mets.Resources.{name}";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        var result = reader.ReadToEnd();
        return result;
    }

    internal static XmlDocument GetXml(string name)
    {
        var xsd = new XmlDocument();
        xsd.LoadXml(Get(name));
        return xsd;
    }
}}

