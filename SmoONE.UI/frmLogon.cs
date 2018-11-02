using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.CommLib;
using SmoONE.Domain;
using SmoONE.UI.UserInfo;
using SmoONE.DTOs;

namespace SmoONE.UI
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ��¼����
    // ******************************************************************
    partial class frmLogon : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogon_Click(object sender, EventArgs e)
        {
            try
            {
                
                string userID = txtTel.Text.Trim();
                string pwd = txtPwd.Text.Trim();
                if (userID.Length <= 0)
                {
                    throw new Exception("�������ֻ����룡");
                }
                if (pwd.Length <= 0)
                {
                    throw new Exception("���������룡");
                }
                if (pwd.Length < 6 || pwd.Length > 12)
                {
                    throw new Exception("�������Ϊ6-12λ��");
                }
                LoadClientData(MobileServer.ServerID + "user", userID);
                if (chkRememberPwd.Checked == true)
                {
                    //��ס����
                    LoadClientData(MobileServer.ServerID + "pwd", pwd);
                }
                else
                {
                    //ɾ���ͻ�������
                    RemoveClientData(MobileServer.ServerID + "pwd", (object s, ClientDataResultHandlerArgs args) => txtPwd.Text = "");
                }
                //���봦��,��������
                string encryptPwd = AutofacConfig.userService.Encrypt(DateTime.Now.ToString("yyyyMMddHHmmss") + pwd);
                ReturnInfo result = AutofacConfig.userService.Login(userID, encryptPwd);

                if (result.IsSuccess == true)
                {
                    List<Role> role = AutofacConfig.userService.GetRoleByUserID(userID);
                    Client.Session["U_ID"] = userID;
                    Client.Session["Roler"] = role;
                    SmoONE.UI.Work.frmEmpProfile frmWork = new SmoONE.UI.Work.frmEmpProfile();
                    Show (frmWork);
                }
                else
                {
                    throw new Exception(result.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// �˳��ͻ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogon_KeyDown(object sender, KeyDownEventArgs e)
        {
            Client.Exit();
        }

        /// <summary>
        /// ��ת��ע�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            frmRegisterTel frmRegister = new frmRegisterTel();
            Show (frmRegister);
        }
        /// <summary>
        /// ��֤��¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                string userID = txtTel.Text.Trim();
                if (userID.Length <= 0)
                {
                    throw new Exception("�������ֻ����룡");
                }

                frmVerificationCode frmVerificationCode = new frmVerificationCode();
                frmVerificationCode.isVerifyLogon = true;
                frmVerificationCode.Tel = userID;
                Show(frmVerificationCode);
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// ���ݼ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogon_Load(object sender, EventArgs e)
        {
            Smobiler.Plugins.RongIM.IM im = new Smobiler.Plugins.RongIM.IM();
            this.Components.Add(im);
            im.Logout();
            ReadClientData(MobileServer.ServerID + "user", (object s, ClientDataResultHandlerArgs args) =>
            {
                try
                {
                    if (string.IsNullOrEmpty(args.error))
                    {
                        txtTel.Text = args.Value;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            });
            ReadClientData(MobileServer.ServerID + "pwd", (object s, ClientDataResultHandlerArgs args) =>
            {
                if (string.IsNullOrEmpty(args.error))
                {
                    txtPwd.Text = args.Value;
                    if (txtPwd.Text.Length > 0)
                    {
                        chkRememberPwd.Checked = true;
                    }
                }
            });


        }

        /// <summary>
        /// ������֤
        /// </summary>
        private void ScreenGestures()
        {
            this.Client.Pattern.Password = null;
            string userID = txtTel.Text.Trim();
            if (userID.Length <=0) Toast("�������ֻ��ţ�", ToastLength.SHORT);
            UserDetailDto user = AutofacConfig.userService.GetUserByUserID(userID);
            if (user != null  )
            {
                if (string.IsNullOrEmpty(user.U_Gestures) == false)
                { 
                this.Client.Pattern.Password = user.U_Gestures;
                }
            }
            else
            {
                Toast("�û�" + userID + "�����ڣ�", ToastLength.SHORT);
            }

            if (string.IsNullOrEmpty(this.Client.Pattern.Password) == false)
            {
                this.Client.Pattern.VerifyLocal((object s, Smobiler.Core.RPC.PatternLocalVerifiedEventArgs eee) =>
                {
                    if (eee.isError == false)
                    {
                        //ʹ��ģ����֤���¼
                        ReturnInfo result = AutofacConfig.userService.GesturesLogin(userID, Client.Pattern.Password);
                        if (result.IsSuccess == true)
                        {
                            List<Role> role = AutofacConfig.userService.GetRoleByUserID(userID);
                            Client.Session["U_ID"] = userID;
                            Client.Session["Roler"] = role;
                            //��ת���˵�����
                            SmoONE.UI.Work.frmWork frmWork = new SmoONE.UI.Work.frmWork();
                            Show(frmWork);
                        }
                        else
                        {
                            Toast(result.ErrorInfo, ToastLength.SHORT);
                        }
                    }
                });
            }

            else
            {
                Toast("��û�����ƣ������������¼��",ToastLength.SHORT);
            }

        }
        /// <summary>
        /// ���Ƶ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Press(object sender, EventArgs e)
        {
            ScreenGestures();
        }

    }
}