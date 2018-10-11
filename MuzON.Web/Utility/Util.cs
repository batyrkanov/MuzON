using AutoMapper;
using MuzON.BLL.DTO;
using System;
using System.Collections.Generic;
using System.IO;
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
            else if (uploadImage == null && existingImage == null)
            {
                var path = "~/Content/images/nophoto.jpg";
                var defaultPhoto = System.IO.File.ReadAllBytes(HttpContext.Current.Server.MapPath(path));
                image = defaultPhoto;
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
            if (service is IEnumerable<ArtistDTO>)
            {
                return new MultiSelectList(dataList, "Id", "FullName", selectedItems);
            }
            return new MultiSelectList(dataList, "Id", "Name", selectedItems);
        }

        public MultiSelectList GetMultiSelectListArtists<S, D>(IEnumerable<S> service, List<Guid> selectedItems = null)
        {
            var DTOs = service;
            IEnumerable<D> dataList = Mapper.Map<IEnumerable<D>>(DTOs);

            return new MultiSelectList(dataList, "Id", "FullName", selectedItems);

        }

        public List<string> GetErrorList(ICollection<ModelState> values)
        {
            var errorList = new List<string>();
            foreach (var value in values)
            {
                if (value.Errors != null)
                {
                    foreach (var item in value.Errors)
                    {
                        errorList.Add(item.ErrorMessage);
                    }
                }
            }
            return errorList;
        }
    }
}