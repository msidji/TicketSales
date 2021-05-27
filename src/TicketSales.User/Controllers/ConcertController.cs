using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using MassTransit;
using TicketSales.Messages.Commands;
using TicketSales.User.Models;
using TicketSales.User.Services;

namespace TicketSales.User.Controllers
{
    public class ConcertController : Controller
    {
        private readonly IBus _bus;
        private readonly ConcertMessageStore _store;
        private readonly IMapper _mapper;

        public ConcertController(IBus bus, ConcertMessageStore store, IMapper mapper)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _store = store;
            _mapper = mapper;
        }

        // GET: ConcertController
        public ActionResult Index()
        {
            return View(_store.Concerts);
        }

        // GET: ConcertController/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            return View(_store.GetNewTicketForConcert(id));
        }

        // POST: ConcertController/BuyTickets
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuyTickets(NewTicketViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", model);
            }

            var cmd = new BuyTicketsForConcertCommand()
            {
                ConcertId = model.ConcertId,
                Username = model.Username,
                TicketsToBuy = model.TicketsToBuy
            };
            _bus.Send(_mapper.Map<BuyTicketsForConcertCommand>(cmd));

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Index", _store.Concerts);
            }
        }
    }
}