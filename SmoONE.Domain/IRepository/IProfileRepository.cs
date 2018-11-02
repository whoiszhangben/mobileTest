using SmoONE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoONE.Domain.IRepository
{
    // ******************************************************************
    // 文件版本： SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // 创建时间： 2016/11
    // 主要内容：  请假单的仓储接口,仅用于查询
    // ******************************************************************

    /// <summary>
    /// 请假单的仓储接口,仅用于查询
    /// </summary>
    public interface IProfileRepository : IRepository<Profile>
    {
        /// <summary>
        /// 根据档案ID返回档案对象
        /// </summary>
        /// <param name="ID">档案ID</param>
        IQueryable<Profile> GetByID(string ID);

        /// <summary>
        /// 根据是否离职标志返回档案对象
        /// </summary>
        /// <param name="IsDimissioned"></param>
        /// <returns></returns>
        IQueryable<Profile> GetProfilesByStatus(int IsDimissioned);

        /// <summary>
        /// 生成新的档案编号
        /// </summary>
        string GenerateProfileNo();

    }
}