using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using System.Data;

namespace SmoONE.UI.CostCenter
{
    partial class frmCostCenterFXDetail : Smobiler.Core.Controls.MobileForm
    {

        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        public string CCID;//�ɱ�����
        public decimal CC_Amount;//�ɱ����Ľ��
        private  decimal CC_confirmAmount=0;//�ѱ������
        private decimal CC_createAmount = 0;//�����н��
        private List<ReimbursementDto> listRBDto=new List<ReimbursementDto>();
        #endregion
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostCenterFXDetail_Load(object sender, EventArgs e)
        {
            getRBDate();
            getCCFXDate();
            getBindtableDate("�ѱ���");
        }
        /// <summary>
        /// ��ȡ�ɱ����ı�����������
        /// </summary>
        private  void getRBDate()
        {

            try
            {
                //��ȡ��ǰ�ɱ����ĵı�������
                listRBDto = AutofacConfig.rBService.GetByCCID(CCID);
                if (listRBDto.Count > 0)
                {
                    foreach (ReimbursementDto reimbursement in listRBDto)
                    {
                        switch (reimbursement.RB_Status)
                        {
                            case (int)RB_Status.�½�:
                            case (int)RB_Status.����������:
                            case (int)RB_Status.��������:
                                CC_createAmount += reimbursement.RB_TotalAmount;
                                break;
                            case (int)RB_Status.��������:
                                CC_confirmAmount += reimbursement.RB_TotalAmount;

                                break;

                        }
                    }
                }
              }
              
                    catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// ��ƴͼ��������
        /// </summary>
        private  void getCCFXDate()
        {

            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("Type", typeof(System.String));
                table.Columns.Add("Amount", typeof(System.Decimal));
                if (CC_Amount > 0)
                {
                    DataRow dr1 = table.NewRow();

                    dr1["Type"] = "�ѱ���";
                    dr1["Amount"] = CC_confirmAmount;
                    table.Rows.Add(dr1);
                    DataRow dr2 = table.NewRow();
                    dr2["Type"] = "������";
                    dr2["Amount"] = CC_createAmount;
                    table.Rows.Add(dr2);
                    DataRow dr3 = table.NewRow();
                    dr3["Type"] = "ʣ����";
                    dr3["Amount"] =CC_Amount - CC_confirmAmount- CC_createAmount;
                    table.Rows.Add(dr3);
                   
                    pieChart1.DataSource = table;
                    pieChart1.DataBind();
                }
                   


            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }

        }
        /// <summary>
        /// ��tableview����
        /// </summary>
        /// <param name="type"></param>
        private  void getBindtableDate(string type)
        {

            try
            {
                tableView1.Rows.Clear();  
                //�������������������0����������
                if (listRBDto.Count > 0 & (type.Equals("�ѱ���") || type.Equals("������")))
                {
                    DataTable rbtable = new DataTable();
                    rbtable.Columns.Add("RB_ID", typeof(System.String));
                    rbtable.Columns.Add("R_ConsumeDate", typeof(System.String ));
                    rbtable.Columns.Add("R_TypeName", typeof(System.String));
                    rbtable.Columns.Add("R_Amount", typeof(System.String));
                    rbtable.Columns.Add("R_CreateUser", typeof(System.String));
                    List<string> listRBNO = new List<string>();
                    foreach (ReimbursementDto reimbursement in listRBDto)
                    {
                        if ((type.Equals("�ѱ���") & reimbursement.RB_Status.Equals((int)RB_Status.��������)) || (type.Equals("������")&(reimbursement.RB_Status.Equals((int)RB_Status.�½�)|| reimbursement.RB_Status.Equals((int)RB_Status.���������� )|| reimbursement.RB_Status.Equals((int)RB_Status.��������))))
                        {
                            listRBNO.Add(reimbursement.RB_ID);
                        }
                    }
                    if (listRBNO.Count  >0)
                    {
                        foreach (string  rbNO in listRBNO)
                        {
                            List<RB_RowsDto> Rows = AutofacConfig.rBService.GetRowByRBID(rbNO);         //���ұ����������Ѽ�¼��Ϣ
                            if (Rows .Count >0)
                            {
                                foreach (RB_RowsDto Row in Rows)
                                {
                                    DataRow dr = rbtable.NewRow();
                                    dr["RB_ID"] = Row.RB_ID ;
                                    dr["R_ConsumeDate"] = Row.R_ConsumeDate.ToString("yyyy/MM/dd");
                                    dr["R_TypeName"] = Row.R_TypeName;
                                    dr["R_Amount"] = "��" + Row.R_Amount.ToString();
                                    UserDetailDto user = AutofacConfig.userService.GetUserByUserID(Row.R_CreateUser);
                                    if (user != null & string.IsNullOrEmpty(user.U_Name) == false)
                                    {
                                        dr["R_CreateUser"] = user.U_Name;
                                    }
                                      
                                    rbtable.Rows.Add(dr);
                                }
                            }
                           
                        }
                      }
                    tableView1.DataSource = rbtable;
                    tableView1.DataBind();
                }
                }
                    catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }

        }

        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostCenterFXDetail_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostCenterFXDetail_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pieChart1_ValueSelected(object sender, PieChartValueSelectedEventArgs e)
        {
            try
            {
              if ( e.XValue.Equals ("�ѱ���") || e.XValue.Equals("������"))
              {
                    getBindtableDate(e.XValue);
                }

            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
    }
}