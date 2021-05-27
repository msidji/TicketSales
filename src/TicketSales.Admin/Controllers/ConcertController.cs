using Microsoft.AspNetCore.Mvc;
using System;
using AutoMapper;
using MassTransit;
using TicketSales.Admin.Models;
using TicketSales.Admin.Services;
using TicketSales.Messages.Commands;

namespace TicketSales.Admin.Controllers
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

        public IActionResult Index()
        {
            return View(_store.Concerts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewConcertViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            _bus.Send(_mapper.Map<CreateConcertCommand>(model));

            ModelState.Clear();
            return View("Create");
        }
    }
}