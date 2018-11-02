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
    partial class frmRBModelLayout : Smobiler.Core.Controls.MobileUserControl
    {
        

        /// <summary>
        /// ѡ��ģ�壬��ģ���Ŵ��ݵ��ϸ�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plContent_Press(object sender, EventArgs e)
        {
            try
            {
                
                if (this.Form.ToString() == "SmoONE.UI.RB.frmRTypeTempChoose")
                {
                    ((frmRTypeTempChoose)(this.Form)).RTTemplaetID = lblRT_Money.BindDataValue.ToString();
                    this.Form.ShowResult = ShowResult.Yes;
                    this.Form.Close();
                }
                else if (this.Form.ToString() == "SmoONE.UI.RB.frmRTypeTemplate")
                {
                    frmRTypeTempCreate frm = new frmRTypeTempCreate();              //����ģ�崴����������ҳ��
                    frm.ID = lblRT_Money.BindDataValue.ToString(); 
                    this.Form.Show(frm, (MobileForm sender1, object args) =>
                    {
                        if (frm.ShowResult == ShowResult.Yes)
                        {
                            ((frmRTypeTemplate)(this.Form)).Bind();            //���¼�������
                           
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }

       

        /// <summary>
        /// ���
        /// </summary>
        internal string NO
        {
            get
            {
                return   this.lblRT_Money.BindDataValue.ToString();
            }
            
        }
    }
}