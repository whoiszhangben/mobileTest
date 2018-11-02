using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.ComponentModel;

namespace SmoONE.UI.Layout
{
    [System.ComponentModel.ToolboxItem(true)]
     public partial class Title : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ��������
        /// </summary>
        [Browsable(true),Category("Appearance"),DefaultValue(""),Description("����")]
        public string TitleText
        {
            get
            {
                return this.label1.Text;
            }
            set
            {
                this.label1.Text = value;

            }
        }

     
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Panel1_Press(object sender, EventArgs e)
        {
            this.Form.Close();
        }

      
    }
}