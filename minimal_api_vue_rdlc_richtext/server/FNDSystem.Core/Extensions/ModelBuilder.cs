using Bogus;
using Microsoft.EntityFrameworkCore;
using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Helpers;

namespace FNDSystem.Core.Extensions;

public static class ModelBuilderExtension
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        for (int i = 1; i < 10; i++)
        {

        }
        modelBuilder.Entity<Tenant>().HasData(
            FakeData.Tenants
        );

        modelBuilder.Entity<Account>().HasData(
           FakeData.Accounts
       );
    }

    public static class FakeData
    {
        public static List<Tenant> Tenants = new List<Tenant>();
        public static List<Account> Accounts = new List<Account>();

        public static void Init(string tenantCode)
        {
            Tenants.Clear();
            Accounts.Clear();

            var tenantId = 1;
            var tenantFaker = new Faker<Tenant>(locale: "ja")
               .RuleFor(t => t.Id, _ => tenantId++)
               .RuleFor(t => t.Code, f => tenantCode)
               .RuleFor(t => t.Name, (f, u) => f.Person.Company.Name);

            var accountId = 1;
            var accountFaker = new Faker<Account>(locale: "ja")
               .RuleFor(a => a.Id, _ => accountId++)
               .RuleFor(a => a.Code, f => tenantCode)
               .RuleFor(a => a.Code, f => f.Random.String(10))
               .RuleFor(a => a.Name, (f, u) => f.Person.Company.Name)
               .RuleFor(a => a.RoleKbn, (f, u) => accountId == 1 ? "01" : "02")
               .RuleFor(a => a.Email, (f, u) => f.Person.Email)
               .RuleFor(a => a.Password, (f, u) => StringUtils.PasswordHash("20150601"));


            var tenant = tenantFaker.Generate(1);
            Tenants.AddRange(tenant);
            var accounts = accountFaker.Generate(new Random().Next(30, 50));
            Accounts.AddRange(accounts);
        }
    }
}