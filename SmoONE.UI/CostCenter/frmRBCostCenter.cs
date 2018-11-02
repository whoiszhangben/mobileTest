using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using SmoONE.Domain;
using SmoONE.UI.Layout;

namespace SmoONE.UI.CostCenter
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  �ɱ�����ѡ���б����
    // ******************************************************************
    partial class frmRBCostCenter : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        frmCCSearchLayout CCSearch = new frmCCSearchLayout();
        public string CCID;//�ɱ����ı��
        public string liableMan = "";//������
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion

        private void frmCostCenter_Load(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void Bind()
        {
        
          List<CCDto> listCC=  AutofacConfig.costCenterService.GetAllCC();
          gridCCData.Rows.Clear();//��ճɱ�����ѡ���б�����
          if (listCC.Count > 0)
          {
              foreach (CCDto cc in listCC)
              {
                  UserDetailDto user = AutofacConfig.userService.GetUserByUserID(cc.CC_LiableMan);
                  cc.CC_LiableMan = user.U_Name;
              }
              gridCCData.DataSource = listCC;
              gridCCData.DataBind();
          }
       
        }

        /// <summary>
        /// ���ݲ�ѯ������ѯ�ɱ�����
        /// </summary>
        public void GetCC(string Name, string LiableMan)
        {
            List<CCDto> listCC = AutofacConfig.costCenterService.QueryCC(Name,LiableMan);
            gridCCData.Rows.Clear();//����б�����
            if (listCC.Count > 0)
            {
                foreach (CCDto cc in listCC)
                {
                    UserDetailDto user = AutofacConfig.userService.GetUserByUserID(cc.CC_LiableMan);
                    cc.CC_LiableMan = user.U_Name;
                }
                gridCCData.DataSource = listCC;
                gridCCData.DataBind();
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRBCostCenter_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRBCostCenter_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// ɸѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnSearch_Press(object sender, EventArgs e)
        {       
            ShowDialog(CCSearch);
        }
        /// <summary>
        /// ��ֵ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popList1_Selected(object sender, EventArgs e)
        {
            if (popList1.Selection != null)
            {
                liableMan = popList1.Selection.Value;
                CCSearch.ChangeValue(popList1.Selection.Text);
            }
        }
        /// <summary>
        /// ɸѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpSearch_Press(object sender, EventArgs e)
        {
            ShowDialog(CCSearch);
        }

        /// <summary>
        /// layoutDialog�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void layoutDialog1_ItemClick(object sender, MobileFormLayoutItemEventArgs e)
        //{
        //    try
        //    {
        //        switch (e.CellItem.Name)
        //        {
        //            case "btnccuser":
        //            case "btnccuser1":
        //                popList1.Groups.Clear();
        //                PopListGroup poli = new PopListGroup();
        //                popList1.Groups.Add(poli);
        //                poli.Title = "������ѡ��";
        //                List<UserDto> listuser = AutofacConfig.userService.GetAllUsers();
        //                foreach (UserDto user in listuser)
        //                {
        //                    poli.AddListItem(user.U_Name, user.U_ID);
        //                    if (liableMan.Trim().Length > 0)
        //                    {
        //                        if (liableMan.Trim().Equals(user.U_ID))
        //                        {
        //                        popList1.SetSelections(poli.Items[(poli.Items.Count - 1)]);
        //                        }
        //                    }
        //                }
        //                popList1.Show();
        //                break;
        //            case "btncurrentUser":
        //                liableMan = Client.Session["U_ID"].ToString();
        //                UserDetailDto userInfo = AutofacConfig.userService.GetUserByUserID(liableMan);
        //                layoutDialog1.LayoutData.Items["btnccuser"].DefaultValue = userInfo.U_Name;
        //                break;
        //            case "btnCancel":
        //                layoutDialog1.Close();
        //                break;
        //            case "btnOK":
        //                string ccname = layoutDialog1.LayoutData.Items["txtCCName"].DefaultValue.ToString();
        //                GetCC(ccname,liableMan);
        //                layoutDialog1.Close();
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Toast(ex.Message, ToastLength.SHORT);
        //    }
        //}
        /// <summary>
        /// ��ֵ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void popList1_Selected(object sender, EventArgs e)
        //{
        //    if (popList1.Selection != null)
        //    {
        //        liableMan = popList1.Selection.Value;
        //        layoutDialog1.LayoutData.Items["btnccuser"].DefaultValue = popList1.Selection.Text;
        //    }
        //}
    }
}