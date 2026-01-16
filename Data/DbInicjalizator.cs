using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using BDwAI_BugTrackSys.Models;

namespace BDwAI_BugTrackSys.Data
{
    public class DbInicjalizator
    {
        public static async Task Inicjalizuj(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            string[] roleNames = { "Admin", "Uzytkownik" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminEmail = "admin@admin.pl";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(newAdmin, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }

            var userEmail = "test@test.pl";
            var normalUser = await userManager.FindByEmailAsync(userEmail);
            if (normalUser == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(newUser, "Test123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "Uzytkownik");
                }
                normalUser = newUser;
            }

            if (!context.Statusy.Any())
            {
                context.Statusy.AddRange(
                    new Status { Nazwa = "Nowe" },
                    new Status { Nazwa = "W trakcie" },
                    new Status { Nazwa = "Zakończone" },
                    new Status { Nazwa = "Odrzucone" }
                );
            }

            if (!context.Priorytety.Any())
            {
                context.Priorytety.AddRange(
                    new Priorytet { Nazwa = "Niski" },
                    new Priorytet { Nazwa = "Normalny" },
                    new Priorytet { Nazwa = "Wysoki" },
                    new Priorytet { Nazwa = "Krytyczny" }
                );
            }

            if (!context.Projekty.Any())
            {
                context.Projekty.AddRange(
                    new Projekt { Nazwa = "Strona WWW Sklepu", Opis = "Rozwój strony e-commerce" },
                    new Projekt { Nazwa = "System Magazynowy", Opis = "Aplikacja wewnętrzna" },
                    new Projekt { Nazwa = "Aplikacja Mobilna", Opis = "Wersja iOS i Android" }
                );
            }

            await context.SaveChangesAsync();

            if (!context.Zgloszenia.Any() && normalUser != null)
            {
                var statusNowe = await context.Statusy.FirstOrDefaultAsync(s => s.Nazwa == "Nowe");
                var statusWtrakcie = await context.Statusy.FirstOrDefaultAsync(s => s.Nazwa == "W trakcie");

                var priorytetWysoki = await context.Priorytety.FirstOrDefaultAsync(p => p.Nazwa == "Wysoki");
                var priorytetNiski = await context.Priorytety.FirstOrDefaultAsync(p => p.Nazwa == "Niski");

                var projektWWW = await context.Projekty.FirstOrDefaultAsync(p => p.Nazwa == "Strona WWW Sklepu");

                if (statusNowe != null && priorytetWysoki != null && projektWWW != null)
                {
                    context.Zgloszenia.AddRange(
                        new Zgloszenie
                        {
                            Temat = "Błąd logowania",
                            Opis = "Nie można zalogować się przy użyciu Firefox.",
                            DataUtworzenia = DateTime.Now,
                            ProjektId = projektWWW.Id,
                            PriorytetId = priorytetWysoki.Id,
                            StatusId = statusNowe.Id,
                            UzytkownikId = normalUser.Id
                        },
                        new Zgloszenie
                        {
                            Temat = "Literówka w menu",
                            Opis = "Napisane 'Konakt' zamiast 'Kontakt'",
                            DataUtworzenia = DateTime.Now.AddDays(-2),
                            ProjektId = projektWWW.Id,
                            PriorytetId = priorytetNiski != null ? priorytetNiski.Id : priorytetWysoki.Id,
                            StatusId = statusWtrakcie != null ? statusWtrakcie.Id : statusNowe.Id,
                            UzytkownikId = normalUser.Id
                        }
                    );

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}