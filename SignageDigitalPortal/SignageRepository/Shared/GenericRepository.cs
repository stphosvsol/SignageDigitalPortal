using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using Infrastructure.JqGrid;
using Infrastructure.JqGrid.Model;
using SignageRepository.Database;

namespace SignageRepository.Shared
{
    public class GenericRepository<TEntity> : BaseRepository where TEntity : class
    {
        protected readonly DbSet DbSet;

        public GenericRepository(SignageDigitalEntities db) : base(db)
        {
            DbSet = Db.Set(typeof (TEntity));
        }

        public GenericRepository() : base(new SignageDigitalEntities())
        {
            DbSet = Db.Set(typeof (TEntity));
        }

        public TEntity FindById(object id)
        {
            try
            {
                return (TEntity) DbSet.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Add(TEntity model, bool bSaveChanges = true)
        {
            DbSet.Add(model);
            if (bSaveChanges) Db.SaveChanges();
        }

        public void Update(TEntity model, bool bSaveChanges = true)
        {
            DbSet.Attach(model);
            Db.Entry(model).State = EntityState.Modified;
            if (bSaveChanges) Db.SaveChanges();
        }

        public void Delete(Object id, bool bSaveChanges = true)
        {
            var model = DbSet.Find(id);
            if(model == null)
                return;

            DbSet.Remove(model);
            if (bSaveChanges) Db.SaveChanges();
        }

        public void Delete(TEntity model, bool bSaveChanges = true)
        {
            DbSet.Attach(model);
            DbSet.Remove(model);
            if (bSaveChanges) Db.SaveChanges();
        }

        public JqGridResultModel JqGridFindBy(JqGridFilterModel opts, String sKey, String sColumns,
            Expression<Func<TEntity, bool>> extraFilter = null)
        {
            try
            {
                var result = new JqGridResultModel();
                var query = (IQueryable<TEntity>)DbSet;

                if (opts._search)
                {
                    var filterDyn = new JavaScriptSerializer().Deserialize<JqGridMultipleFilterModel>(opts.filters);
                    var whereOpts = JqGridClause.ExpressionToExec(filterDyn);
                    query = query.Where(whereOpts.Query, whereOpts.ArrParams);
                }

                if (extraFilter != null)
                {
                    query = query.Where(extraFilter);
                }

                if (String.IsNullOrWhiteSpace(opts.sidx) == false && String.IsNullOrWhiteSpace(opts.sord) == false)
                    query = query.OrderBy(JqGridClause.OrderBy(opts));

                result.total = 0;
                result.page = 1;
                result.records = query.Count();

                if (opts.page.HasValue && opts.rows.HasValue)
                {
                    query = query.Skip((opts.page.Value - 1)*opts.rows.Value).Take(opts.rows.Value);
                    result.page = opts.page.Value;
                    result.total = (int) Math.Ceiling((double) result.records/opts.rows.Value);
                }

                dynamic qSel = query.Select(String.Format("New({0})", sColumns));
                var lstObj = new List<DynamicClass>(qSel);

                if (lstObj.Count == 0)
                {
                    result.rows = new List<JqGridRowsModel>();
                }
                else
                {
                    var prop = lstObj[0].GetType().GetProperty(sKey);
                    result.rows = lstObj.Select(e => new JqGridRowsModel {id = prop.GetValue(e), cell = e}).ToList();
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public List<TEntity> FindBy(Expression<Func<TEntity, bool>> exp)
        {
            var query = (IQueryable<TEntity>) DbSet;
            return query.Where(exp).ToList();
        }
    }
}
