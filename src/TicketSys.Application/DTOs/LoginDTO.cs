using System.ComponentModel.DataAnnotations;

namespace TicketSys.Application.DTOs;

public class LoginDTO
{
    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    [DataType(DataType.Password)]
    [StringLength(50, ErrorMessage = "Senha deve ter no mínimo {2} e no máximo {1} caracteres", MinimumLength = 3)]
    public string? Password { get; set; }

    public string? ReturnUrl { get; set; }

}
