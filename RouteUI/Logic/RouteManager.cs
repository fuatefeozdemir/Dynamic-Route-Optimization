using System;
using System.Collections.Generic;
using RouteUI.Interop;
using RouteUI.Models;

namespace RouteUI.Logic
{
    public class RouteManager : IDisposable
    {
        private IntPtr _nativeGraph = IntPtr.Zero; // C++'taki GridGraph* adresi
        private bool _disposed = false;

        public RouteManager(int width, int height)
        {
            // C++ tarafında haritayı oluştur ve adresini sakla
            _nativeGraph = EngineBridge.CreateGraph(width, height);

            if (_nativeGraph == IntPtr.Zero)
                throw new Exception("C++ Motoru başlatılamadı!");
        }

        // Engel durumunu değiştirir
        public void ToggleObstacle(int id)
        {
            EngineBridge.ToggleObstacle(_nativeGraph, id);
        }

        // Komşuluk bağlantılarını günceller (C++ tarafında BuildConnections çağırır)
        public void BuildConnections()
        {
            EngineBridge.BuildConnections(_nativeGraph);
        }

        // En kısa yolu bulur ve C# listesi olarak döner
        public List<int> FindPath(int startId, int endId, int queueType, out Metrics metrics)
        {
            metrics = new Metrics();

            // C++'tan ham dizi adresini al
            IntPtr pathPtr = EngineBridge.FindPath(_nativeGraph, startId, endId, queueType, ref metrics);

            if (pathPtr == IntPtr.Zero || !metrics.RouteFound)
                return new List<int>();

            // C++'tan gelen ID dizisini C# array'ine kopyala
            int[] pathArray = new int[metrics.PathLength];
            System.Runtime.InteropServices.Marshal.Copy(pathPtr, pathArray, 0, metrics.PathLength);

            // C++ tarafında 'new int[]' ile açılan belleği temizle
            EngineBridge.DeletePath(pathPtr);

            return new List<int>(pathArray);
        }

        // --- IDisposable Uygulaması ---
        // Uygulama kapandığında C++ tarafındaki GridGraph'ı siler
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (_nativeGraph != IntPtr.Zero)
                {
                    EngineBridge.DeleteGraph(_nativeGraph);
                    _nativeGraph = IntPtr.Zero;
                }
                _disposed = true;
            }
        }

        ~RouteManager() => Dispose(false);
    }
}