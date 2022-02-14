using System.Xml.Schema;

namespace Eplicta.Mets.Entities
{ 
public class XmlValidatorResult
{
    internal XmlValidatorResult(string message, XmlSeverityType xmlSeverityType, XmlSchemaException xmlSchemaException)
    {
        Message = message;
        XmlSeverityType = xmlSeverityType;
        XmlSchemaException = xmlSchemaException;
    }

    public string Message { get; }
    public XmlSeverityType XmlSeverityType { get; }
    public XmlSchemaException XmlSchemaException { get; }
}}

