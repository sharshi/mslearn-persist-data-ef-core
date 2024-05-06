using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext _context;
    private readonly ITenantProvider _tenantProvider;

    public PizzaService(PizzaContext context, ITenantProvider tenantProvider)
    {
        _context = context;
        _tenantProvider = tenantProvider;

        _context.TenantId = _tenantProvider.GetTenantId();
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas
            .AsNoTracking()
            .ToList();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    public Pizza? Create(Pizza newPizza)
    {
        _context.Pizzas.Add(newPizza);
        _context.SaveChanges();
        return newPizza;
    }

    public void AddTopping(int PizzaId, int ToppingId)
    {
        var pizza = _context.Pizzas.Find(PizzaId);
        var topping = _context.Toppings.Find(ToppingId);

        if (pizza is null || topping is null)
        {
            throw new Exception("Pizza or Topping does not exist");
        }

        if (pizza.Toppings is null)
        {
            pizza.Toppings = new List<Topping>();
        }

        pizza.Toppings.Add(topping);

        _context.SaveChanges();
    }

    public void UpdateSauce(int PizzaId, int SauceId)
    {
        var pizza = _context.Pizzas.Find(PizzaId);
        var sauce = _context.Sauces.Find(SauceId);

        if (pizza is null || sauce is null)
        {
            throw new Exception("Pizza or Sauce does not exist");
        }

        pizza.Sauce = sauce;

        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var pizza = _context.Pizzas.Find(id);

        if (pizza is not null)
        {
            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
        }
    }
}