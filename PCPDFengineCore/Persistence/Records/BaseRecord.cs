using System.ComponentModel.DataAnnotations.Schema;

namespace PCPDFengineCore.Persistence.Records
{
    public abstract class BaseRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
