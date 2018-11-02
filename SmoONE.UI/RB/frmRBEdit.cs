using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.Data;
using SmoONE.DTOs;
using SmoONE.UI;
using SmoONE.CommLib;
using SmoONE.UI.Layout;
using SmoONE.UI.CostCenter;

namespace SmoONE.UI.RB
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  �������༭����
    // ******************************************************************
    partial class frmRBEdit : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        private string RBCC;                 //�ɱ����ı��
        internal string ID;                //���������
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ɱ�����ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRBCC_Press(object sender, EventArgs e)
        {
            try
            {
                frmRBCostCenter frmCostCenter = new frmRBCostCenter();
                this.Show(frmCostCenter, (MobileForm sender1, object args) =>
                {
                    try
                    {
                        if (frmCostCenter.ShowResult == ShowResult.Yes)
                        {
                            string CCID = frmCostCenter.CCID;
                            string[] CCS = CCID.Split(new char[] { '/' });
                            RBCC = CCS[0];         //�ɱ����ı��
                            this.btnRBCC.Text = CCS[1] + "   > ";           //�ɱ���������
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
        /// ���汨���༭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                RBInputDto ReimBur = new RBInputDto();
                ReimBur.RB_Rows = new List<RB_RowsInputDto>();
                ReimBur.RB_ID = this.lblRBNO.Text;            //���������
                ReimBur.CC_ID = RBCC;                   //�ɱ����ı��
                ReimBur.RB_Note = this.TxtNote.Text;                  //��ע
                //���Ѽ�¼�������
                foreach (ListViewRow Row in listRBRowData.Rows)
                {
                    //�ж����Ѽ�¼�Ƿ�ѡ��
                    frmConsumption1Layout layout = Row.Control as frmConsumption1Layout;
                    if (layout.checkNum() == 1)
                    {
                        //��ѡ���е�Row��������ӵ���������
                        int RID = layout.getID();
                        RB_RowsDto RBRow = AutofacConfig.rBService.GetRowByRowID(RID);
                        RB_RowsInputDto NewRBRow = new RB_RowsInputDto();
                        NewRBRow.R_ID = RBRow.R_ID;
                        NewRBRow.R_TypeID = RBRow.R_TypeID;
                        NewRBRow.R_Amount = RBRow.R_Amount;
                        NewRBRow.R_Note = RBRow.R_Note;
                        NewRBRow.R_ConsumeDate = RBRow.R_ConsumeDate;
                        ReimBur.RB_Rows.Add(NewRBRow);
                    }
                }
                ReturnInfo r = AutofacConfig.rBService.UpdateRB(ReimBur);       //��������¼���µ����ݿ���
                if (r.IsSuccess == true)
                {
                    this.ShowResult = ShowResult.Yes;
                    this.Close();
                    Toast("�����ύ�ɹ���");
                }
                else
                {
                    throw new Exception(r.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ��ʼ�����ݣ���������ʾ��ҳ����
        /// </summary>
        private void Bind()
        {
            try
            {
                //��ʾ������ϸ
                List<RB_RowsDto> Rows = AutofacConfig.rBService.GetRowByRBID(ID);
                List<RB_RowsDto> UnReimRows = AutofacConfig.rBService.GetRowsByCreateUser(Client.Session["U_ID"].ToString());
                this.lblRBNO.Text = ID;                 //�������
                RBDetailDto Reim = AutofacConfig.rBService.GetByID(ID);
                RBCC = Reim.CC_ID;                //�ɱ����ı��
                CCDetailDto Cost = AutofacConfig.costCenterService.GetCCByID(Reim.CC_ID);
                this.btnRBCC.Text = Cost.CC_Name + "   > ";               //�ɱ���������
                this.TxtNote.Text = Reim.RB_Note;                       //��ע
                DataTable rowTable = new DataTable();
                rowTable.Columns.Add("ID", typeof(System.Int32));                       //���Ѽ�¼���
                rowTable.Columns.Add("RBCHECKED", typeof(System.Boolean));              //�Ƿ�ѡ��
                rowTable.Columns.Add("RB_NO", typeof(System.String));                   //���������
                rowTable.Columns.Add("RBROW_DATE", typeof(System.String));              //��������
                rowTable.Columns.Add("RBROW_TYPE", typeof(System.String));              //�������ͱ��
                rowTable.Columns.Add("RBROW_TYPENAME", typeof(System.String));          //������������
                rowTable.Columns.Add("RBROW_AMOUNT", typeof(System.Decimal));           //���ѽ��
                rowTable.Columns.Add("RBROW_NOTE", typeof(System.String));              //���ѱ�ע 
                foreach (RB_RowsDto Row in Rows)          //��ԭ�������е�������ӵ�����Դ��
                {
                    string TypeName = AutofacConfig.rBService.GetTypeNameByID(Row.R_TypeID);

                    rowTable.Rows.Add(Row.R_ID, true, Row.RB_ID, Row.R_ConsumeDate.ToString("yyyy/MM/dd"), Row.R_TypeID, TypeName, Row.R_Amount, Row.R_Note);
                }
                foreach (RB_RowsDto Row in UnReimRows)          //����ǰ�û�δ������������ӵ�����Դ��
                {
                    string TypeName = AutofacConfig.rBService.GetTypeNameByID(Row.R_TypeID);
                    rowTable.Rows.Add(Row.R_ID, false, Row.RB_ID, Row.R_ConsumeDate.ToString("yyyy/MM/dd"), Row.R_TypeID, TypeName, Row.R_Amount, Row.R_Note);
                }
                if (rowTable.Rows.Count > 0)
                {

                    rowTable.Columns.Add("ROW_NOTE", typeof(System.String));
                    rowTable.Columns.Add("ROW_DATE", typeof(System.String));
                    this.listRBRowData.DataSource = rowTable;
                    this.listRBRowData.DataBind();
                    getAmount();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRBEdit_Load(object sender, EventArgs e)
        {
            try
            {               
                Bind();               //��ʼ������
                upCheckState();
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
                frmConsumption1Layout layout = Row.Control as frmConsumption1Layout;
                int num = layout.checkNum();
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
                    frmConsumption1Layout layout = Row.Control as frmConsumption1Layout;
                    decimal num = layout.getNum();       //��ȡѡ������������
                    sumAmount += num;
                }
                //�����������ѡ�е����Ѽ�¼�ܽ����ʾ�ڵײ�
                lblAmount.Text = "��" + sumAmount.ToString();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ȫѡ����ȫ��ѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewRow Row in listRBRowData.Rows)
            {
                frmConsumption1Layout Layout = Row.Control as frmConsumption1Layout;
                Layout.setCheck(Checkall.Checked);
            }
            getAmount();      //���㱨�����ܽ��
        }
        /// <summary>
        /// �ֻ��Դ����ؼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRBEdit_KeyDown(object sender, KeyDownEventArgs e)
        {
            //�����ֻ����ؼ����򷵻ص���һҳ
            if (e.KeyCode == KeyCode.Back)
            {
                this.Close();            //�رյ�ǰҳ��
            }
        }
    }
}