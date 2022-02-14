using System.Linq;
using System.Xml;
using System.Xml.Schema;
using Eplicta.Mets.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Eplicta.Mets.Tests
{
    public class XmlValidatorTests
{
    [Fact]
    public void Sample()
    {
        //Arrange
        var document = Resource.GetXml("sample.xml");
        var schema = Resource.GetXml("sample.xsd");
        var sut = new XmlValidator();

        //Act
        var result = sut.Validate(document, schema);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void A()
    {
        //Arrange
        var document = Resource.GetXml("a.xml");
        var schema = Resource.GetXml("a.xsd");
        var sut = new XmlValidator();

        //Act
        var result = sut.Validate(document, schema);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void mods_version_3_5()
    {
        //Arrange
        var document = Resource.GetXml("mods99042030_linkedDataAdded.xml");
        var schema = Mets.Helpers.Resource.GetXml("mods-3-5.xsd");
        var sut = new XmlValidator();

        //Act
        var result = sut.Validate(document, schema);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void MODS_enligt_FGS_PUBL_exempel_1()
    {
        //Arrange
        var document = Resource.GetXml("MODS_enligt_FGS-PUBL_exempel_1.xml");
        var schema = Mets.Helpers.Resource.GetXml("MODS_enligt_FGS-PUBL_xml1_0.xsd");
        var sut = new XmlValidator();

        //Act
        var result = sut.Validate(document, schema);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void MODS_enligt_FGS_PUBL_exempel_2()
    {
        //Arrange
        var document = Resource.GetXml("MODS_enligt_FGS-PUBL_exempel_2.xml");
        var schema = Mets.Helpers.Resource.GetXml("MODS_enligt_FGS-PUBL_xml1_0.xsd");
        var sut = new XmlValidator();

        //Act
        var result = sut.Validate(document, schema);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Invalid()
    {
        //Arrange
        var document = new XmlDocument();
        var root = document.CreateElement("root");
        document.AppendChild(root);
        root.SetAttribute("xmlns", "http://www.contoso.com/books");
        var child1 = document.CreateElement("Child");
        root.AppendChild(child1);
        child1.SetAttribute("A", "B");

        var schema = Resource.GetXml("sample.xsd");

        var sut = new XmlValidator();

        //Act
        var result = sut.Validate(document, schema).ToArray();

        //Assert
        result.Should().NotBeEmpty();
        result.Should().HaveCount(1);
        result.First().Message.Should().Be("The 'http://www.contoso.com/books:root' element is not declared.");
        result.First().XmlSeverityType.Should().Be(XmlSeverityType.Error);
        result.First().XmlSchemaException.Should().NotBeNull();
    }
}
}

