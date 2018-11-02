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
    // ��Ҫ���ݣ� ���ڱ���ͳ��(����ͳ��)
    // ******************************************************************
    partial class frmAttendanceStatMonthTypeDay : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string UserID;//�û�
        public string atType;         //����
        public string Daytime;         //ʱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
      

        /// <summary>
        /// �����û����������ͺ��·ݻ�ȡ����
        /// </summary>
        private void Bind()
        {
            try
            {
                lblATMonth.Text = Convert.ToDateTime(Daytime).ToString("yyyy��  M��");
                List<DateTime> listDate = new List<DateTime>();
                if (string.IsNullOrEmpty(UserID) == false & string.IsNullOrEmpty(Daytime) == false & string.IsNullOrEmpty(atType) == false)
                {
                    listDate = AutofacConfig.attendanceService.GetDayOfType(UserID, Convert.ToDateTime(Daytime), (StatisticsType)Enum.Parse(typeof(StatisticsType), atType));
                 switch ((StatisticsType)Enum.Parse(typeof(StatisticsType), atType))
                    {
                        case StatisticsType.����:
                            gridATTypeDaydata.TemplateControlName = "frmATStatSelfDetailDayLayout";
                            break;
                        case StatisticsType.׼��:
                        case StatisticsType.�ٵ�:
                        case StatisticsType.����:
                        case StatisticsType.δǩ��:
                        case StatisticsType.δǩ��:
                            gridATTypeDaydata.TemplateControlName = "frmAttendanceStatDayLayout";
                            break;
                    }
                }
                gridATTypeDaydata.Rows.Clear();//�������
                if (listDate.Count > 0)
                {
                    DataTable table = new DataTable();
                    switch ((StatisticsType)Enum.Parse(typeof(StatisticsType), atType))
                    {
                        case StatisticsType.����:
                            table.Columns.Add("AL_Date");         //ǩ������
                            table.Columns.Add("AL_DateDesc");         //ǩ����������
                            foreach (DateTime Row in listDate)
                            {
                                string atSignTime = Row.ToString("yyyy��M��d��    dddd", new System.Globalization.CultureInfo("zh-CN"));
                                table.Rows.Add(Row,atSignTime);
                            }
                            break;
                        case StatisticsType.׼��:
                        case StatisticsType.�ٵ�:
                        case StatisticsType.����:
                        case StatisticsType.δǩ��:
                        case StatisticsType.δǩ��:
                           table.Columns.Add("Day");         //ǩ������
                            foreach (DateTime Row in listDate)
                            {
                                string atSignTime = Row.ToString("yyyy��M��d��    dddd", new System.Globalization.CultureInfo("zh-CN"));
                                table.Rows.Add(atSignTime);
                            }
                            break;
                    }
                    
                    if (table.Rows.Count > 0)
                    {
                        this.gridATTypeDaydata.DataSource = table;
                        this.gridATTypeDaydata.DataBind();
                    }
                }
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ��ʼ��ҳ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatMonthTypeDay_Load(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatMonthTypeDay_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatMonthTypeDay_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}