using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using SmoONE.UI;

namespace SmoONE.UI.UserInfo
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  �û��������
    // ******************************************************************
    partial class frmUserDetail : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        internal string U_ID;//�û����
        private Sex sex;//�Ա�
        private string email = "";//�ʼ�
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUserDetail_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUserDetail_Load(object sender, EventArgs e)
        {
            GetUser();
        }
        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        private void GetUser()
        {
            try
            {
                if (string.IsNullOrEmpty(U_ID) == true)
                {
                    throw new Exception("������绰���룡");
                }
                UserDetails userDetails = new UserDetails();
                UserDetailDto user = userDetails.getUser(U_ID);
                if (user != null)
                {
                    imgPortrait.ResourceID = user.U_Portrait;
                    string name = user.U_Name;
                    sex = (Sex)user.U_Sex;
                    switch (sex)
                    {
                        case Sex.��:
                            lblName.Text = name + "  ��";
                            break;
                        case Sex.Ů:
                            lblName.Text = name + "  Ů";
                            break;

                    }
                    lblTelShow .Text = U_ID;
                    lblBirShow.Text = user.U_Birthday.ToString("yyyy/MM/dd");
                    email = user.U_Email;
                    lblEmailShow .Text = user.U_Email;
                    phoneButton1.PhoneNumber = U_ID;
                }
                else
                {
                    throw new Exception("�û�" + U_ID + "�����ڣ����飡");
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ��绰
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpTel_Press(object sender, EventArgs e)
        {
            Client.TelCall(U_ID);
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpMes_Press(object sender, EventArgs e)
        {
            Client.SendSMS("", U_ID);
        }
        /// <summary>
        /// ���ʼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpEmail_Press(object sender, EventArgs e)
        {
            Client.SendEmail("", "", email);
        }
        /// <summary>
        /// ��绰
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void phoneButton1_Press(object sender, EventArgs e)
        {
           
        }
    }
}