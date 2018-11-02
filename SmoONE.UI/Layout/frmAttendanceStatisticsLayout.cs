using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;

namespace SmoONE.UI.Layout
{
    partial class frmAttendanceStatisticsLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ������Ӧѡ����ҳ������ʾ��Ӧ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Press(object sender, EventArgs e)
        {
            try
            {
                string[] Year = ((SmoONE.UI.Attendance.frmAttendanceStatistics)this.Form).btnYear.Text.Split('��');      //���
                string[] Month = ((SmoONE.UI.Attendance.frmAttendanceStatistics)this.Form).btnMonth.Text.Split('��');       //�·�
                SmoONE.UI.Attendance.frmAttendanceStatDay frmDay = new SmoONE.UI.Attendance.frmAttendanceStatDay();
                frmDay.UserID = image1.BindDataValue .ToString();
                frmDay.DayTime = Year[0] + Month[0];         //��������Ϣ���ݵ��¸�ҳ��
                this.Form.Show(frmDay);
            
            }
            catch (Exception ex)
            {
                this.Form .Toast(ex.Message);
            }
        }

      

    }
}