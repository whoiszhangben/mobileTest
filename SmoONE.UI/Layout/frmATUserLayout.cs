using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;

namespace SmoONE.UI.Layout
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ� �����û�ѡ�н���
    // ******************************************************************
    partial class frmATUserLayout : Smobiler.Core.Controls.MobileUserControl
    {
    
        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            //switch (Convert.ToBoolean(Check.Checked))
            //{
            //    case true:
            //        Check.Checked = false;
            //        break;
            //    case false:
            //        Check.Checked = true;
            //        break;
            //}
            ((SmoONE.UI.Attendance.frmATUser)this.Form).upCheckState();
        }
    }
}