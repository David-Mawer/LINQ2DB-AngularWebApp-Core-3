using AngularWebApp.Auth.DB;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWebApp.Auth
{
    public class PersistedGrantStore : IPersistedGrantStore, IDisposable
    {
        private DataConnection _db;

        public PersistedGrantStore()
        {
            _db = new DataConnection();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return await _db.GetTable<PersistedGrants>().Where(x => x.SubjectId == subjectId).ToListAsync();
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            return await _db.GetTable<PersistedGrants>().Where(x => x.Key == key).FirstOrDefaultAsync();
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            await _db.GetTable<PersistedGrants>().Where(x => x.SubjectId == subjectId && x.ClientId == clientId).DeleteAsync();
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            await _db.GetTable<PersistedGrants>().Where(x => x.SubjectId == subjectId && x.ClientId == clientId && x.Type == type).DeleteAsync();
        }

        public async Task RemoveAsync(string key)
        {
            await _db.GetTable<PersistedGrants>().Where(x => x.Key == key).DeleteAsync();
        }

        public async Task StoreAsync(PersistedGrant grant)
        {

            await _db.InsertOrReplaceAsync(new PersistedGrants()
            {
                ClientId = grant.ClientId,
                CreationTime = grant.CreationTime,
                Data = grant.Data,
                Expiration = grant.Expiration,
                Key = grant.Key,
                SubjectId = grant.SubjectId,
                Type = grant.Type
            });
        }

        Task<IEnumerable<PersistedGrant>> IPersistedGrantStore.GetAllAsync(PersistedGrantFilter filter)
        {
            throw new NotImplementedException();
        }

        Task IPersistedGrantStore.RemoveAllAsync(PersistedGrantFilter filter)
        {
            throw new NotImplementedException();
        }

    }
}
