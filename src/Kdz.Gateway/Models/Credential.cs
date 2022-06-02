using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kdz.Gateway.Models;

public class Credential
{
    [Required(ErrorMessage = @"""{0}"" не заполнено")]
    [MinLength(5, ErrorMessage = @"Количество символов в поле ""{0}"" должно быть не меньше '{1}'.")]
    [MaxLength(50, ErrorMessage = @"Количество символов в поле ""{0}"" не должно превышать '{1}'.")]
    [DataType(DataType.Text)]
    [DisplayName("Логин")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = @"""{0}"" не заполнено")]
    [MinLength(5, ErrorMessage = @"Количество символов в поле ""{0}"" должно быть не меньше '{1}'.")]
    [MaxLength(50, ErrorMessage = @"Количество символов в поле ""{0}"" не должно превышать '{1}'.")]
    [DataType(DataType.Password)]
    [DisplayName("Пароль")]
    public string Password { get; set; } = string.Empty;
}