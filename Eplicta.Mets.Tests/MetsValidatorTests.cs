using AutoFixture;
using Eplicta.Mets.Entities;
using FluentAssertions;
using Xunit;

namespace Eplicta.Mets.Tests
{
    public class MetsValidatorTests
{
    [Fact]
    public void Empty()
    {
        //Arrange
        var modsData = new ModsData();
        var renderer = new Renderer(modsData);
        var document = renderer.Render();
        var sut = new MetsValidator();

        //Act
        var result = sut.Validate(document, Version.Mods_3_5);

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Complete()
    {
        //Arrange
        var fixture = new Fixture();
        var modsData = fixture.Build<ModsData>().Create();
        var renderer = new Renderer(modsData);
        var document = renderer.Render();
        var sut = new MetsValidator();

        //Act
        var result = sut.Validate(document, Version.Mods_3_5);

        document.Save(@"C:\Temp\x.xml");

        //Assert
        result.Should().BeEmpty();
    }
}
}

