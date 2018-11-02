using SmoONE.Domain;
using SmoONE.Domain.IRepository;
using SmoONE.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoONE.Repository
{
    // ******************************************************************
    // 文件版本： SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // 创建时间： 2016/11
    // 主要内容：  请假类型仓储的实现,仅用于查询
    // ******************************************************************

    /// <summary>
    /// 请假类型仓储的实现,仅用于查询
    /// </summary>
    public class ProfileTypeRepository: BaseRepository<ProfileType>, IProfileTypeRepository
    {
        /// <summary>
        /// 仓储类的构造函数
        /// </summary>
        /// <param name="dbContext">数据库上下文</param>
        public ProfileTypeRepository(IDbContext dbContext)
            : base(dbContext)
        { }

        /// <summary>
        /// 根据请假类型ID返回请假类型对象
        /// </summary>
        /// <param name="ID">请假类型ID</param>
        public ProfileType GetByID(string ID)
        {
            return _entities.Where(x => x.PT_ID == ID).FirstOrDefault();
        }

    }
}


