using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.Data;
using SmoONE.DTOs;

namespace SmoONE.UI.Attendance
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� ѡ���û���ĳ��Ӧǩ������
    // ******************************************************************
    partial class frmAttendanceStatDay : Smobiler.Core.Controls.MobileForm
    {      
        #region "definition"
        internal string UserID;       //�û�ID
        public string DayTime;        //ѡ�е�������
        AutofacConfig AutofacConfig = new AutofacConfig();//����������     
        #endregion
        /// <summary>
        /// ���Ͻǹر�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatDay_TitleImageClick(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// �ֻ��Դ����ؼ����˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatDay_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)       //������ؼ�
            {
                this.Close();
            }
        }
        /// <summary>
        /// ҳ�渳ֵ����ȡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatDay_Load(object sender, EventArgs e)
        {
            try
            {
                this.btnDate.Text = DayTime + "���ڱ���>";         //��ʾ
                UserDetails userDetails = new UserDetails();
                UserDetailDto user = userDetails.getUser(UserID);
                if (user != null)                //������ڸ��û�
                {
                   // TitleText = user.U_Name+ "�Ŀ���";      //��ʾҳ�����
                }
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void Bind()
        {
            try
            {
                List<DateTime> listDate = AutofacConfig.attendanceService.GetDayOfMonthlyStatistics(UserID, Convert.ToDateTime(DayTime));
                DataTable table = new DataTable();
                table.Columns.Add("Day");     //��Ҫǩ��������
                foreach (DateTime Row in listDate)
                {
                    string Time = Row.ToString("yyyy��M��d��    dddd", new System.Globalization.CultureInfo("zh-CN"));
                    table.Rows.Add(Time);
                }
                gridATdata.Rows.Clear();
                if (table.Rows.Count > 0)
                {
                    this.gridATdata.DataSource = table;
                    this.gridATdata.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
     
        /// <summary>
        /// �������鿴���ڱ���ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButDate_Click(object sender, EventArgs e)
        {
            try
            {
                frmAttendanceStatSelf frmStat = new frmAttendanceStatSelf();
               // frmStat.TitleText = TitleText;             //����
                frmStat.UserID = UserID;                   //�û�ID
                frmStat.Daytime = DayTime;                 //ѡ��鿴������
                this.Show(frmStat);
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}