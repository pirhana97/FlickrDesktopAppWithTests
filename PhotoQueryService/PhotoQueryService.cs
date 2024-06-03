using FlickrDesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unsplasharp;

namespace PhotoQueryService
{
    public class PhotoQueryService : IPhotoQueryService
    {
        private string PhotoSearchString;
        private string AccessKey = "T7oOFF-VuXqwWVRD06TULiWXUHpDqthhdNgAJLDNDMM";
        private int PhotosPerPage = 12;
        private int page = 1;
        ObservableCollection<ImageModel> Images;

        public PhotoQueryService(string photosToSearch, ObservableCollection<ImageModel> images)
        {
            PhotoSearchString = photosToSearch;
            Images = images;
        }
        public async void LoadInitialImages()
        {
            //Page is reset to 1 when new query is passed in the input
            page = 1;

            //Previous result is cleared when a new input query is added
            Images.Clear();
            await LoadImages();
        }

        public async void LoadMoreImages()
        {
            page++;
            await LoadImages();
        }

        private async Task LoadImages()
        {
            //var photosToSearch = inputQuery.Text;
            var client = new UnsplasharpClient(AccessKey);

            //var photoUrl = await client.SearchPhotos(photosToSearch); // Also works

            string url = string.Format("{0}?query={1}&page={2}&per_page={3}&client_id={4}", "https://api.unsplash.com/search/photos", PhotoSearchString, page, PhotosPerPage, AccessKey);
            var photoUrl = await client.FetchSearchPhotosList(url);

            for (int i = 0; i < PhotosPerPage; i++)
            {
                var imagePath = photoUrl[i].Urls.Regular;
                Images.Add(new ImageModel { ImageUrl = imagePath });
            }

            //photoListView.ItemsSource = Images;
        }
    }
}
