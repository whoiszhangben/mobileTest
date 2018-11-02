using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;

namespace SmoONE.UI.Layout
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� ����ģ���б�ģ��
    // ******************************************************************
    partial class frmAttendanceManagerLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ��ת�����ڱ༭����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Press(object sender, EventArgs e)
        {

            try
            {
                string ID = lblId.BindDataValue.ToString();
                SmoONE.UI.Attendance.frmAttendanceCreate frmAttendanceCreate = new SmoONE.UI.Attendance.frmAttendanceCreate();
                frmAttendanceCreate.ATNo = ID;
                this.Form.Show(frmAttendanceCreate, (MobileForm form, object args) =>
                {
                    if (frmAttendanceCreate.ShowResult == ShowResult.Yes)
                    {
                        ((SmoONE.UI.Attendance.frmAttendanceManager)this.Form).Bind();
                    }
                });

            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message, ToastLength.SHORT);
            }
        }
    }
}