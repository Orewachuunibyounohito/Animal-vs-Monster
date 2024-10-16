using System.Collections.Generic;

public interface ILibrary
{
    public dynamic GetData(string name);
    public void    AddData(string name, dynamic data);
}
