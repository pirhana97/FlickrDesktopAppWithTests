using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FlickrDesktopApp
{
    /// <summary>   
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        //Empty Constructor
        public ImageWindow()
        {

        }

        //Depedency Injection in the parameterized constructor 
        public ImageWindow(ImageSource imageSource)
        {
            InitializeComponent();
            DisplayedImage.Source = imageSource;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            //Capture the mouse wheel event 
            base.OnMouseWheel(e);

            // Zoom in or out
            ZoomInAndOut(e);

        }

        private void ZoomInAndOut(MouseWheelEventArgs e)
        {
            // Zoom in or out
            if (e.Delta > 0)
            {
                var transform = (ScaleTransform)DisplayedImage.RenderTransform;
                transform.ScaleX *= 1.1;
                transform.ScaleY *= 1.1;
            }
            else if (e.Delta < 0)
            {
                var transform = (ScaleTransform)DisplayedImage.RenderTransform;
                transform.ScaleX /= 1.1;
                transform.ScaleY /= 1.1;
            }
        }

    }


}
