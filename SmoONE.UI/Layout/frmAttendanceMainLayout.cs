using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using SmoONE.UI.Attendance;
using SmoONE.CommLib;

namespace SmoONE.UI.Layout
{
    partial class frmAttendanceMainLayout : Smobiler.Core.Controls.MobileUserControl
    {
        AutofacConfig AutofacConfig = new AutofacConfig();//����������  
        private void panel1_Press(object sender, EventArgs e)
        {
            try
            {
                string userID = ((SmoONE.UI.Attendance.frmAttendanceMain)this.Form).UserID;
                if ((ATMainState)Enum.Parse(typeof(ATMainState), ((SmoONE.UI.Attendance.frmAttendanceMain)this.Form).enter.ToString()) == ATMainState.ͳ�Ʋ鿴)                   //������ǲ鿴ǩ��ҳ��
                {
                    List<ALDto> listStats = AutofacConfig.attendanceService.GetALByUserAndDate(userID, Convert.ToDateTime(((SmoONE.UI.Attendance.frmAttendanceMain)this.Form).DayTime));
                    foreach (ALDto Row in listStats)
                    {
                        if (Row.AL_OnTime.ToString("HH:mm") == lblTime.BindDisplayValue.ToString() && string.IsNullOrEmpty(Row.AL_Reason) == false)
                        {
                            //DetailLayout.LayoutData.Items["lblLocation"].Text = Row.AL_Position;        //��ʾ���ڵص�
                            //DetailLayout.LayoutData.Items["lblReason"].Text = Row.AL_Reason;            //��ʾ�ٵ�����ԭ��
                            //DetailLayout.Show();
                            frmAttendanceMainDetailLayout atMainDControl = new frmAttendanceMainDetailLayout();
                            atMainDControl.lblLocation.Text = Row.AL_Position;
                            atMainDControl.lblReason.Text = Row.AL_Reason;
                            this.Form.ShowDialog(atMainDControl);
                        }
                    }
                }
                else if (string.IsNullOrEmpty(btnCheck.BindDisplayValue.ToString()) == true)                  //��ǰ������ǩ��
                {
                    List<ALDto> listLogs = AutofacConfig.attendanceService.GetALByUserAndDate(userID, DateTime.Now);
                    foreach (ALDto Row in listLogs)
                    {
                        if (Row.AL_OnTime.ToString("HH:mm") == lblTime.BindDisplayValue.ToString() && string.IsNullOrEmpty(Row.AL_Reason) == false)
                        {
                            //DetailLayout.LayoutData.Items["lblLocation"].Visible = false;         //���ؿ��ڵص���Ϣ
                            //DetailLayout.LayoutData.Items["imgLocation"].Visible = false;         //����λ��ͼ��
                            //DetailLayout.LayoutData.Items["lblReason"].Top = 0;
                            //DetailLayout.LayoutData.Items["lblReason"].Text = Row.AL_Reason;            //��ʾ�ٵ�����ԭ��
                            //DetailLayout.Show();
                            frmAttendanceMainDetailLayout atMainDControl = new frmAttendanceMainDetailLayout();
                            atMainDControl.lblLocation.Visible = false;
                            atMainDControl.imgLocation.Visible = false;
                            atMainDControl.lblReason.Top = 0;
                            atMainDControl.lblReason.Text = Row.AL_Reason;
                            this.Form.ShowDialog(atMainDControl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }
        /// <summary>
        /// ����ǩ����ǩ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Press(object sender, EventArgs e)
        {
            try
            {
                if (((frmAttendanceMain)(Form)).hasgps.HasValue == false) throw new Exception("��λ��δ��ɣ����Ժ�����");
                if (((frmAttendanceMain)(Form)).hasgps.Value == false) throw new Exception("δ��ȡGPS��Ϣ������Ȩ��");
                ((frmAttendanceMain)(Form)).newLog.AL_Reason = "";
                switch ((StatisticsTime)Enum.Parse(typeof(StatisticsTime), lblType.BindDataValue.ToString()))
                {
                    case StatisticsTime.�����ϰ�ʱ��:
                        ((frmAttendanceMain)(Form)).newLog.AL_LogTimeType = StatisticsTime.�����ϰ�ʱ��;
                        break;
                    case StatisticsTime.�����°�ʱ��:
                        ((frmAttendanceMain)(Form)).newLog.AL_LogTimeType = StatisticsTime.�����°�ʱ��;
                        break;
                    case StatisticsTime.�����ϰ�ʱ��:
                        ((frmAttendanceMain)(Form)).newLog.AL_LogTimeType = StatisticsTime.�����ϰ�ʱ��;
                        break;
                    case StatisticsTime.�����°�ʱ��:
                        ((frmAttendanceMain)(Form)).newLog.AL_LogTimeType = StatisticsTime.�����°�ʱ��;
                        break;
                    case StatisticsTime.�ϰ�ʱ��:
                        ((frmAttendanceMain)(Form)).newLog.AL_LogTimeType = StatisticsTime.�ϰ�ʱ��;
                        break;
                    case StatisticsTime.�°�ʱ��:
                        ((frmAttendanceMain)(Form)).newLog.AL_LogTimeType = StatisticsTime.�°�ʱ��;
                        break;
                }
                ((frmAttendanceMain)(Form)).newLog.AL_Date = DateTime.Now;          //ǩ��ʱ��
                ((frmAttendanceMain)(Form)).newLog.AL_UserID = Client.Session["U_ID"].ToString();              //�û�ID
                if ((WorkTimeType)Enum.Parse(typeof(WorkTimeType), ((frmAttendanceMain)(Form)).CommutingType) == WorkTimeType.һ��һ���°�)
                {
                    ((frmAttendanceMain)(Form)).newLog.AL_CommutingType = WorkTimeType.һ��һ���°�;
                }
                else
                {
                    ((frmAttendanceMain)(Form)).newLog.AL_CommutingType = WorkTimeType.һ������°�;
                }
                ((frmAttendanceMain)(Form)).newLog.AL_Position = ((frmAttendanceMain)(Form)).Location;                //ǩ��λ��
                ((frmAttendanceMain)(Form)).newLog.AL_OnTime = Convert.ToDateTime(lblTime.BindDisplayValue.ToString());       //ǩ��׼��
                if (((frmAttendanceMain)(Form)).LocState == 1)              //�ѳɹ���λ
                {
                    if (btnCheck.Text == "ǩ��")
                    {
                        if (DateTime.Now > Convert.ToDateTime(lblTime.BindDisplayValue.ToString()))
                        {
                            ((frmAttendanceMain)(Form)).newLog.AL_Status = AttendanceType.�ٵ�;         //ǩ��״̬  
                            frmAttendanceMainLayoutDialog frmReason = new frmAttendanceMainLayoutDialog();
                            frmReason.newLog = ((frmAttendanceMain)(Form)).newLog;
                            frmReason.lblTitle.Text = "�ٵ�����";
                            this.Form.ShowDialog(frmReason);
                            //this.Form.Show(frmReason, (MobileForm from, object args) =>
                            //{
                            //    if (frmReason.ShowResult == true)
                            //    {
                            //        ((frmAttendanceMain)(Form)).Bind();          //���¼�������
                            //    }
                            //});
                        }
                        else
                        {
                            ((frmAttendanceMain)(Form)).newLog.AL_Status = AttendanceType.׼��;             //ǩ��״̬
                            ReturnInfo r = AutofacConfig.attendanceService.AddAttendanceLog(((frmAttendanceMain)(Form)).newLog);
                            if (r.IsSuccess == true)         //��Ӽ�¼�ɹ�
                            {
                                this.Form.Toast("ǩ���ɹ���");
                                ((frmAttendanceMain)(Form)).Bind();                     //ˢ��ҳ��
                            }
                            else
                            {
                                throw new Exception(r.ErrorInfo);        //��ʾ��Ӽ�¼ʧ��ԭ��
                            }
                        }
                    }
                    else if (btnCheck.Text == "ǩ��")
                    {
                        if (DateTime.Now < Convert.ToDateTime(lblTime.BindDisplayValue.ToString()))
                        {
                            MessageBox.Show("����������ʱ�䣬ȷ��ǩ����", MessageBoxButtons.OKCancel, (object o, MessageBoxHandlerArgs args) =>
                            {
                                if (args.Result == ShowResult.OK)
                                {
                                    ((frmAttendanceMain)(Form)).newLog.AL_Status = AttendanceType.����;           //ǩ��״̬
                                    frmAttendanceMainLayoutDialog frmReason = new frmAttendanceMainLayoutDialog();
                                    frmReason.newLog = ((frmAttendanceMain)(Form)).newLog;
                                    frmReason.lblTitle.Text = "��������";
                                    this.Form.ShowDialog(frmReason);
                                    //this.Form.Show(frmReason, (MobileForm from, object arg) =>
                                    //{
                                    //    if (frmReason.ShowResult == true)
                                    //    {
                                    //        ((frmAttendanceMain)(Form)).Bind();          //���¼�������
                                    //    }
                                    //});
                                }
                            });
                        }
                        else
                        {
                            ((frmAttendanceMain)(Form)).newLog.AL_Status = AttendanceType.׼��;
                            ReturnInfo r = AutofacConfig.attendanceService.AddAttendanceLog(((frmAttendanceMain)(Form)).newLog);
                            if (r.IsSuccess == true)             //��Ӽ�¼�ɹ�
                            {
                                this.Form.Toast("ǩ�˳ɹ���");
                                ((frmAttendanceMain)(Form)).Bind();                  //ˢ��ҳ��
                            }
                            else
                            {
                                throw new Exception(r.ErrorInfo);
                            }
                        }
                    }
                }
                else if (((frmAttendanceMain)(Form)).LocState == 2)
                {
                    if (btnCheck.Text == "ǩ��")
                    {
                        throw new Exception("ǩ��ʧ�ܣ�����ǰ���ڹ�˾����");
                    }
                    else if (btnCheck.Text == "ǩ��")
                    {
                        throw new Exception("ǩ��ʧ�ܣ�����ǰ���ڹ�˾����");
                    }
                }
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }
    }
}