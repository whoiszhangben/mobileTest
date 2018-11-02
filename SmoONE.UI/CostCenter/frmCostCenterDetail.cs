using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Domain;
using SmoONE.CommLib;
using SmoONE.DTOs;

namespace SmoONE.UI.CostCenter
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  �ɱ������������
    // ******************************************************************
    partial class frmCostCenterDetail : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string CCID ;//�ɱ����ı��
        private int currantSatus;//��ǰ�ɱ�����״̬
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��ת���ɱ����ı༭����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //��ת���༭����
            frmCostCenterCreate frm = new frmCostCenterCreate();
            frm.CCID = CCID;
            Show(frm, (MobileForm form, object args) =>
            {
                if (frm.ShowResult == ShowResult.Yes)
                {
                    ShowResult = ShowResult.Yes;
                    Bind();
                }
            });
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostCenterDetail_Load(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void Bind()
        {
            try
            {
                    //���ݳɱ����ı�Ż�ȡ�ɱ���������
                    CCDetailDto cc = AutofacConfig.costCenterService.GetCCByID(CCID);
                    lblCC_Name.Text = cc.CC_Name;
                    lblType.Text = cc.CC_TypeName;
                    lblStartDate.Text = cc.CC_StartDate.ToString("yyyy/MM/dd");
                    lblEndDate.Text = cc.CC_EndDate.ToString("yyyy/MM/dd");
                    lblAmount.Text = cc.CC_Amount.ToString();
                    lblRBAmount.Text = cc.CC_UsedAmount.ToString();
                    UserDetailDto user = AutofacConfig.userService.GetUserByUserID(cc.CC_LiableMan);
                    lblLiableMan.Text = user.U_Name;
                    lblDep.Text = cc.CC_DepName;
                    lblTemplate.Text = cc.CC_TemplateID;
                    currantSatus = cc.CC_IsActive;
                    switch (cc.CC_IsActive)
                    {
                        case (int)IsActive.����:
                            lblActive.Text = "����";
                            btnActive.Text = "����";
                            btnActive.BackColor= System.Drawing.Color.FromArgb(97, 121, 138);
                            break;
                        case (int)IsActive.����:
                            lblActive.Text = "����";
                            btnActive.Text = "����";
                            btnActive.BackColor= System.Drawing.Color.FromArgb(229,96,79);
                            break;
                    }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActive_Click(object sender, EventArgs e)
        {
            try
            {
                IsActive upStatus = IsActive.����;//����״̬
                switch (currantSatus)
                {
                    case (int)IsActive.����:
                        upStatus = IsActive.����;
                        break;
                    case (int)IsActive.����:
                        upStatus = IsActive.����;
                        break;
                }
                AutofacConfig AutofacConfig = new AutofacConfig();
                //���³ɱ�����״̬
                ReturnInfo result = AutofacConfig.costCenterService.UpdateCostCenterStatus(CCID, upStatus,Client .Session ["U_ID"].ToString ());
                if (result.IsSuccess == false)
                {
                    throw new Exception(result.ErrorInfo);
                }
                else
                {
                    Toast("��"+btnActive .Text +"�ɹ���", ToastLength.SHORT);
                    Bind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message , ToastLength.SHORT);
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostCenterDetail_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmCostCenterDetail_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}