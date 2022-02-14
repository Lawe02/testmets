using System.IO;
using System.Text;
using System.Threading.Tasks;
using Eplicta.Mets.Entities;

namespace Eplicta.Mets.Console.Commands
{


public class CreateConsoleCommand : Tharga.Toolkit.Console.Commands.Base.AsyncActionCommandBase
{
    public CreateConsoleCommand()
        : base("Create")
    {
    }

    public override async Task InvokeAsync(string[] param)
    {
        //MRSS Example:
        //<item>
        //    <title>Moln- och virtualiseringspecialist</title>
        //    <link>https://www.alingsas.se/utbildning-och-barnomsorg/vuxenutbildning/jag-vill-studera/program-i-alingsas/moln-och-virtualiseringspecialist/</link>
        //    <pubDate>Thu, 03 Feb 2022 15:48:04 +0100</pubDate>
        //    <guid>C5385FBC5FC559E7C43AB6700DB28EF3</guid>
        //    <dcterms:creator>Alingsås Kommun</dcterms:creator>
        //    <dcterms:accessRights>gratis</dcterms:accessRights>
        //    <dcterms:publisher>http://id.kb.se/organisations/SE2120001553</dcterms:publisher>
        //    <media:content url="https://www.alingsas.se/wp-content/uploads/2020/06/Moln-och-virtualiseringsspecialist.pdf" type="application/pdf"/>
        //    <dcterms:format>text/html</dcterms:format>
        //</item>

        //MODS Example (metadata.xml):
        //<?xml version="1.0" encoding="UTF-8"?>
        //<mods xmlns:xlink="http://www.w3.org/1999/xlink" version="3.5" xmlns="http://www.loc.gov/mods/v3" xsi:schemaLocation="http://www.loc.gov/mods/v3 http://www.loc.gov/standards/mods/v3/mods-3-5.xsd" xml:lang="SE" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
        //    <titleInfo extraInfo="asdasd">
        //        <title>Moln- och virtualiseringspecialist</title>
        //    </titleInfo>
        //    <name type="personal" authorityURI="http://id.loc.gov/authorities/names" valueURI="http://id.loc.gov/authorities/names/n92101908" />
        //    <typeOfResource>text</typeOfResource>
        //    <genre authority="marcgt">bibliography</genre>
        //    <originInfo eventType="publication">
        //        <place>
        //	        <placeTerm authority="marccountry" type="code" authorityURI="http://id.loc.gov/vocabulary/countries" valueURI="http://id.loc.gov/vocabulary/countries/nyu">nyu</placeTerm>
        //        </place>
        //        <place>
        //	        <placeTerm type="text">Ithaca, N.Y</placeTerm>
        //        </place>
        //        <publisher>Cornell University Press</publisher>
        //        <dateIssued>c1999</dateIssued>
        //        <dateIssued encoding="marc">1999</dateIssued>
        //        <issuance>monographic</issuance>
        //    </originInfo>
        //</mods>

        //NOTE: Download this file and save it in 'C:\temp' bo test the code. https://www.alingsas.se/wp-content/uploads/2020/06/Moln-och-virtualiseringsspecialist.pdf
        //var fileData = File.ReadAllBytes("C:\\temp\\Moln-och-virtualiseringsspecialist.pdf");

        var metsData = new ModsData
        {
            Name = new ModsData.NameData
            {
                NamePart = null,
            },
            TitleInfo = new ModsData.TitleInfoData
            {
                Title = "Moln- och virtualiseringspecialist",
                SubTitle = null
            },
            Creator = "Alingsås Kommun",
            //Resources = new[]
            //{
            //    new ModsData.Resource
            //    {
            //        Name = "Moln-och-virtualiseringsspecialist.pdf",
            //        Data = fileData
            //    },
            //}
        };

        var renderer = new Renderer(metsData);

        //NOTE: This code saves the metadata to the temp-folder.
        var xmlData = renderer.Render().OuterXml;
        await File.WriteAllBytesAsync("C:\\temp\\metadata.xml", Encoding.UTF8.GetBytes(xmlData));

        //NOTE: This code craetes a zip-archive with metadata and resource-files amd saves to the temp-folder.
        await using var archive = renderer.GetArchiveStream();
        await File.WriteAllBytesAsync("C:\\temp\\mods-archive.zip", archive.ToArray());

        OutputInformation("Done");
    }
}
    }