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
    public class TamagotchisController : Controller
    {
        private ITamagotchiRepository _tamagotchiRepository;
        public TamagotchisController(ITamagotchiRepository tamagotchiRepository)
        {
            _tamagotchiRepository = tamagotchiRepository;
        }

        public TamagotchisController()
        {
            _tamagotchiRepository = new TamagotchiRepository(new TamagotchiEntities());

        }

        // GET: Tamagotchis
        public ActionResult Index()
        {
            var tamagotchi = _tamagotchiRepository.GetAll();
            return View(tamagotchi);
        }

        // GET: Tamagotchis/Create
        public ActionResult Create()
        {
            //ViewBag.Room = new SelectList(db.Room, "Id", "Type"); TODO
            return View();
        }

        // POST: Tamagotchis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Age,Money,Level,Health,Boredom,Alive,Room")] Tamagotchi tamagotchi)
        {
            if (ModelState.IsValid && tamagotchi.Name != null)
            {
                _tamagotchiRepository.Add(tamagotchi);
                return RedirectToAction("Index");
            }
            return View(tamagotchi);
        }

        // GET: Tamagotchis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamagotchi tamagotchi = _tamagotchiRepository.Get(id);
            if (tamagotchi == null)
            {
                return HttpNotFound();
            }
            return View(tamagotchi);
        }

        // POST: Tamagotchis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Age,Money,Level,Health,Boredom,Alive,Room")] Tamagotchi tamagotchi)
        {
            if (ModelState.IsValid)
            {
                var tama = _tamagotchiRepository.Get(tamagotchi.Id);
                tama.Name = tamagotchi.Name;
                _tamagotchiRepository.Update(tama);
                return RedirectToAction("Index");
            }
            return View(tamagotchi);
        }

        // GET: Tamagotchis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tamagotchi tamagotchi = _tamagotchiRepository.Get(id);
            if (tamagotchi == null)
            {
                return HttpNotFound();
            }
            return View(tamagotchi);
        }

        // POST: Tamagotchis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tamagotchi tamagotchi = _tamagotchiRepository.Get(id);
            _tamagotchiRepository.Delete(tamagotchi);
            return RedirectToAction("Index");
        }
    }
}
