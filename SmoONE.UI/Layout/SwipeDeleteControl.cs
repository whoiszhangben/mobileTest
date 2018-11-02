using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.CommLib;

namespace SmoONE.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class SwipeDeleteControl : Smobiler.Core.Controls.MobileUserControl
    {
        #region "Properties"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
       

        /// <summary>
        /// ɾ���б�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelRow_Press(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("ȷ��Ҫɾ��ѡ�����", MessageBoxButtons.YesNo, (Object s1, MessageBoxHandlerArgs args) =>
                {
                    //��ί��Ϊ�첽ί���¼�
                    if (args.Result == Smobiler.Core.Controls.ShowResult.Yes)
                    {
                        switch (this.Form.ToString())
                        {
                            case "SmoONE.UI.RB.frmRTypeTemplate":
                                ReturnInfo r = AutofacConfig.rBService.DeleteRB_Type_Template(((frmRBModelLayout)this.Parent.Parent).NO);
                                if (r.IsSuccess == true)
                                {
                                    ListViewRow row = this.Parent.Parent.Tag as ListViewRow;
                                    ((RB.frmRTypeTemplate)(this.Form)).RemoveRow(row);//ɾ����ǰ�б�����
                                    Toast("ɾ������ģ��ɹ�");
                                }
                                else
                                {
                                    throw new Exception(r.ErrorInfo);
                                }
                                break;
                            case "SmoONE.UI.RB.frmRBRows":
                                ReturnInfo r1 = AutofacConfig.rBService.DeleteRB_Rows(Convert.ToInt32(((frmConsumptionLayout)this.Parent.Parent).NO));
                                if (r1.IsSuccess == true)
                                {
                                    ListViewRow row = this.Parent.Parent.Tag as ListViewRow;
                                    ((RB.frmRBRows)(this.Form)).RemoveRow(row);//ɾ����ǰ�б�����
                                    Toast("ɾ�����Ѽ�¼�ɹ�");
                                }
                                else
                                {
                                    throw new Exception(r1.ErrorInfo);
                                }
                                break;
                        }
                    }
                });
                   
               
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

       
    }
}