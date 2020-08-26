using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Test.FrameVIew), typeof(Test.iOS.FrameVIew))]
namespace Test.iOS
{
   public class FrameVIew : ViewRenderer<Test.FrameVIew, UIView>
    {
        byte[] bytes;
        public EventHandler OnDrawBitmap;

        protected override void OnElementChanged(ElementChangedEventArgs<Test.FrameVIew> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Test.FrameVIew view = (Test.FrameVIew)e.NewElement;
                view.OnGetImageBytes += GetImageBytes;
                e.NewElement.OnDrawBitmap += NewElement_OnDrawBitmap;
            }
        }

        void GetImageBytes(string parameter, Action<byte[]> action)
        {
            action(bytes);
        }

        private void NewElement_OnDrawBitmap(object sender, EventArgs e)
        {
            UIGraphics.BeginImageContextWithOptions(this.Bounds.Size, true, 0);
            this.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            var img = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            using (NSData imageData = img.AsPNG())
            {
                bytes = new Byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
            }
        }
    }
}