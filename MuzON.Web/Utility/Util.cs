using AutoMapper;
using MuzON.BLL.DTO;
using MuzON.Web.Models;
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
            else if(uploadImage == null && existingImage == null)
            {
                image = null;
            }
            else
                image = Convert.FromBase64String(existingImage);
            return image;
        }

        // generic method with mapping from S - Source to D - Destination
        public SelectList GetSelectListItems<S, D>(IEnumerable<S> service, Guid? selectedItem = null)
        {
            var DTOs = service;
            IEnumerable<D> dataList = Mapper.Map<IEnumerable<D>>(DTOs);
            return new SelectList(dataList, "Id", "Name", selectedItem);
        }

        // generic method with mapping from S - Source to D - Destination
        public MultiSelectList GetMultiSelectListItems<S, D>(IEnumerable<S> service, List<Guid> selectedItems = null)
        {
            var DTOs = service;
            IEnumerable<D> dataList = Mapper.Map<IEnumerable<D>>(DTOs);
            if(service is IEnumerable<ArtistDTO>)
            {
                return new MultiSelectList(dataList, "Id", "FullName", selectedItems);
            }
            return new MultiSelectList(dataList, "Id", "Name", selectedItems);
        }
    }
}