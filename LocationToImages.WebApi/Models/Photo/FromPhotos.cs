using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationToImages.WebApi.Models.Photo
{
    public class FromPhotos
    {
        public IEnumerable<Photo> Photos { get; set; }
    }
}