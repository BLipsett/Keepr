using System;
using Keepr.Models;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace Keepr.Repositories
{

  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int CreateKeep(Keep keepData)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, creatorId)
      VALUES
      (@Name, @Description, @Img, @CreatorId);
      SELECT LAST_INSERT_ID();
      ";
      return _db.ExecuteScalar<int>(sql, keepData);
    }

    internal int UpdateKeep(Keep keepData)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      img = @Img,
      description = @Description,
      keeps = @Keeps,
      views = @Views,
      shares = @Shares
      WHERE id = @Id;
      ";
      return _db.Execute(sql, keepData);

      // if (rowsAffected > 1)
      // {
      //   throw new Exception("many roes have been deleted");
      // }
    }

    internal Keep GetOne(int id)
    {
      string sql = @"
      SELECT 
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id
      WHERE k.id = @id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, new { id }).FirstOrDefault();
    }

    internal int UpdateKeepStats(Keep keep)
    {
      string sql = @"
      UPDATE keeps
      SET
      keeps = @Keeps,
      views = @Views,
      shares = @Shares
      WHERE id = @Id;
      ";
      return _db.Execute(sql, keep);
    }


    internal List<Keep> GetAll()
    {
      string sql = @"
      SELECT 
      k.* ,
      a.*
      FROM 
      keeps k
      JOIN accounts a ON k.creatorId = a.id;
      ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, splitOn: "id").ToList();
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM keeps
      WHERE id = @id LIMIT 1;
      ";
      _db.Execute(sql, new { id });
    }
  }
}