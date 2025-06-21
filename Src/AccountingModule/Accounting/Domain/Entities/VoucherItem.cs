using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("voucher_items", Schema = "accounting")]
[Index("BankBranchForPayableDocId", Name = "voucher_items_bank_branch_for_payable_doc_id_305a9b46")]
[Index("BankBranchForReceivableDocId", Name = "voucher_items_bank_branch_for_receivable_doc_id_7e2ff8a1")]
[Index("BankForPayableDocId", Name = "voucher_items_bank_for_payable_doc_id_e602a063")]
[Index("BankForReceivableDocId", Name = "voucher_items_bank_for_receivable_doc_id_5b97c79f")]
[Index("BankOperationBankAccountId", Name = "voucher_items_bank_operation_bank_account_id_5fe511b5")]
[Index("BankOperationTypeId", Name = "voucher_items_bank_operation_type_id_2ea895a7")]
[Index("BranchId", Name = "voucher_items_branch_id_e26c223a")]
[Index("CashierPeriodId", Name = "voucher_items_cashier_period_id_a6ab7714")]
[Index("CompanyId", Name = "voucher_items_company_id_6d32d694")]
[Index("CompanyUnitId", Name = "voucher_items_company_unit_id_0a963bbd")]
[Index("CostCenterId", Name = "voucher_items_cost_center_id_4afbb6e5")]
[Index("CurrencyId", Name = "voucher_items_currency_id_85cae847")]
[Index("LedgerId", Name = "voucher_items_ledger_id_439dcc5d")]
[Index("ManualFloat1Id", Name = "voucher_items_manual_float_1_id_a2c88b6e")]
[Index("ManualFloat2Id", Name = "voucher_items_manual_float_2_id_60bbb623")]
[Index("ManualFloat3Id", Name = "voucher_items_manual_float_3_id_4f9d7506")]
[Index("ManualFloat4Id", Name = "voucher_items_manual_float_4_id_35e1cb30")]
[Index("ManualFloat5Id", Name = "voucher_items_manual_float_5_id_a5393d5c")]
[Index("ManualFloat6Id", Name = "voucher_items_manual_float_6_id_a56fc8dc")]
[Index("ManualFloat7Id", Name = "voucher_items_manual_float_7_id_858fb1f1")]
[Index("ManualFloat8Id", Name = "voucher_items_manual_float_8_id_a738253d")]
[Index("MasterId", Name = "voucher_items_master_id_f8105cae")]
[Index("PeriodId", Name = "voucher_items_period_id_3525580e")]
[Index("PersonId", Name = "voucher_items_person_id_754c8c99")]
[Index("PettyCashierPeriodId", Name = "voucher_items_petty_cashier_period_id_df390d00")]
[Index("ProjectContractId", Name = "voucher_items_project_contract_id_90384ab2")]
[Index("ProjectContractTypeId", Name = "voucher_items_project_contract_type_id_4a970f8e")]
[Index("ProjectId", Name = "voucher_items_project_id_420c3c83")]
[Index("ResourceAndExpenditureId", Name = "voucher_items_resource_and_expenditure_id_c457dd03")]
[Index("SlaveCompanyId", Name = "voucher_items_slave_company_id_51b34aa7")]
[Index("SlaveId", Name = "voucher_items_slave_id_36e4e833")]
[Index("StoreId", Name = "voucher_items_store_id_76c76e09")]
[Index("VoucherId", Name = "voucher_items_voucher_id_e75336c1")]
public partial class VoucherItem
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_initial")]
    public long? IdInitial { get; set; }

    [Column("cross_reference_id")]
    [StringLength(32)]
    public string? CrossReferenceId { get; set; }

    [Column("master_code")]
    public int MasterCode { get; set; }

    [Column("slave_code")]
    public int SlaveCode { get; set; }

    [Column("sequence")]
    public int Sequence { get; set; }

    [Column("deal_type")]
    public short? DealType { get; set; }

    [Column("cost_center_code")]
    [StringLength(18)]
    public string? CostCenterCode { get; set; }

    [Column("company_unit_code")]
    [StringLength(18)]
    public string? CompanyUnitCode { get; set; }

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
    [StringLength(13)]
    public string? StoreGoodCode { get; set; }

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

    [Column("voucher_id")]
    public int VoucherId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("VoucherItems")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("ManualFloat1Id")]
    [InverseProperty("VoucherItemManualFloat1s")]
    public virtual ManualFloatAccount? ManualFloat1 { get; set; }

    [ForeignKey("ManualFloat2Id")]
    [InverseProperty("VoucherItemManualFloat2s")]
    public virtual ManualFloatAccount? ManualFloat2 { get; set; }

    [ForeignKey("ManualFloat3Id")]
    [InverseProperty("VoucherItemManualFloat3s")]
    public virtual ManualFloatAccount? ManualFloat3 { get; set; }

    [ForeignKey("ManualFloat4Id")]
    [InverseProperty("VoucherItemManualFloat4s")]
    public virtual ManualFloatAccount? ManualFloat4 { get; set; }

    [ForeignKey("ManualFloat5Id")]
    [InverseProperty("VoucherItemManualFloat5s")]
    public virtual ManualFloatAccount? ManualFloat5 { get; set; }

    [ForeignKey("ManualFloat6Id")]
    [InverseProperty("VoucherItemManualFloat6s")]
    public virtual ManualFloatAccount? ManualFloat6 { get; set; }

    [ForeignKey("ManualFloat7Id")]
    [InverseProperty("VoucherItemManualFloat7s")]
    public virtual ManualFloatAccount? ManualFloat7 { get; set; }

    [ForeignKey("ManualFloat8Id")]
    [InverseProperty("VoucherItemManualFloat8s")]
    public virtual ManualFloatAccount? ManualFloat8 { get; set; }

    [ForeignKey("MasterId")]
    [InverseProperty("VoucherItems")]
    public virtual MasterAccount Master { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("VoucherItems")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("ResourceAndExpenditureId")]
    [InverseProperty("VoucherItems")]
    public virtual ResourceAndExpenditure? ResourceAndExpenditure { get; set; }

    [ForeignKey("SlaveId")]
    [InverseProperty("VoucherItems")]
    public virtual SlaveAccount Slave { get; set; } = null!;

    [ForeignKey("SlaveCompanyId")]
    [InverseProperty("VoucherItems")]
    public virtual SlaveAccountCompany SlaveCompany { get; set; } = null!;

    [InverseProperty("VoucherItem")]
    public virtual TtmsBuy? TtmsBuy { get; set; }

    [InverseProperty("VoucherItem")]
    public virtual TtmsContractorInfo? TtmsContractorInfo { get; set; }

    [InverseProperty("VoucherItem")]
    public virtual TtmsEmployerInfo? TtmsEmployerInfo { get; set; }

    [InverseProperty("VoucherItem")]
    public virtual TtmsExportation? TtmsExportation { get; set; }

    [InverseProperty("VoucherItem")]
    public virtual TtmsImportation? TtmsImportation { get; set; }

    [InverseProperty("VoucherItem")]
    public virtual TtmsLeaseAgreement? TtmsLeaseAgreement { get; set; }

    [InverseProperty("VoucherItem")]
    public virtual TtmsPreSell? TtmsPreSell { get; set; }

    [InverseProperty("VoucherItem")]
    public virtual TtmsSell? TtmsSell { get; set; }

    [InverseProperty("VoucherItem")]
    public virtual TtmsWage? TtmsWage { get; set; }

    [ForeignKey("VoucherId")]
    [InverseProperty("VoucherItems")]
    public virtual Voucher Voucher { get; set; } = null!;

    [InverseProperty("VoucherItem")]
    public virtual ICollection<VoucherItemAttach> VoucherItemAttaches { get; set; } = new List<VoucherItemAttach>();
}
