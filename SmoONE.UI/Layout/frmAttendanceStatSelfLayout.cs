using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;

namespace SmoONE.UI.Layout
{
    partial class frmAttendanceStatSelfLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ��ת��ǩ����ϸ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Press(object sender, EventArgs e)
        {
            try
            {
                if ( Convert.ToInt32(lblNumber.BindDataValue) > 0)
                {
                    SmoONE.UI.Attendance.frmAttendanceStatSelfDetail newFrm = new SmoONE.UI.Attendance.frmAttendanceStatSelfDetail();
                    newFrm.Type = lblID.BindDisplayValue.ToString ();           //ǩ������
                    newFrm.Daytime = ((SmoONE.UI.Attendance.frmAttendanceStatSelf)this.Form).Daytime;                   //ʱ��
                    newFrm.UserID = ((SmoONE.UI.Attendance.frmAttendanceStatSelf)this.Form).UserID;                     //�û�ID
                 //   newFrm.TitleText =((SmoONE.UI.Attendance.frmAttendanceStatSelf)this.Form). lblName.Text + lblID.BindDisplayValue.ToString () + "��¼";
                    this.Form.Show(newFrm);
                }
            }
            catch (Exception ex)
            {
               this.Form. Toast(ex.Message);
            }
        }

       
    }
}