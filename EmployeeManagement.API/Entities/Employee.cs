using EmployeeManagement.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Địa chỉ Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Mã Nhân Viên 
        /// </summary>
        [Required(ErrorMessage ="Mã nhân viên không được để trống")]
        [MaxLength(20, ErrorMessage ="Mã nhân viên không được vượt quá 20 ký tự")]
        public string Code { get; set; }

        /// <summary>
        /// Họ Và Tên 
        /// </summary>
        [Required(ErrorMessage = "Tên nhân viên không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên nhân viên không được vượt quá 100 ký tự")]
        public string Fullname { get; set; }

        /// <summary>
        /// Giới Tính 
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Ngày Sinh 
        /// </summary>
        public DateTime DateOfBirth   { get; set; }

        /// <summary>
        /// Số Điện Thoại 
        /// </summary>
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [MaxLength(25, ErrorMessage = "Số điện thoại không được vượt quá 25 ký tự")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa Chỉ Email 
        /// </summary>
        [Required(ErrorMessage = "Emali không được để trống")]
        [MaxLength(50, ErrorMessage = "Email không được vượt quá 50 ký tự")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Id Phòng Ban 
        /// </summary>
        public Guid DeparmentId  { get; set; }

        /// <summary>
        /// Id Vị Trí  
        /// </summary>
        public Guid JobPositionId { get; set; }

        /// <summary>
        /// Số Id 
        /// </summary>
        public string IdNumber  { get; set; }

        /// <summary>
        /// Ngày Cấp Giấy Tờ Tùy Thân 
        /// </summary>
        public DateTime IdentityIssueDate { get; set; }

        /// <summary>
        /// Mã Số Cá Nhân  
        /// </summary>
        public string TaxCode { get; set; }

        /// <summary>
        /// Tình Trạng Làm Việc 
        /// </summary>
        public WorkingStatus WorkingStatus  { get; set;}

        /// <summary>
        /// Ngày Tham Gia 
        /// </summary>
        public DateTime dateOfJoining { get; set; }

        /// <summary>
        /// Nơi Cấp Giấy Tờ Cá Nhân 
        /// </summary>
        public string IdenityIssuePlace { get; set; }
        
    }
}
