using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;

namespace SmoONE.UI.Layout
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� ���ڱ���ͳ��(����ͳ��)ģ��
    // ******************************************************************
    partial class frmATStatSelfDetailDayLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ��ת��������ϸ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Press(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(((SmoONE.UI.Attendance.frmAttendanceStatSelfDetail)this.Form).Type ) == false)
                //{
                //    switch ((StatisticsType)Enum.Parse(typeof(StatisticsType), ((SmoONE.UI.Attendance.frmAttendanceStatMonthTypeDay)this.Form).atType))
                //    {
                //        case StatisticsType.׼��:
                //        case StatisticsType.�ٵ�:
                //        case StatisticsType.����:
                //        case StatisticsType.δǩ��:
                //        case StatisticsType.δǩ��:
                //            SmoONE.UI.Attendance.frmAttendanceMain frmMain = new SmoONE.UI.Attendance.frmAttendanceMain();
                //            frmMain.DayTime = lblDay.Text;            //ѡ��鿴������
                //            frmMain.enter = (int)Enum.Parse(typeof(ATMainState), ATMainState.ͳ�Ʋ鿴.ToString());
                //            frmMain.UserID = ((SmoONE.UI.Attendance.frmAttendanceStatMonthTypeDay)this.Form).UserID;
                //            this.Form.Show(frmMain);
                //            break;
                //    }
                //}
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }

    }
}