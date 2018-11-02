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
    partial class frmAttendanceStatDayLayout : Smobiler.Core.Controls.MobileUserControl
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
                switch (this .Form.ToString () )
                {
                    case "SmoONE.UI.Attendance.frmAttendanceStatMonthTypeDay":
                        if (string.IsNullOrEmpty(((SmoONE.UI.Attendance.frmAttendanceStatMonthTypeDay)this.Form).atType) == false)
                        {
                            switch ((StatisticsType)Enum.Parse(typeof(StatisticsType), ((SmoONE.UI.Attendance.frmAttendanceStatMonthTypeDay)this.Form).atType))
                            {
                                case StatisticsType.׼��:
                                case StatisticsType.�ٵ�:
                                case StatisticsType.����:
                                case StatisticsType.δǩ��:
                                case StatisticsType.δǩ��:
                                    SmoONE.UI.Attendance.frmAttendanceMain frmMain = new SmoONE.UI.Attendance.frmAttendanceMain();
                                    frmMain.DayTime = lblDay.BindDisplayValue .ToString ();             //ѡ��鿴������
                                    frmMain.enter = (int)Enum.Parse(typeof(ATMainState), ATMainState.ͳ�Ʋ鿴.ToString());
                                    frmMain.UserID = ((SmoONE.UI.Attendance.frmAttendanceStatMonthTypeDay)this.Form).UserID;
                                    this.Form .Show(frmMain);
                                    break;
                            }
                        }
                     break;
                    case "SmoONE.UI.Attendance.frmAttendanceStatDay":
                         SmoONE.UI.Attendance.frmAttendanceMain frmMain1 = new SmoONE.UI.Attendance.frmAttendanceMain();
                        frmMain1.DayTime = lblDay.BindDisplayValue .ToString ();            //ѡ��鿴������
                        frmMain1.UserID =((SmoONE.UI.Attendance.frmAttendanceStatDay)this.Form ). UserID;             //���鿴�û�ID
                        frmMain1.enter = (int)Enum.Parse(typeof(ATMainState), ATMainState.ͳ�Ʋ鿴.ToString());
                        this.Form.Show(frmMain1);
                     break;
                    case "SmoONE.UI.Attendance.frmAttendanceStatistics":
                             SmoONE.UI.Attendance.frmAttendanceStatMan frmMan = new SmoONE.UI.Attendance.frmAttendanceStatMan();
                             frmMan.DayTime =lblDay.BindDisplayValue .ToString ();     //��ѡ�����ڣ����ݵ��¸�ҳ��
                             this.Form .Show(frmMan);
                     break;
                    case "SmoONE.UI.Attendance.frmAttendanceStatSelfDay":
                        SmoONE.UI.Attendance.frmAttendanceMain frmMain2 = new SmoONE.UI.Attendance.frmAttendanceMain();
                        frmMain2.DayTime = lblDay.BindDisplayValue.ToString();            //ѡ��鿴������
                        frmMain2.UserID = ((SmoONE.UI.Attendance.frmAttendanceStatSelfDay)this.Form).UserID;             //���鿴�û�ID
                        frmMain2.enter = (int)Enum.Parse(typeof(ATMainState), ATMainState.ͳ�Ʋ鿴.ToString());
                        this.Form.Show(frmMain2);
                        break;

                 }
            }
            catch (Exception ex)
            {
               this.Form . Toast(ex.Message);
            }
        }

       
    }
}