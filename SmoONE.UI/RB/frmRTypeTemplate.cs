using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.Data;
using SmoONE.UI;
using SmoONE.DTOs;

namespace SmoONE.UI.RB
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ����ģ���б����
    // ******************************************************************
    partial class frmRTypeTemplate : Smobiler.Core.Controls.MobileForm
    {
        #region "Properties"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��������ģ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Press(object sender, EventArgs e)
        {
            try
            {
                frmRTypeTempCreate frm = new frmRTypeTempCreate();         //����ģ�崴��ҳ��
                this.Show(frm, (MobileForm from, object args) =>
                {
                    if (frm.ShowResult == ShowResult.Yes)
                    {
                        Bind();
                    }
                });
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ��ʼ��ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRTypeTemplate_Load(object sender, EventArgs e)
        {
            Bind();      //��������
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="row"></param>
        internal void RemoveRow(ListViewRow row)
        {
            this.listRBModelData.Rows.Remove(row);
        }
        ///// <summary>
        ///// �ر�����໬
        ///// </summary>
        //public void CloseRowSwipe()
        //{
        //    foreach (ListViewRow Row in listRBModelData.Rows)
        //    {
        //        Layout.frmRBModelLayout RBModeLayout = Row.Control as Layout.frmRBModelLayout;
        //        RBModeLayout.CloseSwipe();
        //    }
        //}
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <remarks></remarks>
        internal void Bind()
        {
            try
            {
                List<RB_RType_TemplateDto> RBRTypeTemplate = AutofacConfig.rBService.GetTemplateByCreateUser(Client.Session["U_ID"].ToString());    //���ݵ�ǰ�û�ID��ѯ����ģ��
                DataTable table = new DataTable();
                table.Columns.Add("RB_RTT_TemplateID");         //����ģ����
                table.Columns.Add("RB_RTT_TypeID");             //�������ͱ��
                table.Columns.Add("RB_RTT_TypeName");           //������������
                table.Columns.Add("RB_RTT_Amount");             //���ѽ��
                table.Columns.Add("RB_RTT_Note");               //���ѱ�ע  
                foreach (RB_RType_TemplateDto row in RBRTypeTemplate)
                {
                    String TypeName = AutofacConfig.rBService.GetTypeNameByID(row.RB_RTT_TypeID);
                    table.Rows.Add(row.RB_RTT_TemplateID, row.RB_RTT_TypeID, TypeName, row.RB_RTT_Amount, row.RB_RTT_Note);
                }
                this.listRBModelData.Rows.Clear(); //�������ģ���б�����
                if (table.Rows.Count > 0)              //��������������ʱ
                {
                    this.lblInfor.Visible = false;            //������ʾ����
                    //��ֵ����
                    this.listRBModelData.DataSource = table;
                    this.listRBModelData.DataBind();
                }
                else
                {
                    this.lblInfor.Visible = true;

                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// �ֻ��Դ����ؼ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRTypeTemplate_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                this.Close();         //�رյ�ǰҳ��
            }
        }
    }
}