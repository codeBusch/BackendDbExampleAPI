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

public class CardTypesController : Controller
{
	private ICardTypeRepository _cardTypeRepository;

	public CardTypesController(ICardTypeRepository cardTypeRepository)
	{
		_cardTypeRepository = cardTypeRepository;
	}

	[HttpGet("GetAll")]
	public IActionResult GetAll()
	{
		return Ok(_cardTypeRepository.GetAll());
	}
	[HttpGet("GetAllWithCards")]
	public IActionResult GetAllWithCardType()
	{
		return Ok(_cardTypeRepository.GetAll(include: cardTypes => cardTypes.Include(ct => ct.Name)));
	}

	[HttpGet("GetById/{id}")]
	public IActionResult Get(Guid id)
	{
		return Ok(_cardTypeRepository.Get(cardTypes => cardTypes.Id == id));
	}

	[HttpPost("Add")]
	public IActionResult Add([FromBody] CardType cardType)
	{
		return Ok(_cardTypeRepository.Add(cardType));
	}

	[HttpPut("Update")]
	public IActionResult Update([FromBody] CardType cardType)
	{
		return Ok(_cardTypeRepository.Update(cardType));
	}

	[HttpDelete("DeleteById/{id}")]
	public IActionResult Delete(Guid id)
	{
		var cardType = _cardTypeRepository.Get(cardType => cardType.Id == id);
		if (cardType == null) return BadRequest("Card not found");
		return Ok(_cardTypeRepository.Delete(cardType));
	}
}

