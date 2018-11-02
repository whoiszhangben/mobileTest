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
    // ��Ҫ���ݣ� ����ģ���б����
    // ******************************************************************
    partial class frmAttendanceManager : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceManager_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
       
        /// <summary>
        /// ��ȡ��ʼ������
        /// </summary>
        public  void Bind()
        {
            List<ATDto> listATDto = AutofacConfig.attendanceService.GetAll();
            gridATData.Rows.Clear();//�������ģ���б�����
            //�������ģ��������������0
            if (listATDto.Count > 0)
            {
                List<AttendanceManager> listAT = new List<AttendanceManager>();
                foreach (ATDto at in listATDto)
                {
                    AttendanceManager atManager = new AttendanceManager();
                    atManager.AT_ID = at.AT_ID;
                    atManager.AT_Name = at.AT_Name;
                    atManager.AT_CommutingType = at.AT_CommutingType;
                    atManager.AT_WeeklyWorkingDay = atManager.getWeekDesc(at.AT_WeeklyWorkingDay);
                    switch ((WorkTimeType)Enum.Parse(typeof(WorkTimeType), at.AT_CommutingType))
                    {
                        case WorkTimeType.һ��һ���°�:
                            atManager.WorkDate = "�ϰࣺ" + Convert.ToDateTime(at.AT_StartTime).ToString("HH:mm") + "    �°ࣺ" + Convert.ToDateTime(at.AT_EndTime).ToString("HH:mm");
                            break;
                        case WorkTimeType.һ������°�:
                            atManager.WorkDate = "�����ϰࣺ" + Convert.ToDateTime(at.AT_AMStartTime).ToString("HH:mm") + "    �����°ࣺ" + Convert.ToDateTime(at.AT_AMEndTime).ToString("HH:mm");
                            atManager.WorkDate1 = "�����ϰࣺ" + Convert.ToDateTime(at.AT_PMStartTime).ToString("HH:mm") + "    �����°ࣺ" + Convert.ToDateTime(at.AT_PMEndTime).ToString("HH:mm");
                            break;
                    }
                    atManager.AT_EffectiveDesc = at.AT_EffectiveDate.ToString ("yyyy��MM��dd��")+"������Ч";
                    listAT.Add(atManager);
                }
                gridATData.DataSource = listAT;
                gridATData.DataBind();
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceManager_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ��ת���������ڽ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmAttendanceCreate frm = new frmAttendanceCreate();
            Show(frm, (MobileForm form, object args) =>
            {
                if (frm.ShowResult == ShowResult.Yes)
                {
                    Bind();
                }
            });
        }
        /// <summary>
        /// ҳ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceManager_Load(object sender, EventArgs e)
        {
            Bind();
        }
    }
}