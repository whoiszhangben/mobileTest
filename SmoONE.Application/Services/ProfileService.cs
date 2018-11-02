using SmoONE.Domain;
using SmoONE.Domain.IRepository;
using SmoONE.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using SmoONE.DTOs;
using SmoONE.CommLib;
using System.Data.Entity;
using AutoMapper;

namespace SmoONE.Application
{
    // ******************************************************************
    // 文件版本： SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // 创建时间： 2016/11
    // 主要内容：  请假单的服务实现
    // ******************************************************************
    /// <summary>
    /// 请假单的服务实现
    /// </summary>
    public class ProfileService: IProfileService
    {
        /// <summary>
        /// 工作单元的接口
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// 请假单的仓储类的接口
        /// </summary>
        private IProfileRepository _profileRepository;

        /// <summary>
        /// 请假类型的仓储类的接口
        /// </summary>
        private IProfileTypeRepository _profileTypeRepository;

        /// <summary>
        /// 用户的仓储类的接口
        /// </summary>
        private IUserRepository _userRepository;


        /// <summary>
        /// 请假单服务实现的构造函数
        /// </summary>
        public ProfileService(IUnitOfWork unitOfWork,
            IProfileRepository leaveRepository,
            IProfileTypeRepository leaveTypeRepository,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _profileRepository = leaveRepository;
            _profileTypeRepository = leaveTypeRepository;
            _userRepository = userRepository;
        }

        #region 查询
        /// <summary>
        /// 根据请假单ID返回请假单对象
        /// </summary>
        /// <param name="ID">请假单ID</param>
        public ProfileDto GetByID(string ID)
        {
            ProfileDto lv = Mapper.Map<SmoONE.Domain.Profile,ProfileDto> (_profileRepository.GetByID(ID).AsNoTracking().FirstOrDefault());
            return lv;
        }

        /// <summary>
        /// 根据审批用户ID返回待审批的请假单对象
        /// </summary>
        /// <param name="UserID">用户ID</param>
        public List<ProfileDto> GetProfilesByStatus(int status)
        {
            List<Domain.Profile> inputLst = _profileRepository.GetProfilesByStatus(status).AsNoTracking().ToList();
            Mapper.CreateMap<Domain.Profile, ProfileDto>();
            List<ProfileDto> temp = Mapper.Map<List<Domain.Profile>,List<ProfileDto>>(inputLst);
            return temp;
        }

        /// <summary>
        /// 根据请假类型ID返回请假类型名称
        /// </summary>
        /// <param name="TypeID">请假类型ID</param>
        public string GetTypeNameByID(string TypeID)
        {
            return _profileTypeRepository.GetByID(TypeID).PT_Name;
        }

        /// <summary>
        /// 得到所有的请假类型
        /// </summary>
        public List<ProfileType> GetAllType()
        {
            return _profileTypeRepository.GetAll().AsNoTracking().ToList();
        }
        #endregion


        #region 操作
        public ReturnInfo AddProfile(ProfileInputDto lvInputDto)
        {
            ReturnInfo RInfo = new ReturnInfo();
            StringBuilder sb = new StringBuilder();
            string ProfileNo = lvInputDto.ProfileNo;

            string ValidateInfo = Helper.ValidateProfileInputDto(lvInputDto);
            sb.Append(ValidateInfo);
            if (string.IsNullOrEmpty(ValidateInfo))
            {
                try
                {                  
                    _unitOfWork.RegisterNew(lvInputDto);
                    bool result = _unitOfWork.Commit();
                    RInfo.IsSuccess = result;
                    RInfo.ErrorInfo = sb.ToString();
                    return RInfo;
                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                    sb.Append(ex.Message);
                    RInfo.IsSuccess = false;
                    RInfo.ErrorInfo = sb.ToString();
                    return RInfo;
                }
            }
            else
            {
                RInfo.IsSuccess = false;
                RInfo.ErrorInfo = sb.ToString();
                return RInfo;
            }
        }

        public ReturnInfo UpdateLeave(ProfileInputDto entity)
        {
            ReturnInfo RInfo = new ReturnInfo();
            StringBuilder sb = new StringBuilder();
            string ValidateInfo = Helper.ValidateProfileInputDto(entity);
            sb.Append(ValidateInfo);
            if (string.IsNullOrEmpty(ValidateInfo))
            {
                Domain.Profile lv = _profileRepository.GetByID(entity.ProfileNo).FirstOrDefault();
                if (lv != null)
                {
                    try
                    {
                        lv.EmpName = entity.EmpName;
                        lv.EmpIDNo = entity.EmpIDNo;
                        lv.EmpTelNo = entity.EmpTelNo;
                        lv.RelationVal = entity.RelationVal;
                        lv.IsDimissioned = entity.IsDimissioned;
                        lv.UpdateTime = DateTime.Now;
                        lv.Updator = entity.Updator;
                        _unitOfWork.RegisterDirty(lv);
                        bool result = _unitOfWork.Commit();
                        RInfo.IsSuccess = result;
                        RInfo.ErrorInfo = sb.ToString();
                        return RInfo;
                    }
                    catch (Exception ex)
                    {
                        _unitOfWork.RegisterClean(lv);
                        _unitOfWork.Rollback();
                        sb.Append(ex.Message);
                        RInfo.IsSuccess = false;
                        RInfo.ErrorInfo = sb.ToString();
                        return RInfo;
                    }

                }
                else
                {
                    RInfo.IsSuccess = false;
                    RInfo.ErrorInfo = "未找到该档案!";
                    return RInfo;
                }
            }
            else
            {
                RInfo.IsSuccess = false;
                RInfo.ErrorInfo = sb.ToString();
                return RInfo;
            }
        }
        #endregion

        #region 通用方法
        #endregion
    }
}
