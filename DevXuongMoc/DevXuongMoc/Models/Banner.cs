using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevXuongMoc.Models
{
    public partial class Banner
    {
        public int Id { get; set; }

        [Display(Name = "Ảnh")]
        public string? Image { get; set; }

        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }

        [Display(Name = "Phụ đề")]
        public string? SubTitle { get; set; }

        [Display(Name = "URL")]
        public string? Urls { get; set; }

        [Display(Name = "Thứ tự")]
        public int Orders { get; set; }

        [Display(Name = "Loại")]
        public string? Type { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }")]
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }")]
        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Người tạo")]
        public string? AdminCreated { get; set; }

        [Display(Name = "Người cập nhật")]
        public string? AdminUpdated { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Notes { get; set; }

        [Display(Name = "Trạng thái")]
        public string StatusText => GetStatusText(Status);
        [Display(Name = "Trạng thái")]
        public byte Status { get; set; }

        [Display(Name = "Đã xóa")]
        public bool Isdelete { get; set; }
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
