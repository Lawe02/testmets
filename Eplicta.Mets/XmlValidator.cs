using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Eplicta.Mets.Entities;
using Eplicta.Mets.Helpers;

[assembly: InternalsVisibleTo("Eplicta.Mets.Tests")]

namespace Eplicta.Mets
{
    public class XmlValidator
{
    public IEnumerable<XmlValidatorResult> Validate(XmlDocument document, XmlDocument schema)
    {
        if (document == null) throw new ArgumentNullException(nameof(document));

        var responses = new List<XmlValidatorResult>();

        try
        {
            var schemas = new XmlSchemaSet();
            using var fs = new StringReader(schema.OuterXml);
            using var reader = XmlReader.Create(fs, new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore });
            var xmlns = document.FirstChild?.Attributes?["xmlns"]?.InnerText;

            schemas.Add(xmlns, reader);

            LoadSchema(schemas, "xmldsig-core-schema.xsd", @"http://www.w3.org/2000/09/xmldsig#");
            LoadSchema(schemas, "xml.xsd", @"http://www.w3.org/XML/1998/namespace");
            LoadSchema(schemas, "xlink.xsd", @"http://www.w3.org/1999/xlink");
            LoadSchema(schemas, "eARD_Paket_FGS-PUBL_mets.xsd", @"http://www.loc.gov/METS/");

            const XmlSchemaValidationFlags validationFlags = XmlSchemaValidationFlags.ProcessInlineSchema | XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.AllowXmlAttributes;
            var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema, ValidationFlags = validationFlags, DtdProcessing = DtdProcessing.Ignore };

            using var ss1 = new StringReader(document.OuterXml);
            using var xr = XmlReader.Create(ss1, settings);
            var xdoc = XDocument.Load(xr);
            xdoc.Validate(schemas, (_, e) => { responses.Add(new XmlValidatorResult(e.Message, e.Severity, e.Exception)); });
        }
        catch (XmlSchemaValidationException e)
        {
            responses.Add(new XmlValidatorResult(e.Message, XmlSeverityType.Error, new XmlSchemaException(e.Message, e)));
        }

        return responses;
    }

    private static void LoadSchema(XmlSchemaSet schemas, string name, string schemaNamespace)
    {
        var xmlReaderSettings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Ignore };

        using var fs = new StringReader(Resource.Get(name));
        using var reader = XmlReader.Create(fs, xmlReaderSettings);
        schemas.Add(schemaNamespace, reader);
    }
}
}

