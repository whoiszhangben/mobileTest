using System;
using Smobiler.Core.Controls;
using SmoONE.UI.UserInfo;

namespace SmoONE.UI.UserInfo
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ע�����
    // ******************************************************************
    partial class frmRegisterTel : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRegisterTel_KeyDown(object sender, KeyDownEventArgs e)
        {
            if(e.KeyCode==KeyCode.Back)
            {
                Close();
            }
        }
        /// <summary>
        /// ��һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (txtTel.Text.Trim().Length <= 0)
                {
                    throw new Exception("������绰���룡");

                }
                else
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^1(3[0-9]|4[57]|5[0-35-9]|7[0135678]|8[0-9])\d{8}$");
                    if (regex.IsMatch(txtTel.Text.Trim()) == false)
                    {
                        throw new Exception("�ֻ������ʽ����ȷ��");
                    }
                }
                //��֤�绰�����Ƿ��Ѿ�ע�ᣬ������ֵΪtrueʱ����ʾ��ע��
                bool isRegister = AutofacConfig.userService.IsExists(txtTel.Text.Trim());
                if (isRegister == true)
                {
                    throw new Exception("�绰����" + txtTel.Text.Trim() + "��ע�ᣡ");
                }
                //�ж��豸�Ƿ����ע�ᣬ������ֵΪtrueʱ����ʾ�Ѷ���ע��
                bool isMalicious = AutofacConfig.userService.IsMalicious(Client.DeviceID);
                if (isMalicious == true)
                {
                    throw new Exception("���ܶ���ע�ᣡ");
                }
                frmVerificationCode frmVerificationCode = new frmVerificationCode();
                frmVerificationCode.Tel = txtTel.Text.Trim();
               // frmVerificationCode.isVerifyLogon = true;
                Show(frmVerificationCode);
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
    }
}