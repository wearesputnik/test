using Test.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using Xamarin.Forms;

namespace Test.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public Command TakePhotoCommand { get; }
        public Command ChoosePhotoCommand { get; }
        public Command EditCommand { get; }

        private bool isTakePhoto;
        public bool IsTakePhoto
        {
            get => isTakePhoto;
            set
            {
                isTakePhoto = value;
                OnPropertyChanged("IsTakePhoto");
            }
        }


        private ImageSource photo;
        public ImageSource Photo
        {
            get => photo;
            set
            {
                photo = value;
                OnPropertyChanged("Photo");
            }
        }

        public MainViewModel()
        {
            TakePhotoCommand = new Command(TakePhoto);
            ChoosePhotoCommand = new Command(ChoosePhoto);
            EditCommand = new Command(Edit);
        }

        private async void TakePhoto()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Sample",
                    Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                });

                if (file == null)
                    return;

                Resize(ImageSource.FromFile(file.Path));
            }
        }

        private async void ChoosePhoto()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                if (photo != null)
                {
                    Resize(ImageSource.FromFile(photo.Path));
                }
            }
        }

        private async void Edit()
        {
            Resize(Photo);
        }


        private async void Resize(ImageSource source)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new ResizePhotoPage(source));
        }
    }
}
