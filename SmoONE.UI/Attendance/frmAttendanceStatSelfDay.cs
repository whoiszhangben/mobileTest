using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.Data;

namespace SmoONE.UI.Attendance
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� ���˿��ڰ���ͳ��ҳ��
    // ******************************************************************
    partial class frmAttendanceStatSelfDay : Smobiler.Core.Controls.MobileForm
    {

        #region "definition"
        internal string UserID;         //�û�ID
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ���Ͻǰ�ť���˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatSelfDay_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// �ֻ��Դ����ؼ����˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatSelfDay_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }
        /// <summary>
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatSelfDay_Load(object sender, EventArgs e)
        {
            try
            {
                UserID = Client.Session["U_ID"].ToString();                //�û�ID
                this.btnYear.Text = DateTime.Now.Year.ToString() + "�ꨋ";          //�����ʾ
                this.btnMonth.Text = DateTime.Now.Month.ToString() + "�¨�";        //�·���ʾ
                PopListGroup popYearG = new PopListGroup();
                popYearG.Title = "��ѡ�����";
                popListYear.Groups.Add(popYearG);
                for (int i = DateTime.Now.Year; DateTime.Now.Year - i <10; i--)        //������ѡ��Χ
                {
                    PopListItem YearItem = new PopListItem();
                    YearItem.Text = i.ToString();
                    popYearG.Items.Add(YearItem);
                    if (i == DateTime.Now.Year)
                    {
                        popListYear.SetSelections(YearItem);
                    }
                }
                popListMonth.SetSelections(popListMonth.Groups[0].Items[(DateTime.Now.Month - 1)]);
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        private void Bind()
        {
            try
            {
                DataTable table = new DataTable();
                string[] Year = this.btnYear.Text.Split('��');           //�����ʾ
                string[] Month = this.btnMonth.Text.Split('��');         //�·���ʾ
                List<DateTime> listDate = AutofacConfig.attendanceService.GetDayOfMonthlyStatistics(UserID, Convert.ToDateTime(Year[0] + Month[0]));
                table.Columns.Add("Day");      //������Ҫ���ڵ�����
                foreach (DateTime Row in listDate)
                {
                    string Time = Row.ToString("yyyy��M��d��    dddd", new System.Globalization.CultureInfo("zh-CN"));
                    table.Rows.Add(Time);
                }
                gridATdata.Rows.Clear();         //�������
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
        /// �������ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButYear_Click(object sender, EventArgs e)
        {
            popListYear.Show();    //��ʾ���ѡ���б�
        }
        /// <summary>
        /// �����·�ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButMonth_Click(object sender, EventArgs e)
        {
            popListMonth.Show();     //��ʾ�·�ѡ���б�
        }
        /// <summary>
        /// ����鿴���ڱ���ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButCheck_Click(object sender, EventArgs e)
        {
            try
            {
                frmAttendanceStatSelf newFrm = new frmAttendanceStatSelf();
                string[] Year = this.btnYear.Text.Split('��');
                string[] Month = this.btnMonth.Text.Split('��');
                newFrm.UserID = UserID;          //�û�ID���ݵ��¸�ҳ��
                newFrm.Daytime = Year[0] + Month[0];       //�����´��ݵ��¸�ҳ��
                this.Show(newFrm);
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /////// <summary>
        /////// ����鿴��������ҳ��
        /////// </summary>
        /////// <param name="sender"></param>
        /////// <param name="e"></param>
        ////private void gridATdata_CellClick(object sender, GridViewCellEventArgs e)
        ////{
        ////    try
        ////    {
        ////        frmAttendanceMain frmMain = new frmAttendanceMain();
        ////        frmMain.DayTime = e.Cell.Items["lblDay"].Text;         //��ѡ��ʱ�䴫�ݵ��¸�ҳ��
        ////        frmMain.enter = (int)Enum.Parse(typeof(ATMainState), ATMainState.ͳ�Ʋ鿴.ToString());
        ////        frmMain.UserID = UserID;
        ////        this.Show(frmMain);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Toast(ex.Message);
        ////    }
        ////}
        /// <summary>
        ///  ѡ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popListYear_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popListYear.Selection != null)         //���ѡ������ݣ�����ҳ������
                {
                    this.btnYear.Text = popListYear.Selection.Text + "�ꨋ";
                    Bind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        ///  ѡ���·�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popListMonth_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popListMonth.Selection != null)     //���ѡ�����·ݣ�����ҳ������
                {
                    this.btnMonth.Text = popListMonth.Selection.Text + "�¨�";
                    Bind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}