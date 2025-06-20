using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.Dtos.Person;
using dotnetbackend.models;

namespace dotnetbackend.Mappers
{
  public static class PersonMappers
  {
    public static PersonDto ToPersonDto(this Person person)
    {
      return new PersonDto
      {
        Id = person.Id,
        Email = person.Email,
        Password = person.Password,
        FirstName = person.FirstName,
        LastName = person.LastName,
        Phone = person.Phone
      };
    }

    public static Person ToPersonFromCreateDto(this CreatePersonRequest personDto)
    {
      return new Person
      {
        Email = personDto.Email,
        Password = personDto.Password,
        FirstName = personDto.FirstName,
        LastName = personDto.LastName,
        Phone = personDto.Phone
      };

    }
  }
}