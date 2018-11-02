using System;
using System.Collections.Generic;
using Smobiler.Core.Controls;
using System.Data;
using SmoONE.DTOs;
using SmoONE.CommLib;
using SmoONE.UI.Layout;

namespace SmoONE.UI.RB
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  �������������
    // ******************************************************************
    partial class frmRBDetail : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        internal string ID;                 //���������
        internal string UserID;               //��ǰ�û�ID
        internal RB_Status Status;               //������״̬
        RefuseDialog Refuse = new RefuseDialog();        //�����ܾ�������
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void Bind()
        {
            try
            {
                UserID = Client.Session["U_ID"].ToString();               //��ǰ�û�ID
                List<RB_RowsDto> Rows = AutofacConfig.rBService.GetRowByRBID(ID);         //���ұ����������Ѽ�¼��Ϣ
                RBDetailDto Reim = AutofacConfig.rBService.GetByID(ID);             //���ұ������������Ϣ
                Status = (RB_Status)Reim.RB_Status;             //����������ǰ״̬                             
                string[] ReimAEACheckers = Reim.RB_AEACheckers.Split(Convert.ToChar(","));       //��ȡ����������Ա
                string[] ReimFinancialCheckers = Reim.RB_FinancialCheckers.Split(Convert.ToChar(","));       //��ȡ����������Ա
                UserDetailDto userInfo = AutofacConfig.userService.GetUserByUserID(Reim.RB_CreateUser);
                title1.TitleText = userInfo.U_Name + "�ı���";                                     //����
                lblUserName.Text = userInfo.U_Name;                                   //�����������û�               
                //��û�������û�ͷ��ʱ�������û��Ա���ʾĬ��ͷ��
                if (string.IsNullOrEmpty(userInfo.U_Portrait) == true)
                {
                    switch (userInfo.U_Sex)
                    {
                        case (int)Sex.��:
                            imgPortrait.ResourceID = "boy";
                            break;
                        case (int)Sex.Ů:
                            imgPortrait.ResourceID = "girl";
                            break;
                    }
                }
                else
                {
                    imgPortrait.ResourceID = userInfo.U_Portrait;
                }
                //Ĭ��״̬�£�����ʾͬ�⣬�ܾ����༭��ť
                btnAgreed.Visible = false;
                btnRefuse.Visible = false;
                btnEdit.Visible = false;
                switch (Status)
                {
                    case RB_Status.�½�:                        //������Ѵ���״̬����ǰ�û�Ϊ�����ˣ�����ʾ������ť
                        lblRBState.Text = "�ȴ�����������";
                        if (UserID == Reim.RB_LiableMan)
                        {
                            btnAgreed.Visible = true;
                            btnRefuse.Visible = true;
                            if (UserID == Reim.RB_CreateUser)
                            {
                                btnEdit.Visible = true;
                                btnAgreed.Width = 85;
                                btnAgreed.Left = 10;
                                btnRefuse.Width = 85;
                                btnRefuse.Left = 108;
                                btnEdit.Width = 85;
                                btnEdit.Left = 205;
                            }
                            else
                            {
                                btnAgreed.Width = 134;
                                btnAgreed.Left = 10;
                                btnRefuse.Width = 134;
                                btnRefuse.Left = 156;
                            }
                        }
                        else
                        {
                            if (UserID == Reim.RB_CreateUser)
                            {
                                btnEdit.Visible = true;
                                btnEdit.Width = 280;
                                btnEdit.Left = 10;
                            }
                            else
                            {
                                plButton.Border = new Border(0);
                            }
                        }
                        break;
                    case RB_Status.����������:                  //�����������������״̬����ǰ�û�Ϊ���������ˣ�����ʾ�������ܰ�ť
                        lblRBState.Text = "�ȴ���������";
                        foreach (string ReimAEAChecker in ReimAEACheckers)
                        {
                            if (UserID == ReimAEAChecker)
                            {
                                btnAgreed.Visible = true;
                                btnRefuse.Visible = true;
                                btnAgreed.Width = 134;
                                btnAgreed.Left = 10;
                                btnRefuse.Width = 134;
                                btnRefuse.Left = 156;
                            }
                        }
                        break;
                    case RB_Status.��������:                //�����ǰ��������������ǰ�û�Ϊ��������ʾ�������ܰ�ť
                        lblRBState.Text = "�ȴ���������";
                        foreach (string ReimFinancialChecker in ReimFinancialCheckers)
                        {
                            if (UserID == ReimFinancialChecker)
                            {
                                btnAgreed.Visible = true;
                                btnRefuse.Visible = true;
                                btnAgreed.Width = 134;
                                btnAgreed.Left = 10;
                                btnRefuse.Width = 134;
                                btnRefuse.Left = 156;
                            }
                        }
                        break;
                    case RB_Status.��������:                     //����ͨ��������ʾ�κΰ�ť
                        lblRBState.Text = "����������ɣ�";
                        plButton.Border = new Border(0);
                        break;
                    case RB_Status.�Ѿܾ�:                   //�������أ������ǰ�û�Ϊ��������ʼ�ˣ�����ʾ�༭��ť
                        lblRBState.Text = "���������ܾ���";
                        if (UserID == Reim.RB_CreateUser)
                        {
                            btnEdit.Visible = true;
                            btnEdit.Width = 280;
                            btnEdit.Left = 10;
                        }
                        else
                        {
                            plButton.Border = new Border(0);
                        }
                        break;
                }
                CCDetailDto Cost = AutofacConfig.costCenterService.GetCCByID(Reim.CC_ID);
                lblRBNO.Text = Reim.RB_ID;     //���������
                lblRBCC.Text = Cost.CC_Name;        //�ɱ���������
                lblAmount.Text = Reim.RB_TotalAmount.ToString();           //�ܽ��
                lblAmount.Text = Reim.RB_TotalAmount.ToString();//�������ܽ��
                lblCreateTime.Text = Reim.RB_CreateDate.ToString("yyyy/MM/dd");
                lblnote.Text = Reim.RB_Note;              //��������ע

                if (string.IsNullOrEmpty(Reim.RB_RejectionReason) == true)
                {
                    lblReason.Text = "";
                    lblReason.Height = 25;
                    lblReason1.Height = 25;
                    lblReason.VerticalAlignment = VerticalAlignment.Center;
                    lblReason1.VerticalAlignment = VerticalAlignment.Center;
                    lblReason.Padding = new Padding(0, 0, 0, 0);
                    lblReason1.Padding = new Padding(0, 0, 0, 0);
                }
                else
                {
                    lblReason.Text = Reim.RB_RejectionReason;       //�������ܾ�ԭ��
                    lblReason.Height = 50;
                    lblReason1.Height = 50;
                    lblReason.VerticalAlignment = VerticalAlignment.Top;
                    lblReason1.VerticalAlignment = VerticalAlignment.Top;
                    lblReason.Padding = new Padding(0, 5, 0, 0);
                    lblReason1.Padding = new Padding(0, 5, 0, 0);
                }
                listRBRowData.Top = lblReason.Top + lblReason.Height;

                //��������ѽ����������ذ�ť��ռ����
                if (btnAgreed.Visible == false && btnEdit.Visible == false && btnRefuse.Visible==false)
                {
                    plButton.Visible = false;
                }

                //�����������ݽ��д���������ڣ�Ȼ����ʾ��ҳ����
                DataTable rbrowtable = new DataTable();
                rbrowtable.Columns.Add("ID", typeof(System.Int32));         //���Ѽ�¼���
                rbrowtable.Columns.Add("RB_NO", typeof(System.String));   //��������
                rbrowtable.Columns.Add("RBROW_DATE", typeof(System.String));       //��������
                rbrowtable.Columns.Add("RBROW_TYPE", typeof(System.String));      //��������ID
                rbrowtable.Columns.Add("RBROW_TYPENAME", typeof(System.String));   //������������
                rbrowtable.Columns.Add("RBROW_AMOUNT", typeof(System.Decimal));      //��������
                rbrowtable.Columns.Add("RBROW_NOTE", typeof(System.String));       //���Ѽ�¼��ע
                foreach (RB_RowsDto Row in Rows)
                {
                    string TypeName = AutofacConfig.rBService.GetTypeNameByID(Row.R_TypeID);
                    rbrowtable.Rows.Add(Row.R_ID, Row.RB_ID, Row.R_ConsumeDate.ToString("yyyy/MM/dd"), Row.R_TypeID, TypeName, Row.R_Amount, Row.R_Note);
                }
                listRBRowData.Rows.Clear();//��ձ����������б�����
                if (rbrowtable.Rows.Count > 0)
                {
                    this.listRBRowData.DataSource = rbrowtable;
                    this.listRBRowData.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRBDetail_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                this.Close();            //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRBDetail_Load(object sender, EventArgs e)
        {
            try
            {
                title1.TitleText = "��������";
                Bind();               //���ݳ�ʼ������
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ͬ�ⱨ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgreed_Press(object sender, EventArgs e)
        {
            try
            {
                switch (Status)
                {
                    case RB_Status.�½�:
                        Status = RB_Status.����������;
                        break;
                    case RB_Status.����������:
                        Status = RB_Status.��������;
                        break;
                    case RB_Status.��������:
                        Status = RB_Status.��������;
                        break;
                }
                ReturnInfo r = AutofacConfig.rBService.UpdateRBStatus(ID, Status, UserID, "");           //���汨����
                if (r.IsSuccess == true)
                {
                    Bind();               //ˢ��ҳ��
                    this.ShowResult = ShowResult.Yes;
                    Toast("�����ɹ�");
                }
                else
                {
                    throw new Exception(r.ErrorInfo);
                }
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// �ܾ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefuse_Press(object sender, EventArgs e)
        {
            try
            {
                //��������ܾ����ɽ���               
                this.ShowDialog(Refuse);
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        //�ύ�ܾ����������ݿ�
        public void SureRefuse()
        {
            try
            {
                ReturnInfo r = AutofacConfig.rBService.UpdateRBStatus(ID, RB_Status.�Ѿܾ�, UserID, this.lblReason.Text);
                if (r.IsSuccess == true)
                {
                    Bind();     //�����ɹ���ˢ��ҳ��
                    this.ShowResult = ShowResult.Yes;
                    //Refuse.Close();
                    this.Form.Toast("�����ɹ�");
                }
                else
                {
                    throw new Exception(r.ErrorInfo);
                }
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// �����༭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Press(object sender, EventArgs e)
        {
            try
            {
                frmRBEdit RBEdit = new frmRBEdit();
                RBEdit.ID = ID;
                this.Show(RBEdit, (MobileForm from, object args) =>
                {
                    if (RBEdit.ShowResult == ShowResult.Yes)
                    {
                        Bind();          //���¼�������
                    }
                });
                this.ShowResult = ShowResult.Yes;
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}