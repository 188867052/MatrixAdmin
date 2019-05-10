namespace Core.Model
{
    public class Pager
    {
        private static readonly int defaultPageSize = 10;
        private static readonly int defaultPageIndex = 1;

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
                PageSize = Pager.defaultPageSize,
                PageIndex = Pager.defaultPageIndex,
            };

            return pager;
        }
    }
}