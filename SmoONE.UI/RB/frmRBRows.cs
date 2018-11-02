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
    // ��Ҫ���ݣ�  ���Ѽ�¼�б����
    // ******************************************************************
    partial class frmRBRows : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <remarks></remarks>
        public void Bind()
        {
            try
            {
                List<RB_RowsDto> RBRows = AutofacConfig.rBService.GetRowsByCreateUser(Client.Session["U_ID"].ToString());
                //���Ѽ�¼��������
                DataTable table = new DataTable();
                table.Columns.Add("ID", typeof(System.Int32));               //���Ѽ�¼���
                table.Columns.Add("RB_NO", typeof(System.String));           //���������
                table.Columns.Add("RBROW_DATE", typeof(System.String));      //��������
                table.Columns.Add("RBROW_TYPE", typeof(System.String));      //�������ͱ��
                table.Columns.Add("RBROW_TYPENAME", typeof(System.String));  //������������
                table.Columns.Add("RBROW_AMOUNT", typeof(System.Decimal));   //���ѽ��
                table.Columns.Add("RBROW_NOTE", typeof(System.String));      //���ѱ�ע
                foreach (RB_RowsDto rows in RBRows)
                {
                    table.Rows.Add(rows.R_ID, rows.RB_ID, rows.R_ConsumeDate.ToString("yyyy/MM/dd"), rows.R_TypeID, rows.R_TypeName, rows.R_Amount, rows.R_Note);
                }
                this.listRBRowData.Rows.Clear();//������Ѽ�¼�б�����
                if (table.Rows.Count > 0)    //��������������ʱ
                {

                    this.lblInfor.Visible = false;          //������ʾ����
                    this.listRBRowData.DataSource = table;
                    this.listRBRowData.DataBind();
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

        ///// <summary>
        ///// �ر�����໬
        ///// </summary>
        //public void CloseRowSwipe()
        //{
        //    foreach (ListViewRow Row in listRBRowData.Rows)
        //    {                
        //        Layout.frmConsumptionLayout RBRowsLayout = Row.Control as Layout.frmConsumptionLayout;
        //        RBRowsLayout.swipeView1 .Close ();
        //    }
        //}
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRBRows_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                this.Close();             //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ɾ���б�����
        /// </summary>
        /// <param name="row"></param>
        internal void RemoveRow(ListViewRow row)
        {
            this.listRBRowData.Rows.Remove(row);
        }

        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRBRows_Load(object sender, EventArgs e)
        {
            Bind();            //��ʼ������
            
        }
        /// <summary>
        /// �������Ѽ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Press(object sender, EventArgs e)
        {
            frmRowsCreate frm = new frmRowsCreate();
            this.Show(frm, (MobileForm from, object args) =>
            {
                if (frm.ShowResult == ShowResult.Yes)
                {
                    Bind();          //���¼�������
                }
            });
        }
    }
}