using System.Globalization;
using System.Reflection;
using Xunit.Sdk;

namespace UsbForensicsTests;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class FileDataAttribute : DataAttribute
{
    public FileDataAttribute(Type @dataClass, string filePath, string expectedValuePath)
    {
        DataClass = dataClass;
        DataPath = filePath;
        ExpectedValuePath = expectedValuePath;
    }

    public Type DataClass { get; }
    public string DataPath { get; }
    public string ExpectedValuePath{ get; }


    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        if (Activator.CreateInstance(DataClass, DataPath, ExpectedValuePath) is not IEnumerable<object[]> data)
        {
            throw new ArgumentException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "{0} must implement IEnumerable<object[]> to be used as ClassData for the test method named '{1}' on {2}",
                    DataClass.FullName,
                    testMethod.Name,
                    testMethod.DeclaringType?.FullName
                )
            );
        }

        return data;
    }
}