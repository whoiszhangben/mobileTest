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
    // ��Ҫ���ݣ� ѡ�����ڣ���ǩ����Ա��ǩ��״��ͳ��
    // ******************************************************************
    partial class frmAttendanceStatMan : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string DayTime;
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ҳ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatMan_Load(object sender, EventArgs e)
        {
            try
            {
                this.lblDate.Text = DayTime;        //������
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
                List<string> Users = AutofacConfig.attendanceService.GetUserNameByDate(Convert.ToDateTime(DayTime));
                DataTable table = new DataTable();
                table.Columns.Add("U_ID");          //�û����
                table.Columns.Add("Pict");        //�û�ͷ��     
                table.Columns.Add("Name");        //�û��ǳ�
                table.Columns.Add("Total");       //�û�Ӧǩ������
                table.Columns.Add("Al");          //�û�����ǩ������
                table.Columns.Add("ProAl");          //�û�����ǩ������
                table.Columns.Add("Late");        //�û��ٵ�����
                table.Columns.Add("ProLate");        //�û��ٵ�����
                table.Columns.Add("Early");       //�û����˴���
                table.Columns.Add("ProEarly");       //�û����˴���
                table.Columns.Add("No");          //�û�δǩ����
                table.Columns.Add("ProNo");          //�û�δǩ����
                table.Columns.Add("Absenteeism"); //��������
                table.Columns.Add("ISAbsenteeism");//�Ƿ����               
                foreach (string User in Users)
                {
                    DailyStatisticsDto Stat = AutofacConfig.attendanceService.GetUserDailyStatistics(User, Convert.ToDateTime(DayTime));
                    UserDetailDto UserInfo = AutofacConfig.userService.GetUserByUserID(User);

                    string absenteeism = "";
                    bool isAbsenteeism = false;
                    if (Stat.DS_AllCount > 0 & Stat.DS_AllCount.Equals(Stat.DS_NoSignInCount + Stat.DS_NoSignOutCount))
                    {
                        absenteeism = "ȫ���������";
                        isAbsenteeism = true;
                    }
                    float proAl=0;
                    float proLate = 0;
                    float proEarly = 0;
                    float proNo = 0;
                    if (Stat.DS_AllCount > 0)
                    {
                        proAl =(float ) Stat.DS_InTimeCount / Stat.DS_AllCount;
                        proLate = (float)Stat.DS_ComeLateCount / Stat.DS_AllCount;
                        proEarly = (float)Stat.DS_LeaveEarlyCount / Stat.DS_AllCount;
                        proNo = (float)(Stat.DS_NoSignInCount + Stat.DS_NoSignOutCount) / Stat.DS_AllCount;
                    }
                    table.Rows.Add(UserInfo.U_ID, UserInfo.U_Portrait, UserInfo.U_Name, UserInfo.U_Name + " Ӧǩ�� " + Stat.DS_AllCount + " ��", Stat.DS_InTimeCount + " ��",proAl, Stat.DS_ComeLateCount + " ��",proLate , Stat.DS_LeaveEarlyCount + " ��",proEarly , Stat.DS_NoSignInCount + Stat.DS_NoSignOutCount + " ��",proNo , absenteeism, isAbsenteeism);
                }
                gridATdata.Rows.Clear();//���ĳ�տ���ͳ���б�����
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
        /// �ֻ��Դ����ؼ����˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatMan_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ���ϽǷ��ؼ����˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatMan_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }

        private void title1_Load(object sender, EventArgs e)
        {

        }
      
    }
}