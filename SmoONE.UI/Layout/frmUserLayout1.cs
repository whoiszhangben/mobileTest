using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using Smobiler.Plugins.RongIM;

namespace SmoONE.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmUserLayout1 : Smobiler.Core.Controls.MobileUserControl
    {
       
        /// <summary>
        /// �û�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpUser_Press(object sender, EventArgs e)
        {
            try
            {
            
                //����ϵ��ģ���е���im�û�����
                if (((SmoONE.UI.Im .frmConcent)this.Form).im1  != null)
                {
                    ((SmoONE.UI.Im.frmConcent)this.Form).im1.StartPrivateChat(lblUser .BindDataValue .ToString (), lblUser.BindDisplayValue.ToString());
                }
            
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }
    }
}