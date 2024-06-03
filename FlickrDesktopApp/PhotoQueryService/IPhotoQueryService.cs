using System.Threading.Tasks;

namespace FlickrDesktopApp
{
    public interface IPhotoQueryService
    {
        Task LoadInitialImages();

        Task LoadMoreImages();

        void Dispose();
    }
}
