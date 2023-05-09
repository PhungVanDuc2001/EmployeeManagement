namespace EmployeeManagement.API.Controllers
{
    internal class PagingResult
    {
        internal int TotalRecords;

        public List<object> Data { get; set; }
    }
}