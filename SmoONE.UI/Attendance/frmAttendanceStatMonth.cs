using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;

namespace SmoONE.UI.Attendance
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� ���ڱ���ͳ��(����ͳ��)
    // ******************************************************************
    partial class frmAttendanceStatMonth : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string Year;               //���
        public string month; //�·�
        private string atType;//��������
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        private string[] atTypeUser= {"","","","","" };//�����û�
        #endregion
        /// <summary>
        /// ���ϽǷ��ؼ����˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatMonth_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// �ֻ��Դ����ؼ����˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatMonth_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }
        /// <summary>
        /// ���ѡ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButYear_Click(object sender, EventArgs e)
        {
            popListYear.Show();
        }
        /// <summary>
        /// ���ѡ���·�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButMonth_Click(object sender, EventArgs e)
        {
            popListMonth.Show();
        }
        /// <summary>
        /// ѡ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popListYear_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popListYear.Selection != null)     //���ѡ�����·ݣ�����ʾ��ҳ����
                {
                    this.btnYear.Text = popListYear.Selection.Text;
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ѡ���·�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popListMonth_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popListMonth.Selection != null)          //���ѡ������ݣ�����ʾ��ҳ����
                {
                    this.BtnMonth.Text = popListMonth.Selection.Text;
                }
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ���س�ʼҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatMonth_Load(object sender, EventArgs e)
        {
            try
            {
                this.btnYear.Text = Year;                       //���
                this.BtnMonth.Text = month;                     //�·�
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }

        }
        /// <summary>
        /// ���ݼ���
        /// </summary>
        private void Bind()
        {
            try
            {
                //�������»�ȡ����ͳ������
                MonthlyResultDto monthlyResultDto = AutofacConfig.attendanceService.GetMonthlyResult(Convert.ToDateTime(Year + "/" + month));
                if (monthlyResultDto != null)
                {
                    if (monthlyResultDto.MR_AllUserCount > 0)
                    {
                        this.btnMSAllCount.Text = monthlyResultDto.MR_AllUserCount.ToString() + "��";            //Ӧǩ������
                        this.progress1.Value = 1;
                        this.btnMSInTimeCount.Text = monthlyResultDto.MR_InTimeUserCount.ToString() + "��";       //����ǩ������
                        this.proMSInTime.Value = (float)monthlyResultDto.MR_InTimeUserCount / monthlyResultDto.MR_AllUserCount;
                        if (string.IsNullOrEmpty(monthlyResultDto.MR_InTimeUser) == false)
                        {
                            atTypeUser[0] = monthlyResultDto.MR_InTimeUser;
                        }
                        this.btnMSCLateCount.Text = monthlyResultDto.MR_ComeLateUserCount.ToString() + "��";      //�ٵ�����
                        this.proMSCLate.Value = (float)monthlyResultDto.MR_ComeLateUserCount / monthlyResultDto.MR_AllUserCount;
                        if (string.IsNullOrEmpty(monthlyResultDto.MR_ComeLateUser) == false)
                        {
                            atTypeUser[1] = monthlyResultDto.MR_ComeLateUser;
                        }
                        this.btnMSLEarlyCount.Text = monthlyResultDto.MR_LeaveEarlyUserCount.ToString() + "��";     //��������
                        this.proMSLEarly.Value = (float)monthlyResultDto.MR_LeaveEarlyUserCount / monthlyResultDto.MR_AllUserCount;
                        if (string.IsNullOrEmpty(monthlyResultDto.MR_LeaveEarlyUser) == false)
                        {
                            atTypeUser[2] = monthlyResultDto.MR_LeaveEarlyUser;
                        }
                        this.btnMSNoSignCount.Text = monthlyResultDto.MR_NoSignUserCount.ToString() + "��";    //δǩ������
                        this.proMSNoSign.Value = (float)monthlyResultDto.MR_NoSignUserCount / monthlyResultDto.MR_AllUserCount;
                        if (string.IsNullOrEmpty(monthlyResultDto.MR_NoSignUser) == false)
                        {
                            atTypeUser[3] = monthlyResultDto.MR_NoSignUser;
                        }
                        this.btnATAbsentCount.Text = monthlyResultDto.MR_AbsentUserCount.ToString() + "��";    //ȫ���������
                        this.proATAbsent.Value = (float)monthlyResultDto.MR_AbsentUserCount / monthlyResultDto.MR_AllUserCount;
                        if (string.IsNullOrEmpty(monthlyResultDto.MR_AbsentUser) == false)
                        {
                            atTypeUser[4] = monthlyResultDto.MR_AbsentUser;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ��ת����ǰ�±������û�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnATTypeSign_Click(object sender, EventArgs e)
        {
            try
            {
                bool isForm = false;//�Ƿ���ת����
                int i=0;
                switch (((Button)sender).Name)
                {
                    //����
                    case "btnATInTime":
                    case "btnMSInTimeCount":
                        if (proMSInTime.Value > 0)
                        {
                            i = 0;
                            atType = StatisticsType.׼��.ToString();
                            isForm = true;
                        }
                        break;
                    //�ٵ�
                    case "btnATLate":
                    case "btnMSCLateCount":
                        if (proMSCLate.Value > 0)
                        {
                            i = 1;
                            atType = StatisticsType.�ٵ�.ToString();
                            isForm = true;
                        }
                        break;
                    //����
                    case "btnLEarly":
                    case "btnMSLEarlyCount":
                        if (proMSLEarly.Value > 0)
                        {
                            i = 2;
                            atType = StatisticsType.����.ToString();
                            isForm = true;
                        }
                        break;
                    //δǩ��
                    case "btnATNoSign":
                    case "btnMSNoSignCount":
                        if (proMSNoSign.Value > 0)
                        {
                            i = 3;
                            atType = StatisticsType.δǩ��.ToString();
                            isForm = true;
                        }
                        break;
                    //ȫ�����
                    case "btnATAbsent":
                    case "btnATAbsentCount":
                        if (proATAbsent.Value > 0)
                        {
                            i = 4;
                            atType = StatisticsType.����.ToString();
                            isForm = true;
                        }
                        break;

                }
                if (isForm == true)
                {
                    frmAttendanceStatUser frm = new frmAttendanceStatUser();
                    frm.atDate = Year + "/" + month;
                    if (string.IsNullOrEmpty(atType) == false)
                    {
                        frm.atType = atType;
                    }
                    if (string.IsNullOrEmpty(atTypeUser[i]) == false)
                    {
                        frm.atTypeUser = atTypeUser[i];
                    }
                    Show(frm);
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}