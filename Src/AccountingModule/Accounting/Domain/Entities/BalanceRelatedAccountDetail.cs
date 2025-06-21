using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Domain.Entities;

[Table("balance_related_account_details", Schema = "accounting")]
[Index("BankBranchForPayableDocId", Name = "balance_related_account_details_bank_branch_for_payable_doc_id_f62750bd")]
[Index("BankBranchForReceivableDocId", Name = "balance_related_account_details_bank_branch_for_receivable_doc_id_3f00ae31")]
[Index("BankForPayableDocId", Name = "balance_related_account_details_bank_for_payable_doc_id_a61b2771")]
[Index("BankForReceivableDocId", Name = "balance_related_account_details_bank_for_receivable_doc_id_6d949455")]
[Index("BankOperationBankBranchId", Name = "balance_related_account_details_bank_operation_bank_branch_id_b2e2213b")]
[Index("BankOperationTypeId", Name = "balance_related_account_details_bank_operation_type_id_12f7966f")]
[Index("BranchId", Name = "balance_related_account_details_branch_id_c4524e9e")]
[Index("CompanyId", Name = "balance_related_account_details_company_id_f90e854f")]
[Index("CompanyUnitId", Name = "balance_related_account_details_company_unit_id_a4a8a95f")]
[Index("CostCenterId", Name = "balance_related_account_details_cost_center_id_1cebfa10")]
[Index("CurrencyId", Name = "balance_related_account_details_currency_id_a94bd7cd")]
[Index("LedgerId", Name = "balance_related_account_details_ledger_id_1fcf3bc8")]
[Index("ManualFloat1Id", Name = "balance_related_account_details_manual_float_1_id_3cf4e319")]
[Index("ManualFloat2Id", Name = "balance_related_account_details_manual_float_2_id_79cca910")]
[Index("ManualFloat3Id", Name = "balance_related_account_details_manual_float_3_id_c7afe8b7")]
[Index("ManualFloat4Id", Name = "balance_related_account_details_manual_float_4_id_bba5925c")]
[Index("ManualFloat5Id", Name = "balance_related_account_details_manual_float_5_id_2f0ad7f7")]
[Index("ManualFloat6Id", Name = "balance_related_account_details_manual_float_6_id_eeaf3bf1")]
[Index("ManualFloat7Id", Name = "balance_related_account_details_manual_float_7_id_c140028e")]
[Index("ManualFloat8Id", Name = "balance_related_account_details_manual_float_8_id_68b0bdc0")]
[Index("MasterId", Name = "balance_related_account_details_master_id_f76438d4")]
[Index("PeriodId", Name = "balance_related_account_details_period_id_2b2d6794")]
[Index("PersonId", Name = "balance_related_account_details_person_id_e1e5844f")]
[Index("ProjectContractId", Name = "balance_related_account_details_project_contract_id_926e56be")]
[Index("ProjectContractTypeId", Name = "balance_related_account_details_project_contract_type_id_4819f840")]
[Index("ProjectId", Name = "balance_related_account_details_project_id_6072d815")]
[Index("ResourceAndExpenditureId", Name = "balance_related_account_details_resource_and_expenditure_id_78f2b804")]
[Index("SlaveId", Name = "balance_related_account_details_slave_id_8bf18f14")]
[Index("StoreId", Name = "balance_related_account_details_store_id_9f40ae42")]
public partial class BalanceRelatedAccountDetail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

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

    [Column("bank_operation_bank_branch_id")]
    public int? BankOperationBankBranchId { get; set; }

    [Column("bank_operation_type_id")]
    public int? BankOperationTypeId { get; set; }

    [Column("branch_id")]
    public int BranchId { get; set; }

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

    [Column("store_id")]
    public int? StoreId { get; set; }

    [ForeignKey("LedgerId")]
    [InverseProperty("BalanceRelatedAccountDetails")]
    public virtual Ledger Ledger { get; set; } = null!;

    [ForeignKey("ManualFloat1Id")]
    [InverseProperty("BalanceRelatedAccountDetailManualFloat1s")]
    public virtual ManualFloatAccount? ManualFloat1 { get; set; }

    [ForeignKey("ManualFloat2Id")]
    [InverseProperty("BalanceRelatedAccountDetailManualFloat2s")]
    public virtual ManualFloatAccount? ManualFloat2 { get; set; }

    [ForeignKey("ManualFloat3Id")]
    [InverseProperty("BalanceRelatedAccountDetailManualFloat3s")]
    public virtual ManualFloatAccount? ManualFloat3 { get; set; }

    [ForeignKey("ManualFloat4Id")]
    [InverseProperty("BalanceRelatedAccountDetailManualFloat4s")]
    public virtual ManualFloatAccount? ManualFloat4 { get; set; }

    [ForeignKey("ManualFloat5Id")]
    [InverseProperty("BalanceRelatedAccountDetailManualFloat5s")]
    public virtual ManualFloatAccount? ManualFloat5 { get; set; }

    [ForeignKey("ManualFloat6Id")]
    [InverseProperty("BalanceRelatedAccountDetailManualFloat6s")]
    public virtual ManualFloatAccount? ManualFloat6 { get; set; }

    [ForeignKey("ManualFloat7Id")]
    [InverseProperty("BalanceRelatedAccountDetailManualFloat7s")]
    public virtual ManualFloatAccount? ManualFloat7 { get; set; }

    [ForeignKey("ManualFloat8Id")]
    [InverseProperty("BalanceRelatedAccountDetailManualFloat8s")]
    public virtual ManualFloatAccount? ManualFloat8 { get; set; }

    [ForeignKey("MasterId")]
    [InverseProperty("BalanceRelatedAccountDetails")]
    public virtual MasterAccount Master { get; set; } = null!;

    [ForeignKey("PeriodId")]
    [InverseProperty("BalanceRelatedAccountDetails")]
    public virtual Period Period { get; set; } = null!;

    [ForeignKey("ResourceAndExpenditureId")]
    [InverseProperty("BalanceRelatedAccountDetails")]
    public virtual ResourceAndExpenditure? ResourceAndExpenditure { get; set; }

    [ForeignKey("SlaveId")]
    [InverseProperty("BalanceRelatedAccountDetails")]
    public virtual SlaveAccount Slave { get; set; } = null!;
}
