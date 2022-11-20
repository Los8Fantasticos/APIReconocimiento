using MinimalAPI_Reconocimiento.Models.ApplicationModel;
using Bogus;
using System.Linq;

namespace MinimalAPI_Reconocimiento.Infrastructure.Storage
{
    public static class PatenteStorage
    {
        public static ApplicationDbContext AddPatente(this ApplicationDbContext context, int count = 15, bool randomBoolean = true)
        {
            var authors = new Faker<PatenteModel>()
                       //.RuleFor(a => a.IdPatente, f => f.UniqueIndex)
                       .RuleFor(a => a.Patente, new Func<Faker, PatenteModel, string>((f, a) =>
                       {
                           int tipoPatente = new Random().Next(1, 5);
                           if (tipoPatente == 1)
                               return (f.Random.String2(3, 3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + " " + f.Random.String2(3, 3, "0123456789")).Trim();
                           else if (tipoPatente == 3)
                               return (f.Random.String2(3, 3, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + f.Random.String2(3, 3, "0123456789")).Trim();
                           else
                               return (f.Random.String2(2, 2, "ABCDEFGHIJKLMNOPQRSTUVWXYZ") + " " + f.Random.String2(3, 3, "0123456789") + " " + f.Random.String2(2, 2, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")).Trim();
                       }))
                       .RuleFor(a => a.FechaAlta, DateTime.Now)
                       .RuleFor(a => a.Active, randomBoolean)
                       .Generate(count);

            context.AddRange(authors);

            context.SaveChanges();

            return context;
        }


    }
}
