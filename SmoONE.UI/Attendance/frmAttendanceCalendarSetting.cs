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
    // ��Ҫ���ݣ� ����ģ���Զ������ڽ���
    // ******************************************************************
    partial class frmAttendanceCalendarSetting : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string ATNo;//����ģ����
        public AttendanceWorkDate atWorkDate;//����ʱ��
        private WorkOrRest workType;//�Զ�����������
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        public List<AT_CDInputDto> listatcdInput;//�Զ��忼�ڼ���
        private AT_CDInputDto atcdInput ;//�Զ��忼��
        public string weekdays;//��������
        #endregion
    
    
        /// <summary>
        /// �Զ��忼��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnATMode_Click(object sender, EventArgs e)
        {
            try
            {
                //����Զ��忼�ڣ���Ϣ����״̬��ѡ�������ǵ�ǰ���ڻ�ǰ����ǰ���ڣ��򲻿����Զ��忼��
                if (calendar1.SelectDate.Date < DateTime.Now.AddDays(+1).Date)
                {
                    throw new Exception("��ǰ���ڲ����Զ��忼�ڣ�");
                }
                ShowResult = ShowResult.Yes;
                switch (workType)
                {
                    case WorkOrRest.�ϰ�:
                        workType = WorkOrRest.��Ϣ;
                        break;
                    case WorkOrRest.��Ϣ:
                        workType = WorkOrRest.�ϰ�;
                        break;
                }
                 upSettingDateType ();
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceCalendarSetting_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                ShowResult = ShowResult.Yes;
                Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceCalendarSetting_TitleImageClick(object sender, EventArgs e)
        {
            ShowResult = ShowResult.Yes;
            Close();
        }
        /// <summary>
        /// �����Զ�������
        /// </summary>
        private void upSettingDateType()
        {
            if (string.IsNullOrEmpty(ATNo) == true)
            {
                //��������ʱ������ѡ�����ڴ����������ڣ���ģ������Զ��壬����ʾ�Զ���״̬
                if (calendar1.SelectDate.Date >= DateTime.Now.AddDays(+1).Date)
                {
                    string startTime = null; //�ϰ�ʱ��
                    string endTime = null;//�°�ʱ��
                    string amStartTime = null; //�����ϰ�ʱ��
                    string amEndTime = null;//�����°�ʱ��
                    string pmStartTime = null; //�����ϰ�ʱ��
                    string pmEndTime = null;//�����°�ʱ��

                    atcdInput = new AT_CDInputDto();
                    //�����Զ�������
                    atcdInput.AT_CD_Date = calendar1.SelectDate.Date;
                    //�������°�����
                    atcdInput.AT_CD_CommutingType = atWorkDate.AT_Type;

                    //��������ʱ��������֮���ѡ������������Զ����ҿ���ģʽ��ȣ����Զ����������ڸ�ֵ����ǰ�Զ������ڿؼ�
                    if (IsCATSettingDay(atcdInput.AT_CD_Date) == true)
                    {
                        foreach (AT_CDInputDto atcdInputDto in listatcdInput)
                        {
                            if (atcdInputDto.AT_CD_Date.Date.Equals(atcdInput.AT_CD_Date.Date) & atcdInputDto.AT_CD_CommutingType == atWorkDate.AT_Type & workType == WorkOrRest.�ϰ�)
                            {
                                //workType = atcdInputDto.AT_CD_CDType;
                                //�ϰ�ʱ��
                                if (atcdInputDto.AT_CD_StartTime != null)
                                {
                                    startTime = atcdInputDto.AT_CD_StartTime.ToString();
                                }
                                else
                                {
                                    startTime = atWorkDate.AT_StartTime.ToString();
                                }
                                //�°�ʱ��
                                if (atcdInputDto.AT_CD_EndTime != null)
                                {
                                    endTime = atcdInputDto.AT_CD_EndTime.ToString();
                                }
                                else
                                {
                                    endTime = atWorkDate.AT_EndTime.ToString();
                                }
                                //�����ϰ�ʱ��
                                if (atcdInputDto.AT_CD_AMStartTime != null)
                                {
                                    amStartTime = atcdInputDto.AT_CD_AMStartTime.ToString();
                                }
                                else
                                {
                                    amStartTime = atWorkDate.AT_AMStartTime.ToString();
                                }
                                //�����°�ʱ��
                                if (atcdInputDto.AT_CD_AMEndTime != null)
                                {
                                    amEndTime = atcdInputDto.AT_CD_AMEndTime.ToString();
                                }
                                else
                                {
                                    amEndTime = atWorkDate.AT_AMEndTime.ToString();
                                }
                                //�����ϰ�ʱ��
                                if (atcdInputDto.AT_CD_PMStartTime != null)
                                {
                                    pmStartTime = atcdInputDto.AT_CD_PMStartTime.ToString();
                                }
                                else
                                {
                                    pmStartTime = atWorkDate.AT_PMStartTime.ToString();
                                }
                                //�����°�ʱ��
                                if (atcdInputDto.AT_CD_PMEndTime != null)
                                {
                                    pmEndTime = atcdInputDto.AT_CD_PMEndTime.ToString();
                                }
                                else
                                {
                                    pmEndTime = atWorkDate.AT_PMEndTime.ToString();
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        //�ϰ�ʱ��
                        if (atWorkDate.AT_StartTime != null)
                        {
                            startTime = atWorkDate.AT_StartTime.ToString();
                        }
                        //�°�ʱ��
                        if (atWorkDate.AT_EndTime != null)
                        {
                            endTime = atWorkDate.AT_EndTime.ToString();
                        }
                        //�����ϰ�ʱ��
                        if (atWorkDate.AT_AMStartTime != null)
                        {
                            amStartTime = atWorkDate.AT_AMStartTime.ToString();
                        }
                        //�����°�ʱ��
                        if (atWorkDate.AT_AMEndTime != null)
                        {
                            amEndTime = atWorkDate.AT_AMEndTime.ToString();
                        }
                        //�����ϰ�ʱ��
                        if (atWorkDate.AT_PMStartTime != null)
                        {
                            pmStartTime = atWorkDate.AT_PMStartTime.ToString();
                        }
                        //�����°�ʱ��
                        if (atWorkDate.AT_PMEndTime != null)
                        {
                            pmEndTime = atWorkDate.AT_PMEndTime.ToString();
                        }
                    }
                    upsettingDate(atcdInput.AT_CD_CommutingType, startTime, endTime, amStartTime, amEndTime, pmStartTime, pmEndTime);
                    atcdInput.AT_CD_CDType = workType;
                  
                }
            }
            //�༭�����Զ�����������
            else
            {
                //�༭����ʱ������ѡ������С�ڵ����������ڣ���ģ������Զ��壬����ʾ�Զ���״̬
                if (calendar1.SelectDate.Date < DateTime.Now.AddDays(+1).Date)
                {
                    WorkTimeDto atcd = AutofacConfig.attendanceService.GetASByATIDAndDate(ATNo, calendar1.SelectDate.Date);
                    if (atcd != null)
                    {
                        switch ((WorkOrRest)Enum.Parse(typeof(WorkOrRest), atcd.AT_ASType))
                        {
                            case WorkOrRest.�ϰ�:
                                switch ((WorkTimeType)Enum.Parse(typeof(WorkTimeType), atcd.AT_CommutingType))
                                {
                                    case WorkTimeType.һ��һ���°�:

                                        if (atcd.AT_StartTime != null)
                                        {
                                            dpStartWork.Value = Convert.ToDateTime(atcd.AT_StartTime);
                                        }

                                        if (atcd.AT_EndTime != null)
                                        {
                                            dpEndWork.Value = Convert.ToDateTime(atcd.AT_EndTime);
                                        }
                                        break;
                                    case WorkTimeType.һ������°�:
                                        if (atcd.AT_AMStartTime != null)
                                        {
                                            dpAMStartWork.Value = Convert.ToDateTime(atcd.AT_AMStartTime);
                                        }

                                        if (atcd.AT_AMEndTime != null)
                                        {
                                            dpAMEndWork.Value = Convert.ToDateTime(atcd.AT_AMEndTime);
                                        }

                                        if (atcd.AT_PMStartTime != null)
                                        {
                                            dpPMStartWork.Value = Convert.ToDateTime(atcd.AT_PMStartTime);
                                        }

                                        if (atcd.AT_PMEndTime != null)
                                        {
                                            dpPMEndWork.Value = Convert.ToDateTime(atcd.AT_PMEndTime);
                                        }

                                        break;
                                }
                                break;
                        }
                    }
                }
                if (calendar1.SelectDate.Date >= DateTime.Now.AddDays(+1).Date)
                {
                    string startTime = null; //�ϰ�ʱ��
                    string endTime = null;//�°�ʱ��
                    string amStartTime = null; //�����ϰ�ʱ��
                    string amEndTime = null;//�����°�ʱ��
                    string pmStartTime = null; //�����ϰ�ʱ��
                    string pmEndTime = null;//�����°�ʱ��

                    atcdInput = new AT_CDInputDto();
                    //�����Զ�������
                    atcdInput.AT_CD_Date = calendar1.SelectDate.Date;
                    //�������°�����
                    atcdInput.AT_CD_CommutingType = atWorkDate.AT_Type;
                    //�༭����ʱ��������֮���ѡ������������Զ����ҿ���ģʽ��ȣ����Զ����������ڸ�ֵ����ǰ�Զ������ڿؼ�
                    if (IsCATSettingDay(atcdInput.AT_CD_Date) == true)
                    {
                        foreach (AT_CDInputDto atcdInputDto in listatcdInput)
                        {
                            if (atcdInputDto.AT_CD_Date.Date.Equals(atcdInput.AT_CD_Date.Date) & atcdInputDto.AT_CD_CommutingType == atWorkDate.AT_Type & workType==WorkOrRest .�ϰ�)
                            {
                                //workType = atcdInputDto.AT_CD_CDType;
                                //�ϰ�ʱ��
                                if (atcdInputDto.AT_CD_StartTime != null)
                                {
                                    startTime = atcdInputDto.AT_CD_StartTime.ToString();
                                }
                                else
                                {
                                    startTime = atWorkDate.AT_StartTime.ToString();
                                }
                                //�°�ʱ��
                                if (atcdInputDto.AT_CD_EndTime != null)
                                {
                                    endTime = atcdInputDto.AT_CD_EndTime.ToString();
                                }
                                else
                                {
                                    endTime = atWorkDate.AT_EndTime.ToString();
                                }
                                //�����ϰ�ʱ��
                                if (atcdInputDto.AT_CD_AMStartTime != null)
                                {
                                    amStartTime = atcdInputDto.AT_CD_AMStartTime.ToString();
                                }
                                else
                                {
                                    amStartTime = atWorkDate.AT_AMStartTime.ToString();
                                }
                                //�����°�ʱ��
                                if (atcdInputDto.AT_CD_AMEndTime != null)
                                {
                                    amEndTime = atcdInputDto.AT_CD_AMEndTime.ToString();
                                }
                                else
                                {
                                    amEndTime = atWorkDate.AT_AMEndTime .ToString();
                                }
                                //�����ϰ�ʱ��
                                if (atcdInputDto.AT_CD_PMStartTime != null)
                                {
                                    pmStartTime = atcdInputDto.AT_CD_PMStartTime.ToString();
                                }
                                else
                                {
                                    pmStartTime = atWorkDate.AT_PMStartTime .ToString();
                                }
                                //�����°�ʱ��
                                if (atcdInputDto.AT_CD_PMEndTime != null)
                                {
                                    pmEndTime = atcdInputDto.AT_CD_PMEndTime.ToString();
                                }
                                else
                                {
                                    pmEndTime = atWorkDate.AT_PMEndTime .ToString();
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        //��ȡ��ǰѡ�����ڿ��ڵ��Զ���
                        WorkTimeDto atcd = AutofacConfig.attendanceService.GetASByATIDAndDate(ATNo, calendar1.SelectDate.Date);
                        //������ڴ����Զ����������Զ��������뿼������һ��ʱ����ȡ�����ϰ�ʱ��
                        if (atcd != null)
                        {
                            atcdInput.AT_CD_ATID = ATNo;
                            //���Ͳ��ԣ��õ���ֵ����false
                            if (atWorkDate.AT_Type.Equals((WorkTimeType)Enum.Parse(typeof(WorkTimeType), atcd.AT_CommutingType)))
                            {
                                workType = (WorkOrRest)Enum.Parse(typeof(WorkOrRest), atcd.AT_ASType);
                                if (workType == WorkOrRest.�ϰ�)
                                {
                                    //�ϰ�ʱ��
                                    if (atcd.AT_StartTime != null)
                                    {
                                        startTime = atcd.AT_StartTime.ToString();
                                    }
                                    else
                                    {
                                        startTime = atWorkDate.AT_StartTime.ToString();
                                    }
                                    //�°�ʱ��
                                    if (atcd.AT_EndTime != null)
                                    {
                                        endTime = atcd.AT_EndTime.ToString();
                                    }
                                    else
                                    {
                                        endTime = atWorkDate.AT_EndTime.ToString();
                                    }
                                    //�����ϰ�ʱ��
                                    if (atcd.AT_AMStartTime != null)
                                    {
                                        amStartTime = atcd.AT_AMStartTime.ToString();
                                    }
                                    else
                                    {
                                        amStartTime = atWorkDate.AT_AMStartTime.ToString();
                                    }
                                    //�����°�ʱ��
                                    if (atcd.AT_AMEndTime != null)
                                    {
                                        amEndTime = atcd.AT_AMEndTime.ToString();
                                    }
                                    else
                                    {
                                        amEndTime = atWorkDate.AT_AMEndTime.ToString();
                                    }
                                    //�����ϰ�ʱ��
                                    if (atcd.AT_PMStartTime != null)
                                    {
                                        pmStartTime = atcd.AT_PMStartTime.ToString();
                                    }
                                    else
                                    {
                                        pmStartTime = atWorkDate.AT_PMStartTime.ToString();
                                    }
                                    //�����°�ʱ��
                                    if (atcd.AT_PMEndTime != null)
                                    {
                                        pmEndTime = atcd.AT_PMEndTime.ToString();
                                    }
                                    else
                                    {
                                        pmEndTime = atWorkDate.AT_PMEndTime.ToString();
                                    }
                                }
                            }
                            else
                            {
                                switch (workType)
                                {
                                    case WorkOrRest.�ϰ�:
                                        //�ϰ�ʱ��
                                        if (atWorkDate.AT_StartTime != null)
                                        {
                                            startTime = atWorkDate.AT_StartTime.ToString();
                                        }
                                        //�°�ʱ��
                                        if (atWorkDate.AT_EndTime != null)
                                        {
                                            endTime = atWorkDate.AT_EndTime.ToString();
                                        }
                                        //�����ϰ�ʱ��
                                        if (atWorkDate.AT_AMStartTime != null)
                                        {
                                            amStartTime = atWorkDate.AT_AMStartTime.ToString();
                                        }
                                        //�����°�ʱ��
                                        if (atWorkDate.AT_AMEndTime != null)
                                        {
                                            amEndTime = atWorkDate.AT_AMEndTime.ToString();
                                        }
                                        //�����ϰ�ʱ��
                                        if (atWorkDate.AT_PMStartTime != null)
                                        {
                                            pmStartTime = atWorkDate.AT_PMStartTime.ToString();
                                        }
                                        //�����°�ʱ��
                                        if (atWorkDate.AT_PMEndTime != null)
                                        {
                                            pmEndTime = atWorkDate.AT_PMEndTime.ToString();
                                        }
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (workType)
                            {
                                case WorkOrRest.�ϰ�:
                                    //�ϰ�ʱ��
                                    if (atWorkDate.AT_StartTime != null)
                                    {
                                        startTime = atWorkDate.AT_StartTime.ToString();
                                    }
                                    //�°�ʱ��
                                    if (atWorkDate.AT_EndTime != null)
                                    {
                                        endTime = atWorkDate.AT_EndTime.ToString();
                                    }
                                    //�����ϰ�ʱ��
                                    if (atWorkDate.AT_AMStartTime != null)
                                    {
                                        amStartTime = atWorkDate.AT_AMStartTime.ToString();
                                    }
                                    //�����°�ʱ��
                                    if (atWorkDate.AT_AMEndTime != null)
                                    {
                                        amEndTime = atWorkDate.AT_AMEndTime.ToString();
                                    }
                                    //�����ϰ�ʱ��
                                    if (atWorkDate.AT_PMStartTime != null)
                                    {
                                        pmStartTime = atWorkDate.AT_PMStartTime.ToString();
                                    }
                                    //�����°�ʱ��
                                    if (atWorkDate.AT_PMEndTime != null)
                                    {
                                        pmEndTime = atWorkDate.AT_PMEndTime.ToString();
                                    }
                                    break;
                            }
                        }
                    }
                    upsettingDate(atcdInput.AT_CD_CommutingType, startTime, endTime, amStartTime, amEndTime, pmStartTime, pmEndTime);
                    atcdInput.AT_CD_CDType = workType;
                }
            }
            if (atcdInput != null)
            {
                if (listatcdInput != null & listatcdInput.Count > 0)
                {
                    foreach (AT_CDInputDto atcdInputDto in listatcdInput)
                    {
                        if (atcdInputDto.AT_CD_Date.Date.Equals(atcdInput.AT_CD_Date.Date))
                        {
                            listatcdInput.Remove(atcdInputDto);//����ռ����е���ѡ�����ڵ�����
                            break;
                        }
                    }
                }         
                    listatcdInput.Add(atcdInput);//���ѡ����������             
            }
            upSettingDateTypeControl();
        }

        /// <summary>
        /// ���������Զ������Ϳؼ�
        /// </summary>
        private void upSettingDateTypeControl()
        {
            if (calendar1.SelectDate.Date < DateTime.Now.AddDays(+1).Date)
            {
                //��������ʱ����ǰѡ������С��������ʱ���Զ������ڿ��ڿؼ�����ʾ
                if (string.IsNullOrEmpty(ATNo) == true)
                {
                    lblRest.Visible = false;
                    dpStartWork.Visible = false;
                    dpEndWork.Visible = false;
                    lblPMEndWork.Visible = false;
                    lblPMStartWork.Visible = false;
                    dpAMStartWork.Visible = false;
                    dpAMEndWork.Visible = false;
                    dpPMStartWork.Visible = false;
                    dpPMEndWork.Visible = false;
                    lblStartWork.Visible = false; ;
                    lblEndWork.Visible = false;
                    btnCDType.Visible = false;
                }
                else
                {
                    //�༭����ʱ������ѡ������С�ڵ����������ڣ���ģ������Զ��壬����ʾ�Զ���״̬
                    WorkTimeDto atcd = AutofacConfig.attendanceService.GetASByATIDAndDate(ATNo, calendar1.SelectDate .Date);
                    if (atcd != null)
                    {
                        switch ((WorkOrRest)Enum.Parse(typeof(WorkOrRest), atcd.AT_ASType))
                        {
                            case WorkOrRest.�ϰ�:
                                switch ((WorkTimeType)Enum.Parse(typeof(WorkTimeType), atcd.AT_CommutingType))
                                {
                                    case WorkTimeType.һ��һ���°�:

                                        if (atcd.AT_StartTime != null)
                                        {
                                            dpStartWork.Value = Convert.ToDateTime(atcd.AT_StartTime);
                                        }

                                        if (atcd.AT_EndTime != null)
                                        {
                                            dpEndWork.Value  = Convert.ToDateTime(atcd.AT_EndTime);
                                        }
                                        break;
                                    case WorkTimeType.һ������°�:
                                        if (atcd.AT_AMStartTime != null)
                                        {
                                            dpAMStartWork.Value  = Convert.ToDateTime(atcd.AT_AMStartTime);
                                        }

                                        if (atcd.AT_AMEndTime != null)
                                        {
                                            dpAMEndWork.Value  = Convert.ToDateTime(atcd.AT_AMEndTime);
                                        }

                                        if (atcd.AT_PMStartTime != null)
                                        {
                                            dpPMStartWork.Value  = Convert.ToDateTime(atcd.AT_PMStartTime);
                                        }

                                        if (atcd.AT_PMEndTime != null)
                                        {
                                            dpPMEndWork.Value  = Convert.ToDateTime(atcd.AT_PMEndTime);
                                        }

                                        break;
                                }
                                break;
                        }
                        switch ((WorkOrRest)Enum.Parse(typeof(WorkOrRest), atcd.AT_ASType))
                        {
                            case WorkOrRest.��Ϣ:
                                lblRest.Visible = true;
                                dpStartWork.Visible = false;
                                dpEndWork.Visible = false;
                                lblPMEndWork.Visible = false;
                                lblPMStartWork.Visible = false;
                                dpAMStartWork.Visible = false;
                                dpAMEndWork.Visible = false;
                                dpPMStartWork.Visible = false;
                                dpPMEndWork.Visible = false;
                                lblStartWork.Visible = false; ;
                                lblEndWork.Visible = false;
                                btnCDType.Visible = false;
                                break;
                            case WorkOrRest.�ϰ�:
                                lblStartWork.Top = lblRest.Top;
                                lblRest.Visible = false;
                                switch ((WorkTimeType)Enum.Parse(typeof(WorkTimeType), atcd.AT_CommutingType))
                                {
                                    case WorkTimeType.һ��һ���°�:
                                        lblStartWork.Visible = true;
                                        lblEndWork.Visible = true;
                                        dpStartWork.Visible = true;
                                        dpEndWork.Visible = true;
                                        dpStartWork.Enabled = false;
                                        dpEndWork.Enabled = false;
                                        lblPMEndWork.Visible = false;
                                        lblPMStartWork.Visible = false;
                                        dpAMStartWork.Visible = false;
                                        dpAMEndWork.Visible = false;
                                        dpPMStartWork.Visible = false;
                                        dpPMEndWork.Visible = false;
                                        lblStartWork.Text = "�ϰ�ʱ��";
                                        lblEndWork.Text = "�°�ʱ��";
                                        dpStartWork.Top = lblStartWork.Top;
                                        dpEndWork.Top = lblEndWork.Top;
                                        break;
                                    case WorkTimeType.һ������°�:
                                        lblStartWork.Visible = true;
                                        lblEndWork.Visible = true;
                                        dpStartWork.Visible = false;
                                        dpEndWork.Visible = false;
                                        lblPMEndWork.Visible = true;
                                        lblPMStartWork.Visible = true;
                                        dpAMStartWork.Visible = true;
                                        dpAMEndWork.Visible = true;
                                        dpPMStartWork.Visible = true;
                                        dpPMEndWork.Visible = true;
                                        dpAMStartWork.Enabled = false;
                                        dpAMEndWork.Enabled = false;
                                        dpPMStartWork.Enabled = false;
                                        dpPMEndWork.Enabled = false;
                                        lblStartWork.Text = "�����ϰ�";
                                        lblEndWork.Text = "�����°�";
                                        dpAMStartWork.Top = lblStartWork.Top;
                                        dpAMEndWork.Top = lblEndWork.Top;
                                        lblPMStartWork.Top = lblEndWork.Top + lblEndWork.Height;
                                        dpPMStartWork.Top = lblPMStartWork.Top;
                                        lblPMEndWork.Top = lblPMStartWork.Top + lblPMStartWork.Height;
                                        dpPMEndWork.Top = lblPMEndWork.Top;
                                        break;
                                }
                                btnCDType.Visible = false;
                                break;
                        }
                    }
                    else
                    {
                        lblRest.Visible = false;
                        dpStartWork.Visible = false;
                        dpEndWork.Visible = false;
                        lblPMEndWork.Visible = false;
                        lblPMStartWork.Visible = false;
                        dpAMStartWork.Visible = false;
                        dpAMEndWork.Visible = false;
                        dpPMStartWork.Visible = false;
                        dpPMEndWork.Visible = false;
                        lblStartWork.Visible = false; ;
                        lblEndWork.Visible = false;
                        btnCDType.Visible = false;
                    }
                }
            }
            //�����༭����ʱ������ѡ�����ڴ����������ڣ�����ѡ�������Զ���������ʾ���ڿؼ�             
            if (calendar1.SelectDate.Date >= DateTime.Now.AddDays(+1).Date)
            {
                switch (workType)
                {
                    case WorkOrRest.�ϰ�:
                        lblRest.Visible = false;
                       // lblStartWork.Top = calendar1.Top + calendar1.Height + 10;
                        lblStartWork.Top = lblRest.Top ;
                        lblEndWork.Top = lblStartWork.Top + lblStartWork.Height;
                        switch (atWorkDate.AT_Type)
                        {
                            case WorkTimeType.һ��һ���°�:
                                lblStartWork.Visible = true;
                                lblEndWork.Visible = true;
                                dpStartWork.Visible = true;
                                dpEndWork.Visible = true;
                                dpStartWork.Enabled = true;
                                dpEndWork.Enabled = true;
                                lblPMEndWork.Visible = false;
                                lblPMStartWork.Visible = false;
                                dpAMStartWork.Visible = false;
                                dpAMEndWork.Visible = false;
                                dpPMStartWork.Visible = false;
                                dpPMEndWork.Visible = false;
                                lblStartWork.Text = "�ϰ�ʱ��";
                                lblEndWork.Text = "�°�ʱ��";
                                dpStartWork.Top = lblStartWork.Top;
                                dpEndWork.Top = lblEndWork.Top;
                                btnCDType.Top = lblEndWork.Top + lblEndWork.Height;
                                break;
                            case WorkTimeType.һ������°�:
                                dpStartWork.Visible = false;
                                dpEndWork.Visible = false;
                                lblStartWork.Visible = true;
                                lblEndWork.Visible = true;
                                lblPMEndWork.Visible = true;
                                lblPMStartWork.Visible = true;
                                dpAMStartWork.Visible = true;
                                dpAMEndWork.Visible = true;
                                dpPMStartWork.Visible = true;
                                dpPMEndWork.Visible = true;
                                dpAMStartWork.Enabled = true;
                                dpAMEndWork.Enabled = true;
                                dpPMStartWork.Enabled = true;
                                dpPMEndWork.Enabled = true;
                                lblStartWork.Text = "�����ϰ�";
                                lblEndWork.Text = "�����°�";
                                dpAMStartWork.Top = lblStartWork.Top;
                                dpAMEndWork.Top = lblEndWork.Top;
                                lblPMStartWork.Top = lblEndWork.Top + lblEndWork.Height;
                                dpPMStartWork.Top = lblPMStartWork.Top;
                                lblPMEndWork.Top = lblPMStartWork.Top + lblPMStartWork.Height;
                                dpPMEndWork.Top = lblPMEndWork.Top;
                                btnCDType.Top = lblPMEndWork.Top + lblPMEndWork.Height;
                                break;
                        }
                        btnCDType.Visible = true;
                        btnCDType.Text = "����Ϊ��Ϣ";
                        break;
                    case WorkOrRest.��Ϣ:
                        dpStartWork.Visible = false;
                        dpEndWork.Visible = false;
                        lblPMEndWork.Visible = false;
                        lblPMStartWork.Visible = false;
                        dpAMStartWork.Visible = false;
                        dpAMEndWork.Visible = false;
                        dpPMStartWork.Visible = false;
                        dpPMEndWork.Visible = false;
                        lblStartWork.Visible = false; ;
                        lblEndWork.Visible = false;
                        lblRest.Visible = true;
                        btnCDType.Visible = true;
                        btnCDType.Top = lblRest.Top + lblRest.Height;
                        btnCDType.Text = "����Ϊ����";
                        break;
                }
            }
        }
       
        /// <summary>
        /// �Զ������ڸ���
        /// </summary>
        /// <param name="atType">��������</param>
        /// <param name="StartTime">�ϰ�ʱ��</param>
        /// <param name="EndTime">�°�ʱ��</param>
        /// <param name="AMStartTime">�����ϰ�ʱ��</param>
        /// <param name="AMEndTime">�����°�ʱ��</param>
        /// <param name="PMStartTime">�����ϰ�ʱ��</param>
        /// <param name="PMEndTime">�����°�ʱ��</param>
        private void upsettingDate(WorkTimeType atType, string StartTime, string EndTime, string AMStartTime, string AMEndTime, string PMStartTime, string PMEndTime)
        {
            try
            {
                switch (atType)
                {
                    case WorkTimeType.һ��һ���°�:

                        if (string.IsNullOrEmpty(StartTime)==false )
                        {
                            DateTime atsettingDate = atcdInput.AT_CD_Date.Date.AddHours(Convert.ToDateTime(StartTime).Hour).AddMinutes(Convert.ToDateTime(StartTime).Minute);
                            dpStartWork.Value = atsettingDate;
                            atcdInput.AT_CD_StartTime = dpStartWork.Value;
                        }

                        if (string.IsNullOrEmpty(EndTime) == false)
                        {
                            DateTime atsettingDate = atcdInput.AT_CD_Date.Date.AddHours(Convert.ToDateTime(EndTime).Hour).AddMinutes(Convert.ToDateTime(EndTime).Minute);
                            dpEndWork.Value = atsettingDate;
                            atcdInput.AT_CD_EndTime = dpEndWork.Value;
                        }
                        break;
                    case WorkTimeType.һ������°�:
                        if (string.IsNullOrEmpty(AMStartTime ) == false)
                        {
                            DateTime atsettingDate = atcdInput.AT_CD_Date.Date.AddHours(Convert.ToDateTime(AMStartTime).Hour).AddMinutes(Convert.ToDateTime(AMStartTime).Minute);
                            dpAMStartWork.Value = atsettingDate;
                            atcdInput.AT_CD_AMStartTime = dpAMStartWork.Value;
                        }

                        if (string.IsNullOrEmpty(AMEndTime ) == false)
                        {
                            DateTime atsettingDate = atcdInput.AT_CD_Date.Date.AddHours(Convert.ToDateTime(AMEndTime).Hour).AddMinutes(Convert.ToDateTime(AMEndTime).Minute);
                            dpAMEndWork.Value = atsettingDate;
                            atcdInput.AT_CD_AMEndTime = dpAMEndWork.Value;
                        }

                        if (string.IsNullOrEmpty(PMStartTime) == false)
                        {
                            DateTime atsettingDate = atcdInput.AT_CD_Date.Date.AddHours(Convert.ToDateTime(PMStartTime).Hour).AddMinutes(Convert.ToDateTime(PMStartTime).Minute);
                            dpPMStartWork.Value = atsettingDate;
                            atcdInput.AT_CD_PMStartTime = dpPMStartWork.Value;
                        }

                        if (string.IsNullOrEmpty(PMEndTime) == false)
                        {
                            DateTime atsettingDate = atcdInput.AT_CD_Date.Date.AddHours(Convert.ToDateTime(PMEndTime).Hour).AddMinutes(Convert.ToDateTime(PMEndTime).Minute);
                            dpPMEndWork.Value = atsettingDate;
                            atcdInput.AT_CD_PMEndTime = dpPMEndWork.Value;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }

        /// <summary>
        /// �жϵ�ǰѡ�������Ƿ����Զ��忼��
        /// </summary>
        /// <returns></returns>
        private bool IsCATSettingDay(DateTime atcdDate)
        {
            bool isatSettingDay = false;
            if (listatcdInput != null & listatcdInput.Count > 0)
            {
                foreach (AT_CDInputDto atcdInputDto in listatcdInput)
                {
                    if (atcdInputDto.AT_CD_Date.Date.Equals(atcdDate.Date))
                    {
                        isatSettingDay = true;
                        break;
                    }

                }
            }
            return isatSettingDay;
        }
 
        /// <summary>
        /// �����ϰ�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpStartWork_DatePicked(object sender, EventArgs e)
        {
            ShowResult = ShowResult.Yes;
            foreach (AT_CDInputDto atct in listatcdInput)
            {
                if (atct.AT_CD_Date.Date.Equals(atcdInput.AT_CD_Date.Date))
                {
                    atct.AT_CD_StartTime = dpStartWork .Value ;
                    break;
                }
            }
        }
        /// <summary>
        /// �����°�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpEndWork_DatePicked(object sender, EventArgs e)
        {
            ShowResult = ShowResult.Yes;
            foreach (AT_CDInputDto atct in listatcdInput)
            {
                if (atct.AT_CD_Date.Date.Equals(atcdInput.AT_CD_Date.Date))
                {                  
                    atct.AT_CD_EndTime = dpEndWork .Value ;
                    break;
                }
            }
        }

        /// <summary>
        /// ���������ϰ�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpAMStartWork_DatePicked(object sender, EventArgs e)
        {
            ShowResult = ShowResult.Yes;
            foreach (AT_CDInputDto atct in listatcdInput)
            {
                if (atct.AT_CD_Date.Date.Equals(atcdInput.AT_CD_Date.Date))
                {
                    atct.AT_CD_AMStartTime = dpAMStartWork.Value ;
                    break;
                }
            }
        }
      
        /// <summary>
        /// ���������°�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpAMEndWork_DatePicked(object sender, EventArgs e)
        {
            ShowResult = ShowResult.Yes;
            foreach (AT_CDInputDto atct in listatcdInput)
            {
                if (atct.AT_CD_Date.Date.Equals(atcdInput.AT_CD_Date.Date))
                {
                    atct.AT_CD_AMEndTime = dpAMEndWork.Value;
                    break;
                }
            }
        }
       
        /// <summary>
        /// ���������ϰ�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpPMStartWork_DatePicked(object sender, EventArgs e)
        {
            ShowResult = ShowResult.Yes;
            foreach (AT_CDInputDto atct in listatcdInput)
            {
                if (atct.AT_CD_Date.Date.Equals(atcdInput.AT_CD_Date.Date))
                {
                    atct.AT_CD_AMStartTime = dpPMStartWork.Value ;
                    break;
                }
            }
        }

        /// <summary>
        /// ���������°�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpPMEndWork_DatePicked(object sender, EventArgs e)
        {
           
            foreach (AT_CDInputDto atct in listatcdInput)
            {
                if (atct.AT_CD_Date.Date.Equals(atcdInput.AT_CD_Date.Date))
                {
                   atct.AT_CD_PMEndTime = dpPMEndWork.Value ;

                    break;
                }
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceCalendarSetting_Load(object sender, EventArgs e)
        {
            try
            {
                upSettingDateType ();
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }

        /// <summary>
        /// �������ڵ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendar1_DateChanged(object sender, EventArgs e)
        {
            try
            {
                int selectday = (int)Enum.Parse(typeof(Week), calendar1.SelectDate.DayOfWeek.ToString());//��ȡѡ��������ĳ�����ڵĵڼ���
                //�����ǰѡ�������ڿ��ڹ��������У����Զ�����������Ϊ�ϰ࣬����Ϊ��Ϣ
                if (weekdays.Split(',').Contains(selectday.ToString()) == true)
                {
                    workType = WorkOrRest.�ϰ�;
                }
                else
                {
                    workType = WorkOrRest.��Ϣ;
                }
                //�����ǰѡ���������ڿ����Զ��弯�������Զ������͵��ڿ������ͣ���ֵ�Զ�����������
                if (IsCATSettingDay(calendar1.SelectDate.Date) == true)
                {
                    foreach (AT_CDInputDto atcdInputDto in listatcdInput)
                    {
                        if (atcdInputDto.AT_CD_Date.Date.Equals(calendar1.SelectDate.Date) & atcdInputDto.AT_CD_CommutingType == atWorkDate.AT_Type)
                        {
                            workType = atcdInputDto.AT_CD_CDType;
                            break;
                        }
                    }
                }
                upSettingDateType();
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
           
        }
    }
}
