using NGErp.Base.Service.RequestParameters;
using NGErp.General.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGErp.General.Service.Interfaces.Services
{
    public interface ICountryService
    {
        (IEnumerable<CountryDto> countries, MetaData metaData) GetAll(CountryParameters countryParameters, bool trackChanges);
        //LedgerDto? Get(int LedgerId, bool trackChanges);
        //LedgerDto Create(LedgerForCreationDto ledger);
        //IEnumerable<LedgerDto> GetByIds(IEnumerable<int> ids, bool trackChanges);
        //(IEnumerable<LedgerDto> ledgers, string ids) CreateCollection(IEnumerable<LedgerForCreationDto> ledgerCollection);
        //void Delete(int ledgerId, bool trackChanges);
        //void Update(int ledgerId, LedgerForUpdateDto ledger, bool trackChanges);
        //(LedgerForUpdateDto ledgerForUpdate, Ledger ledgerEntity) GetLedgerForPatch(int ledgerId, bool trackChanges);
        //void SaveChangesForPatch(LedgerForUpdateDto ledgerToPatch, Ledger ledgerEntity);
    }
}
