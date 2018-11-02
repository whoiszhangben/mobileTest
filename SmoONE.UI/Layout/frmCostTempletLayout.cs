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
    // ��Ҫ���ݣ�  �ɱ�����ģ���б�ģ��
    // ******************************************************************
    partial class frmCostTempletLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// �ɱ�����ģ���б�ģ���������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_Press(object sender, EventArgs e)
        {
           ((SmoONE.UI.CostCenter.frmCostTemplet)this.Form).CTempID = lblID.BindDataValue.ToString();
            //�����Ϊ�ɱ�ģ��ѡ�񣬵��ʱ��رս���
            if (((SmoONE.UI.CostCenter.frmCostTemplet)this.Form).IsSelectCTemPlet == true)
            {
                this.Form .ShowResult = Smobiler.Core.Controls.ShowResult.Yes;
               this.Form . Close();
            }
            else
            {
                //��ת���ɱ�ģ����ϸ����
                SmoONE.UI.CostCenter.frmCostTempletDetail frm = new SmoONE.UI.CostCenter.frmCostTempletDetail();
                frm.CTempID =((SmoONE.UI.CostCenter.frmCostTemplet)this.Form). CTempID;
                this.Form.Show(frm, (MobileForm form, object args) =>
                {
                    if (frm.ShowResult == ShowResult.Yes)
                    {
                        ((SmoONE.UI.CostCenter.frmCostTemplet)this.Form).Bind();
                    }
                });
            }
        }

       
    }
}