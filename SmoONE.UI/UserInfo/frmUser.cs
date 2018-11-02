using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.Data;
using SmoONE.DTOs;
using SmoONE.UI;
using SmoONE.CommLib;
using SmoONE.UI.Layout;

namespace SmoONE.UI.UserInfo
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ��ǰ�û��������
    // ******************************************************************
    partial class frmUser : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        internal string toolbarItemName = "";
        private int eInfo;
        private Sex sex;//�Ա�
        private DateTime toasttime;//toastʱ��
        public EditUserInfoLayout EditUserInfo = new EditUserInfoLayout();   //��Ϣ�༭������
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ϴ�ͷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Press(object sender, EventArgs e)
        {
            cameraPortrait.GetPhoto();
        }
        /// <summary>
        /// �޸��ǳ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnName_Press(object sender, EventArgs e)
        {
            eInfo = (int)EuserInfo.�޸��ǳ�;
            ShowlayoutDialog();
        }
        /// <summary>
        /// ѡ���Ա�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSex_Press(object sender, EventArgs e)
        {
            popSex.Groups.Clear();
            PopListGroup poli = new PopListGroup();
            popSex.Groups.Add(poli);
            poli.Title = "�Ա�ѡ��";
            UserSex UserSex = new UserSex();
            DataTable table = UserSex.GetSex();
            foreach (DataRow row in table.Rows)
            {
                poli.Items.Add(new PopListItem(row["SexName"].ToString(), row["SexID"].ToString()));
                if (((int)sex).Equals(row["SexID"]))
                {
                    popSex.SetSelections(poli.Items[(poli.Items.Count - 1)]);
                }
            }
            popSex.ShowDialog();
        }
        /// <summary>
        /// �޸ĳ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dpkBirthday_DateChanged(object sender, EventArgs e)
        {
            UserDetailDto user = AutofacConfig.userService.GetUserByUserID(Client.Session["U_ID"].ToString());
            if (user != null)
            {
                UserInputDto upuser = new UserInputDto();
                upuser.U_ID = Client.Session["U_ID"].ToString();
                upuser.U_Birthday = dpkBirthday.Value;
                ReturnInfo result = AutofacConfig.userService.UpdateUser(upuser);
                //���ؽ�����Ϊfalse�����޸�ʧ��
                if (result.IsSuccess == false)
                {
                    dpkBirthday.Value = (DateTime)upuser.U_Birthday;
                    Toast(result.ErrorInfo, ToastLength.SHORT);
                }
            }
        }
        /// <summary>
        /// �޸�Email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmail_Press(object sender, EventArgs e)
        {
            eInfo = (int)EuserInfo.�޸��ʼ�;
            ShowlayoutDialog();
        }
        /// <summary>
        /// �޸ĵ�¼����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPwd_Press(object sender, EventArgs e)
        {
            eInfo = (int)EuserInfo.�޸ĵ�¼����;
            ShowlayoutDialog();
        }
        /// <summary>
        /// ��ʾlayoutDialog
        /// </summary>
        private void ShowlayoutDialog()
        {
            EditUserInfo.eInfo = eInfo;

            string editLbltxt;
            if (eInfo == (int)EuserInfo.�޸ĵ�¼����)
            {
                editLbltxt = "�޸�����ǰ����дԭ����";
            }
            else
            {
                editLbltxt = ((EuserInfo)Enum.ToObject(typeof(EuserInfo), eInfo)).ToString();
            }
            EditUserInfo. lblEditInfo.Text = editLbltxt;
            switch (eInfo)
            {
                case (int)EuserInfo.�޸��ǳ�:
                    if (((frmUser)(this.Form)).btnName.Text.Trim().Length > 0)
                    {
                        EditUserInfo. txtEditInfo.Text = btnName.Text.Trim();

                    }
                    else
                    {
                        EditUserInfo. txtEditInfo.Text = "";
                    }
                    break;
                case (int)EuserInfo.�޸��ʼ�:
                    if (((frmUser)(this.Form)).btnEmail.Text.Trim().Length > 0)
                    {
                        EditUserInfo. txtEditInfo.Text = btnEmail.Text.Trim();
                    }
                    else
                    {
                        EditUserInfo. txtEditInfo.Text = "";
                    }
                    break;
                case (int)EuserInfo.�޸ĵ�¼����:
                    EditUserInfo. txtEditInfo.Text = "";
                    break;

            }
            ShowDialog(EditUserInfo);
        }
        /// <summary>
        /// �˳���¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Press(object sender, EventArgs e)
        {
            MessageBox.Show("�Ƿ��˳���ǰϵͳ��", MessageBoxButtons.YesNo, (object o, MessageBoxHandlerArgs args) =>
            {
                try
                {
                    if (args.Result == ShowResult.Yes)
                    {
                        this.Close();
                        //frmLogon frmLogon = new frmLogon();
                        //Show(frmLogon);
                        ////�˳��ͻ���
                        //this.Client.Exit();
                    }
                }
                catch (Exception ex)
                {
                    Toast(ex.Message, ToastLength.SHORT);
                }
            });
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUser_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUser_Load(object sender, EventArgs e)
        {
            GetUser();
        }
        /// <summary>
        /// ��ȡ�û���Ϣ
        /// </summary>
        public void GetUser()
        {
            try
            {
                UserDetails userDetails = new UserDetails();
                UserDetailDto user = userDetails.getUser(Client.Session["U_ID"].ToString());       

                if (user != null)
                {
                   // imgPortrait.ResourceID = user.U_Portrait;
                    cameraButton1.ResourceID = user.U_Portrait;
                    btnName.Text = user.U_Name;
                    sex = (Sex)user.U_Sex;
                    switch (sex)
                    {
                        case Sex.��:
                            btnSex.Text = "��";
                            break;
                        case Sex.Ů:
                            btnSex.Text = "Ů";
                            break;
                    }
                    dpkBirthday.Value = user.U_Birthday;
                    btnEmail.Text = user.U_Email;
                }
                else
                {
                    throw new Exception("�û�" + Client.Session["U_ID"].ToString() + "�����ڣ����飡");
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// Toolbar����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBar_ToolbarItemClick(object sender, ToolbarClickEventArgs e)
        {
            if (!e.Name.Equals("Me"))
            {
                toolbarItemName = e.Name;
                Close();
            }
        }
        /// <summary>
        /// �޸��û�ͷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cameraPortrait_ImageCaptured(object sender, BinaryResultArgs e)
        {
            if (e.isError == false)
            {
                UserInputDto upuser = new UserInputDto();
                upuser.U_ID = Client.Session["U_ID"].ToString();
                upuser.U_Portrait = e.ResourceID;
                ReturnInfo result = AutofacConfig.userService.UpdateUser(upuser);
                if (result.IsSuccess == false)
                {
                    Toast(result.ErrorInfo, ToastLength.SHORT);
                }
                else
                {
                    e.SaveFile(e.ResourceID);
                    
                    //imgPortrait.ResourceID = e.ResourceID;
                   
                    //imgPortrait.Refresh();
                    cameraButton1.ResourceID= e.ResourceID;
                   
                }
            }
        }
        /// <summary>
        /// �޸��Ա�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popSex_Selected(object sender, EventArgs e)
        {
            if (popSex.Selection != null)
            {
                UserInputDto upuser = new UserInputDto();
                upuser.U_ID = Client.Session["U_ID"].ToString();
                upuser.U_Sex = (Sex)Convert.ToInt32(popSex.Selection.Value);
                ReturnInfo result = AutofacConfig.userService.UpdateUser(upuser);
                if (result.IsSuccess == true)
                {
                    sex = (Sex)Convert.ToInt32(popSex.Selection.Value);
                    //btnSex.Text = popSex.Selection.Text;
                    GetUser();
                }
                else
                {
                    Toast(result.ErrorInfo, ToastLength.SHORT);
                }
            }
        }
    }
}