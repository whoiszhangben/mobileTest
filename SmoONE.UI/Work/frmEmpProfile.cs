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
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  �����б����
    // ******************************************************************
    partial class frmEmpProfile : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        internal string type = "";//����
        internal string state = "";//״̬
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmEmpProfile_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            
            }
        }

        private void frmEmpProfile_Load(object sender, EventArgs e)
        {
            type = "";
            state = "";
            Bind();
        }
        /// <summary>
        /// ѡ��������¼�
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
        /// ��ȡ��ʼ������
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
                listCheckData.DataSource = listCheck;//��gridview����
                listCheckData.DataBind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// ��ȡɸѡ����
        /// </summary>
        private void GetCheckData()
        {
            
        }
        /// <summary>
        /// ɸѡ��ѯ
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
        /// ɸѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpSearch_Press(object sender, EventArgs e)
        {
            
        }
    }
}