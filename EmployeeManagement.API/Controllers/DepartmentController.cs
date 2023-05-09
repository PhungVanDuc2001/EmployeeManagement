using EmployeeManagement.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        /// <summary>
        /// Api lấy danh sách phòng ban 
        /// </summary>
        /// <param name="keyword">Khóa chính </param>
        /// <param name="linit">Số bản ghi muốn lấy </param>
        /// <param name="offset">Số bản ghi bắt đầu lấy </param>
        /// <returns>Trả về 1 đối tượng PagingResylt, bao gồm danh sách phòng ban trên 1 trang và tổng số bản ghi thỏa mãn đièu kiện</returns>
        [HttpGet]
        public IActionResult GetPaging(
            [FromQuery]string? keyword,
            [FromQuery]int linit =10,
            [FromQuery]int offset = 0)
        {
            return Ok(new PagingResult
            {
                Data = new List<object>
                {
                    new Department 
                    {
                        Id = Guid.NewGuid(),
                        Code = "PB001",
                        Name = "Phòng tổng giám đốc "
                    },
                    new Department
                    {
                        Id = Guid.NewGuid(),
                        Code = "PB002",
                        Name = "Phòng kế toán "
                    },
                    new Department
                    {
                        Id = Guid.NewGuid(),
                        Code = "PB003",
                        Name = "Phòng marketing  "
                    }
                },
                TotalRecords = 3
            });
        }
    }
}
