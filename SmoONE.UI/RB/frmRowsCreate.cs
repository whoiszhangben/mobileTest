using System;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using SmoONE.CommLib;

namespace SmoONE.UI.RB
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ���Ѽ�¼������༭����
    // ******************************************************************
    partial class frmRowsCreate : Smobiler.Core.Controls.MobileForm
    {
        #region "Properties"
        internal string ID;               //���Ѽ�¼���
        private string TYPEID;            //�������ͱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����ؼ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRowsCreate_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                this.Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRowsCreate_Load(object sender, EventArgs e)
        {
            Bind();           //��ʼ������
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <remarks></remarks>
        private void Bind()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ID) == false)
                {
                    int RID = Convert.ToInt32(ID);       //��IDת����Int����
                    RB_RowsDto row = AutofacConfig.rBService.GetRowByRowID(RID);
                    string TypeName = AutofacConfig.rBService.GetTypeNameByID(row.R_TypeID);
                    title.TitleText = "���Ѽ�¼";
                    TYPEID = row.R_TypeID;           //�������ͱ��
                    this.btnRBType.Text = TypeName+ "   > ";           //������������
                    this.txtMoney.Text = row.R_Amount.ToString();       //���ѽ��
                    this.txtNote.Text = row.R_Note;                 //���ѱ�ע
                }
                else
                {
                    title.TitleText = "���Ѽ�¼����";
                    this.btnRBType.Text = "ѡ������"+ "   > ";
                    this.btnDelete.Visible = false;
                    btnSave.Width = 280;
                    btnSave.Left = 10;

                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ����ģ��ѡ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRBModel_Press(object sender, EventArgs e)
        {
            try
            {
                //��������ģ���б�
                frmRTypeTempChoose frm = new frmRTypeTempChoose();
                this.Show(frm, (MobileForm sender1, object args) =>
                {
                    if (frm.ShowResult == ShowResult.Yes)
                    {
                        //�ɹ�ѡ������ģ��󣬸�ҳ���Զ���ֵ
                        string TemplateID = frm.RTTemplaetID;
                        RB_RType_TemplateDto RBTemp = AutofacConfig.rBService.GetTemplateByTemplateID(TemplateID);
                        string RBTypeName = AutofacConfig.rBService.GetTypeNameByID(RBTemp.RB_RTT_TypeID);
                        this.btnRBModel.Text = "��ѡ��" + "   > ";
                        TYPEID = RBTemp.RB_RTT_TypeID;                //��������ID
                        this.txtMoney.Text = RBTemp.RB_RTT_Amount.ToString();          //���ѽ��
                        this.btnRBType.Text = RBTypeName + "   > ";       //������������
                        this.txtNote.Text = RBTemp.RB_RTT_Note;          //���ѱ�ע
                    }
                });
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ��������ѡ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRBType_Press(object sender, EventArgs e)
        {
            try
            {
                //�������������б�
                frmRTypeChoose frm = new frmRTypeChoose();
                this.Show(frm, (MobileForm sender1, object args) =>
                {
                    try
                    {
                        if (frm.ShowResult == ShowResult.Yes)
                        {
                            string TYPEIDs = frm.TYPEID;
                            if (TYPEIDs.Length > 0)
                            {
                                string[] types = TYPEIDs.Split(new char[] { '/' });
                                TYPEID = types[0];       //�������ͱ��
                                this.btnRBType.Text = types[1] + "   > ";               //������������
                            }
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
        /// �������Ѽ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtMoney.Text))
                {
                    throw new Exception("���������ѽ�");
                }
                else
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txtMoney.Text.Trim(), @"^(?!0+(?:\.0+)?$)(?:[1-9]\d*|0)(?:\.\d{1,2})?$") == false)
                    {
                        throw new Exception("������Ϊ����0�����֣�");
                    }
                }
                if (string.IsNullOrEmpty(TYPEID))
                {
                    throw new Exception("��ѡ���������");
                }
                if (string.IsNullOrEmpty(this.txtNote.Text))
                {
                    throw new Exception("�����뱸ע��");
                }
                RB_RowsInputDto Row = new RB_RowsInputDto();
                Row.R_Amount = decimal.Parse(this.txtMoney.Text);            //���ѽ��
                Row.R_Note = this.txtNote.Text;                    //��ϸ
                Row.R_TypeID = TYPEID;           //��������
                Row.R_ConsumeDate = this.DatePicker.Value;  //���Ѽ�¼����
                if (string.IsNullOrWhiteSpace(ID) == false)
                {
                    int RID = Convert.ToInt32(ID);       //��IDת����Int����
                    Row.R_ID = Convert.ToInt32(ID);
                    ReturnInfo r = AutofacConfig.rBService.UpdateRB_Rows(Row);
                    if (r.IsSuccess == true)
                    {
                        this.ShowResult = ShowResult.Yes;
                        this.Close();
                        Toast("���Ѽ�¼�޸ĳɹ�");
                    }
                    else
                    {
                        throw new Exception(r.ErrorInfo);
                    }
                }
                else
                {
                    Row.R_CreateUser = Client.Session["U_ID"].ToString();                      //�����û�                     
                    ReturnInfo r = AutofacConfig.rBService.CreateRB_Rows(Row);           //���ݿⴴ�����Ѽ�¼
                    if (r.IsSuccess == true)
                    {
                        this.ShowResult = ShowResult.Yes;
                        this.Close();
                        Toast("���Ѽ�¼�ύ�ɹ���",ToastLength.SHORT);
                    }
                    else
                    {
                        throw new Exception(r.ErrorInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ִ��ɾ�����Ѽ�¼����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Press(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("�Ƿ�ȷ��ɾ�����Ѽ�¼��", MessageBoxButtons.YesNo, (Object s1, MessageBoxHandlerArgs args1) =>
                {
                    if (args1.Result == Smobiler.Core.Controls.ShowResult.Yes)
                    {
                        int RID = Convert.ToInt32(ID);
                        ReturnInfo r = AutofacConfig.rBService.DeleteRB_Rows(RID);
                        if (r.IsSuccess == true)
                        {
                            this.ShowResult = ShowResult.Yes;
                            this.Close();
                            Toast("���ѳɹ�ɾ�����Ѽ�¼");
                        }
                        else
                        {
                            throw new Exception(r.ErrorInfo);
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