using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.UI;
using SmoONE.DTOs;
using SmoONE.CommLib;

namespace SmoONE.UI.RB
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ����ģ�崴����༭����
    // ******************************************************************
    partial class frmRTypeTempCreate : Smobiler.Core.Controls.MobileForm
    {
        #region "Properties"
        internal string ID;                 //����ģ����
        private string TYPEID;              //�������ͱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��������ѡ��ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRBType_Press(object sender, EventArgs e)
        {
            try
            {
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
                                TYPEID = types[0];               //�������ͱ��
                                this.btnRBType.Text = types[1] + "   > ";  //������������
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
        /// ����ģ�崴�����߱���
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
                RBRTTInputDto RBModel = new RBRTTInputDto();                    //����һ��������
                RBModel.RB_RTT_Amount = Convert.ToDecimal(this.txtMoney.Text);             //���ѽ��
                RBModel.RB_RTT_TypeID = TYPEID;                             //�������
                RBModel.RB_RTT_Note = this.txtNote.Text;                       //���ѱ�ע
                if (string.IsNullOrWhiteSpace(ID) == false)
                {
                    RBModel.RB_RTT_TemplateID = ID;
                    ReturnInfo r = AutofacConfig.rBService.UpdateRB_Type_Template(RBModel);           //��������ģ����Ϣ
                    if (r.IsSuccess == true)
                    {
                        this.ShowResult = ShowResult.Yes;
                        this.Close();
                        Toast("����ģ���޸ĳɹ�");
                    }
                    else
                    {
                        throw new Exception(r.ErrorInfo);
                    }
                }
                else
                {
                    RBModel.RB_RTT_CreateUser = Client.Session["U_ID"].ToString();                           //ģ�崴���û�
                    ReturnInfo r = AutofacConfig.rBService.CreateRB_Type_Template(RBModel);
                    if (r.IsSuccess == true)
                    {
                        this.ShowResult = ShowResult.Yes;
                        this.Close();
                        Toast("����ģ���ύ�ɹ���");
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
        /// ����ģ��ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Press(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("�Ƿ�ȷ��ɾ������ģ�壿", MessageBoxButtons.YesNo, (Object s1, MessageBoxHandlerArgs args1) =>
                {
                    if (args1.Result == Smobiler.Core.Controls.ShowResult.Yes)
                    {
                        ReturnInfo r = AutofacConfig.rBService.DeleteRB_Type_Template(ID);
                        if (r.IsSuccess == true)
                        {
                            this.ShowResult = ShowResult.Yes;
                            this.Close();
                            Toast("ɾ������ģ��ɹ�");
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
        /// <summary>
        /// �ֻ��Դ����ؼ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRTypeTempCreate_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmRTypeTempCreate_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ID) == false)
                {
                    RB_RType_TemplateDto RBRTypeTem = AutofacConfig.rBService.GetTemplateByTemplateID(ID);
                    string RBType = AutofacConfig.rBService.GetTypeNameByID(RBRTypeTem.RB_RTT_TypeID);
                    this.txtMoney.Text = RBRTypeTem.RB_RTT_Amount.ToString();
                    TYPEID = RBRTypeTem.RB_RTT_TypeID;       //����ģ����
                    this.btnRBType.Text = RBType;
                    this.txtNote.Text = RBRTypeTem.RB_RTT_Note;
                    title.TitleText = "����ģ��";
                }
                else
                {
                    title.TitleText = "����ģ�崴��";
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
    }
}