using EmployeeManagement.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionController : ControllerBase
    {
        /// <summary>
        /// Api Lấy Danh Sách Vị Trí
        /// </summary>
        /// <param name="keyword"> Khóa Chính </param>
        /// <param name="linit">Số Bản Ghi Muốn Lấy </param>
        /// <param name="offset">Số Bản Ghi Bắt Đầu Lấy </param>
        /// <returns>
        /// Trả về 1 đối tượng PagingResylt, bao gồm danh sách vị trí trên 1 trang và tổng số bản ghi thỏa mãn đièu kiện
        /// </returns>
        [HttpGet]
        public IActionResult GetPaging (
           [FromQuery]string ? keyword,
           [FromQuery]int linit =10,
           [FromQuery]int offset =0 )
        {
            return Ok(new PagingResult
            {
                Data = new List<object>
                {
                    new JobPosition
                    {
                        Id = Guid.NewGuid(),
                        Code = "VT001",
                        Name = "Tổng giám đốc "
                    },
                    new JobPosition
                    {
                        Id = Guid.NewGuid(),
                        Code = "VT002",
                        Name = "Kế toán "
                    },
                    new JobPosition
                    {
                        Id = Guid.NewGuid(),
                        Code = "VT003",
                        Name = "Bảo vệ "
                    }
                },
                TotalRecords =3 
            });
        }
    }
}
