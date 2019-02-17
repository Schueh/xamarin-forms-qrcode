namespace IPWPrototype.Windows
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            ZXing.Net.Mobile.Forms.WindowsUniversal.ZXingScannerViewRenderer.Init();

            LoadApplication(new IPWPrototype.App());
        }
    }
}
