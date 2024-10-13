using System.ComponentModel.DataAnnotations;

namespace TechChallenge.Domain.Entities
{
    public abstract class BaseModel
    {
        [Key]
        public long Id { get; set; }
    }
}
