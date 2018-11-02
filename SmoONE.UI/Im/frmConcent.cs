using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using Smobiler.Plugins.RongIM;
using SmoONE.DTOs;
using SmoONE.UI.Layout;

namespace SmoONE.UI.Im
{
    partial class frmConcent : Smobiler.Core.Controls.MobileForm
    {

        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        /// <summary>
        /// appKey��appSecret��ȥ����ע��Ӧ�ã�http://www.rongcloud.cn/
        /// </summary>
        string key = "";//����Ӧ��KEY
        string secret = "";//����Ӧ��Secret

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void InitialIM()
        {
            im1.InitialInstance(key, secret);//��ʼ��
            if (System.String.IsNullOrEmpty(im1.Token) == true)
            {
                string tokenuser = Client.Session["U_ID"].ToString();
                if (System.String.IsNullOrEmpty(tokenuser) == false)
                {
                    //�������û���ʼ������������tokeʧЧ����ȥ�����в鿴token�Ƿ�ʧЧ
                    UserDetails userDetails = new UserDetails();
                    UserDetailDto userDetailDto = userDetails.getUser(tokenuser);
                    string portraitUri = "";
                    if (userDetailDto != null)
                    {
                        portraitUri =System.IO.Path.Combine( MobileResourceManager.DefaultImagePath , userDetailDto.U_Portrait);
                    }
                    im1.InitialToken(tokenuser, tokenuser, portraitUri);
                }
                else
                {
                    MessageBox.Show("TokenUser Not Be NullOrEmpty!");
                }
            }
            else
            {
                im1.InitialToken();
            }
        }


        private void segmentedControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabPageView1 .PageIndex =segmentedControl1.SelectedIndex;         
            Bind();
        }
        
         
        private void frmConcent_Load(object sender, EventArgs e)
        {
            InitialIM();
            tabPageView1.Controls.Add(listContact);
            tabPageView1.Controls.Add(listGroup);        
            Bind();
        }

        public void Bind()
        {
            try
            {              
                switch (segmentedControl1.SelectedIndex)
                {
                    case 0:
                        lblAdd.Text = "�����ϵ��";
                        //��ȡ��ǰ�û���ϵ��
                        List<ContactDto> listContactDto = AutofacConfig.contactService .GetByCreateUsers(Client.Session["U_ID"].ToString());
                        listContact.Rows.Clear();
                        if (listContactDto.Count >0)
                        {
                            List<UserDetailDto> listuser = new List<UserDetailDto>();
                            foreach (ContactDto contact in listContactDto)
                            {
                                UserDetails userDetails = new UserDetails();
                                UserDetailDto user = userDetails.getUser(contact.C_USER);
                                if (user != null)
                                {
                                    listuser.Add(user);
                                }
                            }
                            listContact.DataSource = listuser;
                            listContact.DataBind();
                        }
                        break;
                    case 1:
                        lblAdd.Text = "����Ⱥ��";
                        //��ȡ��ǰ�û�Ⱥ��
                        List<CGroupDto> listCGroupDto = AutofacConfig.cGroupService.GetByCreateUsers(Client.Session["U_ID"].ToString());
                        listGroup.Rows.Clear();
                        if (listCGroupDto.Count > 0)
                        {
                            listGroup.DataSource = listCGroupDto;
                            listGroup.DataBind();
                        }
                        break;
                }
            }
            catch(Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        public void getchatMsg()
        {
  
           im1.StartConversationList();
        }
      
       
        private void im1_LoadGroupInfo(object sender, IMLoadGroupInfoArgs e)
        {
            IMGroupEntry group = new IMGroupEntry();
            CGroupDto CGroupDto = AutofacConfig.cGroupService.GetByID(e.GroupId);
            group.GroupID = e.GroupId;
            if (CGroupDto != null)
            {

                group.GroupName = CGroupDto.G_NAME;

            }
            group.PortraitUri = MobileResourceManager .DefaultImagePath + "group.PNG";
            e.Result = group;
        }
        private void  im1_LoadUserInfo(object sender, IMLoadUserInfoArgs e)
        {
            IMUserEntry user = new IMUserEntry();
            UserDetails userDetails = new UserDetails();
            UserDetailDto userDetailDto = userDetails.getUser(e.UserId);
            user.UserID = e.UserId;
            if (userDetailDto != null)
            {                
                user.UserName = userDetailDto.U_Name;
                // user.PortraitUri = ".\\Image\\" + userDetailDto.U_Portrait + ".PNG";
                user.PortraitUri = System.IO.Path.Combine(MobileResourceManager.DefaultImagePath, userDetailDto.U_Portrait);
            }
            //����IMUserEntry
            e.Result = user;
        }

        private void  im1_LoadGroupMembers(object sender, IMLoadGroupMembersArgs e)
        {
            CGroupDto CGroupDto = AutofacConfig.cGroupService.GetByID(e.GroupId );
            string[] groupusers ;
            if (CGroupDto!=null )
            {
                groupusers = CGroupDto.G_USER.Split(',');
            }
            else
            {
                groupusers = new String[1];
            }
            e.Result =groupusers;
        }

        private void  im1_TokenExpired(object sender, IMTokenExpiredArgs e)
        {
            if (im1 != null && System.String.IsNullOrEmpty(e.UserId ) == false)
            {
                //���õ�ǰ���û�Token
              //  im1.Token   = im1.GetUserToken(userid, userid, ".\\Image\\" + userid + ".PNG");
                //����Token
                e.Result = im1.Token;
            }
            else
            {
                e.Result = "";
            }
        }
        /// <summary>
        /// �����ϵ�˻�Ⱥ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pAdd_Press(object sender, EventArgs e)
        {
            frmAddConcentOrGroup frm = new frmAddConcentOrGroup(im1);
            switch (segmentedControl1.SelectedIndex)
            {
                case 0:
                    frm.ctype = ConcentState.Concent;

                    break;
                case 1:
                    frm.ctype = ConcentState.Group;
                    break;
            }
            Show(frm, (MobileForm form, object args) =>
            {
                if (frm.ShowResult == ShowResult.Yes)
                {
                    ShowResult = ShowResult.Yes;
                    Bind();
                }
            });
        }

        private void tabPageView1_PageIndexChanged(object sender, EventArgs e)
        {
            segmentedControl1.SelectedIndex = tabPageView1.PageIndex;
            Bind();
        }

        private void toolBar1_ToolbarItemClick(object sender, ToolbarClickEventArgs e)
        {
            if (e.Name .Equals ("msg"))
            {
                getchatMsg();
            }
        }
        /// <summary>
        /// ��ʾδ����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void im1_UnReadMessage(object sender, IMUnReadMessageArgs e)
        {
            Toast("��ǰ��δ����Ϣ"+e.UnReadMsgCount.ToString()+"��");
        }
    }
}