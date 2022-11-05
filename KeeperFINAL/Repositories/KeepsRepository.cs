namespace KeeperFINAL.Repositories;
public class KeepsRepository : BaseRepository
{
  public KeepsRepository(IDbConnection db) : base(db)
  { }

  internal Keep CreateKeep(Keep keepData)
  {
    var sql = @"
              INSERT INTO
              keeps (
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
    keepData.CreatedAt = DateTime.Now;
    keepData.UpdatedAt = DateTime.Now;
    int keepId = _db.ExecuteScalar<int>(sql, keepData);
    keepData.Id = keepId;
    return keepData;
  }

  internal List<Keep> GetAllKeeps()
  {
    var sql = @"
    SELECT
    k.*,
    COUNT(vk.id) AS Kept,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    LEFT JOIN vaultKeeps vk on vk.keepId = k.id
    GROUP BY k.id
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }).ToList();
  }

  internal Keep EditKeep(Keep keepData)
  {
    string sql = @" 
    UPDATE keeps 
    SET
    name = @name,
    description = @description,
    img = @img,
    views = @views
    WHERE
    id = @Id LIMIT 1

    ;";
    int rowsAffected = _db.Execute(sql, keepData);
    if (rowsAffected == 0)
    {
      throw new Exception("Unable to edit.");
    }
    return keepData;
  }

  internal void DeleteKeep(int keepId)
  {
    var sql = @"
      DELETE FROM keeps WHERE id = @keepId
      ; ";
    var rows = _db.Execute(sql, new { keepId });
    if (rows != 1)
    { throw new Exception("Bad data."); }
    return;

  }

  internal Keep GetKeepById(int keepId)
  {
    var sql = @"
    SELECT
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    WHERE k.id = @keepId
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
    {
      keep.Creator = profile;
      return keep;
    }, new { keepId }).FirstOrDefault();
  }
}
