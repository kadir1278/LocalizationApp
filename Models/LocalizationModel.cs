using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalizationApp.Models
{
    public class LocalizationModel
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CultureInfo { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
