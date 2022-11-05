namespace KeeperFINAL.Models;
using KeeperFINAL.Interfaces;
using System.ComponentModel.DataAnnotations;

public class Keep : IHasCreator
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public int Kept { get; set; }
  public int Views { get; set; } = 0;


  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}