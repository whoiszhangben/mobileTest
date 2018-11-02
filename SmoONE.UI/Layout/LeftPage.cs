using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;

namespace SmoONE.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class LeftPage : Smobiler.Core.Controls.MobileUserControl
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
       
        #endregion

        /// <summary>
        /// ��ϸ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plPerson_Press(object sender, EventArgs e)
        {
            SmoONE.UI.UserInfo. frmUserDetail frmUserDetail = new SmoONE.UI.UserInfo.frmUserDetail();
            frmUserDetail.U_ID = Client.Session["U_ID"].ToString();
            this.Form .Show(frmUserDetail);
        }

        private void plExit_Press(object sender, EventArgs e)
        {
            //this.Client.Exit();
            //this.Form .Close();
       
            this.Client.ReStart();
            
        }
        /// <summary>
        /// ���ݼ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftPage_Load(object sender, EventArgs e)
        {
            getUser();
        }

        public  void getUser()
        {
            try
            {
                DTOs.UserDetailDto user = AutofacConfig.userService.GetUserByUserID(Client.Session["U_ID"].ToString());
                if (user != null)
                {
                    lblPhone.Text = Client.Session["U_ID"].ToString();
                    lblName.Text = user.U_Name;
                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                    {
                        switch (user.U_Sex)
                        {
                            case (int)Sex.��:
                                user.U_Portrait = "boy";
                                break;
                            case (int)Sex.Ů:
                                user.U_Portrait = "girl";
                                break;
                        }
                    }
                    imgPortrait.ResourceID = user.U_Portrait;
                }
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message, ToastLength.SHORT);
            }
            
        }
    }
}