using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevXuongMoc.Models
{
    public partial class AdminUser
    {
        public int Id { get; set; }

        [Display(Name = "Tài khoản")]
        public string? Account { get; set; }

        [Display(Name = "Mật khẩu")]
        public string? Password { get; set; }

        [Display(Name = "Mã nhân sự")]
        public int? MaNhanSu { get; set; }

        [Display(Name = "Tên")]
        public string? Name { get; set; }

        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string? Avatar { get; set; }

        [Display(Name = "ID phòng ban")]
        public int? IdPhongBan { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy }")]
        [Display(Name = "Ngày tạo")]
        public DateTime? NgayTao { get; set; }

        [Display(Name = "Người tạo")]
        public string? NguoiTao { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy a}")]
        [Display(Name = "Ngày cập nhật")]
        public DateTime? NgayCapNhat { get; set; }

        [Display(Name = "Người cập nhật")]
        public string? NguoiCapNhat { get; set; }

        [Display(Name = "Mã phiên làm việc")]
        public string? SessionToken { get; set; }

        [Display(Name = "Mã muối")]
        public string? Salt { get; set; }

        [Display(Name = "Là quản trị viên")]
        public bool? IsAdmin { get; set; }

        // Status in string format
        [Display(Name = "Trạng thái")]
        public string TrangThaiText => GetTrangThaiText(TrangThai);

        [Display(Name = "Trạng thái")]
        public int? TrangThai { get; set; }

        [Display(Name = "Đã xóa")]
        public bool? IsDelete { get; set; }

        // Enum to represent status codes
        public enum Status
        {
            HoatDong = 1,   // Active
            KhongHoatDong = 2, // Inactive
            ChoDuyet = 3,    // Pending
            DaXoa = 4        // Deleted
        }

        // Helper method to convert numeric status to text in Vietnamese
        private string GetTrangThaiText(int? trangThai)
        {
            if (trangThai.HasValue)
            {
                switch (trangThai.Value)
                {
                    case (int)Status.HoatDong:
                        return "Hoạt động";
                    case (int)Status.KhongHoatDong:
                        return "Không hoạt động";
                    case (int)Status.ChoDuyet:
                        return "Chờ duyệt";
                    case (int)Status.DaXoa:
                        return "Đã xóa";
                    default:
                        return "Không xác định";
                }
            }
            return "Không xác định";
        }
    }
}
