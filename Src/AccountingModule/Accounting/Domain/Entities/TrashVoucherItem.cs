using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("trash_voucher_items", Schema = "accounting")]
[Index("BankBranchForPayableDocId", Name = "trash_voucher_items_bank_branch_for_payable_doc_id_c7192fe1")]
[Index("BankBranchForReceivableDocId", Name = "trash_voucher_items_bank_branch_for_receivable_doc_id_18f99fa4")]
[Index("BankForPayableDocId", Name = "trash_voucher_items_bank_for_payable_doc_id_b0e53bf9")]
[Index("BankForReceivableDocId", Name = "trash_voucher_items_bank_for_receivable_doc_id_52a2c187")]
[Index("BankOperationBankAccountId", Name = "trash_voucher_items_bank_operation_bank_account_id_2cdf8f2c")]
[Index("BankOperationTypeId", Name = "trash_voucher_items_bank_operation_type_id_4f027778")]
[Index("BranchId", Name = "trash_voucher_items_branch_id_1cd5a4ea")]
[Index("CashierPeriodId", Name = "trash_voucher_items_cashier_period_id_ec0ac32d")]
[Index("CompanyId", Name = "trash_voucher_items_company_id_b595ca9c")]
[Index("CompanyUnitId", Name = "trash_voucher_items_company_unit_id_ab43aa30")]
[Index("CostCenterId", Name = "trash_voucher_items_cost_center_id_7225ec9a")]
[Index("CurrencyId", Name = "trash_voucher_items_currency_id_bf31a904")]
[Index("LedgerId", Name = "trash_voucher_items_ledger_id_58204329")]
[Index("ManualFloat1Id", Name = "trash_voucher_items_manual_float_1_id_0cdae92d")]
[Index("ManualFloat2Id", Name = "trash_voucher_items_manual_float_2_id_985905b9")]
[Index("ManualFloat3Id", Name = "trash_voucher_items_manual_float_3_id_11645efa")]
[Index("ManualFloat4Id", Name = "trash_voucher_items_manual_float_4_id_60ae5d18")]
[Index("ManualFloat5Id", Name = "trash_voucher_items_manual_float_5_id_3eb06b68")]
[Index("ManualFloat6Id", Name = "trash_voucher_items_manual_float_6_id_a4afd007")]
[Index("ManualFloat7Id", Name = "trash_voucher_items_manual_float_7_id_85ed9abc")]
[Index("ManualFloat8Id", Name = "trash_voucher_items_manual_float_8_id_d2f59901")]
[Index("MasterId", Name = "trash_voucher_items_master_id_0c48bb81")]
[Index("PeriodId", Name = "trash_voucher_items_period_id_e1c57ee1")]
[Index("PersonId", Name = "trash_voucher_items_person_id_0cca821d")]
[Index("PettyCashierPeriodId", Name = "trash_voucher_items_petty_cashier_period_id_1e714a51")]
[Index("ProjectContractId", Name = "trash_voucher_items_project_contract_id_967b39b0")]
[Index("ProjectContractTypeId", Name = "trash_voucher_items_project_contract_type_id_4e9e97ee")]
[Index("ProjectId", Name = "trash_voucher_items_project_id_e5915af2")]
[Index("ResourceAndExpenditureId", Name = "trash_voucher_items_resource_and_expenditure_id_fb349f90")]
[Index("SlaveCompanyId", Name = "trash_voucher_items_slave_company_id_976d2e14")]
[Index("SlaveId", Name = "trash_voucher_items_slave_id_8ef343db")]
[Index("StoreId", Name = "trash_voucher_items_store_id_93066542")]
[Index("TrashVoucherId", Name = "trash_voucher_items_trash_voucher_id_4c9d9a23")]
public partial class TrashVoucherItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("voucher_item_id_initial")]
    public long? VoucherItemIdInitial { get; set; }

    [Column("id_initial")]
    public long? IdInitial { get; set; }

    [Column("master_code")]
    public int MasterCode { get; set; }

    [Column("slave_code")]
    public int SlaveCode { get; set; }

    [Column("sequence")]
    public int Sequence { get; set; }

    [Column("deal_type")]
    public short? DealType { get; set; }

    [Column("cost_center_code")]
    public int? CostCenterCode { get; set; }

    [Column("company_unit_code")]
    public int? CompanyUnitCode { get; set; }

    [Column("project_date")]
    public DateOnly? ProjectDate { get; set; }

    [Column("project_status_report")]
    [StringLength(30)]
    public string? ProjectStatusReport { get; set; }

    [Column("project_receipt")]
    public long? ProjectReceipt { get; set; }

    [Column("store_doc_type")]
    [StringLength(20)]
    public string? StoreDocType { get; set; }

    [Column("store_good_code")]
    public int? StoreGoodCode { get; set; }

    [Column("store_good_unit_price", TypeName = "numeric(20, 6)")]
    public decimal? StoreGoodUnitPrice { get; set; }

    [Column("store_good_quantity", TypeName = "numeric(16, 4)")]
    public decimal? StoreGoodQuantity { get; set; }

    [Column("currency_amount", TypeName = "numeric(24, 8)")]
    public decimal? CurrencyAmount { get; set; }

    [Column("currency_exchange_rate", TypeName = "numeric(20, 4)")]
    public decimal? CurrencyExchangeRate { get; set; }

    [Column("bank_operation_receipt")]
    public long? BankOperationReceipt { get; set; }

    [Column("bank_operation_date")]
    public DateOnly? BankOperationDate { get; set; }

    [Column("bank_operation_check_no")]
    [StringLength(30)]
    public string? BankOperationCheckNo { get; set; }

    [Column("receivable_doc_date")]
    public DateOnly? ReceivableDocDate { get; set; }

    [Column("receivable_doc_check_no")]
    [StringLength(30)]
    public string? ReceivableDocCheckNo { get; set; }

    [Column("payable_doc_date")]
    public DateOnly? PayableDocDate { get; set; }

    [Column("payable_doc_check_no")]
    [StringLength(30)]
    public string? PayableDocCheckNo { get; set; }

    [Column("manual_float_1_extra_1")]
    [StringLength(50)]
    public string? ManualFloat1Extra1 { get; set; }

    [Column("manual_float_1_extra_2")]
    [StringLength(50)]
    public string? ManualFloat1Extra2 { get; set; }

    [Column("manual_float_1_extra_3")]
    [StringLength(50)]
    public string? ManualFloat1Extra3 { get; set; }

    [Column("manual_float_2_extra_1")]
    [StringLength(50)]
    public string? ManualFloat2Extra1 { get; set; }

    [Column("manual_float_2_extra_2")]
    [StringLength(50)]
    public string? ManualFloat2Extra2 { get; set; }

    [Column("manual_float_2_extra_3")]
    [StringLength(50)]
    public string? ManualFloat2Extra3 { get; set; }

    [Column("manual_float_3_extra_1")]
    [StringLength(50)]
    public string? ManualFloat3Extra1 { get; set; }

    [Column("manual_float_3_extra_2")]
    [StringLength(50)]
    public string? ManualFloat3Extra2 { get; set; }

    [Column("manual_float_3_extra_3")]
    [StringLength(50)]
    public string? ManualFloat3Extra3 { get; set; }

    [Column("manual_float_4_extra_1")]
    [StringLength(50)]
    public string? ManualFloat4Extra1 { get; set; }

    [Column("manual_float_4_extra_2")]
    [StringLength(50)]
    public string? ManualFloat4Extra2 { get; set; }

    [Column("manual_float_4_extra_3")]
    [StringLength(50)]
    public string? ManualFloat4Extra3 { get; set; }

    [Column("manual_float_5_extra_1")]
    [StringLength(50)]
    public string? ManualFloat5Extra1 { get; set; }

    [Column("manual_float_5_extra_2")]
    [StringLength(50)]
    public string? ManualFloat5Extra2 { get; set; }

    [Column("manual_float_5_extra_3")]
    [StringLength(50)]
    public string? ManualFloat5Extra3 { get; set; }

    [Column("manual_float_6_extra_1")]
    [StringLength(50)]
    public string? ManualFloat6Extra1 { get; set; }

    [Column("manual_float_6_extra_2")]
    [StringLength(50)]
    public string? ManualFloat6Extra2 { get; set; }

    [Column("manual_float_6_extra_3")]
    [StringLength(50)]
    public string? ManualFloat6Extra3 { get; set; }

    [Column("manual_float_7_extra_1")]
    [StringLength(50)]
    public string? ManualFloat7Extra1 { get; set; }

    [Column("manual_float_7_extra_2")]
    [StringLength(50)]
    public string? ManualFloat7Extra2 { get; set; }

    [Column("manual_float_7_extra_3")]
    [StringLength(50)]
    public string? ManualFloat7Extra3 { get; set; }

    [Column("manual_float_8_extra_1")]
    [StringLength(50)]
    public string? ManualFloat8Extra1 { get; set; }

    [Column("manual_float_8_extra_2")]
    [StringLength(50)]
    public string? ManualFloat8Extra2 { get; set; }

    [Column("manual_float_8_extra_3")]
    [StringLength(50)]
    public string? ManualFloat8Extra3 { get; set; }

    [Column("debit")]
    public long? Debit { get; set; }

    [Column("credit")]
    public long? Credit { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("due_date")]
    public DateOnly? DueDate { get; set; }

    [Column("printed")]
    public bool Printed { get; set; }

    [Column("have_attach")]
    public bool HaveAttach { get; set; }

    [Column("attaches")]
    [StringLength(500)]
    public string? Attaches { get; set; }

    [Column("is_tick")]
    public bool IsTick { get; set; }

    [Column("bank_branch_for_payable_doc_id")]
    public int? BankBranchForPayableDocId { get; set; }

    [Column("bank_branch_for_receivable_doc_id")]
    public int? BankBranchForReceivableDocId { get; set; }

    [Column("bank_for_payable_doc_id")]
    public int? BankForPayableDocId { get; set; }

    [Column("bank_for_receivable_doc_id")]
    public int? BankForReceivableDocId { get; set; }

    [Column("bank_operation_bank_account_id")]
    public int? BankOperationBankAccountId { get; set; }

    [Column("bank_operation_type_id")]
    public int? BankOperationTypeId { get; set; }

    [Column("branch_id")]
    public int BranchId { get; set; }

    [Column("cashier_period_id")]
    public int? CashierPeriodId { get; set; }

    [Column("company_id")]
    public int CompanyId { get; set; }

    [Column("company_unit_id")]
    public int? CompanyUnitId { get; set; }

    [Column("cost_center_id")]
    public int? CostCenterId { get; set; }

    [Column("currency_id")]
    public int? CurrencyId { get; set; }

    [Column("ledger_id")]
    public int LedgerId { get; set; }

    [Column("manual_float_1_id")]
    public int? ManualFloat1Id { get; set; }

    [Column("manual_float_2_id")]
    public int? ManualFloat2Id { get; set; }

    [Column("manual_float_3_id")]
    public int? ManualFloat3Id { get; set; }

    [Column("manual_float_4_id")]
    public int? ManualFloat4Id { get; set; }

    [Column("manual_float_5_id")]
    public int? ManualFloat5Id { get; set; }

    [Column("manual_float_6_id")]
    public int? ManualFloat6Id { get; set; }

    [Column("manual_float_7_id")]
    public int? ManualFloat7Id { get; set; }

    [Column("manual_float_8_id")]
    public int? ManualFloat8Id { get; set; }

    [Column("master_id")]
    public int MasterId { get; set; }

    [Column("period_id")]
    public int PeriodId { get; set; }

    [Column("person_id")]
    public int? PersonId { get; set; }

    [Column("petty_cashier_period_id")]
    public int? PettyCashierPeriodId { get; set; }

    [Column("project_id")]
    public int? ProjectId { get; set; }

    [Column("project_contract_id")]
    public int? ProjectContractId { get; set; }

    [Column("project_contract_type_id")]
    public int? ProjectContractTypeId { get; set; }

    [Column("resource_and_expenditure_id")]
    public int? ResourceAndExpenditureId { get; set; }

    [Column("slave_id")]
    public int SlaveId { get; set; }

    [Column("slave_company_id")]
    public int SlaveCompanyId { get; set; }

    [Column("store_id")]
    public int? StoreId { get; set; }

    [Column("trash_voucher_id")]
    public int TrashVoucherId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("TrashVoucherItems")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("ManualFloat1Id")]
    [InverseProperty("TrashVoucherItemManualFloat1s")]
    public virtual ManualFloatAccount? ManualFloat1 { get; set; }

    [ForeignKey("ManualFloat2Id")]
    [InverseProperty("TrashVoucherItemManualFloat2s")]
    public virtual ManualFloatAccount? ManualFloat2 { get; set; }

    [ForeignKey("ManualFloat3Id")]
    [InverseProperty("TrashVoucherItemManualFloat3s")]
    public virtual ManualFloatAccount? ManualFloat3 { get; set; }

    [ForeignKey("ManualFloat4Id")]
    [InverseProperty("TrashVoucherItemManualFloat4s")]
    public virtual ManualFloatAccount? ManualFloat4 { get; set; }

    [ForeignKey("ManualFloat5Id")]
    [InverseProperty("TrashVoucherItemManualFloat5s")]
    public virtual ManualFloatAccount? ManualFloat5 { get; set; }

    [ForeignKey("ManualFloat6Id")]
    [InverseProperty("TrashVoucherItemManualFloat6s")]
    public virtual ManualFloatAccount? ManualFloat6 { get; set; }

    [ForeignKey("ManualFloat7Id")]
    [InverseProperty("TrashVoucherItemManualFloat7s")]
    public virtual ManualFloatAccount? ManualFloat7 { get; set; }

    [ForeignKey("ManualFloat8Id")]
    [InverseProperty("TrashVoucherItemManualFloat8s")]
    public virtual ManualFloatAccount? ManualFloat8 { get; set; }

    [ForeignKey("MasterId")]
    [InverseProperty("TrashVoucherItems")]
    public virtual MasterAccount Master { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("TrashVoucherItems")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("ResourceAndExpenditureId")]
    [InverseProperty("TrashVoucherItems")]
    public virtual ResourceAndExpenditure? ResourceAndExpenditure { get; set; }

    [ForeignKey("SlaveId")]
    [InverseProperty("TrashVoucherItems")]
    public virtual SlaveAccount Slave { get; set; } = null!;

    [ForeignKey("SlaveCompanyId")]
    [InverseProperty("TrashVoucherItems")]
    public virtual SlaveAccountCompany SlaveCompany { get; set; } = null!;

    [ForeignKey("TrashVoucherId")]
    [InverseProperty("TrashVoucherItems")]
    public virtual TrashVoucher TrashVoucher { get; set; } = null!;

    [InverseProperty("TrashVoucherItem")]
    public virtual ICollection<TrashVoucherItemAttach> TrashVoucherItemAttaches { get; set; } = new List<TrashVoucherItemAttach>();
}
