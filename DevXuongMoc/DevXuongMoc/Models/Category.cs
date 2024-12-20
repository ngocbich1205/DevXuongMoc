using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevXuongMoc.Models
{
    public partial class Category
    {
        public int Id { get; set; }

        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }

        [Display(Name = "Biểu tượng")]
        public string? Icon { get; set; }

        [Display(Name = "Tiêu đề Meta")]
        public string? MateTitle { get; set; }

        [Display(Name = "Từ khóa Meta")]
        public string? MetaKeyword { get; set; }

        [Display(Name = "Mô tả Meta")]
        public string? MetaDescription { get; set; }

        [Display(Name = "Slug")]
        public string? Slug { get; set; }

        [Display(Name = "Thứ tự")]
        public int? Orders { get; set; }

        [Display(Name = "ID danh mục cha")]
        public int? Parentid { get; set; }

        // Format the date and time for CreatedDate
        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime? CreatedDate { get; set; }

        // Format the date and time for UpdatedDate
        [Display(Name = "Ngày cập nhật")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Người tạo")]
        public string? AdminCreated { get; set; }

        [Display(Name = "Người cập nhật")]
        public string? AdminUpdated { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Notes { get; set; }

        // Convert Status byte to text
        [Display(Name = "Trạng thái")]
        public string StatusText => GetStatusText(Status);

        [Display(Name = "Trạng thái")]
        public byte? Status { get; set; }

        [Display(Name = "Đã xóa")]
        public bool? Isdelete { get; set; }

        // Enum to represent possible status values
        public enum StatusEnum
        {
            Active = 1,    // Active
            Inactive = 2,  // Inactive
            Pending = 3,   // Pending
            Deleted = 4    // Deleted
        }

        // Helper method to convert byte status to text
        private string GetStatusText(byte? status)
        {
            if (status.HasValue)
            {
                switch (status.Value)
                {
                    case (byte)StatusEnum.Active:
                        return "Hoạt động";
                    case (byte)StatusEnum.Inactive:
                        return "Không hoạt động";
                    case (byte)StatusEnum.Pending:
                        return "Chờ duyệt";
                    case (byte)StatusEnum.Deleted:
                        return "Đã xóa";
                    default:
                        return "Không xác định";
                }
            }
            return "Không xác định";
        }
    }
}
