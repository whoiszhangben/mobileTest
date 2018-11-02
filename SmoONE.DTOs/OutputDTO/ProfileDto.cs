using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmoONE.DTOs
{
    /// <summary>
    /// 档案列表里的传输对象,用于返回查询结果
    /// </summary>
    public class ProfileDto
    {
        /// <summary>
        /// 档案编号
        /// </summary>
        public string ProfileNo { get; set; }

        /// <summary>
        /// 员工名称
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string EmpIDNo { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string EmpTelNo { get; set; }

        /// <summary>
        /// 档案明细
        /// </summary>
        public int RelationVal { get; set; }
    }
}

