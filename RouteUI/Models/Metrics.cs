using System.Runtime.InteropServices;

namespace RouteUI.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Metrics
    {
        public long TimeMicroseconds;
        public int NodesExamined;
        public int PathLength;

        [MarshalAs(UnmanagedType.U1)]
        public bool RouteFound;
    }
}