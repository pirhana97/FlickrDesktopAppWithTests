using FlickrDesktopApp;
using FlickrDesktopApp.Model;
using Moq;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

namespace FlickrDesktopApp_uTests
{
    public class PhotoQueryServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PhotoQueryService_InvokeConstructor_PropertiesArePlacedAsExpected()
        {
            //Arrange
            string photosToSearch = "dog";
            Mock<ObservableCollection<ImageModel>> mockImageModelObservableCollection = new Mock<ObservableCollection<ImageModel>>();

            //Act
            PhotoQueryService sut = new PhotoQueryService(photosToSearch, mockImageModelObservableCollection.Object);

            //Assert
            Assert.AreEqual(photosToSearch, sut.PhotoSearchString,"The expected query is not equal to the actual photoSearchString.");
            Assert.AreEqual(mockImageModelObservableCollection.Object, sut.Images, "The expected Images are not equal to the actual ObservableCollection of ImageModel.");

        }

        [Test]
        public void PhotoQueryService_LoadInitialImages_ObservableCollectionImagesAreSet()
        {
            //Arrange
            string photosToSearch = "cat";
            Mock<ObservableCollection<ImageModel>> mockImageModelObservableCollection = new Mock<ObservableCollection<ImageModel>>();
            IPhotoQueryService sut = new PhotoQueryService(photosToSearch, mockImageModelObservableCollection.Object);

            //Act
            var initialImageLoadTask = sut.LoadInitialImages();

            //Assert
            initialImageLoadTask.ContinueWith(task =>
            {
                Assert.IsNotNull(task,"The task is null.");
                Assert.AreEqual(task.IsCompleted, TaskStatus.RanToCompletion,"The task didn't run to completion.");
            });

            //Dispose
            sut.Dispose();
        }

        [Test]
        public void PhotoQueryService_LoadMoreImages_ObservableCollectionImagesAreSet()
        {
            //Arrange
            string photosToSearch = "morning";
            Mock<ObservableCollection<ImageModel>> mockImageModelObservableCollection = new Mock<ObservableCollection<ImageModel>>();
            IPhotoQueryService sut = new PhotoQueryService(photosToSearch, mockImageModelObservableCollection.Object);

            //Act
            var loadMoreImageTask = sut.LoadMoreImages();

            //Assert
            loadMoreImageTask.ContinueWith(task =>
            {
                Assert.IsNotNull(task,"The task is null.");
                Assert.AreEqual(task.IsCompleted, TaskStatus.RanToCompletion, "The task didn't run to completion.");
            });

            //Dispose
            sut.Dispose();
        }

        [Test]
        public void PhotoQueryService_LoadInitialImages_DoesNotThrowException()
        {
            //Arrange
            string photosToSearch = "cat";
            Mock<ObservableCollection<ImageModel>> mockImageModelObservableCollection = new Mock<ObservableCollection<ImageModel>>();
            IPhotoQueryService sut = new PhotoQueryService(photosToSearch, mockImageModelObservableCollection.Object);

            //Act
            Assert.DoesNotThrow(() => sut.LoadInitialImages(), "LoadInitialImages throws exception");

            //Dispose
            sut.Dispose();
        }

        [Test]
        public void PhotoQueryService_LoadMoreImages_DoesNotThrowException()
        {
            //Arrange
            string photosToSearch = "morning";
            Mock<ObservableCollection<ImageModel>> mockImageModelObservableCollection = new Mock<ObservableCollection<ImageModel>>();
            IPhotoQueryService sut = new PhotoQueryService(photosToSearch, mockImageModelObservableCollection.Object);

            //Act
            Assert.DoesNotThrow(() => sut.LoadMoreImages(), "LoadMoreImages throws exception");

            //Dispose
            sut.Dispose();
        }

        [Test]
        public void PhotoQueryService_QueryIsEmpty_ArgumentNullExceptionIsThrown()
        {
            //Arrange
            string photosToSearch = "";
            Mock<ObservableCollection<ImageModel>> mockImageModelObservableCollection = new Mock<ObservableCollection<ImageModel>>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => new PhotoQueryService(photosToSearch, mockImageModelObservableCollection.Object), "PhotoQueryService does not throw exception");

        }

        [Test]
        public void PhotoQueryService_ImageModelIsNull_ArgumentNullExceptionIsThrown()
        {
            //Arrange
            string photosToSearch = "night";
            Mock<ObservableCollection<ImageModel>> mockImageModelObservableCollection = null;

            //Act & Assert
            Assert.Throws<NullReferenceException>(() => new PhotoQueryService(photosToSearch, mockImageModelObservableCollection.Object), "PhotoQueryService does not throw exception");

        }

        [Test]
        public void PhotoQueryService_Dispose_PropertiesAreNullOrEmpty()
        {
            //Arrange
            string photosToSearch = "morning";
            Mock<ObservableCollection<ImageModel>> mockImageModelObservableCollection = new Mock<ObservableCollection<ImageModel>>();
            PhotoQueryService sut = new PhotoQueryService(photosToSearch, mockImageModelObservableCollection.Object);

            //Act
            sut.Dispose();

            //Assert
            Assert.IsEmpty(sut.PhotoSearchString,"Search string is not empty after dispose.");
            Assert.IsEmpty(sut.Images, "Observatable Collection of ImageModel is not empty after dispose.");
        }
    }
}