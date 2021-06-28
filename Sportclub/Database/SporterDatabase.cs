using Sportclub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sportclub.Database
{
    public interface ISporterDatabase
    {
        Sporter GetSporter(int id);
        IEnumerable<Sporter> GetSporters();
        Sporter Insert(Sporter sporter);
        void Delete(int id);
        void Update(int id, Sporter updated);
    }
    public class SporterDatabase:ISporterDatabase
    {
        
       
        private readonly SporterDbContext dataDbContext;

        public SporterDatabase(SporterDbContext dataDbContext)
        {
            this.dataDbContext = dataDbContext;
        }
        public void Delete(int id)
        {
            var inkomstUitgave = dataDbContext.Sporters.SingleOrDefault(m => m.Id == id);
            if (inkomstUitgave != null)
            {
                dataDbContext.Remove(inkomstUitgave);
                dataDbContext.SaveChanges();

            }
        }

        public IEnumerable<Sporter> GetSporters()
        {
            return dataDbContext.Sporters.Select(x => x);
        }

        public Sporter GetSporter(int id)
        {
            return dataDbContext.Sporters.FirstOrDefault(m => m.Id == id);
        }

        public Sporter Insert(Sporter sporter)
        {
            dataDbContext.Sporters.Add(sporter);
            dataDbContext.SaveChanges();

            return sporter;
        }

        public void Update(int id, Sporter updated)
        {
            var sporter = dataDbContext.Sporters.SingleOrDefault(m => m.Id == id);
            if (sporter != null)
            {
                sporter.Voornaam = updated.Voornaam;
                sporter.Achternaam = updated.Achternaam;
                sporter.GewichtKg = updated.GewichtKg;
                sporter.LengteCm = updated.LengteCm;
                sporter.Geboortedatum = updated.Geboortedatum;


                dataDbContext.SaveChanges();
            }
        }
    }
}

