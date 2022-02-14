namespace Eplicta.Mets.Entities
{
    public class Version
{
    private Version(string key)
    {
        Key = key;
    }

    public static Version Mods_3_5 => new("mods-3-5");
    public static Version ModsFgsPubl_1_0 => new("MODS_enligt_FGS-PUBL_xml1_0");
    public static Version ModsFgsPubl_1_1 => new("MODS_enligt_FGS-PUBL_xml1_1");

    public string Key { get; }
}
}

