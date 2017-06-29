using System;
using System.Collections.Generic;
using UaicLibrary.Domain.GeneralModels;

namespace UaicLibrary.Domain.BookManagement
{
    public class BookAdministrationModel
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public IList<SelectItemModel> BookAuthors { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public int BookCategoryId { get; set; }
        public int FacultyId { get; set; }
        public string BookUrl { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }
        public string BaseImagePath { get; set; }

    }
}
