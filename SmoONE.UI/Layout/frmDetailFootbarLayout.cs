using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.CommLib;
using SmoONE.DTOs;

namespace SmoONE.UI.Layout
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class frmDetailFootbarLayout : Smobiler.Core.Controls.MobileUserControl
    {
          #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ܾ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefuse_Press(object sender, EventArgs e)
        {
           this.Form .ShowDialog(new frmRefuseLayout());
        }
        /// <summary>
        /// ͬ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgreed_Press(object sender, EventArgs e)
        {
            try
            {
                switch (this .Form.ToString () )
                {
                    case "SmoONE.UI.Leave.frmLeaveDetail":
                        //������ͬ�����
                        ReturnInfo result = AutofacConfig.leaveService.UpdateLeaveStatus(((SmoONE.UI.Leave.frmLeaveDetail)this.Form ).lID, L_Status.������, Client.Session["U_ID"].ToString(), "");
                        //�������true�������ɹ�������ʧ�ܲ��׳�����
                        if (result.IsSuccess == true)
                        {
                            ((SmoONE.UI.Leave.frmLeaveDetail)this.Form).GetLeave();
                            this.Form.ShowResult = ShowResult.Yes;
                            this.Form.Toast("��ͬ�����������", ToastLength.SHORT);
                        }
                        else
                        {
                            throw new Exception (result.ErrorInfo);
                        }
                        break ;

                }
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message ,ToastLength.SHORT);
            }
        }
        /// <summary>
        ///�༭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Press(object sender, EventArgs e)
        {
            try
            {
                switch (this .Form.ToString ())
                {
                    case "SmoONE.UI.Leave.frmLeaveDetail":
                        Leave.frmLeaveCreate frmLeaveCreate = new Leave.frmLeaveCreate();
                        frmLeaveCreate.LID = ((SmoONE.UI.Leave.frmLeaveDetail)this.Form).lID;
                       this.Form . Show(frmLeaveCreate, (MobileForm form, object args) =>
                        {
                            if (frmLeaveCreate.ShowResult == ShowResult.Yes)
                            {
                               this.Form . ShowResult = ShowResult.Yes;
                                ((SmoONE.UI.Leave.frmLeaveDetail)this.Form).GetLeave();

                            }
                        });
                        break;

                }
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message, ToastLength.SHORT);
            }
        }
    }
}