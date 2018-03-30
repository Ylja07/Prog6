using Prog6.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;

namespace Prog6.Repositories
{
    public class TamagotchiRepository : ITamagotchiRepository
    {
        private readonly TamagotchiEntities _tamagotchiEntities;
        public TamagotchiRepository(TamagotchiEntities dbContext)
        {
            _tamagotchiEntities = dbContext;
        }
        public void Add(Tamagotchi tama)
        {
            tama.Alive = true;
            tama.Health = 100;
            tama.Money = 100;
            _tamagotchiEntities.Tamagotchi.Add(tama);
            _tamagotchiEntities.SaveChanges();
        }

        public void Delete(Tamagotchi tama)
        {
            _tamagotchiEntities.Tamagotchi.Remove(tama);
            _tamagotchiEntities.SaveChanges();
        }

        public Tamagotchi Get(int? TamagotchiId)
        {
            return _tamagotchiEntities.Tamagotchi.Find(TamagotchiId);
        }

        public List<Tamagotchi> GetAll()
        {
            return _tamagotchiEntities.Tamagotchi.ToList();
        }

        public void StandardNight(Tamagotchi t)
        {
            t.Age++;
            if (t.Boredom >= 70)
            {
                t.Health -= 20;
            }
            if (t.Health <= 0)
            {
                t.Health = 0;
                t.Alive = false;
            }
            if(t.Health > 100)
            {
                t.Health = 100;
            }
            Update(t);
        }

        public void Update(Tamagotchi tama)
        {
            _tamagotchiEntities.Tamagotchi.AddOrUpdate(tama);
            _tamagotchiEntities.SaveChanges();
        }

        public void UpdateMultiple(List<Tamagotchi> tama)
        {
            tama.ForEach(e => Update(e));
        }
    }
}