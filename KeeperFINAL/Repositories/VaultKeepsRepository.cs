namespace KeeperFINAL.Repositories;

public class VaultKeepsRepository : BaseRepository
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }

  public VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    var sql = @"
              INSERT INTO
              vaultKeeps (
                creatorId, 
                vaultId,
                keepId
              )
              VALUES (
                @CreatorId,
                @VaultId, 
                @KeepId 
                );
              SELECT LAST_INSERT_ID()
                  ; ";
    vaultKeepData.CreatedAt = DateTime.Now;
    vaultKeepData.UpdatedAt = DateTime.Now;
    int vaultKeepId = _db.ExecuteScalar<int>(sql, vaultKeepData);
    vaultKeepData.Id = vaultKeepId;
    return vaultKeepData;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = @"
    SELECT *
    FROM vaultKeeps
    WHERE id = @vaultKeepId
    ;";
    return _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
  }

  internal List<KeptKeep> GetKeepsByVaultId(int vaultId)
  {
    var sql = @"
    SELECT
    vk.*,
    COUNT(k.id) AS Kept,
    vk.id AS VaultKeepId,
    vk.creatorId AS VaultKeepCreatorId,
    k.*,
    a.*
    FROM vaultKeeps vk
    JOIN accounts a ON a.id = vk.creatorId
    JOIN keeps k ON k.id = vk.keepId
    WHERE vk.vaultId = @vaultId
    GROUP BY vk.id
    ;";
    return _db.Query<KeptKeep, Profile, KeptKeep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { vaultId }).ToList();
  }

  internal void DeleteVaultKeep(int vaultKeepId)
  {
    var sql = @"
  DELETE FROM vaultKeeps WHERE id = @vaultKeepId
  ;";
    var rows = _db.Execute(sql, new { vaultKeepId });
    if (rows != 1)
    {
      throw new Exception("Bad ID.");
    }
    return;
  }
}