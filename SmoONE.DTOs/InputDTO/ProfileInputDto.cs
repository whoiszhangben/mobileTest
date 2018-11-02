using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmoONE.DTOs
{
    // ******************************************************************
    // 文件版本： SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // 创建时间： 2016/11
    // 主要内容：  请假单详情传输对象,用于提供新增/更新所需的数据.继承IEntity，方便根据IEntity类型进行DataAnnotations验证,也方便以后扩展
    // ******************************************************************
    /// <summary>
    /// 请假单详情传输对象,用于提供新增/更新所需的数据
    /// </summary>
    public class ProfileInputDto : IEntity
    {
        [Key]
        [StringLength(maximumLength: 20)]
        [DisplayName("档案编号")]
        public string ProfileNo { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "最长为20字节")]
        [DisplayName("员工姓名")]
        public string EmpName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "最长为20字节")]
        [DisplayName("身份证号码")]
        public string EmpIDNo { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "最长为20字节")]
        [DisplayName("电话号码")]
        public string EmpTelNo { get; set; }

        /// <summary>
        /// 是否离职
        /// </summary>
        [DisplayName("是否离职")]
        public int IsDimissioned { get; set; }

        /// <summary>
        /// 档案情况
        /// </summary>
        [DisplayName("档案情况")]
        public int RelationVal { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [StringLength(maximumLength: 20, ErrorMessage = "长度不能超过20")]
        [DisplayName("创建人")]
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [StringLength(maximumLength: 20, ErrorMessage = "长度不能超过20")]
        [DisplayName("更新者")]
        public string Updator { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Required]
        [DisplayName("更新时间")]
        public DateTime UpdateTime { get; set; }
    }
}
