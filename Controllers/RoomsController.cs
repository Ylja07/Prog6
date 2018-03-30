using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Prog6.Models;
using Prog6.Repositories;

namespace Prog6.Controllers
{
    public class RoomsController : Controller
    {
        private IRoomRepository _roomRepository;
        [Inject]
        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public RoomsController()
        {
            _roomRepository = new RoomRepository(new TamagotchiEntities());
        }

        // GET: Rooms
        public ActionResult Index()
        {
            return View(_roomRepository.GetAll());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _roomRepository.Get(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.RoomSizes = RoomSizes();
            return View();
        }

        private SelectList RoomSizes()
        {
            var selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem
            {
                Value = "2",
                Text = "2",
            });
            selectList.Add(new SelectListItem
            {
                Value = "3",
                Text = "3",
            });
            selectList.Add(new SelectListItem
            {
                Value = "5",
                Text = "5",
            });
            return new SelectList(selectList);
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Size")] Room room)
        {
            if (ModelState.IsValid && (room.Size == 2 || room.Size == 3 || room.Size == 5)  && room.Type != null && (room.Type == "Weeb" || room.Type == "Game" || room.Type == "Werk" || room.Type == "Rust" || room.Type == "Vecht"))
            {
                _roomRepository.Add(room);
                return RedirectToAction("Index");
            }

            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _roomRepository.Get(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomSizes = RoomSizes();
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Size,Cost")] Room room)
        {
            if (ModelState.IsValid && (room.Size == 2 || room.Size == 3 || room.Size == 5) && room.Type != null && (room.Type == "Weeb" || room.Type == "Game" || room.Type == "Werk" || room.Type == "Rust" || room.Type == "Vecht"))
            {
                _roomRepository.Update(room);
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _roomRepository.Get(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _roomRepository.Delete(_roomRepository.Get(id));
            return RedirectToAction("Index");
        }
    }
}
