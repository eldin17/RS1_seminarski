using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RS1seminarski.Helpers
{
    public class Pagination<T>
    {
        public List<T> PageData { get; set; } = new List<T>();
        public int Page { get; set; } = 0;
        public int TotalPages { get; set; } = 0;
        public int ItemsPerPage { get; set; } = 0;
        public int TotalItemsAllPages { get; set; } = 0;


        public Pagination(List<T> pageData, int totalItemsCount, int page, int itemsPerPage)
        {
            PageData.AddRange(pageData);
            Page = page;
            TotalPages = (int)Math.Ceiling(totalItemsCount / (double)itemsPerPage);
            ItemsPerPage = itemsPerPage;
            TotalItemsAllPages = totalItemsCount;
        }


        public static Pagination<T> Paginate(List<T> source,int page, int size)
        {
            if (source == null)
                return null;
            var pageItems = source
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();            

            var totalItemsCount = source.Count();

            var obj = new Pagination<T>(pageItems, totalItemsCount, page, size);

            return obj;
        }
    }
}
