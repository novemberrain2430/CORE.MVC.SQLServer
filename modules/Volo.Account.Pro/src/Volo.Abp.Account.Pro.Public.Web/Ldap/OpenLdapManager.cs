using System.Linq;
using System.Threading.Tasks;
using LdapForNet;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ldap;

namespace Volo.Abp.Account.Public.Web.Ldap
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(OpenLdapManager), typeof(ILdapManager), typeof(LdapManager))]
    public class OpenLdapManager : LdapManager
    {
        public OpenLdapManager(IOptions<AbpLdapOptions> ldapSettingsOptions)
            : base(ldapSettingsOptions)
        {
        }

        public virtual async Task<string> GetUserEmailAsync(string userName)
        {
            await LdapOptions.SetAsync();

            using (var conn = await CreateLdapConnectionAsync())
            {
                await AuthenticateLdapConnectionAsync(conn, NormalizeUserName(LdapOptions.Value.UserName), LdapOptions.Value.Password);

                var searchResults = await conn.SearchAsync(GetBaseDn(), GetUserFilter(userName));
                try
                {
                    var userEntry = searchResults.First();
                    return GetUserEmail(userEntry);
                }
                catch (LdapException e)
                {
                    Logger.LogException(e);
                }

                return null;
            }
        }

        protected override Task ConnectAsync(ILdapConnection ldapConnection)
        {
            ldapConnection.Connect(LdapOptions.Value.ServerHost, LdapOptions.Value.ServerPort);
            return Task.CompletedTask;
        }

        protected virtual string NormalizeUserName(string userName)
        {
            return $"cn={userName},{LdapOptions.Value.BaseDc}";
        }

        protected virtual string GetUserEmail(LdapEntry ldapEntry)
        {
            return ldapEntry.ToDirectoryEntry().GetAttribute("mail")?.GetValue<string>();
        }

        protected virtual string GetBaseDn()
        {
            return LdapOptions.Value.BaseDc;
        }

        protected virtual string GetUserFilter(string userName)
        {
            return $"(&(uid={userName}))";
        }
    }
}
