using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Orders.Dtos
{
    public class EditNoteInput
    {
        [Required]
        [MaxLength(256)]
        public string Note { get; set; }
    }
}
