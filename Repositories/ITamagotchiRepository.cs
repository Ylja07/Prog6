using Prog6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog6.Repositories
{
    public interface ITamagotchiRepository
    {

        Tamagotchi Get(int? TamaId);
        List<Tamagotchi> GetAll();
        void Delete(Tamagotchi tama);
        void Add(Tamagotchi tama);
        void Update(Tamagotchi tama);
        void UpdateMultiple(List<Tamagotchi> tama);
        void StandardNight(Tamagotchi t);
    }
}
