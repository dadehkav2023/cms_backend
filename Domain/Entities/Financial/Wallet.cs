using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Attributes;
using Domain.Attributes.Store;
using Domain.Entities.BaseEntity;
using Domain.Entities.Identity.User;

namespace Domain.Entities.Financial;

[Table("Wallet",Schema = "financial")]
[Auditable]
[MainEntity]
public class Wallet :BaseEntityWithIdentityKey 
{
    public long Amount { get; set; }
    
    public int UserId { get; set; }
    
    public virtual User User { get; set; }
    
   
}