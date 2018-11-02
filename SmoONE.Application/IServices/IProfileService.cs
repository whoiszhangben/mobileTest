using SmoONE.CommLib;
using SmoONE.Domain;
using SmoONE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoONE.Application
{
    // ******************************************************************
    // 文件版本： SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // 创建时间： 2016/11
    // 主要内容：  请假单的服务接口
    // ******************************************************************
    /// <summary>
    /// 请假单的服务接口
    /// </summary>
    public interface IProfileService
    {
        #region 查询
        /// <summary>
        /// 根据请假单ID返回请假单对象
        /// </summary>
        /// <param name="ID">请假单ID</param>
        ProfileDto GetByID(string ID);

        /// <summary>
        /// 根据审批用户ID返回待审批请假单传输对象
        /// </summary>
        /// <param name="UserID">用户ID</param>
        List<ProfileDto> GetProfilesByStatus(int flag);


        /// <summary>
        /// 根据请假类型ID返回请假类型名称
        /// </summary>
        /// <param name="TypeID">请假类型ID</param>
        string GetTypeNameByID(string TypeID);

        /// <summary>
        /// 得到所有的请假类型
        /// </summary>
        List<ProfileType> GetAllType();
        #endregion


        #region 操作
        /// <summary>
        /// 添加请假单
        /// </summary>
        /// <param name="entity">请假单对象</param>
        ReturnInfo AddProfile(ProfileInputDto entity);

        /// <summary>
        /// 更新请假单
        /// </summary>
        /// <param name="entity">请假单对象</param>
        ReturnInfo UpdateLeave(ProfileInputDto entity);
        #endregion

    }
}
