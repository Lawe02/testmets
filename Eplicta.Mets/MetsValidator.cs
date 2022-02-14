using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Eplicta.Mets.Entities;

namespace Eplicta.Mets
{
    public class MetsValidator
{
    public IEnumerable<XmlValidatorResult> Validate(XmlDocument document, Version version)
    {
        var schema = GetSchema(version);
        var xmlValidator = new XmlValidator();
        return xmlValidator.Validate(document, schema);
    }

    private static XmlDocument GetSchema(Version version)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"Eplicta.Mets.Resources.{version.Key}.xsd";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream);
        var result = reader.ReadToEnd();

        var xsd = new XmlDocument();
        xsd.LoadXml(result);
        return xsd;
    }
}
}



