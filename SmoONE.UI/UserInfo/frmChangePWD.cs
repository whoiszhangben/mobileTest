using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.CommLib;
using SmoONE.UI;

namespace SmoONE.UI.UserInfo
{
    /// ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  �����޸Ľ���
    // ******************************************************************
    partial class frmChangePWD : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string oldPwd;//�޸�����
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
        /// �����޸�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                string pwd1 = txtPwd1.Text.Trim();
                string pwd2 = txtPwd2.Text.Trim();
                if (string.IsNullOrEmpty(oldPwd) == true )
                {
                    throw new Exception("������ԭ���룡");
                }
                if (pwd1.Length <= 0)
                {
                    throw new Exception("�����������룡");
                }

                if (pwd2.Length <= 0)
                {
                    throw new Exception("������ȷ�����룡");
                }
                if (!pwd1.Equals(pwd2))
                {
                    throw new Exception("�����������벻һ�£����飡");
                }
                if (pwd1.Length < 6 || pwd1.Length > 12)
                {
                    throw new Exception("���������Ϊ6-12λ��");
                }
                if (pwd2.Length < 6 || pwd2.Length > 12)
                {
                    throw new Exception("ȷ���������Ϊ6-12λ��");
                }
               
                if (oldPwd != null)
                {
                    //�����봦��,��������
                    string encryptPwd = AutofacConfig.userService.Encrypt(pwd2);
                    if (oldPwd.Equals(encryptPwd))
                    {
                        throw new Exception("�������������ԭ��������һ�£����������룡");
                    }
                    //��������
                    ReturnInfo result = AutofacConfig.userService.ChangePwd(Client.Session["U_ID"].ToString(), oldPwd, encryptPwd);
                    //�������true���޸ĳɹ������򵯳�����
                    if (result.IsSuccess == true)
                    {
                        Close();
                        Toast("�����޸ĳɹ���", ToastLength.SHORT);
                    }
                    else
                    {
                        throw new Exception(result.ErrorInfo);
                    }
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
        private void frmChangePWD_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }
    }
}