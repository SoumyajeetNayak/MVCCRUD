using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCRUD.Models
{
  [MetadataType(typeof(EmployeeMetaData))]
  public partial class Empployee
  {

  }

  public class EmployeeMetaData
  {
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide FirstName")]
    public string FirstName { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide LastName")]
    public string LastName { get; set; }
  }
}