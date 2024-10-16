public class FactoryMethod_Hamburger
{
    public Hamburger Hamburger() => new Hamburger();
    public BigMac BigMac() => new BigMac();
    public McChicken McChicken() => new McChicken();
    public FiletOFish FiletOFish() => new FiletOFish();
}

public class Hamburger
{
    private string _info;
    public string Info() => _info;

    public Hamburger(string bread = "滿意麵包", string meat = "牛肉排")
        => _info = string.Format(LocalizationConfig.StringTemplates[LocalizedEntry.Info], bread, meat);
}

public class BigMac
{
    private string _info;
    public string Info() => _info;

    public BigMac(){
        _info = new Hamburger("大麥克麵包", "雙層純牛肉").Info();
    }
}

public class McChicken
{
    private string _info;
    public string Info() => _info;

    public McChicken(){
        _info = new Hamburger("滿意麵包", "香雞排").Info();
    }
}

public class FiletOFish
{
    private string _info;
    public string Info() => _info;

    public FiletOFish(){
        _info = new Hamburger("漢堡麵包", "香魚排").Info();
    }
}
