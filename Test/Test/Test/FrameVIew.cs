using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrameVIew : ContentView
    {
        public delegate void GetImageBytesDelegate(string parameter, Action<byte[]> callback);
        public GetImageBytesDelegate OnGetImageBytes;

        public EventHandler OnDrawBitmap;
        public FrameVIew():base()
        {
        }

    }
}
