using System.ComponentModel.DataAnnotations;

namespace TicketSys.Application.DTOs;

public class UserDTO
{
    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória")]
    [StringLength(50, ErrorMessage = "Senha deve ter no mínimo {2} e no máximo {1} caracteres", MinimumLength = 6)]

    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório")]
    [Display(Name = "Nome")]
    public required string Name { get; set; }

    [Display(Name = "Ativo")]
    public bool IsActive { get; set; }
}
