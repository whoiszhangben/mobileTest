using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using SmoONE.CommLib;

namespace SmoONE.UI.Attendance
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� ����ǩ��ԭ����д����
    // ******************************************************************
    partial class frmAttendanceMainLayoutDialog : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public ALInputDto newLog = new ALInputDto();     //�½���־�������
        AutofacConfig AutofacConfig = new AutofacConfig();//����������     
        #endregion
        /// <summary>
        /// �ύǩ����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(this.txtReason.Text)==true)         //ԭ����Ϊ��
                {
                    throw new Exception(this.title1 .TitleText+"����Ϊ��");
                }
                newLog.AL_Reason = this.txtReason.Text;           //�ٵ�����ԭ��
                ReturnInfo r = AutofacConfig.attendanceService.AddAttendanceLog(newLog);
                if (r.IsSuccess == true)               //�ύ��¼�ɹ�
                {
                    this.ShowResult =Smobiler.Core.Controls .ShowResult.Yes;
                    this.Close();
                    if(this.title1 .TitleText=="�ٵ�����")
                    {
                        Toast("ǩ���ɹ�"); 
                    }
                    else
                    {
                        Toast("ǩ�˳ɹ�");
                    }                   
                }
                else
                {
                    throw new Exception(r.ErrorInfo);
                }
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// �ֻ��Դ����ؼ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceMainLayoutDialog_KeyDown(object sender, KeyDownEventArgs e)
        {
            if(e.KeyCode==KeyCode.Back)
            {
                Close();
            }
        }
        /// <summary>
        /// ���ϽǷ��ؼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceMainLayoutDialog_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}