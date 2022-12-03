using System;
using System.Threading.Tasks;
using MasterChef.Domain.Entities;
using MasterChef.Infra.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MasterChef.Web.Controllers
{
  public class LeadsController : Controller
  {
    private readonly ILeadRepository _leadRepository;

    public LeadsController(ILeadRepository leadRepository)
    {
      _leadRepository = leadRepository;
    }

    // GET: Leads
    public async Task<IActionResult> Index()
    {
      var leads = await _leadRepository.GetAll();
      return View(leads);
    }

    public IActionResult Final()
    {
      return View();
    }

    // GET: Leads/Details/5
    public async Task<IActionResult> Details(int id)
    {
      var lead = await _leadRepository.GetById(id);

      if (lead == null)
        return NotFound();

      return View(lead);
    }

    // GET: Leads/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Leads/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nome,Email,Telefone,Id, Cupom, AceitaPropaganda")]
            Lead lead)
    {
      await _leadRepository.CreateNew(lead);
      return RedirectToAction(nameof(Final));
    }

    // GET: Leads/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
      var lead = await _leadRepository.GetById(id);

      if (lead == null)
        return NotFound();

      return View(lead);
    }

    // POST: Leads/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("DataCadastro, Nome,Email,Telefone,Id")]
            Lead lead)
    {
      if (id != lead.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          lead.LastUpdate = DateTime.Now;
          //_context.Update(lead);
          //await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!LeadExists(lead.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }

        return RedirectToAction(nameof(Index));
      }

      return View(lead);
    }

    // GET: Leads/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
      var lead = await _leadRepository.GetById(id);

      if (lead == null)
        return NotFound();

      return View(lead);
    }

    // POST: Leads/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await _leadRepository.Delete(id);
      return RedirectToAction(nameof(Index));
    }

    private bool LeadExists(int id)
    {
      return _leadRepository.Exists(id);
    }
  }
}