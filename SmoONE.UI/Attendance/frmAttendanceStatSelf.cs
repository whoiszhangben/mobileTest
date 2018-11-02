using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using System.Data;
using Smobiler.Core.Controls;
using SmoONE.DTOs;

namespace SmoONE.UI.Attendance
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� �û��·ݿ��ڱ���ͳ�ƽ���
    // ******************************************************************
    partial class frmAttendanceStatSelf : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        internal string UserID;        //�û�ID
        public string Daytime;         //ʱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����ؼ����˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatSelf_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }
        /// <summary>
        /// ���ϽǷ��ؼ����˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatSelf_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// ��ʼ��ҳ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatSelf_Load(object sender, EventArgs e)
        {
            try
            {
                MonthlyStatisticsDto Stat = AutofacConfig.attendanceService.GetUserMonthlyStatistics(UserID, Convert.ToDateTime(Daytime));
                UserDetails userDetails = new UserDetails();
                UserDetailDto UserInfo = userDetails.getUser(UserID);
                if(UserInfo == null)
                {
                    throw new Exception("�û�������");
                }
                if (string.IsNullOrEmpty(this.title1.TitleText) == true)
                {
                    this.title1 .TitleText = UserInfo.U_Name + "�Ŀ��ڱ���";
                }
                lblDay.Text = Stat.DS_NormalDayCount.ToString();//������������
                this.lblAll.Text = Stat.MS_AllCount.ToString() + "��";//Ӧǩ��
                this.lblName.Text = UserInfo.U_Name;                //�û���
                this.image1.ResourceID = UserInfo.U_Portrait;         //�û�ͷ��
                if (string.IsNullOrEmpty(Daytime) == true)
                {
                    Daytime = DateTime.Now.ToString();
                }
                this.lblYear.Text = Convert.ToDateTime(Daytime).ToString("yyyy��");       //��ʾ���
                this.btnMonth.Text = Convert.ToDateTime(Daytime).ToString("MM");          //��ʾ�·�
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ���ݰ�
        /// </summary>
        private void Bind()
        {
            try
            {
                DateTime StatDay = Convert.ToDateTime(this.lblYear.Text + this.btnMonth.Text);
                MonthlyStatisticsDto Stat = AutofacConfig.attendanceService.GetUserMonthlyStatistics(UserID, Convert.ToDateTime(Daytime));
                lblDay.Text = Stat.DS_NormalDayCount.ToString() + "��";
                DataTable table = new DataTable();
                table.Columns.Add("AttendType");            //ǩ������
                table.Columns.Add("AttendNumber");          //ǩ������
                table.Columns.Add("Number");          //ǩ������
                table.Rows.Add(StatisticsType.׼��, Stat.MS_InTimeCount.ToString() + "��", Stat.MS_InTimeCount);
                table.Rows.Add(StatisticsType.�ٵ�, Stat.MS_ComeLateCount.ToString() + "��", Stat.MS_ComeLateCount);
                table.Rows.Add(StatisticsType.����, Stat.MS_LeaveEarlyCount.ToString() + "��", Stat.MS_LeaveEarlyCount);
                table.Rows.Add(StatisticsType.δǩ��, Stat.MS_NoSignInCount.ToString() + "��", Stat.MS_NoSignInCount);
                table.Rows.Add(StatisticsType.δǩ��, Stat.MS_NoSignOutCount.ToString() + "��", Stat.MS_NoSignOutCount);
                gridATdata.Rows.Clear();   //�������
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
        /// ���ѡ���·�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButMonth_Click(object sender, EventArgs e)
        {
            popListMonth.Show();           //��ʾ�·�ѡ���б�
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
                if (popListMonth.Selection != null)    //���ѡ�����·�
                {
                    this.btnMonth.Text = popListMonth.Selection.Text;
                    Bind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        ///// <summary>
        ///// ����������鿴ǩ�����
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gridATdata_CellClick(object sender, GridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (Convert.ToInt32(e.Cell.Items["lblNumber"].Value) == 0)
        //        {
        //            e.Cell.Items["lblDetail"].Enabled = false;
        //            e.Cell.Items["lblNumber"].Enabled = false;
        //        }
        //        else
        //        {
        //            frmAttendanceStatSelfDetail newFrm = new frmAttendanceStatSelfDetail();
        //            newFrm.Type = e.Cell.Items["lblID"].Text;           //ǩ������
        //            newFrm.Daytime = Daytime;                   //ʱ��
        //            newFrm.UserID = UserID;                     //�û�ID
        //            newFrm.TitleText = lblName.Text + e.Cell.Items["lblID"].Text + "��¼";
        //            this.Show(newFrm);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Toast(ex.Message);
        //    }
        //}
    }
}