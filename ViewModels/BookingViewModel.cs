using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prog6.ViewModels
{
    public class BookingViewModel
    {

        public Models.Room Room { get; set; }

        public IEnumerable<string> Tamagotchis { get; set; }

        public BookingViewModel(Models.Room room)
        {
            Room = room;
            Tamagotchis = new List<string>();
        }

        public BookingViewModel()
        {
            Tamagotchis = new List<string>();
        }

    }
}