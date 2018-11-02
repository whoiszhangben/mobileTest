using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using SmoONE.Domain;

namespace SmoONE.UI.CostCenter
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  �ɱ�����ģ���б����
    // ******************************************************************
    partial class frmCostTemplet : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        /// <summary>
        /// �Ƿ�ѡ��ɱ���������ģ��
        /// </summary>
        public bool IsSelectCTemPlet;
        /// <summary>
        /// ģ����
        /// </summary>
        public string CTempID;
        /// <summary>
        /// ģ����
        /// </summary>
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
   
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostTemplet_Load(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        public  void Bind()
        {
            //��ȡ���гɱ�ģ��
          List<CC_Type_TemplateDto> listCCTemp=  AutofacConfig.costCenterService.GetAllCCTTemplate();

          gridCCTempletData.Cells .Clear();//��ճɱ�����ģ���б�����
          if (listCCTemp.Count > 0)
          {
              foreach (CC_Type_TemplateDto ccTemp in listCCTemp)
              {
                  string AEACheckers = "";
                  string[] AEAChecks = ccTemp.CC_TT_AEACheckers.Split(',');
                  foreach (string AEACheck in AEAChecks)
                  {
                      UserDetailDto user = AutofacConfig.userService.GetUserByUserID(AEACheck);
                      if (string.IsNullOrWhiteSpace(AEACheckers) == true)
                      {
                          AEACheckers =user.U_Name;
                      }
                      else
                      {
                          AEACheckers += "," + user.U_Name;
                      }
                  }
                  ccTemp.CC_TT_AEACheckers = AEACheckers;
                  string FCheckers = "";
                  string[] FChecks = ccTemp.CC_TT_FinancialCheckers.Split(',');
                  foreach (string FCheck in FChecks)
                  {
                      UserDetailDto user = AutofacConfig.userService.GetUserByUserID(FCheck);
                      if (string.IsNullOrWhiteSpace(FCheckers) == true)
                      {
                          FCheckers =user.U_Name;
                      }
                      else
                      {
                          FCheckers += "," + user.U_Name;
                      }
                  }
                  ccTemp.CC_TT_FinancialCheckers = FCheckers;
              }
              lblInfor.Visible = false;
              gridCCTempletData.DataSource = listCCTemp;
              gridCCTempletData.DataBind();
          }
          else
          {
              lblInfor.Visible = true ;
          }
        }
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostTemplet_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// �ֻ��Դ����˼��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostTemplet_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ��ת��ģ�崴������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {  //��ת���ɱ�ģ�崴������
            frmCostTempletCreate frm = new frmCostTempletCreate();
            Show(frm, (MobileForm form, object args) =>
            {
                if (frm.ShowResult == ShowResult.Yes)
                {
                    Bind();
                }
            });
        }
    }
}