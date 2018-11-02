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
    // ��Ҫ���ݣ� �ɱ������б����
    // ******************************************************************
    partial class frmCostCenter : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion

        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostCenter_Load(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        public  void Bind()
        {
            //��ȡ���гɱ���������
          List<CCDto> listCC=  AutofacConfig.costCenterService.GetAllCC();

          gridCCData.Rows.Clear();//��ճɱ������б�����
          if (listCC.Count > 0)
          {
              foreach (CCDto cc in listCC)
              {
                  UserDetailDto user = AutofacConfig.userService.GetUserByUserID(cc.CC_LiableMan);
                  cc.CC_LiableMan = user.U_Name;
               
              }
              lblInfor.Visible = false;
              gridCCData.DataSource = listCC;
              gridCCData.DataBind();
          }
          else
          {
              lblInfor.Visible = true ;
          }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostCenter_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmCostCenter_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// ��ת����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            //��ת���ɱ����Ĵ�������
            frmCostCenterCreate frm = new frmCostCenterCreate();
            Show(frm, (MobileForm form, object args) =>
            {
                if (frm.ShowResult == ShowResult.Yes)
                {
                    Bind();
                }
            });
        }

        private void title1_Load(object sender, EventArgs e)
        {

        }
    }
}