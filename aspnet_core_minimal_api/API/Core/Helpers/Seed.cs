using Bogus;
using Core.Entities;
using Infrastructure;
using Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Core.Helpers.Seeder;

public class Seeder: IDisposable
{
    private readonly DBMasterContext masterContext;
    private bool disposedValue;

    public Seeder(string connection)
    {
        var contextOptions = new DbContextOptionsBuilder<DBMasterContext>()
        .UseNpgsql(connection)
        .LogTo(Console.Write)
        .EnableSensitiveDataLogging(true);
        masterContext = new DBMasterContext(contextOptions.Options);
    }
    public void Seed(int count)
    {

        if (this.masterContext.Tenants.Count() == 0)
        {
            var randomBuilder = new Bogus.DataSets.Rant();
            string accountCode = string.Empty;
            Tenant tenant;
            Account account;


            // Administrator - System Owner
            tenant = new Tenant() {
                Code = "SSV",
                Name = "System Owner"
            };

            masterContext.Add<Tenant>(tenant);

            account = new Account() {
                TenantCode = "SSV",
                Code = "SSVADMIN",
                Name = "Saishunkan System",
                Email = "vn@saishunkansys.com",
                Password = StringUtils.PasswordHash("20150601"),
                RoleKbn = "99"
            };

            masterContext.Add<Account>(account);

            // Customer data
            for (int i = 1; i <= count; i++)
            {
                string tenantCode = randomBuilder.Random.AlphaNumeric(10).ToUpper();
                // =========== Tenant =============
                var tenantFaker = new Faker<Tenant>()
                       .RuleFor(t => t.Code, f => tenantCode)
                       .RuleFor(t => t.Name, (f, u) => f.Person.Company.Name);

                tenant = tenantFaker.Generate(1).ElementAt(0);
                masterContext.Add<Tenant>(tenant);


                // =========== Account =============

                // add Tenant Owner
                accountCode = randomBuilder.Random.AlphaNumeric(15);
                var accountOwnerFaker = new Faker<Account>()
                   .RuleFor(a => a.TenantCode, f => tenantCode)
                   .RuleFor(a => a.Code, f => accountCode)
                   .RuleFor(a => a.Name, (f, u) => f.Person.Company.Name)
                   .RuleFor(a => a.RoleKbn, (f, u) => "01") // Tenant Owner
                   .RuleFor(a => a.Email, (f, u) => f.Person.Email.ToLower())
                   .RuleFor(a => a.Password, (f, u) => StringUtils.PasswordHash("20150601"));

                account = accountOwnerFaker.Generate(1).ElementAt(0);
                masterContext.Add<Account>(account);

                for (int accountIdx = 1; accountIdx <= 50; accountIdx++)
                {
                    accountCode = randomBuilder.Random.AlphaNumeric(15).ToUpper();
                    var accountNormalFaker = accountOwnerFaker.Clone()
                    .RuleFor(a => a.Code, f => accountCode)
                    .RuleFor(a => a.RoleKbn, (f, u) => "02"); // Normal User by default

                    account = accountNormalFaker.Generate(1).ElementAt(0);
                    masterContext.Add<Account>(account);
                }
            }

            this.masterContext.SaveChanges();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                this.masterContext.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Seeder()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
