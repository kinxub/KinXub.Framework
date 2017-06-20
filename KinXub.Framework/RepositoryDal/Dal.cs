using KinXub.Framework.RepositoryDal.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KinXub.Framework.RepositoryDal
{
    public class Dal<T> where T : class, new()
    {
        #region ====連接db資訊====
        private SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(DbList.MSdb);
            connection.Open();
            return connection;
        }
        #endregion

        #region ====查詢====
        /// <summary>
        /// 取得資料表所有資料，預設系統排序
        /// </summary>
        /// <returns></returns>
        public List<T> Get()
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetAll<T>().ToList();
            }
        }

        /// <summary>
        /// (非同步)取得資料表所有資料，預設系統排序
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAsync()
        {
            using (var conn = OpenConnection())
            {
                IEnumerable<T> r = await conn.GetAllAsync<T>();
                return r.ToList();
            }
        }

        /// <summary>
        /// 根據Lambda篩選資料表資料，預設系統排序
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public List<T> Get(Expression<Func<T, bool>> whereExpression)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetAll<T>().AsQueryable().Where(whereExpression).ToList();
            }
        }

        /// <summary>
        /// (非同步)根據Lambda篩選資料表資料，預設系統排序
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> whereExpression)
        {
            using (IDbConnection conn = OpenConnection())
            {
                IEnumerable<T> r = await conn.GetAllAsync<T>();
                return r.AsQueryable().Where(whereExpression).ToList();
            }
        }

        /// <summary>
        /// 取得資料表第一筆資料，預設系統排序
        /// </summary>
        /// <returns></returns>
        public T GetFirst()
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetAll<T>().FirstOrDefault();
            }
        }

        /// <summary>
        /// (非同步)取得資料表第一筆資料，預設系統排序
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetFirstAsync()
        {
            using (IDbConnection conn = OpenConnection())
            {
                IEnumerable<T> r = await conn.GetAllAsync<T>();
                return r.FirstOrDefault();
            }
        }

        /// <summary>
        /// 根據Lambda篩選資料表取得第一筆資料，預設系統排序
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public T GetFirst(Expression<Func<T, bool>> whereExpression)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.GetAll<T>().AsQueryable().Where(whereExpression).FirstOrDefault();
            }
        }

        /// <summary>
        /// (非同步)根據Lambda篩選資料表取得第一筆資料，預設系統排序
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression)
        {
            using (IDbConnection conn = OpenConnection())
            {
                IEnumerable<T> r = await conn.GetAllAsync<T>();
                return r.AsQueryable().Where(whereExpression).FirstOrDefault();
            }
        }

        /// <summary>
        /// 取得資料表資料根據KEY_ID，預設系統排序
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T GetByID(int ID)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return conn.Get<T>(ID);
            }
        }

        /// <summary>
        /// (非同步)取得資料表資料根據KEY_ID，預設系統排序
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public async Task<T> GetByIDAsync(int ID)
        {
            using (IDbConnection conn = OpenConnection())
            {
                return await conn.GetAsync<T>(ID);
            }
        }
        #endregion
        #region ====新增====
        /// <summary>
        /// 新增資料，限定KEY含ID
        /// </summary>
        /// <param name="entityToInsert"></param>
        /// <param name="transaction"></param>
        /// <returns>回傳0 新增失敗</returns>
        public long Insert(T entityToInsert, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                long returnVal = 0;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = conn.Insert(entityToInsert, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = conn.Insert(entityToInsert);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// (非同步)新增資料，限定KEY含ID
        /// </summary>
        /// <param name="entityToInsert"></param>
        /// <param name="transaction"></param>
        /// <returns>回傳0 新增失敗</returns>
        public async Task<long> InsertAsync(T entityToInsert, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                long returnVal = 0;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = await conn.InsertAsync(entityToInsert, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = await conn.InsertAsync(entityToInsert);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// 新增多筆資料，限定KEY含ID
        /// </summary>
        /// <param name="entityToInsert"></param>
        /// <param name="transaction"></param>
        /// <returns>回傳0 新增失敗</returns>
        public long Insert(List<T> entityToInsert, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                long returnVal = 0;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = conn.Insert(entityToInsert, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = conn.Insert(entityToInsert);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// (非同步)新增多筆資料，限定KEY含ID
        /// </summary>
        /// <param name="entityToInsert"></param>
        /// <param name="transaction"></param>
        /// <returns>回傳0 新增失敗</returns>
        public async Task<long> InsertAsync(List<T> entityToInsert, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                long returnVal = 0;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = await conn.InsertAsync(entityToInsert, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = await conn.InsertAsync(entityToInsert);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }
        #endregion
        #region ====修改====
        /// <summary>
        /// 修改資料，根據KEY
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Update(T entityToUpdate, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = conn.Update(entityToUpdate, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = conn.Update(entityToUpdate);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// (非同步)修改資料，根據KEY
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(T entityToUpdate, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = await conn.UpdateAsync(entityToUpdate, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = await conn.UpdateAsync(entityToUpdate);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// 修改各別欄位資料，根據KEY
        /// <para>object EX：new { [Key] = Key, ... }</para>
        /// <para>[Key] 一定要填寫，這是where的條件，會排除更新</para>
        /// <para>ex：new { [Key] = Key, ...} </para>
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Update(object entityToUpdate, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = conn.Update<T>(entityToUpdate, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = conn.Update<T>(entityToUpdate);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// (非同步)修改各別欄位資料，根據KEY
        /// <para>object EX：new { [Key] = Key, ... }</para>
        /// <para>[Key] 一定要填寫，這是where的條件，會排除更新</para>
        /// <para>ex：new { [Key] = Key, ...}</para>
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(object entityToUpdate, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = await conn.UpdateAsync<T>(entityToUpdate, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = await conn.UpdateAsync<T>(entityToUpdate);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// 修改多筆資料，根據KEY
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Update(List<T> entityToUpdate, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = conn.Update(entityToUpdate, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = conn.Update(entityToUpdate);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// (非同步)修改多筆資料，根據KEY
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(List<T> entityToUpdate, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = await conn.UpdateAsync(entityToUpdate, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = await conn.UpdateAsync(entityToUpdate);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }
        #endregion
        #region ====刪除====
        /// <summary>
        /// 刪除資料，根據KEY
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Delete(T entityToDelete, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = conn.Delete(entityToDelete, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = conn.Delete(entityToDelete);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// (非同步)刪除資料，根據KEY
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(T entityToDelete, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = await conn.DeleteAsync(entityToDelete, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = await conn.DeleteAsync(entityToDelete);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// 刪除資料，根據KEY
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Delete(List<T> entityToDelete, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = conn.Delete(entityToDelete, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = conn.Delete(entityToDelete);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// (非同步)刪除資料，根據KEY
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(List<T> entityToDelete, bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = await conn.DeleteAsync(entityToDelete, tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = await conn.DeleteAsync(entityToDelete);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// 刪除資料表全部資料
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool DeleteAll(bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = conn.DeleteAll<T>(tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = conn.DeleteAll<T>();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }

        /// <summary>
        /// (非同步)刪除資料表全部資料
        /// </summary>
        /// <param name="entityToDelete"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAllAsync(bool transaction = false)
        {
            using (IDbConnection conn = OpenConnection())
            {
                bool returnVal = false;
                if (transaction)
                {
                    using (IDbTransaction tran = conn.BeginTransaction())
                    {
                        try
                        {
                            returnVal = await conn.DeleteAllAsync<T>(tran);
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                }
                else
                {
                    try
                    {
                        returnVal = await conn.DeleteAllAsync<T>();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return returnVal;
            }
        }
        #endregion
    }
}
