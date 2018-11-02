using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.Data;
using SmoONE.UI;
using SmoONE.DTOs;
using SmoONE.CommLib;
using SmoONE.UI.Layout;
using SmoONE.UI.CostCenter;

namespace SmoONE.UI.RB
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ��������������
    // ******************************************************************
    partial class frmRBCreate : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        private string RBCC;          //�ɱ����ı��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����ؼ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRBCreate_KeyDown(object sender, KeyDownEventArgs e)
        {
            if(e.KeyCode==KeyCode.Back)
            {
                Close();
            }
        }
        /// <summary>
        /// �����ʼ�������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRBCreate_Load(object sender, EventArgs e)
        {        
            try
            {
                Bind();        //���ݼ���
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// Bind����
        /// </summary>
        private void Bind()
        {
            try
            {
                //���û���δ�������Ѽ�¼��ʾ����
                DataTable table = new DataTable();
                table.Columns.Add("ID", typeof(System.Int32));
                table.Columns.Add("RBROW_DATE", typeof(System.String));
                table.Columns.Add("RBROW_TYPE", typeof(System.String));
                table.Columns.Add("RBROW_TYPENAME", typeof(System.String));
                table.Columns.Add("RBROW_AMOUNT", typeof(System.Decimal));
                table.Columns.Add("RBROW_NOTE", typeof(System.String));
                //�û���δ�������Ѽ�¼
                List<RB_RowsDto> Rows = AutofacConfig.rBService.GetRowsByCreateUser(Client.Session["U_ID"].ToString());
                foreach (RB_RowsDto Row in Rows)
                {
                    string TypeName = AutofacConfig.rBService.GetTypeNameByID(Row.R_TypeID);
                    table.Rows.Add(Row.R_ID, Row.R_ConsumeDate.ToString("yyyy/MM/dd"), Row.R_TypeID, TypeName, Row.R_Amount, Row.R_Note);
                }
                listRBRowData.Rows.Clear();//��ձ����������б�����
                if (table.Rows.Count > 0)
                {
                    this.listRBRowData.DataSource = table;
                    this.listRBRowData.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// �ɱ�����ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void btnRBCC_Press(object sender, EventArgs e)
        {
            try
            {
                frmRBCostCenter frmCostCenter = new frmRBCostCenter();           //��ת���ɱ����Ľ���
                this.Show(frmCostCenter, (MobileForm sender1, object args) =>
                {
                    try
                    {
                        //����ɱ�����ѡ��ɹ�
                        if (frmCostCenter.ShowResult == ShowResult.Yes)
                        {
                            string CCID = frmCostCenter.CCID;         //  CCID��ֵΪ�ɱ����ı��/����
                            string[] CCS = CCID.Split(new char[] { '/' });
                            RBCC = CCS[0];                       //�ɱ����ı��              
                            this.btnRBCC.Text = CCS[1]+ "   > ";          //�ɱ���������
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ����ȫѡ״̬
        /// </summary>
        public void upCheckState()
        {
            int selectUserQty = 0;      //���õ�ǰѡ������Ϊ0
            foreach (ListViewRow Row in listRBRowData.Rows)
            {
                frmRBCreateLayout layout = Row.Control  as frmRBCreateLayout;
                int num=layout.checkNum();
                selectUserQty += num;
            }
            if (selectUserQty == listRBRowData.Rows.Count)                //��ѡ����������ʱ
            {
                Checkall.Checked = true;
            }
            else                               //��û��ѡ����������ʱ
            {
                Checkall.Checked = false;
            }
        }
        /// <summary>
        ///���㱨�����ܽ��
        /// </summary>
        /// <remarks></remarks>
        public void getAmount()
        {
            try
            {
                decimal sumAmount = 0;
                foreach (ListViewRow Row in listRBRowData.Rows)
                {
                    //�ж����Ѽ�¼�Ƿ�ѡ�У�����ѡ�е����Ѽ�¼�ܽ��
                    frmRBCreateLayout layout = Row.Control as frmRBCreateLayout;
                    decimal num = layout.getNum();       //��ȡѡ������������
                    sumAmount += num;
                }
                //�����������ѡ�е����Ѽ�¼�ܽ����ʾ�ڵײ�
                lblAmount.Text= "��" + sumAmount.ToString(); 
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ȫѡ��ȫ��ѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewRow Row in listRBRowData.Rows)
            {
                frmRBCreateLayout Layout = Row.Control as frmRBCreateLayout;
                Layout.setCheck(Checkall.Checked);
            }
            getAmount();      //���㱨�����ܽ��
        }
        /// <summary>
        /// �������Ѽ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(RBCC))     //�жϳɱ������Ƿ��Ѿ�ѡ��
                {
                    throw new Exception("��ѡ��ɱ����ģ�");
                }
                else
                {
                    RBInputDto RB = new RBInputDto();       //����һ���µı�����
                    RB.CC_ID = RBCC;             //�ɱ����ı��
                    RB.RB_Note = this.TxtNote.Text;            //��������ע
                    //��ѡ�е����Ѽ�¼������ӵ���������
                    foreach (ListViewRow Row in listRBRowData.Rows)
                    {
                        frmRBCreateLayout layout = Row.Control as frmRBCreateLayout;
                        //�����ǰ�������Ѽ�¼��ѡ��
                        if (layout.checkNum()==1)
                        {
                            //��ѡ���е����Ѽ�¼�����������ӵ���������
                            int RID = layout.getID();
                            RB_RowsDto RBRow = AutofacConfig.rBService.GetRowByRowID(RID);
                            RB_RowsInputDto NewRBRow = new RB_RowsInputDto();
                            NewRBRow.R_ID = RBRow.R_ID;              //���Ѽ�¼���
                            NewRBRow.R_TypeID = RBRow.R_TypeID;           //�������ͱ��
                            NewRBRow.R_Amount = RBRow.R_Amount;            //���Ѽ�¼���
                            NewRBRow.R_Note = RBRow.R_Note;                //���Ѽ�¼����
                            NewRBRow.R_ConsumeDate = RBRow.R_ConsumeDate;           //��������
                            RB.RB_Rows.Add(NewRBRow);
                        }
                    }
                    RB.RB_CreateUser = Client.Session["U_ID"].ToString();               //�����û�
                    ReturnInfo r = AutofacConfig.rBService.CreateRB(RB);
                    if (r.IsSuccess == true)
                    {
                        //������ݿ���ӱ�����¼�ɹ�
                        this.ShowResult = ShowResult.Yes;
                        this.Close();
                        Toast("�����ύ�ɹ���");
                    }
                    else
                    {
                        throw new Exception(r.ErrorInfo);
                    }
                }
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}