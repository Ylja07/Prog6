using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prog6.Models;

namespace Prog6.Repositories
{
    public class DummyTamagotchiRepository : ITamagotchiRepository
    {
        List<Tamagotchi> Tamagotchis { get; set; }
        private int id = 4;
        public DummyTamagotchiRepository()
        {
            Tamagotchis = new List<Models.Tamagotchi>();

            Tamagotchis.Add(new Models.Tamagotchi
            {
                Name = "Tamagod",
                Money = 100,
                Health = 100,
                Alive = true,
                Id = 1,
            });
            Tamagotchis.Add(new Models.Tamagotchi
            {
                Name = "Tamagucci",
                Money = 100,
                Health = 100,
                Alive = true,
                Id = 2,
            });
            Tamagotchis.Add(new Models.Tamagotchi
            {
                Name = "Tamadre",
                Money = 100,
                Health = 100,
                Alive = true,
                Id = 3,
            });
            Tamagotchis.Add(new Models.Tamagotchi
            {
                Name = "Tamagoal",
                Money = 100,
                Health = 100,
                Alive = true,
                Id = 4,
            });

        }
        public Models.Tamagotchi Create(Models.Tamagotchi tama)
        {
            id++;
            tama.Id = id;
            tama.Money = 100;
            tama.Health = 100;
            tama.Alive = true;
            Tamagotchis.Add(tama);
            return tama;
        }

        public void Delete(Models.Tamagotchi tama)
        {
            Tamagotchis.Remove(Tamagotchis.Find(x => x.Id == tama.Id));
        }

        public Models.Tamagotchi DeleteBooking(Models.Tamagotchi tama)
        {
            if (tama.Room != null)
                tama.Room1.Tamagotchi.Remove(tama);

            tama.Room = null;
            tama.Room1 = null;

            return tama;
        }

        public Models.Tamagotchi Get(int? index)
        {
            return Tamagotchis.Find(x => x.Id == index);
        }

        public Models.Tamagotchi Update(Models.Tamagotchi tama)
        {

            return Tamagotchis.Find(e => e.Id == tama.Id);
        }

        public List<Models.Tamagotchi> UpdateRange(List<Models.Tamagotchi> tamagotchis)
        {
            List<Models.Tamagotchi> updated = new List<Models.Tamagotchi>();
            foreach (Models.Tamagotchi t in tamagotchis)
            {
                updated.Add(Update(t));
            }
            return updated;
        }

        public List<Models.Tamagotchi> Get()
        {
            return Tamagotchis;
        }

        public List<Tamagotchi> GetAll()
        {
            return Tamagotchis;
        }

        void ITamagotchiRepository.Add(Tamagotchi tama)
        {
            tama.Alive = true;
            tama.Health = 100;
            tama.Money = 100;
            Tamagotchis.Add(tama);
        }

        void ITamagotchiRepository.Update(Tamagotchi tama)
        {
            //
        }

        public void UpdateMultiple(List<Tamagotchi> tama)
        {
            //
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
            if (t.Health > 100)
            {
                t.Health = 100;
            }
        }
    }
}