namespace KeeperFINAL.Models;
using KeeperFINAL.Interfaces;
using System.ComponentModel.DataAnnotations;

public class Vault : IHasCreator
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public int KeepCount { get; set; }
  public bool? IsPrivate { get; set; } = false;

  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}