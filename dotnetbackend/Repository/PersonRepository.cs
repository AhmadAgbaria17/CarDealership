using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Data;
using dotnetbackend.Dtos.Person;
using dotnetbackend.IRepository;
using dotnetbackend.models;
using Microsoft.EntityFrameworkCore;

namespace dotnetbackend.Repository
{
  public class PersonRepository : IPersonRepository
  {
    private readonly ApplicationDbContext _context;

    public PersonRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<Person> CreateAsync(Person person)
    {
      await _context.Person.AddAsync(person);
      await _context.SaveChangesAsync();
      return person;
    }

    public async Task<bool> DeleteAsync(int id)
    {
      var person = await _context.Person.FindAsync(id);
      if (person is null)
      {
        return false;
      }
      _context.Person.Remove(person);
      await _context.SaveChangesAsync();
      return true;
    }

    public async Task<List<Person>> GetAllAsync()
    {
      return await _context.Person.ToListAsync();
    }

    public async Task<Person?> GetByIdAsync(int id)
    {
      return await _context.Person.FindAsync(id);
    }

    public async Task<Person?> UpdateAsync(int id, UpdatePersonRequest personDto)
    {
      var personModel = await _context.Person.FindAsync(id);
      if (personModel is null)
      {
        return null;
      }

      personModel.Email = personDto.Email;
      personModel.Password = personDto.Password;
      personModel.FirstName = personDto.FirstName;
      personModel.LastName = personDto.LastName;
      personModel.Phone = personDto.Phone;
      
      await _context.SaveChangesAsync();
      
      return personModel;
    }
      
  }
}