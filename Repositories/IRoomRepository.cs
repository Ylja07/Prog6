using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Prog6.Models;

namespace Prog6.Repositories
{
    public interface IRoomRepository
    {
      
        Models.Room Get(int? RoomId);
        List<Models.Room> GetAll();
        void Delete(Models.Room room);
        void Add(Models.Room room);
        void Update(Models.Room room);
        void Night(Room r);
    }
}