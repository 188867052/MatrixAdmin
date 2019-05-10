namespace Core.Model
{
    public class Pager
    {
        public static int DefaultPageSize = 10;
        public static int DefaultPageIndex = 1;

        public static Pager CreateDefaultInstance()
        {
            Pager pager = new Pager
            {
                PageSize = Pager.DefaultPageSize,
                PageIndex = Pager.DefaultPageIndex,
            };

            return pager;
        }

        /// <summary>
        /// 分页大小.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码.
        /// </summary>
        public int PageIndex { get; set; }

        public int TotalCount { get; set; }
    }
}