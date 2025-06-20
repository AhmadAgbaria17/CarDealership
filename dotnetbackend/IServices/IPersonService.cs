using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Person;
using dotnetbackend.models;

namespace dotnetbackend.IServices
{
  public interface IPersonService
  {
    Task<List<PersonDto>> GetAllAsync();
    Task<PersonDto?> GetByIdAsync(int id);
    Task<PersonDto> CreateAsync(CreatePersonRequest personDto);
    Task<PersonDto?> UpdateAsync(int id, UpdatePersonRequest personDto);
    Task<bool> DeleteAsync(int id);
        
    }
}