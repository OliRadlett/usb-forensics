namespace Work_Mentoring_Project
{
    internal interface IRegistryKey
    {
        string Name { get; }
        string[] GetSubKeyNames();
        IRegistryKey OpenSubKey(string name);
        string GetValue(string valueName);
    }
}
