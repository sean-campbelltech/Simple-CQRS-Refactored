using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CQRS.Gui.Models;
using CQRS.Simple.Bus;
using CQRS.Simple.Repositories;
using System;
using CQRS.Simple.Commands;

namespace CQRS.Gui.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogger<HomeController> _logger;
        private readonly IReadModelFacade _readmodel;

        public HomeController(
            ICommandDispatcher commandDispatcher,
            ILogger<HomeController> logger,
            IReadModelFacade readmodel)
        {
            _commandDispatcher = commandDispatcher;
            _readmodel = readmodel;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData.Model = _readmodel.GetInventoryItems();
            return View();
        }

        public ActionResult Details(Guid id)
        {
            ViewData.Model = _readmodel.GetInventoryItemDetails(id);
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(string name)
        {
            _commandDispatcher.Send(new CreateItemCommand(Guid.NewGuid(), name));

            return RedirectToAction("Index");
        }

        public ActionResult ChangeName(Guid id)
        {
            ViewData.Model = _readmodel.GetInventoryItemDetails(id);
            return View();
        }

        [HttpPost]
        public ActionResult ChangeName(Guid id, string name, int version)
        {
            var command = new RenameItemCommand(id, name, version);
            _commandDispatcher.Send(command);

            return RedirectToAction("Index");
        }

        public ActionResult Deactivate(Guid id)
        {
            ViewData.Model = _readmodel.GetInventoryItemDetails(id);
            return View();
        }

        [HttpPost]
        public ActionResult Deactivate(Guid id, int version)
        {
            _commandDispatcher.Send(new DeactivateItemCommand(id, version));
            return RedirectToAction("Index");
        }

        public ActionResult CheckIn(Guid id)
        {
            ViewData.Model = _readmodel.GetInventoryItemDetails(id);
            return View();
        }

        [HttpPost]
        public ActionResult CheckIn(Guid id, int number, int version)
        {
            _commandDispatcher.Send(new CheckInItemsCommand(id, number, version));
            return RedirectToAction("Index");
        }

        public ActionResult Remove(Guid id)
        {
            ViewData.Model = _readmodel.GetInventoryItemDetails(id);
            return View();
        }

        [HttpPost]
        public ActionResult Remove(Guid id, int number, int version)
        {
            _commandDispatcher.Send(new RemoveItemsCommand(id, number, version));
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
