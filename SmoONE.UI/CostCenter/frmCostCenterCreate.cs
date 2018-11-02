using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Domain;
using SmoONE.CommLib;
using SmoONE.DTOs;

namespace SmoONE.UI.CostCenter
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  �ɱ����Ĵ����༭����
    // ******************************************************************
    partial class frmCostCenterCreate : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string ATNO;//����ģ����
        public string CCID ;//�ɱ����ı��
        string type = "";//����
        string CTempID = "";//����ģ����
        string liableMan = "";//������
        string D_ID = "";//���ű��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btntype_Click(object sender, EventArgs e)
        {
            popType.Groups.Clear();
            PopListGroup poli = new PopListGroup();
            popType.Groups.Add(poli);
            poli.Title = "����ѡ��";
            //��ȡ�ɱ��������ͣ�����ֵpoplist����
            List<CostCenter_Type>  listCCType=  AutofacConfig.costCenterService.GetAllCCType();
            foreach (CostCenter_Type ccType in listCCType)
            {
                poli.AddListItem(ccType.CC_T_Description, ccType.CC_T_TypeID);
                if (type.Trim().Length > 0)
                {
                    if (type.Trim().Equals(ccType.CC_T_TypeID))
                    {
                        popType.SetSelections(poli.Items[(poli.Items.Count - 1)]);
                    }
                }
            }
            popType.ShowDialog();
        }
        /// <summary>
        /// ���͸�ֵ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popType_Selected(object sender, EventArgs e)
        {
            if (popType.Selection != null)
            {
                type = popType.Selection.Value;
                btnType.Text = popType.Selection.Text + "   > ";
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCC_Name.Text.Trim().Length <= 0)
                {
                    throw new Exception("������ɱ��������ƣ�");
                }
                if (string.IsNullOrEmpty(type)==true)
                {
                    throw new Exception("���������ͣ�");
                }
                if (txtAmount.Text.Trim().Length <= 0)
                {
                    throw new Exception("���������0��");
                }
                else
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txtAmount.Text.Trim(), @"^(?!0+(?:\.0+)?$)(?:[1-9]\d*|0)(?:\.\d{1,2})?$") == false)
                    {
                        throw new Exception("������Ϊ����0�����֣�");
                    }
                   
                }
                if (string.IsNullOrWhiteSpace(liableMan) == true)
                {
                    throw new Exception("�����������ˣ�");
                }
                if (string.IsNullOrWhiteSpace(CTempID) == true)
                {
                    throw new Exception("������ɱ���������ģ�壡");
                }
                if (string.IsNullOrEmpty(D_ID) == true)
                {
                    throw new Exception("��������δ���䲿�ţ���ȥ���䲿�ţ�");
                }
                //���³ɱ��������ݸ�ֵ
                CCInputDto cc = new CCInputDto();
                cc.CC_Name = txtCC_Name.Text.Trim();
                cc.CC_TypeID = type;
                cc.CC_StartDate = dpkStartDate.Value;
                cc.CC_EndDate = dpkEndDate.Value;
                cc.CC_Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                cc.CC_LiableMan = liableMan;
                cc.CC_DepartmentID = D_ID;
                cc.CC_TemplateID = CTempID;
                ReturnInfo result;
                if (string.IsNullOrEmpty(CCID) == false )
                {
                    cc.CC_ID = CCID;
                    cc.CC_UpdateUser = Client.Session["U_ID"].ToString();
                    //���³ɱ�����
                    result = AutofacConfig.costCenterService.UpdateCostCenter(cc);
                }
                else
                {
                    cc.CC_CreateUser = Client.Session["U_ID"].ToString();
                    //�����ɱ�����
                    result = AutofacConfig.costCenterService.AddCostCenter(cc);
                }
                //�������true���򴴽�����³ɱ����ĳɹ�������ʧ�ܲ��׳�����
                if (result.IsSuccess == false)
                {
                    throw new Exception(result.ErrorInfo);
                }
                else
                {
                    ShowResult = ShowResult.Yes;
                    //if (string.IsNullOrEmpty(CCID) == true)
                    //{
                        Close();
                    //}
                    Toast("�ɱ������ύ�ɹ���", ToastLength.SHORT);
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostCenterCreate_Load(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void Bind()
        {
            try
            {
                //�ɱ����ı�Ų�Ϊ��ʱ���ɱ���������
                if (CCID != null)
                {
                    //���ݳɱ����ı�Ż�ȡ�ɱ�������Ϣ
                    CCDetailDto cc = AutofacConfig.costCenterService.GetCCByID(CCID);
                    txtCC_Name.Text = cc.CC_Name;
                    type = cc.CC_TypeID;
                    btnType.Text = cc.CC_TypeName;
                    dpkStartDate.Value = cc.CC_StartDate;
                    dpkEndDate.Value = cc.CC_EndDate;
                    txtAmount.Text = cc.CC_Amount.ToString();
                    liableMan = cc.CC_LiableMan;
                    UserDetailDto user = AutofacConfig.userService.GetUserByUserID(cc.CC_LiableMan);
                    btnLiableMan.Text = user.U_Name;
                    D_ID = cc.CC_DepartmentID;
                    lblDep.Text = cc.CC_DepName;
                    CTempID = cc.CC_TemplateID;
                    btnTemplate.Text = cc.CC_TemplateID;
                }
                else
                {
                    DateTime t = DateTime.Now;
                    dpkEndDate.Value = t.AddYears(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ��ת��ģ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTemplate_Click(object sender, EventArgs e)
        {
            //��ת��ģ�����
            frmCostTemplet frm = new frmCostTemplet();
            frm.IsSelectCTemPlet = true;
            Show(frm, (MobileForm form, object args) =>
               {
                   if (frm.ShowResult == Smobiler.Core.Controls .ShowResult.Yes)
                   {
                       CTempID = frm.CTempID;
                       btnTemplate.Text = frm.CTempID + "   > ";
                   }
               });
        }
        /// <summary>
        /// ѡ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLiableMan_Click(object sender, EventArgs e)
        {
            popLiable.Groups.Clear();
            PopListGroup poli = new PopListGroup();
            popLiable.Groups.Add(poli);
            poli.Title = "������ѡ��";
            //��ȡ���������ݣ�����ֵpoplist����
            List<UserDto> listuser = AutofacConfig.userService.GetAllUsers();
            foreach (UserDto user in listuser)
            {
                poli.AddListItem(user.U_Name, user.U_ID);
                if (liableMan.Trim().Length > 0)
                {
                    if (liableMan.Trim().Equals(user.U_ID))
                    {
                        popLiable.SetSelections(poli.Items[(poli.Items.Count - 1)]);
                    }
                }
            }
            popLiable.Show();
        }

        /// <summary>
        /// �ؼ���ֵ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popLiable_Selected(object sender, EventArgs e)
        {
            if (popLiable.Selection != null)
            {
                liableMan = popLiable.Selection.Value;
                btnLiableMan.Text = popLiable.Selection.Text + "   > ";
               UserDepDto user= AutofacConfig.userService.GetUseDepByUserID(liableMan);
               D_ID = user.Dep_ID;
               lblDep.Text =user.Dep_Name;
                
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCostCenterCreate_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmCostCenterCreate_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}