using System;
using System.IO;
using Test.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResizePhotoPage : ContentPage
    {
        public ResizePhotoPage(ImageSource imageSource)
        {
            InitializeComponent();
            BindingContext = new ResizePhotoViewModel(imageSource);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            frame.OnDrawBitmap.Invoke(frame, new EventArgs());
            frame.OnGetImageBytes?.Invoke("get byte", (args) =>
            {

                App.photo = ImageSource.FromStream(() => new MemoryStream(args));
                App.Current.MainPage.Navigation.PopModalAsync();
            });
        }
    }
}