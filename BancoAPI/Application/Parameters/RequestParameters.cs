namespace Application.Parameters
{
    public class RequestParameters
    {
        public RequestParameters(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;

            PageSize = pageSize > 10 ? pageSize : 10;
        }
        public RequestParameters()
        {
            PageNumber = 1; PageSize = 10;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
