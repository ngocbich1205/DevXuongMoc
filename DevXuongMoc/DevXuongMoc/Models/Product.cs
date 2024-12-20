using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevXuongMoc.Models
{
    public partial class Product
    {
        public int Id { get; set; }

        [Display(Name = "ID danh mục")]
        public int? Cid { get; set; }

        [Display(Name = "Mã sản phẩm")]
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

        [Display(Name = "Từ khóa Meta")]
        public string? MetaKeyword { get; set; }

        [Display(Name = "Mô tả Meta")]
        public string? MetaDescription { get; set; }

        [Display(Name = "Slug")]
        public string? Slug { get; set; }

        [Display(Name = "Giá cũ")]
        [DisplayFormat(DataFormatString = "{0:F3} VND")]
        public decimal? PriceOld { get; set; }

        [Display(Name = "Giá mới")]
        [DisplayFormat(DataFormatString = "{0:F3} VND")]
        public decimal? PriceNew { get; set; }

        [Display(Name = "Kích thước")]
        public string? Size { get; set; }

        [Display(Name = "Số lượt xem")]
        public int? Views { get; set; }

        [Display(Name = "Số lượt thích")]
        public int? Likes { get; set; }

        [Display(Name = "Đánh giá")]
        public double? Star { get; set; }

        [Display(Name = "Hiển thị trên trang chủ")]
        public byte? Home { get; set; }

        [Display(Name = "Sản phẩm hot")]
        public byte? Hot { get; set; }
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
                        return "Còn hàng";
                    case (byte)StatusEnum.Inactive:
                        return "Hết hàng";
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
