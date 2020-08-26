using System;
using System.IO;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Test.FrameVIew), typeof(Test.Droid.FrameVIew))]
namespace Test.Droid
{
    public class FrameVIew : ViewRenderer<Test.FrameVIew, Android.Views.View>
    {
        byte[] bytes;
        public EventHandler OnDrawBitmap;
        public FrameVIew(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Test.FrameVIew> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Test.FrameVIew view = (Test.FrameVIew)e.NewElement;
                view.OnGetImageBytes += Element_OnDoing;
                e.NewElement.OnDrawBitmap += NewElement_OnDrawBitmap;
            }
        }
        void Element_OnDoing(string parameter, Action<byte[]> action)
        {
            action(bytes);
        }

        private void NewElement_OnDrawBitmap(object sender, EventArgs e)
        {
            if (this.ViewGroup != null)
            {
                //get the subview
                Android.Views.View subView = ViewGroup.GetChildAt(0);
                int width = subView.Width;
                int height = subView.Height;

                //create and draw the bitmap
                Bitmap b = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
                Canvas c = new Canvas(b);
                ViewGroup.Draw(c);

                Bitmap bitmap = Bitmap.CreateBitmap(b, 0, 0, (98 * width) / 100, (82 * height) / 100);

                //save the bitmap to file
                bytes = SaveBitmapToFile(bitmap);
            }
        }

        private byte[] SaveBitmapToFile(Bitmap bm)
        {
            MemoryStream ms = new MemoryStream();
            bm.Compress(Bitmap.CompressFormat.Png, 100, ms);
            return ms.ToArray();
        }
    }
}