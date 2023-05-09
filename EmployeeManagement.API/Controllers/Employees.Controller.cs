using Dapper;
using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Runtime.Intrinsics;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Các api liên quan đến nhân viên 
    /// </summary>
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// api lấy danh sách nhân viên theo điều kiên lọc và phân trang 
        /// </summary>
        /// <param name="Keyword">Từ khóa muốn tìm kiếm(Mã nhân viên, tên hoặc số điện thoại )</param>
        /// <param name="departmentId">Id phòng ban </param>
        /// <param name="jodPositionId">Id vị trí </param>
        /// <param name="Linit">Số bản ghi muốn lấy </param>
        /// <param name="offset">Vị trí bản ghi bắt đầu lấy </param>
        /// <returns>
        /// Trả về 1 đối tượng PagingResylt, bao gồm danh sách nhân viên trên 1 trang và tổng số bản ghi thỏa mãn đièu kiện
        /// </returns>
        [HttpGet]
        public IActionResult GetPaging(
            [FromQuery]string? Keyword, 
            [FromQuery]Guid? departmentId,
            [FromQuery]Guid? jodPositionId,
            [FromQuery]int Linit = 10,
            [FromQuery]int offset = 0)
        {
            return StatusCode(StatusCodes.Status200OK, new  PagingResultm
            {
                Data = new List<object>
                {
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV001",
                        Fullname = " Hoàng Quốc Tuấn",
                        Gender = Gender.Male,
                        DateOfBirth = DateTime.Now,
                        PhoneNumber = "0321554975",
                        Email = "ahgdv@gmail.com",
                        IdNumber = "45664225",
                        IdentityIssueDate = DateTime.Now,
                        TaxCode ="982365422",
                        WorkingStatus = WorkingStatus.Working,
                        dateOfJoining = DateTime.Now,
                        IdenityIssuePlace = "Lào cai "
                    },
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV002",
                        Fullname = " Phạm Tuấn Duy ",
                        Gender = Gender.Male,
                        DateOfBirth = DateTime.Now,
                        PhoneNumber = "0321574975",
                        Email = "agdv@gmail.com",
                        IdNumber = "45664225",
                        IdentityIssueDate = DateTime.Now,
                        TaxCode ="988365422",
                        WorkingStatus = WorkingStatus.Practicing,
                        dateOfJoining = DateTime.Now,
                        IdenityIssuePlace = "Thái bình  "
                    },
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        Code = "NV003",
                        Fullname = " Nguyễn Thị Yến  ",
                        Gender =Gender.Female ,
                        DateOfBirth = DateTime.Now,
                        PhoneNumber = "0329554975",
                        Email = "ajgdv@gmail.com",
                        IdNumber = "45694225",
                        IdentityIssueDate = DateTime.Now,
                        TaxCode ="987365422",
                        WorkingStatus = WorkingStatus.HasRetired,
                        dateOfJoining = DateTime.Now,
                        IdenityIssuePlace = "Hà nội  "
                    }
                },
                TotalRecords = 3
            });
        }
        /// <summary>
        /// Api thêm mới nhân viên 
        /// </summary>
        /// <param name="newEmployee">Đối tượng nhân viên muốn thêm mới </param>
        /// <returns>Id của nhân viên vừa thêm mới </returns>
        [HttpPost]
        public IActionResult InsertEmployees([FromBody]Employee newEmployee)
        {
            return StatusCode(StatusCodes.Status201Created, Guid.NewGuid());
        }

        /// <summary>
        /// Api sửa 
        /// </summary>
        /// <param name="updateEmployee"> đối tượng nhân viên cần sửa </param>
        /// <param name="EmployeeId">Id nhân viên vừa được sửa </param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployees(
            [FromBody]Employee  updateEmployee, 
            [FromRoute]Guid employeeId )
        {
            return Ok(employeeId);
        }

        /// <summary>
        /// Api xóa 
        /// </summary>
        /// <param name="employeeId">Id nhân viên vừa được xóa </param>
        /// <returns></returns>
        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployees([FromRoute]Guid employeeId )
        {
            return Ok(employeeId);
        }

        /// <summary>
        /// Api thêm mới nhân viên 
        /// </summary>
        /// <param name="employeeId">Id nhân viên vừa được thêm </param>
        /// <returns></returns>
        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeById([FromRoute]Guid employeeId)
        {
            try
            {
                // Chuẩn bị tên stored procedure
                string storedProcedureName = "Proc_employee_GetById";

                //Chuẩn bị tham số đầu vào cho stored
                var parameters = new DynamicParameters();
                parameters.Add("p_Id", employeeId);

                //Khởi tạo kết nối Database
                string connectionString = "Server=locahost;Port=3306;Database=employeemanagement;Uid=root;Pwd=duc15032001;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Thực hiện gọi vào Database để chạy stored procedure
                var employee = mySqlConnection.QueryFirstOrDefault<Employee>(connectionString, parameters, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả trả về 
                if (employee == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(employeeId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception",
                    UserMsg = "Có lỗi xảy ra! Vui lòng liên hệ trung trâm tư vấn.",
                    MoreInfo = "https://handleerror.com/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        [HttpGet("new-code")]
        public IActionResult GetNewEmployeeCode()
        {
            return Ok("NV111");
        }
    }
}
