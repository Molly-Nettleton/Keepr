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
          WHERE v.id =  @id 
          ;";
    return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
    {
      vault.Creator = profile;
      return vault;
    }, new { vaultId }).FirstOrDefault();
  }
}