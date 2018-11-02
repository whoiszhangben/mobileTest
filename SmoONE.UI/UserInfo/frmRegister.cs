using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.UI.Work;
using SmoONE.Domain;
using SmoONE.CommLib;

namespace SmoONE.UI.UserInfo
{
    /// ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ���õ�¼��Ϣ����
    // ******************************************************************
    partial class frmRegister : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string Tel;//�绰����
        public string VCode;//��֤��
        bool isPwdC1 = true; //�������Ƿ���ʾ�����ַ�����
        bool isPwdC2 = true;//ȷ�������Ƿ���ʾ�����ַ�����
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �������Ƿ���ʾ�����ַ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpPwd1_Press(object sender, EventArgs e)
        {
            if (isPwdC1 == false)
            {
                txtPwd1.SecurityMode = true;//����textboxΪ�����ַ�
                fontPwd1.ResourceID = "eye-slash";
                isPwdC1 = true;
            }
            else
            {
                txtPwd1.SecurityMode = false;//textbox�����ַ�Ϊ��ʱ����ʾ����
                fontPwd1.ResourceID = "eye";
                isPwdC1 = false;
            }
        }
        /// <summary>
        /// ȷ�������Ƿ���ʾ�����ַ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpPwd2_Press(object sender, EventArgs e)
        {
            if (isPwdC2 == false)
            {
                txtPwd2.SecurityMode = true;//����textboxΪ�����ַ�
                fontPwd2.ResourceID = "eye-slash";
                isPwdC2 = true;
            }
            else
            {
                txtPwd2.SecurityMode = false;//textbox�����ַ�Ϊ��ʱ����ʾ����
                fontPwd2.ResourceID = "eye";
                isPwdC2 = false;
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRegister_Load(object sender, EventArgs e)
        {
            GetRole();
        }
        /// <summary>
        /// ��ȡ���н�ɫ
        /// </summary>
        private void GetRole()
        {
            List<Role> listRole = AutofacConfig.userService.GetAllRoles();
            if (listRole.Count > 0)
            {
                foreach (Role role in listRole)
                {
                    RadioButton rb = new RadioButton();
                    rb.Value = role.R_RoleID;
                    rb.Text = role.R_Name;
                    radioGroup1.Buttons.Add(rb);
                }
                radioGroup1.Buttons[0].Checked = true;
            }
        }
        /// <summary>
        /// ע��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Trim().Length <= 0)
                {
                    throw new Exception("�������ǳƣ�");

                }
                if (VCode == null)
                {
                    throw new Exception("�������ֻ���֤�룡");

                }
                string pwd1 = txtPwd1.Text.Trim();
                string pwd2 = txtPwd2.Text.Trim();
                if (pwd1.Length <= 0)
                {
                    throw new Exception("�����������룡");
                }
                if (pwd1.Length < 6 || pwd1.Length > 12)
                {
                    throw new Exception("���������Ϊ6-12λ��");
                }

                if (pwd2.Length <= 0)
                {
                    throw new Exception("������ȷ�����룡");
                }
                if (pwd2.Length < 6 || pwd2.Length > 12)
                {
                    throw new Exception("���������Ϊ6-12λ��");
                }
                if (!pwd1.Equals(pwd2))
                {
                    throw new Exception("�����������벻һ�£����飡");
                }

                RadioButton selectRole = radioGroup1.CheckedButton;
                List<string> listrole = new List<string>();
                listrole.Add(selectRole.Value);

                //���봦��,��������
                string encryptPwd = AutofacConfig.userService.Encrypt(pwd2);
                ReturnInfo result = AutofacConfig.userService.RegisterByVCode(Tel, encryptPwd, txtName.Text.Trim(), listrole, VCode);
                //�������true��ע��ɹ������򵯳�����
                if (result.IsSuccess == false )
                {
                    throw new Exception(result.ErrorInfo);
                }
                else
                {
                    List<Role> role = AutofacConfig.userService.GetRoleByUserID(Tel);
                    Client.Session["U_ID"] = Tel;
                    Client.Session["Roler"] = role;
                    Close();//�رյ�ǰ����
                    frmWork frmWork = new frmWork();//��ת����������
                    Show(frmWork);
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
    }
}