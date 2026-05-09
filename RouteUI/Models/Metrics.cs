using System.Runtime.InteropServices;

namespace RouteUI.Models
{
    /// <summary>
    /// C++ tarafındaki Metrics struct'ı ile bellekte birebir eşleşen yapı.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Metrics
    {
        // C++ long long (64-bit) -> C# long (64-bit)
        public long TimeMicroseconds;

        // C++ int (32-bit) -> C# int (32-bit)
        public int NodesExamined;

        // C++ int (32-bit) -> C# int (32-bit)
        public int PathLength;

        // C++ bool (1-byte) -> C# bool (1-byte)
        [MarshalAs(UnmanagedType.U1)]
        public bool RouteFound;
    }
}