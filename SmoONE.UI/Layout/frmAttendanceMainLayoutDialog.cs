using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using SmoONE.CommLib;
using SmoONE.UI.Attendance;

namespace SmoONE.UI.Layout
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� ����ǩ��ԭ����д����
    // ******************************************************************
    partial class frmAttendanceMainLayoutDialog : Smobiler.Core.Controls.MobileUserControl
    {
        #region "definition"
        public ALInputDto newLog = new ALInputDto();     //�½���־�������
        AutofacConfig AutofacConfig = new AutofacConfig();//����������     
        public bool ShowResult = false;   //�Ƿ��ύ
        #endregion
        /// <summary>
        /// �ύǩ����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Press(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtReason.Text) == true)         //ԭ����Ϊ��
                {
                    throw new Exception(this.lblTitle + "����Ϊ��");
                }
                newLog.AL_Reason = this.txtReason.Text;           //�ٵ�����ԭ��
                ReturnInfo r = AutofacConfig.attendanceService.AddAttendanceLog(newLog);
                if (r.IsSuccess == true)               //�ύ��¼�ɹ�
                {
                    ShowResult = true;
                    this.Close();
                    if (lblTitle.Text == "�ٵ�����")
                    {
                        this.Form.Toast("ǩ���ɹ�");
                    }
                    else
                    {
                        this.Form.Toast("ǩ�˳ɹ�");
                    }
                    ((frmAttendanceMain)(Form)).Bind();                     //ˢ��ҳ��
                }
                else
                {
                    throw new Exception(r.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }
        /// <summary>
        /// �رյ�ǰ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Press(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}