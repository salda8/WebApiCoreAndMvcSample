namespace WebApiNetCore.Models
{
    public class QueryParameters
    {
        private const int MaxPageCount = 50;
        private int pageCount = MaxPageCount;
        public string OrderBy { get; set; } = "Name";
        public int Page { get; set; } = 1;

        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = (value > MaxPageCount) ? MaxPageCount : value; }
        }

        public string Query { get; set; }
    }
}