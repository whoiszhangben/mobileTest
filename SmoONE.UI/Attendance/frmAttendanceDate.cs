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
    // ��Ҫ���ݣ� ����ģ�幤������ѡ�����
    // ******************************************************************
    partial class frmAttendanceDate : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string ATNo;//����ģ����
        public string weekdays;//��������
        public AttendanceWorkDate atWorkDate;//����ʱ��
        public List<AT_CDInputDto> listatcdInput;//�Զ��忼������
        #endregion
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void Bind()
        {
            if (string.IsNullOrEmpty(weekdays) == false )
            {
                 string [] weeks =weekdays.Split (',');
                foreach (string day in weeks)
                {
                    radioDate.SetRadioButtonCheckedByID(day);
                  
                }
            }
        }
        /// <summary>
        /// ��ת���Զ������ڽ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pCalendar_Press(object sender, EventArgs e)
        {
            try
            {
                bool isOpenAt = false;//�Ƿ�������
                switch (atWorkDate .AT_Type )
                {
                    case WorkTimeType.һ��һ���°�:
                        if (Convert.IsDBNull(atWorkDate.AT_StartTime) == false || Convert.IsDBNull(atWorkDate.AT_EndTime) == false)
                        {

                            isOpenAt = true;
                        }
                        break;
                    case WorkTimeType.һ������°�:
                        if (Convert.IsDBNull(atWorkDate.AT_AMStartTime) == false || Convert.IsDBNull(atWorkDate.AT_AMEndTime) == false || Convert.IsDBNull(atWorkDate.AT_PMStartTime) == false || Convert.IsDBNull(atWorkDate.AT_PMEndTime) == false)
                        {
                            isOpenAt = true;
                        }
                        break;
                }
                if (isOpenAt == true)
                {
                    frmAttendanceCalendarSetting frm = new frmAttendanceCalendarSetting();
                    if (string.IsNullOrEmpty(ATNo) == false)
                    {
                        frm.ATNo = ATNo;
                    }
                    frm.weekdays = weekdays;
                    frm.atWorkDate = atWorkDate;
                    frm.listatcdInput = listatcdInput;
                    Show(frm, (MobileForm form, object args) =>
                    {
                        if (frm.ShowResult == ShowResult.Yes)
                        {
                            ShowResult = ShowResult.Yes;
                            listatcdInput = frm.listatcdInput;
                            if (listatcdInput.Count > 0)
                            {
                                List<AT_CDInputDto> listdeleteSetting = new List<AT_CDInputDto>();//��Ҫɾ�����Զ������ڼ���
                                foreach (AT_CDInputDto atcdInput in listatcdInput)
                                {
                                    //����Զ��������뿼�ڹ��������Զ���������ͬ�Ϳ���ʱ����ͬʱ��ɾ�����Զ��忼��
                                    bool isdeleteSetting = false;
                                    int selectday = (int)Enum.Parse(typeof(Week), atcdInput.AT_CD_Date.DayOfWeek.ToString());//��ȡѡ��������ĳ�����ڵĵڼ���
                                    WorkOrRest selectdateType;
                                    //�����ǰ�Զ��������ڿ��ڹ��������У����Զ�����������Ϊ�ϰ࣬����Ϊ��Ϣ
                                    if (weekdays.Split(',').Contains(selectday.ToString()) == true)
                                    {
                                        selectdateType = WorkOrRest.�ϰ�;
                                    }
                                    else
                                    {
                                        selectdateType = WorkOrRest.��Ϣ;
                                    }
                                    if (atcdInput.AT_CD_CommutingType == atWorkDate.AT_Type)
                                    {
                                        if (selectdateType == atcdInput.AT_CD_CDType)
                                        {
                                            switch (atcdInput.AT_CD_CDType )
                                            {
                                                case  WorkOrRest.��Ϣ:
                                                    isdeleteSetting = true;
                                                    break;
                                                case WorkOrRest.�ϰ�:
                                                    switch (atcdInput.AT_CD_CommutingType)
                                                    {
                                                        case WorkTimeType.һ��һ���°�:

                                                            if (atcdInput.AT_CD_StartTime != null & (Convert.ToDateTime(atcdInput.AT_CD_StartTime).ToString("HH:mm") == Convert.ToDateTime(atWorkDate.AT_StartTime).ToString("HH:mm"))
                                                                & atcdInput.AT_CD_EndTime != null & (Convert.ToDateTime(atcdInput.AT_CD_EndTime).ToString("HH:mm") == Convert.ToDateTime(atWorkDate.AT_EndTime).ToString("HH:mm")))
                                                            {
                                                                isdeleteSetting = true;
                                                            }
                                                            break;
                                                        case WorkTimeType.һ������°�:
                                                            if (atcdInput.AT_CD_AMStartTime != null & (Convert.ToDateTime(atcdInput.AT_CD_AMStartTime).ToString("HH:mm") == Convert.ToDateTime(atWorkDate.AT_AMStartTime).ToString("HH:mm"))
                                                                & atcdInput.AT_CD_AMEndTime != null & (Convert.ToDateTime(atcdInput.AT_CD_AMEndTime).ToString("HH:mm") == Convert.ToDateTime(atWorkDate.AT_AMEndTime).ToString("HH:mm"))
                                                                & atcdInput.AT_CD_PMStartTime != null & (Convert.ToDateTime(atcdInput.AT_CD_PMStartTime).ToString("HH:mm") == Convert.ToDateTime(atWorkDate.AT_PMStartTime).ToString("HH:mm"))
                                                                & atcdInput.AT_CD_PMEndTime != null & (Convert.ToDateTime(atcdInput.AT_CD_PMEndTime).ToString("HH:mm") == Convert.ToDateTime(atWorkDate.AT_PMEndTime).ToString("HH:mm")))
                                                            {
                                                                isdeleteSetting = true;
                                                            }
                                                            break;
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                    if (isdeleteSetting ==true )
                                    {
                                        listdeleteSetting.Add(atcdInput);
                                    }
                                }
                                if (listdeleteSetting.Count > 0)
                                {
                                    foreach (AT_CDInputDto atcdInput in listdeleteSetting)
                                    {
                                        listatcdInput.Remove(atcdInput);
                                    }
                                }
                            }
                        }
                    });
                }
                else
                {
                    throw new Exception("���ȿ����ϰ���°࿼�ڣ�");
                }
            }
            catch (Exception ex)
            {
                Toast (ex.Message , ToastLength.SHORT);
            }
        }
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceDate_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceDate_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ҳ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceDate_Load(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioDate_ButtonPress(object sender, RadioButtonPressEventArgs e)
        {
            ShowResult = ShowResult.Yes;
            upATDate();
        }
        /// <summary>
        /// ��������
        /// </summary>
        private void upATDate()
        {
            weekdays = null;
            foreach (RadioButton rd in radioDate.CheckedButtons)
            {
                if (rd.Checked==true )
                {
                    if (string.IsNullOrEmpty(weekdays) == true)
                    {
                        weekdays = rd.Value.ToString();

                    }
                    else
                    {
                        weekdays += "," + rd.Value.ToString();
                    }
                }         
            }
        }

    }
}