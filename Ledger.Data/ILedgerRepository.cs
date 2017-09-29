using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ledger.Data
{
    public interface ILedgerRepository
    {
        Task<int> AddAccountToUserAsync(Account account, string username);
        Task<int> UpdateAccountAsync(long id, Account account);
        Task<int> AddEntryToAccountAsync(Entry entry, long accountId);
        Task<int> AddUserAsync(UserProfile profile);
        Task<int> DeleteAccountAsync(long accountId);
        Task<int> DeleteEntryFromAccountAsync(long accountId, long id);
        IEnumerable<Account> GetAccountsByUser(string username);
        Account GetAccountById(long id);
        IEnumerable<Entry> GetEntriesForAccount(long accountid);
        Entry GetEntryById(long accountid, long id);
        Task<int> AddEntryAsync(long accountid, Entry viewModel);
        Task<int> SaveEntryAsync(long accountid, long id, Entry viewModel);
    }
}