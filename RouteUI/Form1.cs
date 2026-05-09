using System;
using System.Windows.Forms;
using System.IO; // Klasör yollarını kontrol etmek için

namespace RouteUI
{
    public partial class Form1 : Form
    {
        private IntPtr _nativeGraph = IntPtr.Zero;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                // Motoru oluşturmayı dene
                _nativeGraph = EngineBridge.CreateGraph(10, 10);

                if (_nativeGraph != IntPtr.Zero)
                {
                    MessageBox.Show("C++ Motoru Başarıyla Bağlandı!\nHarita nesnesi oluşturuldu.",
                                    "Bağlantı Testi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Motor bağlantısı kuruldu ancak harita nesnesi oluşturulamadı.",
                                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (DllNotFoundException)
            {
                // DLL bulunamadığında tam olarak nereye bakıyor?
                string currentDir = AppDomain.CurrentDomain.BaseDirectory;

                MessageBox.Show($"Hata: DLL dosyası bulunamadı!\n\n" +
                                $"Aranan Klasör: {currentDir}\n\n" +
                                $"Aranan Dosya: libRouteEngine.dll\n\n" +
                                $"Lütfen DLL dosyasını yukarıdaki klasöre kopyalayıp tekrar deneyin.",
                                "DLL Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmedik bir hata oluştu:\n{ex.Message}\n\n" +
                                $"Detay: {ex.GetType().Name}",
                                "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_nativeGraph != IntPtr.Zero)
            {
                EngineBridge.DeleteGraph(_nativeGraph);
                _nativeGraph = IntPtr.Zero;
            }
            base.OnFormClosing(e);
        }
    }
}