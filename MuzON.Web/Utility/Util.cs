using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MuzON.Web.Utility
{
    public class Util
    {
        public byte[] SetImage(HttpPostedFileBase uploadImage, byte[] image, string existingImage = null)
        {
            if (uploadImage != null)
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    image = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
            }
            else
                image = Convert.FromBase64String(existingImage);
            return image;
        }
        
    }
}