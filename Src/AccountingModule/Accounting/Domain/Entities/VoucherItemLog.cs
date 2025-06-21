using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("voucher_item_logs", Schema = "accounting")]
[Index("BankBranchForPayableDocId", Name = "voucher_item_logs_bank_branch_for_payable_doc_id_2db629ab")]
[Index("BankBranchForReceivableDocId", Name = "voucher_item_logs_bank_branch_for_receivable_doc_id_64132689")]
[Index("BankForPayableDocId", Name = "voucher_item_logs_bank_for_payable_doc_id_61e85f05")]
[Index("BankForReceivableDocId", Name = "voucher_item_logs_bank_for_receivable_doc_id_bf6c6f31")]
[Index("BankOperationBankAccountId", Name = "voucher_item_logs_bank_operation_bank_account_id_44a79bf8")]
[Index("BankOperationTypeId", Name = "voucher_item_logs_bank_operation_type_id_4dcc6132")]
[Index("BranchId", Name = "voucher_item_logs_branch_id_2c9e0bff")]
[Index("CashierPeriodId", Name = "voucher_item_logs_cashier_period_id_c64d915f")]
[Index("CompanyId", Name = "voucher_item_logs_company_id_b3a353fc")]
[Index("CompanyUnitId", Name = "voucher_item_logs_company_unit_id_624a2c28")]
[Index("CostCenterId", Name = "voucher_item_logs_cost_center_id_24cca585")]
[Index("CurrencyId", Name = "voucher_item_logs_currency_id_e08cc027")]
[Index("LedgerId", Name = "voucher_item_logs_ledger_id_8b5e1cd0")]
[Index("ManualFloat1Id", Name = "voucher_item_logs_manual_float_1_id_58b6802b")]
[Index("ManualFloat2Id", Name = "voucher_item_logs_manual_float_2_id_38f71036")]
[Index("ManualFloat3Id", Name = "voucher_item_logs_manual_float_3_id_aab41fd4")]
[Index("ManualFloat4Id", Name = "voucher_item_logs_manual_float_4_id_f5608169")]
[Index("ManualFloat5Id", Name = "voucher_item_logs_manual_float_5_id_d6f8534e")]
[Index("ManualFloat6Id", Name = "voucher_item_logs_manual_float_6_id_356cdede")]
[Index("ManualFloat7Id", Name = "voucher_item_logs_manual_float_7_id_c6d1e3a4")]
[Index("ManualFloat8Id", Name = "voucher_item_logs_manual_float_8_id_97e9e83b")]
[Index("MasterId", Name = "voucher_item_logs_master_id_6b97e99d")]
[Index("PeriodId", Name = "voucher_item_logs_period_id_41dea737")]
[Index("PersonId", Name = "voucher_item_logs_person_id_d42c3aaa")]
[Index("PettyCashierPeriodId", Name = "voucher_item_logs_petty_cashier_period_id_6ca1cb8e")]
[Index("ProjectContractId", Name = "voucher_item_logs_project_contract_id_e8fd3c8e")]
[Index("ProjectContractTypeId", Name = "voucher_item_logs_project_contract_type_id_0a5b7cbb")]
[Index("ProjectId", Name = "voucher_item_logs_project_id_d5ecf069")]
[Index("ResourceAndExpenditureId", Name = "voucher_item_logs_resource_and_expenditure_id_6baa8522")]
[Index("SlaveCompanyId", Name = "voucher_item_logs_slave_company_id_d30d38dd")]
[Index("SlaveId", Name = "voucher_item_logs_slave_id_bcc29d21")]
[Index("StoreId", Name = "voucher_item_logs_store_id_c9d70513")]
[Index("VoucherId", Name = "voucher_item_logs_voucher_id_37e41a3c")]
[Index("VoucherIdInitial", Name = "voucher_item_logs_voucher_id_initial_eae29ba4")]
[Index("VoucherItemIdInitial", Name = "voucher_item_logs_voucher_item_id_initial_38823caf")]
[Index("VoucherLogId", Name = "voucher_item_logs_voucher_log_id_4528bfd9")]
public partial class VoucherItemLog
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("voucher_id_initial")]
    public int VoucherIdInitial { get; set; }

    [Column("voucher_item_id_initial")]
    public long VoucherItemIdInitial { get; set; }

    [Column("action")]
    [StringLength(6)]
    public string Action { get; set; } = null!;

    [Column("master_code")]
    public int MasterCode { get; set; }

    [Column("slave_code")]
    public int SlaveCode { get; set; }

    [Column("sequence")]
    public int Sequence { get; set; }

    [Column("old_sequence")]
    public int? OldSequence { get; set; }

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

    [Column("have_attach")]
    public bool HaveAttach { get; set; }

    [Column("attaches")]
    [StringLength(500)]
    public string? Attaches { get; set; }

    [Column("is_tick")]
    public bool IsTick { get; set; }

    [Column("is_modify")]
    public bool IsModify { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

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
    public int? VoucherId { get; set; }

    [Column("voucher_log_id")]
    public int VoucherLogId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("VoucherItemLogs")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("ManualFloat1Id")]
    [InverseProperty("VoucherItemLogManualFloat1s")]
    public virtual ManualFloatAccount? ManualFloat1 { get; set; }

    [ForeignKey("ManualFloat2Id")]
    [InverseProperty("VoucherItemLogManualFloat2s")]
    public virtual ManualFloatAccount? ManualFloat2 { get; set; }

    [ForeignKey("ManualFloat3Id")]
    [InverseProperty("VoucherItemLogManualFloat3s")]
    public virtual ManualFloatAccount? ManualFloat3 { get; set; }

    [ForeignKey("ManualFloat4Id")]
    [InverseProperty("VoucherItemLogManualFloat4s")]
    public virtual ManualFloatAccount? ManualFloat4 { get; set; }

    [ForeignKey("ManualFloat5Id")]
    [InverseProperty("VoucherItemLogManualFloat5s")]
    public virtual ManualFloatAccount? ManualFloat5 { get; set; }

    [ForeignKey("ManualFloat6Id")]
    [InverseProperty("VoucherItemLogManualFloat6s")]
    public virtual ManualFloatAccount? ManualFloat6 { get; set; }

    [ForeignKey("ManualFloat7Id")]
    [InverseProperty("VoucherItemLogManualFloat7s")]
    public virtual ManualFloatAccount? ManualFloat7 { get; set; }

    [ForeignKey("ManualFloat8Id")]
    [InverseProperty("VoucherItemLogManualFloat8s")]
    public virtual ManualFloatAccount? ManualFloat8 { get; set; }

    [ForeignKey("MasterId")]
    [InverseProperty("VoucherItemLogs")]
    public virtual MasterAccount Master { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("VoucherItemLogs")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("ResourceAndExpenditureId")]
    [InverseProperty("VoucherItemLogs")]
    public virtual ResourceAndExpenditure? ResourceAndExpenditure { get; set; }

    [ForeignKey("SlaveId")]
    [InverseProperty("VoucherItemLogs")]
    public virtual SlaveAccount Slave { get; set; } = null!;

    [ForeignKey("SlaveCompanyId")]
    [InverseProperty("VoucherItemLogs")]
    public virtual SlaveAccountCompany SlaveCompany { get; set; } = null!;

    [ForeignKey("VoucherId")]
    [InverseProperty("VoucherItemLogs")]
    public virtual Voucher? Voucher { get; set; }

    [ForeignKey("VoucherLogId")]
    [InverseProperty("VoucherItemLogs")]
    public virtual VoucherLog VoucherLog { get; set; } = null!;
}
