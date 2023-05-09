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
        public  Guid Id { get; set; }  
        
        /// <summary>
        /// Mã Nhân Viên 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Họ Và Tên 
        /// </summary>
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
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa Chỉ Email 
        /// </summary>
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
