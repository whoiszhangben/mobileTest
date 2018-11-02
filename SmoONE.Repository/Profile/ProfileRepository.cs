using SmoONE.Domain;
using SmoONE.Domain.IRepository;
using SmoONE.DTOs;
using SmoONE.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SmoONE.Repository
{
    // ******************************************************************
    // 文件版本： SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // 创建时间： 2016/11
    // 主要内容：  请假单仓储的实现,仅用于查询
    // ******************************************************************

    /// <summary>
    /// 请假单仓储的实现,仅用于查询
    /// </summary>
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        /// <summary>
        /// 仓储类的构造函数
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        public ProfileRepository(IDbContext dbContext)
            : base(dbContext)
        { }


        public IQueryable<Profile> GetByID(string ID)
        {
            return _entities.Where(x => x.ProfileNo == ID);
        }

        /// <summary>
        /// 根据审批用户ID返回请假单传输对象
        /// </summary>
        /// <param name="UserID">用户ID</param>
        public IQueryable<Profile> GetProfilesByStatus(int IsDimissioned)
        {
            return _entities.Where(x => x.IsDimissioned == IsDimissioned).OrderByDescending(o => o.UpdateTime).AsNoTracking();
        }

        /// <summary>
        /// 得到最大的请假单ID
        /// </summary>
        public string GenerateProfileNo()
        {
            return "";
        }
    }
}

