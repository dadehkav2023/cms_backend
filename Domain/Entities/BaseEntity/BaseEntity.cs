using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace Domain.Entities.BaseEntity
{
    public class BaseEntityWithIdentityKey<TPrimaryKey>
    {
        [Key]
        [Column, NotNull]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TPrimaryKey Id { get; set; }
    }
    public class BaseEntityWithIdentityKey:BaseEntityWithIdentityKey<int>
    {
    }
    
    public class BaseEntityWithoutIdentityKey<TPrimaryKey>
    {
        [Key]
        [Column, NotNull]
        public virtual TPrimaryKey Id { get; set; }
    }
}
