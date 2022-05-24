using System.ComponentModel.DataAnnotations;

namespace Jiepei.ERP.Orders.Admin
{
    public class EditNoteInput
    {
        [Required]
        [MaxLength(256)]
        public string Note { get; set; }
    }
}
