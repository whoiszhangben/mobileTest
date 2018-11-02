using System;
using Smobiler.Core.Controls;
using SmoONE.UI.RB;

namespace SmoONE.UI.Layout
{
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmConsumptionLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ���
        /// </summary>
        internal string NO
        {
            get
            {
                return this.lblMoney.BindDataValue.ToString();
            }

        }

      
        /// <summary>
        /// �鿴���Ѽ�¼����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plContent_Press(object sender, EventArgs e)
        {
            try
            {
                frmRowsCreate frm = new frmRowsCreate();      //�������Ѽ�¼����ҳ��
                frm.ID = lblMoney.BindDataValue.ToString();
                this.Form.Show(frm, (MobileForm sender1, object args) =>
                {
                    if (frm.ShowResult == ShowResult.Yes)
                    {
                        ((frmRBRows)(this.Form)).Bind();//���¼�������
                    }
                    
                });
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }

    }
}