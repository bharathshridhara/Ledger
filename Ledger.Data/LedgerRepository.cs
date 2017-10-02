using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ledger.Data
{
    public class LedgerRepository : ILedgerRepository
    {
        private LedgerDBContext _db = new LedgerDBContext();
            //new LedgerDBContext(new DbContextOptionsBuilder<LedgerDBContext>()
                                      //      .UseSqlServer("Server=LAPTOP-KUC56DD5\\SQLEXPRESS; Database=Ledger;Trusted_Connection=True;MultipleActiveResultSets=true")
                                      //      .Options);

        public IEnumerable<Account> GetAccountsByUser(string username)
        {
            return _db.Accounts.Where(x => x.Name == username)
                    .Include(x => x.Entries);
        }

        public Account GetAccountById(long id)
        {
            return _db.Accounts.Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task<int> AddUserAsync(UserProfile profile)
        {
            _db.Profiles.Add(profile);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> AddAccountToUserAsync(Account account, string username)
        {
            var user = _db.Profiles.Where(x => x.Name == username).FirstOrDefault();
                user.Accounts.Add(account);
                _db.Accounts.Add(account);
                return await _db.SaveChangesAsync();
        }

        public async Task<int> UpdateAccountAsync(long id, Account account)
        {
            var accountModel = _db.Accounts.Where(x => x.Id == id).FirstOrDefault();
                accountModel.Balance = account.Balance;
                accountModel.Name = account.Name;
                return await _db.SaveChangesAsync();
            
        }

        public async Task<int> AddEntryToAccountAsync(Entry entry, long accountId)
        {
            var account = _db.Accounts.Where(x => x.Id == accountId).FirstOrDefault();
            if(entry.IsDeposit)
            {
                account.Balance += entry.Amount;
            }
            else
            {
                account.Balance -= entry.Amount;
            }
            account.Entries.Add(entry);
            _db.Entries.Add(entry);
            return await _db.SaveChangesAsync();
        
        }

        public async Task<int> DeleteEntryFromAccountAsync(long accountId, long id)
        {
            var account = _db.Accounts.Where(x => x.Id == accountId).FirstOrDefault();
            var entry = _db.Entries.Where(x => x.Id == id).FirstOrDefault();
            if (entry != null)
            {
                if (entry.IsDeposit)
                {
                    account.Balance -= entry.Amount;
                }
                else
                {
                    account.Balance += entry.Amount;
                }
                account.Entries.Remove(entry);
                _db.Entries.Remove(entry);
            }
            return await _db.SaveChangesAsync();
        
        }

        public async Task<int> DeleteAccountAsync(long accountId)
        {
            var account = _db.Accounts.Where(x => x.Id == accountId).FirstOrDefault();
            _db.Profiles.Where(x => x.Id == account.ProfileId).FirstOrDefault().Accounts.Remove(account);
            _db.Accounts.Remove(account);
            return await _db.SaveChangesAsync();
        
        }

        public IEnumerable<Entry> GetEntriesForAccount(long accountid)
        {
            var model = _db.Accounts.Where(x => x.Id == accountid).FirstOrDefault().Entries.ToList<Entry>();
            return model;
        }

        public Entry GetEntryById(long accountid, long id)
        {
            var model = _db.Accounts.Where(x => x.Id == accountid).FirstOrDefault().Entries.Where(y => y.Id == id).FirstOrDefault();
            return model;
        }

        public async Task<int> AddEntryAsync(long accountid, Entry entry)
        {
            _db.Accounts.Where(x => x.Id == accountid).FirstOrDefault().Entries.Add(entry);
            await _db.Entries.AddAsync(entry);
            var result = await _db.SaveChangesAsync();
            return result;
        }

        public async Task<int> SaveEntryAsync(long accountid, long id, Entry entry)
        {
            var entryModel = _db.Entries.Where(x => x.Id == id).FirstOrDefault();
            if(entryModel != null)
            {
                _db.Entry(entry).CurrentValues.SetValues(entry);
                return (await _db.SaveChangesAsync());
            }
            return 0;
                
        }

        public UserProfile GetUserDetails(string username)
        {
            var user = _db.Profiles.Where(x => x.Name.Equals(username)).FirstOrDefault();
            return user;
        }
    }
}
