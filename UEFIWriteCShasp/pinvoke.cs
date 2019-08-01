using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UEFIWriteCShasp
{
    class pinvoke
    {
        #region UEFI
        public enum dwAttributes : uint
        {
            VARIABLE_ATTRIBUTE_NON_VOLATILE = 0x00000001,
            VARIABLE_ATTRIBUTE_BOOTSERVICE_ACCESS = 0x00000002,
            VARIABLE_ATTRIBUTE_RUNTIME_ACCESS = 0x00000004,
        }

        /*
            BOOL WINBASEAPI SetFirmwareEnvironmentVariableEx(
            _In_ LPCTSTR lpName,
            _In_ LPCTSTR lpGuid,
            _In_ PVOID   pValue,
            _In_ DWORD   nSize,
            _In_ DWORD   dwAttributes
            );
        */
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetFirmwareEnvironmentVariableEx(String lpName, String lpGuid, IntPtr pValue, uint nSize, uint dwAttributes);
        #endregion

        #region tokens
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        // http://msdn.microsoft.com/en-us/library/bb530716(VS.85).aspx
        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_TIME_ZONE_NAMETEXT = "SeSystemEnvironmentPrivilege";

        public static bool SetPriv()
        {
            bool retVal;
            TokPriv1Luid tp;
            IntPtr hproc = GetCurrentProcess();
            IntPtr htok = IntPtr.Zero;
            retVal = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);
            tp.Count = 1;
            tp.Luid = 0;
            tp.Attr = SE_PRIVILEGE_ENABLED;
            retVal = LookupPrivilegeValue(null, SE_TIME_ZONE_NAMETEXT, ref tp.Luid);
            retVal = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);
            return retVal;
        }
        #endregion
    }
}
