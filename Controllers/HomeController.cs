using Ninject;
using Prog6.Models;
using Prog6.Repositories;
using Prog6.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prog6.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ITamagotchiRepository _tamagotchiRepository;
        [Inject]
        public HomeController(IRoomRepository roomRepository, ITamagotchiRepository tamagotchiRepository)
        {
            _roomRepository = roomRepository;
            _tamagotchiRepository = tamagotchiRepository;
        }
        public HomeController()
        {
            _roomRepository = new RoomRepository(new TamagotchiEntities());
            _tamagotchiRepository = new TamagotchiRepository(new TamagotchiEntities());
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Book(int Id)
        {
            var tamas = _tamagotchiRepository.GetAll().Where(e => e.Alive && e.Room == null && e.Money >= _roomRepository.Get(Id).Cost);

            ViewBag.Tamagotchis = new SelectList(tamas, "Id", "Name", _roomRepository.Get(Id).Tamagotchi);

            return View(new BookingViewModel(_roomRepository.Get(Id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book([Bind(Include = "Room,Tamagotchis")] BookingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                foreach (string i in viewModel.Tamagotchis)
                {
                    viewModel.Room.Tamagotchi.Add(_tamagotchiRepository.Get(int.Parse(i)));
                    _tamagotchiRepository.Get(int.Parse(i)).Room = viewModel.Room.Id;
                    _tamagotchiRepository.Get(int.Parse(i)).Room1 = viewModel.Room;
                    _tamagotchiRepository.Update(_tamagotchiRepository.Get(int.Parse(i)));
                }
                _roomRepository.Update(viewModel.Room);
                return RedirectToAction("Index");
            }
            IEnumerable<SelectListItem> basetypes = _tamagotchiRepository.GetAll().Select(
                b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name });
            ViewData["tamagotchis"] = basetypes;
            return View();
        }
        public ActionResult Booking()
        {
            return View(_roomRepository.GetAll());
        }

        public ActionResult PerformNight()
        {
            var rooms = _roomRepository.GetAll();
            foreach (Room r in rooms)
            {
                _roomRepository.Night(r);
            }
            var tama = _tamagotchiRepository.GetAll();
            foreach (Tamagotchi t in tama)
            {
                _tamagotchiRepository.StandardNight(t);
            }
            return RedirectToAction("Index");
        }
    }
}