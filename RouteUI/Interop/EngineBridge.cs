using System;
using System.Runtime.InteropServices;
using RouteUI.Models;

namespace RouteUI.Interop
{
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