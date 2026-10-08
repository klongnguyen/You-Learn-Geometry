using System.ComponentModel.DataAnnotations;

namespace YouLearnGeometry.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập Email hoặc Tên đăng nhập")]
    [Display(Name = "Email hoặc Tên đăng nhập")]
    public string Identifier { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
}

public class RegisterViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập Email")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    [Display(Name = "Địa chỉ Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập Tên đăng nhập")]
    [Display(Name = "Tên đăng nhập")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
    [MinLength(3, ErrorMessage = "Mật khẩu tối thiểu 3 ký tự")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn Lớp học hiện tại")]
    [Display(Name = "Lớp học")]
    public string GradeLevel { get; set; } = "Lop6"; // "Lop6" | "Lop8"
}

public class ProfileViewModel
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string GradeLevel { get; set; } = "Lop6";
    public int StreakCount { get; set; }
    public List<string> BadgesEarned { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}
