using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetbackend.Helpers
{
  public class CDHQueryObject
  {
    public String? Name { get; set; } = string.Empty;
    public String? City { get; set; } = string.Empty;
    public string? SortBy { get; set; } = string.Empty;
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

  }

  public class CQueryObject
  {
    public String? Company { get; set; } = string.Empty;
    public String? ModelName { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
  }
}