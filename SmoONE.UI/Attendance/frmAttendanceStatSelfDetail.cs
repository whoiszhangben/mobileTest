using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using System.Data;

namespace SmoONE.UI.Attendance
{
        // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
        // ��Ҫ���ݣ� �û��������������¼����
        // ******************************************************************
    partial class frmAttendanceStatSelfDetail : Smobiler.Core.Controls.MobileForm
    {
        
        #region "definition"
        internal string UserID;      //�û�ID
        public string Type;          //ǩ������ 
        public string Daytime;       //ѡ��ʱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����ؼ����˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatSelfDetail_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmAttendanceStatSelfDetail_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// ҳ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatSelfDetail_Load(object sender, EventArgs e)
        {
            try
            {
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// �����ݣ� ��ʾ��Ҫ�鿴ǩ�������µ�����
        /// </summary>
        private void Bind()
        {
            try
            {
                List<StatisticsType> listType = new List<StatisticsType>();
                List<ALDto> listATlog = new List<ALDto>();
                listView1.Rows.Clear();
                listView2.Rows.Clear();
                StatisticsType stype = (StatisticsType)Enum.Parse(typeof(StatisticsType), Type);
                listType.Add(stype);
                listATlog = AutofacConfig.attendanceService.GetAlDtoOfType(UserID, Convert.ToDateTime(Daytime), listType);
                switch (stype)
                {
                    case StatisticsType.δǩ��:
                    case StatisticsType.δǩ��:
                        listView1.Visible = true;
                        listView2.Visible = false ;
                        // listView1.TemplateControlName = "frmATStatSelfDetailDayLayout";
                        if (listATlog.Count > 0)
                        {
                            List<DateTime> listATDate = new List<DateTime>();
                            foreach (ALDto al in listATlog)
                            {
                                if (listATDate.Contains(al.AL_Date.Date) == false)
                                {
                                    listATDate.Add(al.AL_Date.Date);
                                }
                            }
                            //listATDate.Distinct();//ȥ���ظ���������
                            listATDate.Sort();//������������
                            DataTable table = new DataTable();
                            table.Columns.Add("AL_Date");         //ǩ������
                            table.Columns.Add("AL_DateDesc");         //ǩ����������
                            foreach (DateTime atDate in listATDate)
                            {
                                string atSignTime = atDate.ToString("yyyy��M��d��    dddd", new System.Globalization.CultureInfo("zh-CN"));
                                table.Rows.Add(atDate, atSignTime);
                            }
                            listView1.DataSource = table;
                            listView1.DataBind();
                        }
                        break;
                    case StatisticsType.׼��:
                    case StatisticsType.�ٵ�:
                    case StatisticsType.����:
                        listView1.Visible = false ;
                        listView2.Visible = true ;
                        //listView1.TemplateControlName = "frmATStatSelfDetailTypeLayout";
                        if (listATlog.Count > 0)
                        {
                            List<ATStatSelfDetail> listATlogStatistics = new List<ATStatSelfDetail>();
                            foreach (ALDto al in listATlog)
                            {
                                ATStatSelfDetail atDetail = new ATStatSelfDetail();
                                if (string.IsNullOrEmpty(al.AL_Reason) == false)
                                {
                                    switch (stype)
                                    {
                                        case StatisticsType.�ٵ�:
                                            atDetail.AL_Reason = StatisticsType.�ٵ� + "���ɣ�" + al.AL_Reason;
                                            break;
                                        case StatisticsType.����:
                                            atDetail.AL_Reason = StatisticsType.���� + "���ɣ�" + al.AL_Reason;
                                            break;
                                    }
                                }
                                atDetail.AL_DateDesc = "ǩ�� " + al.AL_Date.ToString("yyyy��MM��dd��  HH:mm");
                                atDetail.AL_Position = al.AL_Position;
                                listATlogStatistics.Add(atDetail);
                            }
                            listView2.DataSource = listATlogStatistics;
                            listView2.DataBind();
                        }
                        break;
                }
               
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}