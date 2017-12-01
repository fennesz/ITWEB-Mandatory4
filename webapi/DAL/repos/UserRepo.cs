using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webapi.DAL.models;

namespace webapi.DAL.repos
{
  public class UserRepo
  {
    protected readonly ApplicationContext context;
    protected DbSet<User> entities;
    string errorMessage = string.Empty;

    public UserRepo(ApplicationContext context)
    {
      this.context = context;
      entities = context.Set<User>();
    }

    public IEnumerable<User> GetAll()
    {
      return entities;
    }

    public User Get(string id)
    {
      return entities.SingleOrDefault(e => e._id == id);
    }

    public void Insert(User entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }
      var insertedEntity = entities.Add(entity);
      context.SaveChanges();
    }

    public void Update(User entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }
      context.SaveChanges();
    }

    public void Delete(User entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }
      entities.Remove(entity);
      context.SaveChanges();
    }

    public IEnumerable<User> Find(Func<User, bool> predicate)
    {
      if (predicate == null)
      {
        throw new ArgumentNullException("entity");
      }
      return entities.Where(predicate);
    }
  }
}
