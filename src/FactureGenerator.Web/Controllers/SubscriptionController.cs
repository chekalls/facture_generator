using FactureGenerator.Application.Features.Users.Commands;
using FactureGenerator.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FactureGenerator.Web.Controllers;

public class SubscriptionController : Controller
{
    private readonly ISender _sender;

    public SubscriptionController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Index), model);
        }

        var command = new RegisterUserCommand(
                model.Email,
                model.Password,
                model.FirstName,
                model.LastName,
                model.CompanyName,
                model.CompanyAddress,
                model.CompanyNif,
                model.CompanyStat);
        var result = await _sender.Send(
            command,
            cancellationToken);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(nameof(Index), model);
        }

        TempData["SuccessMessage"] = "Votre compte et votre société ont été créés. Vous pouvez maintenant vous connecter.";
        return RedirectToAction(nameof(Index));
    }
}
