using System;
using System.Runtime.InteropServices;

namespace RouteUI.Interop
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

    public static class EngineBridge
    {
        private const string DllName = "libRouteEngine.dll";

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateGraph(int width, int height);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeleteGraph(IntPtr graph);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ToggleObstacle(IntPtr graph, int id);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void BuildConnections(IntPtr graph);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr FindPath(IntPtr graph, int startId, int endId, int queueType, ref Metrics outMetrics);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DeletePath(IntPtr path);
    }
}