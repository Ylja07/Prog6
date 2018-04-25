using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Prog6.Models;

namespace Prog6.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly TamagotchiEntities _tamagotchiEntities;
        public RoomRepository(TamagotchiEntities dbContext)
        {
            _tamagotchiEntities = dbContext;
        }
        public void Add(Room room)
        {
        
            _tamagotchiEntities.Room.Add(room);
            _tamagotchiEntities.SaveChanges();
        }

        public void Delete(Room room)
        {
            _tamagotchiEntities.Room.Remove(room);
            _tamagotchiEntities.SaveChanges();
        }

        public Room Get(int? RoomId)
        {
            return _tamagotchiEntities.Room.Find(RoomId);
        }

        public List<Room> GetAll()
        {
            return _tamagotchiEntities.Room.ToList();
        }

        public void Night(Room r)
        {
            var tama = r.Tamagotchi;
            var fightingTama = new List<Tamagotchi>();
            foreach(Tamagotchi t in tama)
            {
                switch (r.Type)
                {
                    case "Rust":
                        t.Money -= 10;
                        t.Health += 20;
                        t.Boredom += 10;
                        break;
                    case "Game":
                        t.Money -= 20;
                        t.Boredom = 0;
                        break;
                    case "Weeb":
                        t.Money -= 30;
                        t.Health -= 20;
                        t.Boredom = 0;
                        t.Level += 9001;
                        break;
                    case "Vecht":
                        fightingTama.Add(t);
                        break;
                    case "Werk":
                        t.Money += new Random().Next(10, 60);
                        t.Health += 20;
                        t.Boredom += 10;
                        break;
                }
                _tamagotchiEntities.Entry(t).State = EntityState.Modified;
            }
            r.Tamagotchi.Clear();
            _tamagotchiEntities.SaveChanges();
            Fight(fightingTama);
        }

        private void Fight(List<Tamagotchi> fightingTama)
        {
            var tamas = fightingTama.ToArray();
            int w = new Random().Next(0, tamas.Length);
            for(int i = 0; i < tamas.Length; i++)
            {
                if(i == w)
                {
                    tamas[i].Level++;
                    tamas[i].Money += 20 * (tamas.Length-1);
                } else
                {
                    tamas[i].Money -= 20;
                    tamas[i].Health -= 30;
                }
                _tamagotchiEntities.Entry(tamas[i]).State = EntityState.Modified;
            }
            _tamagotchiEntities.SaveChanges();
        }

        public void Update(Room room)
        {
            _tamagotchiEntities.Room.AddOrUpdate(room);
            _tamagotchiEntities.SaveChanges();
        }
    }
}