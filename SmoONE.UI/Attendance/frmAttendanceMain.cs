using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using System.Data.SqlClient;
using SmoONE.CommLib;
using System.Windows.Forms;

namespace SmoONE.UI.Attendance
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� ����ǩ������(����ͳ�Ʋ鿴����)
    // ******************************************************************
    partial class frmAttendanceMain : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public int LocState;      //��λ״̬
        public string Location;        //��λǩ������λ��
        public string DayTime;   //�鿴ͳ��ʱ�򣬲鿴�ľ�������
        DateTime AL_Date;   //ǩ��ʱ��
        public int enter;  //ҳ�����״̬ 1��ǩ��   2��˵��ͳ�Ʋ鿴  
        public  string UserID;     //�û�ID
        public string CommutingType;    //���°�״̬
        public bool? hasgps = null;    //�Ƿ��ȡGPS   
        internal ALInputDto newLog = new ALInputDto();     //�½���־�������
        AutofacConfig AutofacConfig = new AutofacConfig();//����������     
        #endregion
        /// <summary>
        /// ҳ����أ���ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceMain_Load(object sender, EventArgs e)
        {
            try
            {
                if ((ATMainState)Enum.Parse(typeof(ATMainState), enter.ToString()) == ATMainState.����ǩ��)
                {
                    UserID = Client.Session["U_ID"].ToString();    //�û�ID
                }
                if (string.IsNullOrEmpty(DayTime) == true)     //����ǩ��ҳ��
                {
                    lblWeekDay.Text = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);  //��ʾ��ǰ���ڼ�
                    lblDate.Text = DateTime.Now.ToString("yyyy��MM��dd��");      //��ʾ��ǰ����
                }
                else              //����ͳ�Ʋ鿴ҳ��
                {
                    string[] Day = DayTime.Split(' ');
                    lblWeekDay.Text = Day[4];              //��������ʾ���ڼ�
                    lblDate.Text = Day[0];             //��������ʾ������
                }
                WorkTimeDto WorkTime = AutofacConfig.attendanceService.GetCurrantASByUser(UserID);
                if ((ATMainState)Enum.Parse(typeof(ATMainState), enter.ToString()) == ATMainState.ͳ�Ʋ鿴)
                {
                    Bind();
                }
                else
                {
                    if (WorkTime != null)
                    {
                       // this.gps1.TimeOut = 2000;
                        //��ȡ��λ��Ϣ����γ�Ⱥ�����λ��
                        if ((ATMainState)Enum.Parse(typeof(ATMainState), enter.ToString()) != ATMainState.ͳ�Ʋ鿴)        //ֻ�н���ǩ��ʱ��Ż�ȡ��λ��Ϣ
                        {
                            this.gps1.GetGps(new GpsCallBackHandler((object ss, Smobiler.Core.Controls.GPSResultArgs ee) =>
                            {
                                if (ee.isError  == true)
                                {
                                    hasgps = false;
                                }
                                else
                                {
                                    hasgps = true;
                                }
                                if ((ee.Latitude == Convert.ToDouble(WorkTime.AT_Latitude) && ee.Longitude == Convert.ToDouble(WorkTime.AT_Longitude)) || ATAddressDistance.GetDistance(Convert.ToDouble(ee.Latitude), Convert.ToDouble(ee.Longitude), Convert.ToDouble(WorkTime.AT_Latitude), Convert.ToDouble(WorkTime.AT_Longitude)) < Convert.ToDouble(WorkTime.AT_AllowableDeviation.ToString()))
                                {
                                    LocState = 1;
                                    Location = ee.Location;        //��λǩ������λ��
                                }
                                else
                                {
                                    LocState = 2;
                                }
                            }));
                        }
                        Bind();      //���ݰ�             
                    }
                    else
                    {
                        this.lblInfo.Visible = true;
                        this.lblInfo.Text = "����ʱû���Ű࣬����ϵ����Ա��";
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ���ݰ�
        /// </summary>
        public  void Bind()
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("ID");           //ǩ��ģ����
                table.Columns.Add("Picture");      //ǩ��ͼƬ
                table.Columns.Add("Description");  //ǩ������
                table.Columns.Add("Time", typeof(System.String));         //ǩ��ʱ��
                table.Columns.Add("Img");          //ͼƬID
                table.Columns.Add("Action");       //ǩ����ǩ��
                table.Columns.Add("Info");       //��ʾ��ǰ�Ƿ���ǩ�����Ƿ��Ѿ�ǩ��
                if ((ATMainState)Enum.Parse(typeof(ATMainState), enter.ToString()) == ATMainState.ͳ�Ʋ鿴)        //ͳ��ҳ�����鿴
                {
                    List<ALDto> listStats = AutofacConfig.attendanceService.GetALByUserAndDate(UserID, Convert.ToDateTime(DayTime));
                    if (listStats != null && listStats.Count > 0)
                    {
                        CommutingType = listStats[0].AL_CommutingType;      //���°�����
                        if ((WorkTimeType)Enum.Parse(typeof(WorkTimeType), listStats[0].AL_CommutingType) == WorkTimeType.һ��һ���°�)            //һ��һ���°�
                        {
                            table.Rows.Add("4", "shangban2", "�ϰ�", listStats[0].AL_OnTime.ToString("HH:mm"), null, null, "δ��ʼ");
                            table.Rows.Add("5", "xiaban2", "�°�", listStats[1].AL_OnTime.ToString("HH:mm"), null, null, "δ��ʼ");
                        }
                        else               //һ�������°�
                        {
                            table.Rows.Add("0", "shangban2", "�����ϰ�", listStats[0].AL_OnTime.ToString("HH:mm"), null, null, "δ��ʼ");
                            table.Rows.Add("1", "gongzuozhong2", "�����°�", listStats[1].AL_OnTime.ToString("HH:mm"), null, null, "δ��ʼ");
                            table.Rows.Add("2", "gongzuozhong2", "�����ϰ�", listStats[2].AL_OnTime.ToString("HH:mm"), null, null, "δ��ʼ");
                            table.Rows.Add("3", "xiaban2", "�����°�", listStats[3].AL_OnTime.ToString("HH:mm"), null, null, "δ��ʼ");
                        }
                    }
                }
                else        //����ǩ��ҳ��
                {
                    WorkTimeDto Mode = AutofacConfig.attendanceService.GetCurrantASByUser(UserID);
                    CommutingType = Mode.AT_CommutingType;           //���°�����
                    if (Mode.AT_ASType == "�ϰ�")
                    {
                        if ((WorkTimeType)Enum.Parse(typeof(WorkTimeType), Mode.AT_CommutingType) == WorkTimeType.һ��һ���°�)            //һ��һ���°�
                        {
                            table.Rows.Add("4", "shangban1", "�ϰ�", Convert.ToDateTime(Mode.AT_StartTime).ToString("HH:mm"), null, null, "δ��ʼ");
                            table.Rows.Add("5", "xiaban1", "�°�", Convert.ToDateTime(Mode.AT_EndTime).ToString("HH:mm"), null, null, "δ��ʼ");
                        }
                        else                     //һ�������°�
                        {
                            table.Rows.Add("0", "shangban1", "�����ϰ�", Convert.ToDateTime(Mode.AT_AMStartTime).ToString("HH:mm"), null, null, "δ��ʼ");
                            table.Rows.Add("1", "gongzuozhong1", "�����°�", Convert.ToDateTime(Mode.AT_AMEndTime).ToString("HH:mm"), null, null, "δ��ʼ");
                            table.Rows.Add("2", "gongzuozhong1", "�����ϰ�", Convert.ToDateTime(Mode.AT_PMStartTime).ToString("HH:mm"), null, null, "δ��ʼ");
                            table.Rows.Add("3", "xiaban1", "�����°�", Convert.ToDateTime(Mode.AT_PMEndTime).ToString("HH:mm"), null, null, "δ��ʼ");
                        }
                    }
                    else
                    {
                        this.lblInfo.Visible = true;
                        this.lblInfo.Text = "��������Ϣ�գ�";
                    }
                }
                List<ALDto> listLogs = AutofacConfig.attendanceService.GetALByUserAndDate(UserID, DateTime.Now);        //�жϵ����Ƿ��Ѿ���ǩ��
                if (listLogs.Count == table.Rows.Count && (ATMainState)Enum.Parse(typeof(ATMainState), enter.ToString()) == ATMainState.����ǩ��)       //����������������ǩ��
                {
                    DayTime = DateTime.Now.ToString();            //��DayTimeֵ��ʹ��ҳ����ʾͳ��ģʽ
                }
                if (string.IsNullOrEmpty(DayTime) == false)       //����������ͳ�Ʋ鿴ҳ��
                {
                    List<ALDto> listStats = AutofacConfig.attendanceService.GetALByUserAndDate(UserID, Convert.ToDateTime(DayTime));
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        //table.Rows[i]["Action"] = null;         //����ǩ��(ǩ��)��ť
                        foreach (ALDto Row in listStats)
                        {
                            if (Row.AL_OnTime.ToString("HH:mm") == table.Rows[i]["Time"].ToString())
                            {
                                AL_Date = Row.AL_Date;                  //ǩ��ʱ��
                                switch ((AttendanceType)Enum.Parse(typeof(AttendanceType), Row.AL_Status))
                                {
                                    case AttendanceType.δǩ��:
                                    case AttendanceType.δǩ��:
                                        table.Rows[i]["Info"] = Row.AL_Status;
                                        break;
                                    case AttendanceType.�ٵ�:
                                    case AttendanceType.����:
                                    case AttendanceType.׼��:
                                        if (Convert.ToInt32(table.Rows[i]["ID"].ToString()) % 2 == 0)       //������
                                        {
                                            table.Rows[i]["Info"] = "��ǩ��" + "  " + AL_Date.ToString("HH:mm");
                                        }
                                        else
                                        {
                                            table.Rows[i]["Info"] = "��ǩ��" + "  " + AL_Date.ToString("HH:mm");
                                        }
                                        break;
                                }
                            }
                            if (string.IsNullOrEmpty(Row.AL_Reason) == false && Row.AL_LogTimeType == table.Rows[i]["Description"] + "ʱ��")   //����гٵ�����ԭ������ʾ��Ϣ��ʾ��
                            {
                                table.Rows[i]["Img"] = "!\\ue85d000146223";
                            }
                            ATMainPicture.AllBlackWhite(table);           //ǩ��״̬�£���ͼƬ��ʾΪ�ڰ�
                        }
                    }
                }
                else                //�жϵ����ļ����Ѿ�ǩ�������ҽ�����ʾ(ǩ��ҳ��)
                {
                    int x = 0;
                    while (x < table.Rows.Count - 1)    //����һ������δǩ������ʱ���ѳ�����һ�����ǩ��ʱ��ʱ����ʾδǩ��
                    {
                        if (DateTime.Now >= Convert.ToDateTime(table.Rows[x + 1]["Time"]))
                        {
                            if (Convert.ToInt32(table.Rows[x]["ID"].ToString()) % 2 == 0)
                            {
                                table.Rows[x]["Info"] = "δǩ��";
                            }
                            else
                            {
                                table.Rows[x]["Info"] = "δǩ��";
                            }
                            x++;
                        }
                        else
                        {
                            x++;
                            break;
                        }
                    }
                    int count = 0;             //��ǰҳ��δǩ����δǩ�˵�������
                    foreach (DataRow Row in table.Rows)
                    {
                        if (Row["Info"].ToString() == "δǩ��" || Row["Info"].ToString() == "δǩ��")
                        {
                            count++;
                        }
                    }
                    for (int i = listLogs.Count; i < count; i++)//��δǩ�������������ݿ��Ѽ�¼δǩ����ʱ
                    {
                        if (table.Rows[i]["Info"].ToString() == "δǩ��" || table.Rows[i]["Info"].ToString() == "δǩ��")
                        {
                            newLog.AL_Reason = null;
                            switch ((StatisticsTime)Enum.Parse(typeof(StatisticsTime), table.Rows[i]["Description"] + "ʱ��"))
                            {
                                case StatisticsTime.�����ϰ�ʱ��:
                                    newLog.AL_LogTimeType = StatisticsTime.�����ϰ�ʱ��;
                                    break;
                                case StatisticsTime.�����°�ʱ��:
                                    newLog.AL_LogTimeType = StatisticsTime.�����°�ʱ��;
                                    break;
                                case StatisticsTime.�����ϰ�ʱ��:
                                    newLog.AL_LogTimeType = StatisticsTime.�����ϰ�ʱ��;
                                    break;
                                case StatisticsTime.�����°�ʱ��:
                                    newLog.AL_LogTimeType = StatisticsTime.�����°�ʱ��;
                                    break;
                                case StatisticsTime.�ϰ�ʱ��:
                                    newLog.AL_LogTimeType = StatisticsTime.�ϰ�ʱ��;
                                    break;
                                case StatisticsTime.�°�ʱ��:
                                    newLog.AL_LogTimeType = StatisticsTime.�°�ʱ��;
                                    break;
                            }
                            newLog.AL_Date = DateTime.Now;      //��������
                            newLog.AL_UserID = UserID;          //�û�ID
                            if ((WorkTimeType)Enum.Parse(typeof(WorkTimeType), CommutingType) == WorkTimeType.һ��һ���°�)
                            {
                                newLog.AL_CommutingType = WorkTimeType.һ��һ���°�;       //���°�����
                            }
                            else
                            {
                                newLog.AL_CommutingType = WorkTimeType.һ������°�;        //���°�����
                            }
                            newLog.AL_Position = "��ǩ���ص�";                 //���ڵص�
                            newLog.AL_OnTime = Convert.ToDateTime(table.Rows[i]["Time"]);    //ǩ��׼��ʱ��
                            if (Convert.ToInt32(table.Rows[i]["ID"].ToString()) % 2 == 0)            //�жϿ���״̬
                            {
                                newLog.AL_Status = AttendanceType.δǩ��;
                            }
                            else
                            {
                                newLog.AL_Status = AttendanceType.δǩ��;
                            }
                            ReturnInfo r = AutofacConfig.attendanceService.AddAttendanceLog(newLog);
                        }
                    }
                    if (Convert.ToInt32(table.Rows[x - 1]["ID"]) % 2 == 0)
                    {
                        table.Rows[x - 1]["Action"] = "ǩ��";    //��δǩ���ĵ�һ�У���ʾǩ��(ǩ��)��ť 
                    }
                    else
                    {
                        table.Rows[x - 1]["Action"] = "ǩ��";    //��δǩ���ĵ�һ�У���ʾǩ��(ǩ��)��ť 
                    }
                    List<ALDto> listNewLogs = AutofacConfig.attendanceService.GetALByUserAndDate(UserID, DateTime.Now);        //�жϵ����Ƿ��Ѿ���ǩ��                
                    for (int i = 0; i < listNewLogs.Count; i++)
                    {
                        table.Rows[i]["Action"] = null;           //������ǩ�����е�ǩ����ť
                        if (Convert.ToInt32(table.Rows[i + 1]["ID"]) % 2 == 0)
                        {
                            table.Rows[i + 1]["Action"] = "ǩ��";    //��δǩ���ĵ�һ�У���ʾǩ��(ǩ��)��ť 
                        }
                        else
                        {
                            table.Rows[i + 1]["Action"] = "ǩ��";    //��δǩ���ĵ�һ�У���ʾǩ��(ǩ��)��ť 
                        }
                        foreach (ALDto Row in listNewLogs)
                        {
                            AL_Date = Row.AL_Date;                 //����ʱ��
                            if (Convert.ToInt32(table.Rows[i]["ID"].ToString()) % 2 == 0 && listNewLogs[i].AL_Status != "δǩ��")         //��ʾ��ǩ��ǩ��ʱ��
                            {
                                table.Rows[i]["Info"] = "��ǩ��" + "  " + AL_Date.ToString("HH:mm");
                            }
                            else if (Convert.ToInt32(table.Rows[i]["ID"].ToString()) % 2 == 1 && listNewLogs[i].AL_Status != "δǩ��")
                            {

                                table.Rows[i]["Info"] = "��ǩ��" + "  " + AL_Date.ToString("HH:mm");
                            }
                            if (Row.AL_OnTime == Convert.ToDateTime(table.Rows[i]["Time"]))
                            {
                                if (Row.AL_Status == "δǩ��" || Row.AL_Status == "δǩ��")         //��ʾδǩ
                                {
                                    table.Rows[i]["Info"] = Row.AL_Status;
                                }
                                if (string.IsNullOrEmpty(Row.AL_Reason) == false)   //����гٵ�����ԭ����ʾ��Ϣ��ʾ��
                                {
                                    table.Rows[i]["Img"] = "!\\ue85d000146223";
                                }
                            }
                        }
                        ATMainPicture.BlackWhite(table, i);        //�Ѿ�ǩ���������У�ͼƬ��ʾ�ڰ�
                    }
                    ATMainPicture.getPictures(table);        //��ÿ���������ƥ�䣬��ʾ��ȷ��ͼƬ��Ϣ
                }
                gridATdata.Rows.Clear();
                if (table.Rows.Count > 0)           //������ݲ�Ϊ��
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
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceMain_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceMain_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
        ///// <summary>
        ///// ǩ���鿴ʱ�򣬵���������ʾǩ������ϸ��Ϣ
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gridATdata_CellClick(object sender, GridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if ((ATMainState)Enum.Parse(typeof(ATMainState), enter.ToString()) == ATMainState.ͳ�Ʋ鿴)                   //������ǲ鿴ǩ��ҳ��
        //        {
        //            List<ALDto> listStats = AutofacConfig.attendanceService.GetALByUserAndDate(UserID, Convert.ToDateTime(DayTime));
        //            foreach (ALDto Row in listStats)
        //            {
        //                if (Row.AL_OnTime.ToString("HH:mm") == e.Cell.Items["lblTime"].Text && string.IsNullOrEmpty(Row.AL_Reason) == false)
        //                {
        //                    DetailLayout.LayoutData.Items["lblLocation"].Text = Row.AL_Position;        //��ʾ���ڵص�
        //                    DetailLayout.LayoutData.Items["lblReason"].Text = Row.AL_Reason;            //��ʾ�ٵ�����ԭ��
        //                    DetailLayout.Show();
        //                }
        //            }
        //        }
        //        else if (string.IsNullOrEmpty(e.Cell.Items["btnCheck"].DefaultValue.ToString()) == true)                  //��ǰ������ǩ��
        //        {
        //            List<ALDto> listLogs = AutofacConfig.attendanceService.GetALByUserAndDate(UserID, DateTime.Now);
        //            foreach (ALDto Row in listLogs)
        //            {
        //                if (Row.AL_OnTime.ToString("HH:mm") == e.Cell.Items["lblTime"].Text && string.IsNullOrEmpty(Row.AL_Reason) == false)
        //                {
        //                    DetailLayout.LayoutData.Items["lblLocation"].Visible = false;         //���ؿ��ڵص���Ϣ
        //                    DetailLayout.LayoutData.Items["imgLocation"].Visible = false;         //����λ��ͼ��
        //                    DetailLayout.LayoutData.Items["lblReason"].Top = 0;
        //                    DetailLayout.LayoutData.Items["lblReason"].Text = Row.AL_Reason;            //��ʾ�ٵ�����ԭ��
        //                    DetailLayout.Show();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Toast(ex.Message);
        //    }
        //}
    }
}