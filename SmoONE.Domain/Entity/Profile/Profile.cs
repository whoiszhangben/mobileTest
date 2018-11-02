using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoONE.Domain
{
    /// <summary>
    /// 档案信息表
    /// </summary>
    [Table("Profile")]
    public class Profile: IAggregateRoot
    {
        /// <summary>
        /// 档案编号
        /// </summary>
        [Key]
        [StringLength(maximumLength: 20)]
        [DisplayName("档案编号")]
        public string ProfileNo { get; set; }

        /// <summary>
        /// 员工名称
        /// </summary>
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "最长为20字节")]
        [DisplayName("员工名称")]
        public string EmpName { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
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
        [Column(TypeName = "datetime2")]
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
        [Column(TypeName = "datetime2")]
        [DisplayName("更新时间")]
        public DateTime UpdateTime { get; set; }

    }
}
