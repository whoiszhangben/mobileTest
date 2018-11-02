using System;
using System.Collections.Generic;
using Smobiler.Core.Controls;
using System.Data;
using SmoONE.DTOs;

namespace SmoONE.UI.RB
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ����ģ��ѡ�����
    // ******************************************************************
    partial class frmRTypeTempChoose : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        internal string RTTemplaetID;           //ģ����
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����ز���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRTypeTempChoose_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                this.Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ��ʼ��ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRTypeTempChoose_Load(object sender, EventArgs e)
        {
            Bind();                   //��������ģ������
        }
        /// <summary>
        /// ��ʼ����������������
        /// </summary>
        private void Bind()
        {
            try
            {
                List<RB_RType_TemplateDto> RBRTypeTemplate = AutofacConfig.rBService.GetTemplateByCreateUser(Client.Session["U_ID"].ToString());    //Steven���û�ID
                DataTable table = new DataTable();
                table.Columns.Add("RB_RTT_TemplateID");
                table.Columns.Add("RB_RTT_TypeID");
                table.Columns.Add("RB_RTT_TypeName");
                table.Columns.Add("RB_RTT_Amount");
                table.Columns.Add("RB_RTT_Note");
                foreach (RB_RType_TemplateDto row in RBRTypeTemplate)
                {
                    String TypeName = AutofacConfig.rBService.GetTypeNameByID(row.RB_RTT_TypeID);
                    table.Rows.Add(row.RB_RTT_TemplateID, row.RB_RTT_TypeID, TypeName, row.RB_RTT_Amount, row.RB_RTT_Note);
                }
                listRBModel.Rows.Clear(); //�������ģ��ѡ���б�����
                if (table.Rows.Count > 0)
                {
                    this.lblInfor.Visible = false;
                    this.btnCreate.Visible = false;
                    this.listRBModel.DataSource = table;
                    this.listRBModel.DataBind();
                }

            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ���������ť����ת������ģ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Press(object sender, EventArgs e)
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

        private void title1_Load(object sender, EventArgs e)
        {

        }
    }
}