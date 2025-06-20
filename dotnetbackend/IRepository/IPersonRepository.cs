using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Person;
using dotnetbackend.models;

namespace dotnetbackend.IRepository
{
  public interface IPersonRepository
  {
    Task<List<Person>> GetAllAsync();
    Task<Person?> GetByIdAsync(int id);
    Task<Person> CreateAsync(Person person);
    Task<Person?> UpdateAsync(int id, UpdatePersonRequest personDto);
    Task<bool> DeleteAsync(int id);
        
    }
}