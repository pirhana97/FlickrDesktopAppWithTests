using FlickrDesktopApp.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Unsplasharp;
using System;

namespace FlickrDesktopApp
{
    public class PhotoQueryService : IPhotoQueryService
    {
        public string PhotoSearchString { get; set; }
        public string ExceptionMessage { get; set; }
        private string AccessKey = "T7oOFF-VuXqwWVRD06TULiWXUHpDqthhdNgAJLDNDMM";
        private int PhotosPerPage = 12;
        private int page = 1;
        public ObservableCollection<ImageModel> Images { get; set; }

        public PhotoQueryService(string photosToSearch, ObservableCollection<ImageModel> images)
        {
            if (string.IsNullOrEmpty(photosToSearch))
            {
                throw new ArgumentNullException(nameof(PhotoSearchString));
            }

            PhotoSearchString = photosToSearch;
            Images = images;
        }

        async Task IPhotoQueryService.LoadInitialImages()
        {
            try
            {
                //Page is reset to 1 when new query is passed in the input
                page = 1;

                //Previous result is cleared when a new input query is added
                Images.Clear();
                await LoadImages();
            }

            catch(Exception ex)
            {
                ExceptionMessage = ex.Message;
            }
        }

        async Task IPhotoQueryService.LoadMoreImages()
        {
            try
            {
                page++;
                await LoadImages();
            }
            catch (Exception ex)
            {
                ExceptionMessage = ex.Message;
            }
        }

        private async Task LoadImages()
        {
            var client = new UnsplasharpClient(AccessKey);

            //var photoUrl = await client.SearchPhotos(photosToSearch); // Also works

            string url = string.Format("{0}?query={1}&page={2}&per_page={3}&client_id={4}", "https://api.unsplash.com/search/photos", PhotoSearchString, page, PhotosPerPage, AccessKey);
            var photoUrl = await client.FetchSearchPhotosList(url);

            for (int i = 0; i < PhotosPerPage; i++)
            {
                var imagePath = photoUrl[i].Urls.Regular;
                Images.Add(new ImageModel { ImageUrl = imagePath });
            }
        }

        public void Dispose()
        {
            Images.Clear();
            PhotoSearchString = "";

        }
    }
}
