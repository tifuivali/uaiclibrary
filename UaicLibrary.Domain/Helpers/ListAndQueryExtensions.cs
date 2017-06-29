using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UaicLibrary.Domain.BookManagement;
using UaicLibrary.Domain.GeneralModels;

namespace UaicLibrary.Domain.Helpers
{
    public  static class ListAndQueryExtensions
    {
        public static IList<SelectItemModel> GetSelectedItems(this IList<SelectItemModel> selectItems)
        {
            return selectItems.Where(x => x.IsSelected).ToList();
        }

        public static IList<int> GetSelectedItemsIds(this IList<SelectItemModel> selectItems)
        {
            var selectedItems = selectItems.GetSelectedItems();
            return selectedItems.Select(x => x.Id).ToList();
        }

    }
}
