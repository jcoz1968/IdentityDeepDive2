using Dapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityDeepDive.Models
{
    public class PluralsightUserStore : IUserStore<PluralsightUser>
    {
        public void Dispose()
        {
        }

        public Task<string> GetUserIdAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetNormalizedUserNameAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetUserNameAsync(PluralsightUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(PluralsightUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            using(var connection = GetOpenConnection())
            {
                await connection.ExecuteAsync(
                    "insert into PluralsightUsers([Id]," + 
                    "[UserName]" +
                    "[NormalizedUserName]" + 
                    "[PasswordHash]) " + 
                    "Values(@id, @userName, @normalizedUserName, @passwordHash)",
                    new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    }
                );
            }
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PluralsightUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PluralsightUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(PluralsightUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                                "database=PluralsightIdentityDemo;" +
                                                "trusted_connection=yes;");
            connection.Open();
            return connection;
        }
    }
}
