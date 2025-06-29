using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    // GET all action
    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() => PizzaService.GetAll();

    // GET by Id action
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Pizza), 200)]
    [ProducesResponseType(404)]
    public ActionResult<Pizza?> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        if (pizza is null)
        {
            return NotFound();
        }
        return pizza;
    }

    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {
        // This code will save the pizza and return a result
        PizzaService.Add(pizza);
        return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id) return BadRequest();

        var pizzaToUpdate = PizzaService.Get(id);
        if (pizzaToUpdate is null) return NotFound();

        PizzaService.Update(pizza);
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizzaToDelete = PizzaService.Get(id);

        if (pizzaToDelete is null) return NotFound();

        PizzaService.Delete(id);
        
        return NoContent();
    }
}