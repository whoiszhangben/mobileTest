using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.UI;
using SmoONE.DTOs;

namespace SmoONE.UI.Work
{
    // ******************************************************************
    // 文件版本： SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // 创建时间： 2017/07
    // 主要内容：  档案列表界面
    // ******************************************************************
    partial class frmEmpProfile : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        internal string type = "";//类型
        internal string state = "";//状态
        AutofacConfig AutofacConfig = new AutofacConfig();//调用配置类
        #endregion
        /// <summary>
        /// 手机自带回退按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEmpProfile_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //关闭当前页面
            
            }
        }

        private void frmEmpProfile_Load(object sender, EventArgs e)
        {
            type = "";
            state = "";
            Bind();
        }
        /// <summary>
        /// 选中项更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void segmentedControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            type = "";
            state = "";
            Bind();
        }
        /// <summary>
        /// 获取初始化数据
        /// </summary>
        public void Bind()
        {
            try
            {
                List<ProfileDto> listProfileDto = new List<ProfileDto>();
                listProfileDto = AutofacConfig.profileService.GetProfilesByStatus(segmentedControl1.SelectedIndex);

                List<DataGridviewProfile> listCheck = new List<DataGridviewProfile>();
                if (listProfileDto.Count > 0)
                {
                    foreach (ProfileDto profile in listProfileDto)
                    {
                        DataGridviewProfile dgvItem = new DataGridviewProfile();
                        dgvItem.ProfileNo = profile.ProfileNo;
                        dgvItem.EmpName = profile.EmpName;
                        dgvItem.EmpIDNo = profile.EmpIDNo;
                        dgvItem.EmpTelNo = profile.EmpTelNo;
                        dgvItem.RelationVal = profile.RelationVal;
                        listCheck.Add(dgvItem);
                    }
                }
                listCheckData.DataMember = "ProfileNo";
                listCheckData.Rows.Clear();
                listCheckData.DataSource = listCheck;//绑定gridview数据
                listCheckData.DataBind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// 获取筛选数据
        /// </summary>
        private void GetCheckData()
        {
            
        }
        /// <summary>
        /// 筛选查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popList1_Selected(object sender, EventArgs e)
        {
            if (popList1.Selection != null)
            {
                foreach (PopListItem poitem in popList1.Selections)
                {

                    type = (popList1.Selection.Value.ToString()).Split('/')[0].ToString();
                    state = (popList1.Selection.Value.ToString()).Split('/')[1].ToString();
                }
                GetCheckData();
            }
        }
       
        /// <summary>
        /// 筛选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpSearch_Press(object sender, EventArgs e)
        {
            
        }
    }
}