namespace EmployeeManagement.API.Entities
{
    /// <summary>
    /// Thông Tin Phòng Ban 
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Id Phòng Ban 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Mã Phòng Ban 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tên Phòng Ban 
        /// </summary>
        public string Name { get; set; }
    }
}
