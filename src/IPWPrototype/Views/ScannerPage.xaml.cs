using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace IPWPrototype.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ContentPage
    {
        public ScannerPage()
        {
            InitializeComponent();
        }

        void Handle_Pressed(object sender, System.EventArgs e)
        {
            var opts = new ZXing.Mobile.MobileBarcodeScanningOptions
            {
                PossibleFormats = new List<ZXing.BarcodeFormat> { ZXing.BarcodeFormat.QR_CODE }
            };

            var scanPage = new ZXingScannerPage(opts);
            scanPage.OnScanResult += (result) => {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(() => {
                    var format = result?.BarcodeFormat.ToString() ?? string.Empty;
                    var value = result?.Text ?? string.Empty;

                    Navigation.PopAsync();
                    Navigation.PushModalAsync(new NavigationPage(new NewItemPage(value)));
                });
            };

            Navigation.PushAsync(scanPage);
        }
    }
}