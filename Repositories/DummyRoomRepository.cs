using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prog6.Models;

namespace Prog6.Repositories
{
    public class DummyRoomRepository : IRoomRepository
    {
        List<Models.Room> Rooms { get; set; }
        public DummyRoomRepository()
        {
            Rooms = new List<Models.Room>();

            Rooms.Add(new Models.Room
            {
                Type = "Rust",
                Id = 1,
                Size = 5,
            });
            Rooms.Add(new Models.Room
            {
                Type = "Vecht",
                Id = 2,
                Size = 3,
            });
            Rooms.Add(new Models.Room
            {
                Type = "Weeb",
                Id = 3,
                Size = 2,
            });
            Rooms.Add(new Models.Room
            {
                Type = "Werk",
                Id = 4,
                Size = 5,
            });
            Rooms.Add(new Models.Room
            {
                Type = "Game",
                Id = 5,
                Size = 2,
            });
        }
        public Models.Room Get(int? index)
        {
            return Rooms.Find(x => x.Id == index);
        }

        public List<Room> Get()
        {
            return Rooms;
        }

        public Room Add(Room Room)
        {
            Rooms.Add(Room);
            return Room;
        }

        public Room Update(Room Room)
        {
            return Room;
        }

        public void Delete(Room Room)
        {
            Rooms.Remove(Room);
        }

        public List<Room> GetAll()
        {
            return Rooms;
        }

        void IRoomRepository.Add(Room room)
        {
            Rooms.Add(room);
        }

        void IRoomRepository.Update(Room room)
        {
            //
        }

        public void Night(Room r)
        {
            var tama = r.Tamagotchi;
            var fightingTama = new List<Tamagotchi>();
            foreach (Tamagotchi t in tama)
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
                        t.Boredom += 20; 
                        break;
                }
            }
            Fight(fightingTama);
        }

        private void Fight(List<Tamagotchi> fightingTama)
        {
            var tamas = fightingTama.ToArray();
            int w = new Random().Next(0, tamas.Length);
            for (int i = 0; i < tamas.Length; i++)
            {
                if (i == w)
                {
                    tamas[i].Level++;
                    tamas[i].Money += 20 * (tamas.Length - 1);
                }
                else
                {
                    tamas[i].Money -= 20;
                    tamas[i].Health -= 30;
                }
            }
        }
    }
}