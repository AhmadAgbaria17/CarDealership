using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Person;
using dotnetbackend.IRepository;
using dotnetbackend.IServices;
using dotnetbackend.Mappers;
using dotnetbackend.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace dotnetbackend.Services
{
  public class PersonService : IPersonService
  {
    private readonly IPersonRepository _personRepository;
    public PersonService(IPersonRepository personRepository)
    {
      _personRepository = personRepository;
    }

    public async Task<PersonDto> CreateAsync(CreatePersonRequest personDto)
    {
      var person = personDto.ToPersonFromCreateDto();
      await _personRepository.CreateAsync(person);
      return person.ToPersonDto();
    }

    public async Task<bool> DeleteAsync(int id)
    {
      return await _personRepository.DeleteAsync(id);;
    }

    public async Task<List<PersonDto>> GetAllAsync()
    {
      var persons = (await _personRepository.GetAllAsync())
      .Select(p => p.ToPersonDto())
      .ToList();
      return persons;
    }

    public async Task<PersonDto?> GetByIdAsync(int id)
    {
      var person = (await _personRepository.GetByIdAsync(id))
      ?.ToPersonDto();
      return person;

    }

    public async Task<PersonDto?> UpdateAsync(int id, UpdatePersonRequest personDto)
    {
      var updatedPerson = await _personRepository.UpdateAsync(id, personDto);
      return updatedPerson?.ToPersonDto();
    }
  }
}