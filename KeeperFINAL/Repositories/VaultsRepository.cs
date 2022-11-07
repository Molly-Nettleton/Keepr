namespace KeeperFINAL.Repositories;

public class VaultsRepository : BaseRepository
{
  public VaultsRepository(IDbConnection db) : base(db)
  { }

  public Vault CreateVault(Vault vaultData)
  {
    var sql = @"
              INSERT INTO
              vaults (
                creatorId,
                name,
                description,
                img
               
              )
              VALUES (
                @CreatorId,
                @Name,
                @Description,
                @Img
                
              );
              SELECT LAST_INSERT_ID()
                  ; ";
    vaultData.CreatedAt = DateTime.Now;
    vaultData.UpdatedAt = DateTime.Now;
    int vaultId = _db.ExecuteScalar<int>(sql, vaultData);
    vaultData.Id = vaultId;
    return vaultData;
  }

  internal Vault GetVaultById(int vaultId)
  {
    var sql = @"
          SELECT 
            v.*,
            a.*
          FROM vaults v
          JOIN accounts a ON a.id = v.creatorId
          WHERE v.id =  @vaultId 
          ;";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { vaultId }).FirstOrDefault();
  }

  internal Vault EditVault(Vault vaultData)
  {
    string sql = @" 
    UPDATE vaults 
    SET
    name = @name,
    description = @description,
    img = @img
    WHERE
    id = @Id LIMIT 1

    ;";
    int rowsAffected = _db.Execute(sql, vaultData);
    if (rowsAffected == 0)
    {
      throw new Exception("Unable to edit.");
    }
    return vaultData;
  }

  internal void DeleteVault(int vaultId)
  {
    var sql = @"
      DELETE FROM vaults WHERE id = @vaultId
      ; ";
    var rows = _db.Execute(sql, new { vaultId });
    if (rows != 1)
    { throw new Exception("Bad data."); }
    return;
  }
}