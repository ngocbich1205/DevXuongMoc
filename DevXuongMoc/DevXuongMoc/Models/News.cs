using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevXuongMoc.Models
{
    public partial class News
    {
        public int Id { get; set; }

        [Display(Name = "Mã tin tức")]
        public string? Code { get; set; }

        [Display(Name = "Tiêu đề")]
        public string? Title { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Nội dung")]
        public string? Content { get; set; }

        [Display(Name = "Ảnh")]
        public string? Image { get; set; }

        [Display(Name = "Tiêu đề Meta")]
        public string? MetaTitle { get; set; }

        [Display(Name = "Từ khóa chính")]
        public string? MainKeyword { get; set; }

        [Display(Name = "Từ khóa Meta")]
        public string? MetaKeyword { get; set; }

        [Display(Name = "Mô tả Meta")]
        public string? MetaDescription { get; set; }

        [Display(Name = "Slug")]
        public string? Slug { get; set; }

        [Display(Name = "Số lượt xem")]
        public int? Views { get; set; }

        [Display(Name = "Số lượt thích")]
        public int? Likes { get; set; }

        [Display(Name = "Đánh giá")]
        public double? Star { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }")]
        [Display(Name = "Ngày cập nhật")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Người tạo")]
        public string? AdminCreated { get; set; }

        [Display(Name = "Người cập nhật")]
        public string? AdminUpdated { get; set; }
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
