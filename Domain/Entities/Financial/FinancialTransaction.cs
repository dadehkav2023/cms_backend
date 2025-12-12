using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.EnumList.Financial;
using Common.EnumList.WALLETEnums;
using Common.Enums.Financial;
using Domain.Attributes;
using Domain.Attributes.Store;
using Domain.Entities.BaseEntity;
using Domain.Entities.Identity.User;
using Domain.Entities.Store;

namespace Domain.Entities.Financial;

[Table("FinancialTransaction", Schema = "financial")]
[Auditable]
[MainEntity]
public class FinancialTransaction : BaseEntityWithIdentityKey
{
    public long Amount { get; set; }

    [MaxLength(400)] public string Description { get; set; }

    public FinancialTransactionStatus Status { get; set; }
    public FinancialTransactionTypeEnum Type { get; set; }

    public PaymentTypeEnum? PaymentType { get; set; }

    public string BankToken { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }
    public string BasketCode { get; set; }
    public long RequestAmount { get; set; }

    public string? BankResponseAsJson { get; set; }


    #region Sep Bank Info

    /// <summary>
    ///  این پارامتر مشخص کننده وضعیت تراکنش می باشد
    /// </summary>
    public string State { get; set; }

    /// <summary>
    /// ین پارامتر کدی وضعیت پرداخت را مشخص می کند
    /// </summary>
    public string StateCode { get; set; }

    /// <summary>
    ///  کد پذیرنده یا ترمینال اختصاصی پذیرنده می باشد
    /// </summary>
    public int ResCode { get; set; }

    /// <summary>
    ///  این پارامتر کدی است تا 50 حرف یا عدد که برای هر تراکنش ایجاد می شود.
    /// </summary>
    public string RefNum { get; set; }

    /// <summary>
    /// این پارامتر شماره کارت خریدار بصورت کد شده می باشد
    /// </summary>
    public string CID { get; set; }

    /// <summary>
    /// این پارامتر شماره پیگیری تولید شده توسط سپ می باشد
    /// </summary>
    public string TRACENO { get; set; }

    /// <summary>
    /// ین پارامتر شماره کارت خریدار با الزامات شاپرک می باشد
    /// </summary>
    public string SecurePan { get; set; }

    public double VerifyPaymentResult { get; set; }
    public double ReversePaymentResult { get; set; }

    #endregion Sep Bank Info

    //


    #region BankBranchReceipt

    public string ReceiptFileFullName { get; set; }
    public string ReceiptNumber { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    public string BranchCode { get; set; }
    public string BranchName { get; set; }

    #endregion

    public int UserId { get; set; }

    //public int? BankId { get; set; }
    public virtual User User { get; set; }

}