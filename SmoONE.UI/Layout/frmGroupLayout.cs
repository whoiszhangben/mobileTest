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
    partial class frmGroupLayout : Smobiler.Core.Controls.MobileUserControl
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
      
        #endregion
        //public frmGroupLayout(IM im):this()
        //{
        //    im1 = im;
        //}
        /// <summary>
        /// Ⱥ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpUser_Press(object sender, EventArgs e)
        {
            try
            {
                if (((SmoONE.UI.Im.frmConcent)this.Form).im1 != null)
                {
                    //��Ⱥ���б�ģ���е���Ⱥ������
                    ((SmoONE.UI.Im.frmConcent)this.Form).im1.StartGroupChat(lblGroup.BindDataValue.ToString(), lblGroup.BindDisplayValue.ToString());
                }
                }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message,ToastLength.SHORT );
            }
        }
    }
}