namespace Common.Helpers
{
    public class Pagination
    {
        public SortDirectionEnum SortDirection { get; set; }
        public string SortBy { get; set; }
        public int TotalRecords { get; set; }
        public int NumberOfPages { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public string DefaultSortBy { get; set; }

        public string ReloadUrl { get; set; }
        public string TargetDiv { get; set; }

        public Pagination()
        {

        }

        public Pagination(string reloadUrl, string targetDiv, int totalRecords, int numberOfPages)
        {
            ReloadUrl = reloadUrl;
            TargetDiv = targetDiv;
            PageSize = 10;
            PageNumber = 1;
        }
    }

    public enum SortDirectionEnum
    {
        Ascending = 1,
        Descending = 2
    }
}