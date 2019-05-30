namespace Core.Model
{
    public class Pager
    {
        private const int DefaultPageSize = 10;
        private const int DefaultPageIndex = 1;

        /// <summary>
        /// 分页大小.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码.
        /// </summary>
        public int PageIndex { get; set; }

        public int TotalCount { get; set; }

        public static Pager CreateDefaultInstance()
        {
            Pager pager = new Pager
            {
                PageSize = Pager.DefaultPageSize,
                PageIndex = Pager.DefaultPageIndex,
            };

            return pager;
        }
    }
}