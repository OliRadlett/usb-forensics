using Microsoft.Win32;
using System.Drawing;
using System.Runtime.Versioning;
using Pastel;
using Work_Mentoring_Project;

namespace USBForensics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UsbEnumerationScanner scanner = new UsbEnumerationScanner();
        }
    }
}