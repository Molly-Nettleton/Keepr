namespace KeeperFINAL.Repositories;
public class ProfilesRepository : BaseRepository
{
  public ProfilesRepository(IDbConnection db) : base(db)
  { }

  internal Profile GetProfileById(string profileId)
  {
    string sql = @"
  SELECT *
  FROM
  accounts
  WHERE 
  id = @profileId
  ;";
    return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
  }

  internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    string sql = @"
  SELECT
  v.*,
  a.*
  FROM vaults v
  JOIN accounts a ON a.id = v.creatorId
  WHERE v.creatorId = @profileId
  and v.isPrivate != 1
  GROUP BY v.id
  ;";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { profileId }).ToList();
  }

  internal List<Keep> GetKeepsByProfileId(string profileId)
  {
    string sql = @"
  SELECT
  k.*,
  COUNT(vaultKeep.id) as Kept,
  a.*
  FROM
  keeps k
  JOIN accounts a ON a.id = k.creatorId
  LEFT JOIN vaultKeeps vaultKeep on vaultKeep.keepId = k.Id
  WHERE k.creatorId = @profileId
  GROUP BY k.id
  ;";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { profileId }).ToList();
  }
}