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
    /// 档案类型
    /// </summary>
    [Table("ProfileType")]
    public class ProfileType : IAggregateRoot
    {
        /// <summary>
        /// 请假类型ID
        /// </summary>
        [Key]
        [StringLength(maximumLength: 20)]
        [DisplayName("档案资料ID")]
        public string PT_ID { get; set; }

        /// <summary>
        /// 请假类型名称
        /// </summary>
        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "长度不能超过20")]
        [DisplayName("档案资料名称")]
        public string PT_Name { get; set; }
    }
}
