using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;

namespace SmoONE.UI.Layout
{
    partial class frmATStatisticsDayLayout : Smobiler.Core.Controls.MobileUserControl
    {
       
 
         /// <summary>
        /// ��ת������ģ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Press(object sender, EventArgs e)
        {
            try
            {
                //�������ȫ���������ת�����ڲ鿴��ϸ��������ת��
                if (Convert.ToBoolean(lblAbsenteeism.BindDataValue) == false)
                {
                    SmoONE.UI.Attendance.frmAttendanceMain frmMain = new SmoONE.UI.Attendance.frmAttendanceMain();
                    frmMain.DayTime = ((SmoONE.UI.Attendance.frmAttendanceStatMan)this.Form).DayTime;            //ʱ��
                    frmMain.enter = (int)Enum.Parse(typeof(ATMainState), ATMainState.ͳ�Ʋ鿴.ToString());
                    frmMain.UserID = lblUser.BindDataValue.ToString();
                    this.Form.Show(frmMain);
                }
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }
    }
}