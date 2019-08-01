using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UEFIWriteCShasp
{
    class Program
    {
        static void Main()
        {
            string variableName = "CSHARP-UEFI";
            string GUID = "{E660597E-B94D-4209-9C80-1805B5D19B69}";
            
            int error = 0;
            bool pass = false;
            IntPtr bufferPRE = IntPtr.Zero;
            bool ret = pinvoke.SetPriv();

            System.Runtime.InteropServices.GCHandle pinnedArray = System.Runtime.InteropServices.GCHandle.Alloc(buf.buffer, System.Runtime.InteropServices.GCHandleType.Pinned);
            bufferPRE = pinnedArray.AddrOfPinnedObject();
            pass = pinvoke.SetFirmwareEnvironmentVariableEx(variableName, GUID, bufferPRE, (uint)buf.buffer.Length, (uint)pinvoke.dwAttributes.VARIABLE_ATTRIBUTE_NON_VOLATILE);

            if (!pass)
            {
                error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
            }
            pinnedArray.Free();
        }
    }
}
