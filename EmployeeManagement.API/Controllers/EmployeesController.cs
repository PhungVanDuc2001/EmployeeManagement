using Dapper;
using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Entities.DTO;
using EmployeeManagement.API.Enums;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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
        private string storedProcedureName;

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
            [FromQuery]Guid? jobPositionId,
            [FromQuery]int limit = 10,
            [FromQuery]int offset = 0)
        {
            try
            {
                string storedProcedureName = "Proc_employee_GetPaging";

                var parameters = new DynamicParameters();
                parameters.Add("p_Keyword", Keyword);
                parameters.Add("p_DepartmentId", departmentId);
                parameters.Add("p_JobPositionId", jobPositionId);
                parameters.Add("p_Limit", limit);
                parameters.Add("p_Offset", offset);

                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
                var multiResultSets = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                var employees = multiResultSets.Read<object>().ToList();
                int totaRecords = multiResultSets.Read<int>().FirstOrDefault();

                return Ok(new PagingResult
                {
                    Data = employees,
                    TotalRecords = totaRecords
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfe = "https://handleerror.com/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }
        /// <summary>
        /// Api thêm mới nhân viên 
        /// </summary>
        /// <param name="newEmployee">Đối tượng nhân viên muốn thêm mới </param>
        /// <returns>Id của nhân viên vừa thêm mới </returns>
        [HttpPost]
        public IActionResult InsertEmployees([FromBody]Employee newEmployee)
        {
            try
            {
                // Validate
                var properties = typeof(Employee).GetProperties();

                var validateFailures = new List<string>();

                foreach (var property in properties) 
                {
                    string propertyName = property.Name;
                    var requiredAttribute =(RequiredAttribute)property.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault();
                    if (requiredAttribute != null)
                    {
                        if (string.IsNullOrEmpty(property.GetValue(newEmployee)?.ToString()))
                        {
                            validateFailures.Add(requiredAttribute.ErrorMessage);
                        }
                    }
                    var maxLengthAttribute = (MaxLengthAttribute)property.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault();
                    if (maxLengthAttribute != null)
                    {
                        if (property.GetValue(newEmployee).ToString()?.Length > maxLengthAttribute.Length)
                        {
                            validateFailures.Add(maxLengthAttribute.ErrorMessage);
                        }
                    }
                }
                if (validateFailures.Count > 0)
                {
                    return BadRequest(new ErrorResult
                    {
                        ErrorCode = Enums.ErrorCode.InvalidData,
                        DevMsg = "",
                        UserMsg = "",
                        MoreInfe = validateFailures,
                        TraceId = HttpContext.TraceIdentifier
                    });
                }

                string stroredaprocedureName = "Proc_employee_Insert ";
                var parameters = new DynamicParameters();
                var newId = Guid.NewGuid();
                parameters.Add("p_Id", Guid.NewGuid());
                parameters.Add("p_Code", newEmployee.Code);
                parameters.Add("p_Fullname", newEmployee.Fullname);
                parameters.Add("p_Gender", newEmployee.Gender);
                parameters.Add("p_DataOfBirth ", newEmployee.DateOfBirth);
                parameters.Add("p_PhoneNumber", newEmployee.PhoneNumber);
                parameters.Add("p_Email", newEmployee.Email);
                parameters.Add("p_IdNumber", newEmployee.IdNumber);
                parameters.Add("p_IdentityIssueDate", newEmployee.IdentityIssueDate);
                parameters.Add("p_TaxCode", newEmployee.TaxCode);
                parameters.Add("p_WorkingStatus", newEmployee.WorkingStatus);
                parameters.Add("p_dateOfJoining", newEmployee.dateOfJoining);
                parameters.Add("p_IdenityIssuePlace", newEmployee.IdenityIssuePlace);

                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                int numberOfAffectedRous = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (numberOfAffectedRous == 1)
                {
                    return StatusCode(StatusCodes.Status201Created, newId);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.DatabaseFailed,
                    DevMsg = Resource.DevMsg_DatabaseFailed,
                    UserMsg = Resource.UserMsg_DatabaseFailed,
                    MoreInfe = "http://google.com",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfe = "https://handleerror.com/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
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
            try
            {
                string storeProcedureName = "Proc_employee_Delete ";

                var parameters = new DynamicParameters();
                parameters.Add("p_Id", employeeId);

                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                int numberOfAffectedRous = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                if (numberOfAffectedRous == 1)
                {
                    return Ok(employeeId);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        ErrorCode = Enums.ErrorCode.DatabaseFailed,
                        DevMsg = Resource.DevMsg_DatabaseFailed,
                        UserMsg = Resource.UserMsg_DatabaseFailed,
                        MoreInfe = "http://google.com",
                        TraceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
                    MoreInfe = "https://handleerror.com/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
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
                var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

                //Thực hiện gọi vào Database để chạy stored procedure
                var employee = mySqlConnection.QueryFirstOrDefault<Employee>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

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
                    ErrorCode = Enums.ErrorCode.Exception,
                    DevMsg = Resource.DevMsg_Exception,
                    UserMsg = Resource.UserMsg_Exception,
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
