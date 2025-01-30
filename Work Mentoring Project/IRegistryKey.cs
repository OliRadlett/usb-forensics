namespace Work_Mentoring_Project
{
    internal interface IRegistryKey
    {
        string Name { get; }
        string[] GetSubkeyNames();
        IRegistryKey OpenSubKey(string name);
        string GetValue(string valueName);
    }
}
