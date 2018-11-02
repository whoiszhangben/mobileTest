using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Application ;
using SmoONE.Domain;
using SmoONE.CommLib;
using SmoONE.DTOs;
namespace SmoONE.UI.Leave
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ����������
    // ******************************************************************
    partial class frmLeaveDetail : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string lID;//��ٱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLeaveDef_Load(object sender, EventArgs e)
        {
            GetLeave();
        }
        /// <summary>
        /// ������ٱ�Ż�ȡ�������
        /// </summary>
        public  void GetLeave()
        {
            try
            {
                LeaveDetailDto leave = AutofacConfig.leaveService.GetByID(lID);
                if (Convert.IsDBNull(leave) == false)
                {
                    UserDetails userDetails = new UserDetails();
                    UserDetailDto userInfo = userDetails.getUser(leave.L_CreateUser);
                    if (userInfo !=null )
                    {
                        this.title1 .TitleText = userInfo.U_Name + "�����";
                        lblUserName.Text = userInfo.U_Name;
                        imgPortrait.ResourceID = userInfo.U_Portrait;
                    }
                    //UserDetailDto userInfo = AutofacConfig.userService.GetUserByUserID(leave.L_CreateUser);
                    //TitleText = userInfo.U_Name + "�����";
                    //lblUserName.Text = userInfo.U_Name;
                    //if (string.IsNullOrEmpty(userInfo.U_Portrait) == true)
                    //{
                    //    switch (userInfo.U_Sex)
                    //    {
                    //        case (int)Sex.��:
                    //            imgPortrait.ResourceID = "boy";
                    //            break;
                    //        case (int)Sex.Ů:
                    //            imgPortrait.ResourceID = "girl";
                    //            break;
                    //    }
                    //}
                    //else
                    //{
                    //    imgPortrait.ResourceID = userInfo.U_Portrait;
                    //}
                    switch (leave.L_Status)
                    {
                        case (int)L_Status.�½�:
                            string[] CheckUsers = leave.L_CheckUsers.Split(',');
                            lblStateNote.Text = "�ȴ�����";
                            bool iscuserCheck = false;//��ǰ�û��Ƿ�������Ȩ��
                            //�����ǰ�û��������û�����ʾ����Ȩ�ޣ�ͬ�⡢�ܾ���ť��������ʾ
                            if (CheckUsers.Contains(Client.Session["U_ID"].ToString()) == true)
                            {
                                frmDetailFootbarLayout1.btnAgreed.Visible = true;
                                frmDetailFootbarLayout1.btnRefuse.Visible = true;
                                iscuserCheck = true;
                            }
                            else
                            {
                                frmDetailFootbarLayout1.btnAgreed.Visible = false;
                                frmDetailFootbarLayout1.btnRefuse.Visible = false;
                                iscuserCheck = false;
                            }
                            //�����ǰ�û���������Ĵ����û�������ʾ�༭��ť��������ʾ��
                            if (Client.Session["U_ID"].ToString().Equals(leave.L_CreateUser))
                            {
                                frmDetailFootbarLayout1.btnEdit.Visible = true;
                                if (iscuserCheck == true)
                                {
                                    frmDetailFootbarLayout1.btnAgreed.Width = 85;
                                    frmDetailFootbarLayout1.btnAgreed.Left = 10;
                                    frmDetailFootbarLayout1.btnRefuse.Width = 85;
                                    frmDetailFootbarLayout1.btnRefuse.Left = 108;
                                    frmDetailFootbarLayout1.btnEdit.Width = 85;
                                    frmDetailFootbarLayout1.btnEdit.Left = 205;
                                }
                                else
                                {
                                    frmDetailFootbarLayout1.btnEdit.Width = 280;
                                    frmDetailFootbarLayout1.btnEdit.Left = 10;
                                }
                            }
                            else
                            {
                                frmDetailFootbarLayout1.btnEdit.Visible = false;
                                if (iscuserCheck == true)
                                {
                                    frmDetailFootbarLayout1.btnAgreed.Width = 134;
                                   frmDetailFootbarLayout1.btnAgreed.Left = 10;
                                   frmDetailFootbarLayout1.btnRefuse.Width = 134;
                                   frmDetailFootbarLayout1.btnRefuse.Left = 156;
                                }
                             
                            }

                            break;
                        case (int)L_Status.������:
                            lblStateNote.Text = "����������ɣ�";
                            ////FooterBarLayoutData.Items["btnAgreed"].Visible = false;
                            ////FooterBarLayoutData.Items["btnRefuse"].Visible = false;
                            ////FooterBarLayoutData.Items["btnEdit"].Visible = false;
                            ////FooterBarLayoutData.Items["Line1"].Visible = false;
                            frmDetailFootbarLayout1.Visible = false;
                            break;
                        case (int)L_Status.�Ѿܾ�:
                            lblStateNote.Text = "���������ܾ���";
                            frmDetailFootbarLayout1.btnAgreed.Visible = false;
                            frmDetailFootbarLayout1.btnRefuse.Visible = false;

                            //����ǵ�ǰ�û���������Ĵ����û�������ʾ�༭��ť��������ʾ��
                            if (Client.Session["U_ID"].ToString().Equals(leave.L_CreateUser))
                            {
                               
                                frmDetailFootbarLayout1.btnEdit.Visible = true;
                                frmDetailFootbarLayout1.btnEdit.Width = 280;
                                frmDetailFootbarLayout1.btnEdit.Left = 10;
                            }
                            else
                            {
                                frmDetailFootbarLayout1.btnEdit.Visible = false ;
                            }
                          
                            break;
                    }
                    lblLId.Text = lID;
                    lblLType.Text = leave.L_TypeName;
                    lblStartDate.Text = leave.L_StartDate.ToString();
                    lblEndDate.Text = leave.L_EndDate.ToString();
                    lblLDay.Text = leave.L_LDay.ToString() + "��";
                    lblReson.Text = leave.L_Reason;
                    if (string.IsNullOrWhiteSpace(leave.L_Img1) == false)
                    {
                        imgL.ResourceID = leave.L_Img1;
                    }
                    //��ȡ����nodeview����
                    getNodeItemDate(leave);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
     /// <summary>
        /// ��ӽڵ�����
        /// </summary>
        /// <param name="leave"></param>
        private void getNodeItemDate(LeaveDetailDto leave)
        {

           //������нڵ�
            nodeStateDate.Items.Clear();
            //��ӽڵ�����
            NodeViewItem nodeItem = new NodeViewItem();
            nodeItem.FontIcon = "circle";
            UserDetails userDetails = new UserDetails();
            UserDetailDto createUser = userDetails.getUser(leave.L_CreateUser);
            if (createUser != null)
            {
                nodeItem.Image = createUser.U_Portrait;
                if (Client.Session["U_ID"].Equals(leave.L_CreateUser))
                {
                    nodeItem.Text = "��";
                }
                else
                {
                    nodeItem.Text = createUser.U_Name;
                }
                nodeItem.SubText = "��������";
                nodeItem.Date = leave.L_CreateDate.ToString ();
                nodeItem.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
                nodeItem.SubTextColor = System.Drawing.Color.FromArgb(236, 163, 56);
                nodeItem.DateColor = System.Drawing.Color.FromArgb(145, 145, 145);
                nodeStateDate.Items.Add(nodeItem);//��̬��ӽڵ�
            }
            switch (leave.L_Status)
            {
                case (int)L_Status.�½�:
                    if (string.IsNullOrEmpty(leave.L_CheckUsers) == false)
                    {
                        string[] CheckUsers = leave.L_CheckUsers.Split(',');
                        foreach (string cUser in CheckUsers)
                        {
                            NodeViewItem nodeItem2 = new NodeViewItem();
                            nodeItem2.FontIcon = "circle";

                            UserDetailDto checkUser = userDetails.getUser(cUser);
                            if (checkUser != null)
                              {
                                  nodeItem2.Image = checkUser.U_Portrait;
                                  if (Client.Session["U_ID"].Equals(cUser))
                                  {
                                      nodeItem2.Text = "��";
                                  }
                                  else
                                  {
                                      nodeItem2.Text = checkUser.U_Name;
                                  }
                                  nodeItem2.SubText = "������";
                                  nodeItem2.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
                                  nodeItem2.SubTextColor = System.Drawing.Color.FromArgb(236, 163, 56);
                                  nodeStateDate.Items.Add(nodeItem2);
                              }
                        }
                    }
                    break;
                case (int)L_Status.������:
                    NodeViewItem nodeItem3 = new NodeViewItem();
                    nodeItem3.FontIcon = "circle";
                    UserDetailDto checkUser1 = userDetails.getUser(leave.L_CurrantCheck);
                    if (checkUser1 != null)
                    {
                            nodeItem3.Image = checkUser1.U_Portrait;
                            if (Client.Session["U_ID"].Equals(leave.L_CurrantCheck))
                            {
                                nodeItem3.Text = "��";
                            }
                            else
                            {
                                nodeItem3.Text = checkUser1.U_Name;
                            }
                            nodeItem3.SubText = "��ͬ��";
                            nodeItem3.Date = leave.L_UpdateDate.ToString ();
                            nodeItem3.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
                            nodeItem3.SubTextColor = System.Drawing.Color.FromArgb(143, 187, 78);
                            nodeItem3.DateColor = System.Drawing.Color.FromArgb(145, 145, 145);
                            nodeStateDate.Items.Add(nodeItem3);
                        }
                     
                  
                    break;
                case (int)L_Status.�Ѿܾ�:
                    NodeViewItem nodeItem4 = new NodeViewItem();
                    nodeItem4.FontIcon = "circle";
                    UserDetailDto checkUser2 = userDetails.getUser(leave.L_CurrantCheck);
                    if (checkUser2 != null)
                    {
                        nodeItem4.Image = checkUser2.U_Portrait;
                        if (Client.Session["U_ID"].Equals(leave.L_CurrantCheck))
                        {
                            nodeItem4.Text = "��";
                        }
                        else
                        {
                            nodeItem4.Text = checkUser2.U_Name;
                        }
                        nodeItem4.SubText = "�Ѿܾ�";
                        nodeItem4.Date = leave.L_UpdateDate.ToString ();
                        nodeItem4.TextColor = System.Drawing.Color.FromArgb(45, 45, 45);
                        nodeItem4.SubTextColor = System.Drawing.Color.FromArgb(244, 64, 69);
                        nodeItem4.DateColor = System.Drawing.Color.FromArgb(145, 145, 145);
                        nodeStateDate.Items.Add(nodeItem4);
                     }
                    break;
            }
        }
       

        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLeaveDef_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmLeaveDef_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
      
        
   
    }
}