using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.DAL.models;

namespace webapi.DAL.repos
{
  public class WorkoutProgramRepo
  {

    protected readonly ApplicationContext context;
    protected DbSet<WorkoutProgram> entities;
    string errorMessage = string.Empty;

    public WorkoutProgramRepo(ApplicationContext context)
    {
      this.context = context;
      entities = context.Set<WorkoutProgram>();
    }

    public IEnumerable<WorkoutProgram> GetAll()
    {
      return EntitiesWithMembersIncluded()
        .AsEnumerable();
    }

    public WorkoutProgram Get(string id)
    {
      return EntitiesWithMembersIncluded()
          .SingleOrDefault(e => e._id == id);
    }

    public WorkoutProgram Insert(WorkoutProgram entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }
      var insertedEntity = entities.Add(entity);
      context.SaveChanges();
      return insertedEntity.Entity;
    }

    public WorkoutProgram Update(WorkoutProgram entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }
      context.SaveChanges();

      return entity;
    }

    public void Delete(WorkoutProgram entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("entity");
      }
      entities.Remove(entity);
      context.SaveChanges();
    }

    public IEnumerable<WorkoutProgram> Find(Func<WorkoutProgram, bool> predicate)
    {
      if (predicate == null)
      {
        throw new ArgumentNullException("entity");
      }
      return EntitiesWithMembersIncluded()
          .Where(predicate);
    }

    private IQueryable<WorkoutProgram> EntitiesWithMembersIncluded()
    {
      return entities
        .Include(x => x.ExerciseList)
        .Include(x => x.Logs)
        .AsQueryable();
    }
  }
}
