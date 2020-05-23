namespace CoreStore.Infra.Data.Common
{
    public class QueryCommon
    {
        public static int OffSet(int Page, int PageSize)
        {
            return (Page - 1) * PageSize;
        }

        public static string TotalItemsQuery(string query)
        {
            return $"SELECT COUNT(*) FROM ({query}) t";
        }

        public static string PagedQuery(string orderBy, string query, int offset, int pageSize)
        {
            return $"{query} ORDER BY {orderBy} ASC OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }
    }
}
