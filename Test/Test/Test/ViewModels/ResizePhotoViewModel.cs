using Xamarin.Forms;

namespace Test.ViewModels
{
    class ResizePhotoViewModel : BaseViewModel
    {
        public Command CancelCommand { get; }
        public Command ChooseCommand { get; }

        private ImageSource image;
        public ImageSource Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public ResizePhotoViewModel(ImageSource imageSource)
        {
            Image = imageSource;
            CancelCommand = new Command(Cancel);
            ChooseCommand = new Command(Choose);
        }

        private void Cancel()
        {
            Shell.Current.Navigation.PopModalAsync();
        }

        private void Choose()
        {

        }
    }
}