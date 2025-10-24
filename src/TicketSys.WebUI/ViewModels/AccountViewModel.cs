using System.ComponentModel.DataAnnotations;

namespace TicketSys.WebUI.ViewModels
{
    public class AccountViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MinLength(2, ErrorMessage = "O nome deve ter no mínimo 2 caracteres.")]
        [Display(Name = "Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [Display(Name = "Senha")]
        public string? Password { get; set; }

        [Display(Name = "Ativo")]
        public bool IsActive { get; set; }

        public IEnumerable<TypeofAccountViewModel>? TypesOfAccount { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de usuário.")]
        [Display(Name = "Tipo de Conta")]
        public string? SelectedTypeOfAccountId { get; set; }
    }

    public record TypeofAccountViewModel(
        string Id,
        string Description
    );
}
