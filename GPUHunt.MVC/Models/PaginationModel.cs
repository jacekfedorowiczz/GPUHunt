namespace GPUHunt.MVC.Models
{
    public class PaginationModel<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemsCount { get; set; }
        public int PageNumber { get; set; }

        public PaginationModel(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalItemsCount = totalCount;
            PageNumber = pageNumber;
        }
    }
}
