using EmployeeManagement.API.Enums;

namespace EmployeeManagement.API.Entities.DTO
{
    public class ErrorResult
    {
        public ErrorCode ErrorCode { get; set; }

        public string DevMsg { get; set; }

        public string UserMsg { get; set; }

        public object MoreInfe { get; set; }

        public string TraceId { get; set; }
    }
}
