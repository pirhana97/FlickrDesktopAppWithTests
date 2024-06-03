
   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoQueryService
{
    interface IPhotoQueryService
    {
        void LoadInitialImages();

        void LoadMoreImages();
    }
}
