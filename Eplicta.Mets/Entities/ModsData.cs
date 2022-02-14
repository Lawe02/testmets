using Eplicta.Mets.Helpers;

namespace Eplicta.Mets.Entities
{

public record ModsData //: MetsData
{
    public TitleInfoData TitleInfo { get; set; }
    public NameData Name { get; set; }
    public Resource[] Resources { get; set; }
    public string Creator { get; set; }

    public record TitleInfoData
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
    }

    public record NameData
    {
        public string NamePart { get; set; }
    }

    public record Resource
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
    }