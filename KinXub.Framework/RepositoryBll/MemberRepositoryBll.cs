using KinXub.Models;

namespace KinXub.Framework
{
    public class MemberRepositoryBll: RepositoryDal.Dal<Member>
    {
        /*  
            新增的方法請寫在這裡！
            ex：
            public List<Model> GetModelList()
            {
                using (IDbConnection conn = OpenConnection())
                {
                    var pList = conn.Query<Model>(
                                @"SELECT *
                                  FROM Table t").ToList();
                    return pList;
                }
            }
         */
    }
}
