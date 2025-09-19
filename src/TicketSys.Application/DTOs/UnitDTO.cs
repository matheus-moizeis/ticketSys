using System.ComponentModel.DataAnnotations;

namespace TicketSys.Application.DTOs;

/// <summary>
/// Representa uma unidade (filial, setor ou local de atendimento).
/// </summary>
public class UnitDTO(int id, string name, string address, bool active)
{
    public int Id { get; set; } = id;

    [Required(ErrorMessage = "Nome é obrigatório")]
    [MinLength(3, ErrorMessage = "Nome deve ter no mínimo 3 caracteres")]
    [MaxLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    [Display(Name = "Nome")]
    public string Name { get; set; } = name;

    [Required(ErrorMessage = "Endereço é obrigatório")]
    [MinLength(5, ErrorMessage = "Endereço deve ter no mínimo 5 caracteres")]
    [MaxLength(200, ErrorMessage = "Endereço deve ter no máximo 200 caracteres")]
    [Display(Name = "Endereço")]
    public string Address { get; set; } = address;

    [Display(Name = "Ativo")]
    public bool Active { get; set; } = active;

    public UnitDTO() : this(0, string.Empty, string.Empty, true) { }
}
