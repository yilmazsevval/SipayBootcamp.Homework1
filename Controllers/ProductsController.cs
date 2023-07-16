using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using RestfulApiExample.Models;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private static List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Ürün 1", Price = 10.99m, StockQuantity = 100 },
        new Product { Id = 2, Name = "Ürün 2", Price = 20.49m, StockQuantity = 50 },
        new Product { Id = 3, Name = "Ürün 3", Price = 5.99m, StockQuantity = 200 },
    };

    // GET: api/products
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_products);
    }

    // GET: api/products/1
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public IActionResult Post([FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);

        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    // PUT: api/products/1
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingProduct = _products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
            return NotFound();

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.StockQuantity = product.StockQuantity;

        return NoContent();
    }

    // DELETE: api/products/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product == null)
            return NotFound();

        _products.Remove(product);

        return NoContent();
    }

    // PATCH: api/products/1
    [HttpPatch("{id}")]
    public IActionResult Patch(int id, [FromBody] Product product)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingProduct = _products.FirstOrDefault(p => p.Id == id);
        if (existingProduct == null)
            return NotFound();

        // Apply partial updates (only fields that are present in the request)
        if (product.Name != null)
            existingProduct.Name = product.Name;

        if (product.Price != 0)
            existingProduct.Price = product.Price;

        if (product.StockQuantity != 0)
            existingProduct.StockQuantity = product.StockQuantity;

        return NoContent();
    }

    // GET: api/products/list?name=abc
    [HttpGet("list")]
    public IActionResult List(string name)
    {
        var filteredProducts = _products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        return Ok(filteredProducts);
    }
}