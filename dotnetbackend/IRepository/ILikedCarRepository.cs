using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetbackend.models;

namespace dotnetbackend.IRepository
{
  public interface ILikedCarRepository
  {
    Task<List<Car>> GetPersonLikedCar(Person person);
    Task<bool> LikeCarAsync(Person person, int carId);
    Task<bool> UnlikeCarAsync(Person person, int carId);
    }
}