using FlickrDesktopApp.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Unsplasharp;

namespace FlickrDesktopApp
{
    /// <summary>   
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<ImageModel> Images { get; set; }

        public MainWindow()
        {
            //Components from xaml are initalized
            InitializeComponent();
            Images = new ObservableCollection<ImageModel>();
        }

        private async void photoSearch_Click(object sender, RoutedEventArgs e)
        {
            //Get the input query from the textbox
            var photoSearchQuery = inputQuery.Text;

            //LoadInitialImages method is invoke when photoSearch_Click event is subscribed 
            IPhotoQueryService photoQueryService = new PhotoQueryService(photoSearchQuery, Images);
            await photoQueryService.LoadInitialImages();

            //Populate the ObservableCollections to load images from page=1
            photoListView.ItemsSource = Images;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Get the source of the image
            var imageSource = ((Image)sender).Source;

            // Create and show the new window
            var imageWindow = new ImageWindow(imageSource);
            imageWindow.Show();
        }

        private async void loadMorePhotos_Click(object sender, RoutedEventArgs e)
        {
            //Get the input query from the textbox
            var photoSearchQuery = inputQuery.Text;

            // Load more images when the end of the scroll is reached
            IPhotoQueryService photoQueryService = new PhotoQueryService(photoSearchQuery, Images);
            await photoQueryService.LoadMoreImages();

            //Populate the ObservableCollections to load more images after incrementing pages
            photoListView.ItemsSource = Images;
        }
    }
}
