using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.UI.RB;

namespace SmoONE.UI.Layout
{
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmRBTypeLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ��ȡ��������ID�����ƴ��ݵ���һ��ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plContent_Press(object sender, EventArgs e)
        {
            try
            {
                ((frmRTypeChoose)(this.Form)).TYPEID = lblTypeName.BindDataValue.ToString() + "/" + lblTypeName.BindDisplayValue.ToString();
                this.Form.ShowResult = ShowResult.Yes;
                this.Form.Close();
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }
    }
}