namespace EmployeeManagement.API.Entities
{
    /// <summary>
    /// Thông Tin Vị Trí 
    /// </summary>
    public class JobPosition
    {
        /// <summary>
        /// Id Vị Trí 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Mã Vị Trí 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Tên Vị trí 
        /// </summary>
        public string Name { get; set; }
    }
}
