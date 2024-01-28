using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleAPI.Entities;
using ExampleAPI.Repositories.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleAPI.Controllers;

[Route("api/[controller]")]
public class CardsController : Controller
{
	private ICardRepository _cardRepository;

	public CardsController(ICardRepository cardRepository)
	{
		_cardRepository = cardRepository;
	}

	[HttpGet("GetAll")]
	public IActionResult GetAll()
	{
		return Ok(_cardRepository.GetAll());
	}
	[HttpGet("GetAllWithCardType")]
	public IActionResult GetAllWithCardType()
	{
		return Ok(_cardRepository.GetAll(include: card => card.Include(c => c.CardType)));
	}

	[HttpGet("GetById/{id}")]
	public IActionResult Get(Guid id)
	{
		return Ok(_cardRepository.Get(card => card.Id == id));
	}

	[HttpPost("Add")]
	public IActionResult Add([FromBody] Card card)
	{
		return Ok(_cardRepository.Add(card));
	}

	[HttpPut("Update")]
	public IActionResult Update([FromBody] Card card)
	{
		return Ok(_cardRepository.Update(card));
	}

	[HttpDelete("DeleteById/{id}")]
	public IActionResult Delete(Guid id)
	{
		var card = _cardRepository.Get(card => card.Id == id);
		if (card == null) return BadRequest("Card not found");
		return Ok(_cardRepository.Delete(card));
	}
}
