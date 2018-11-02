using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using Smobiler.Plugins.RongIM;
using SmoONE.DTOs;
using SmoONE.CommLib;
using SmoONE.UI.Layout;

namespace SmoONE.UI.Im
{
 

    partial class frmAddConcentOrGroup : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        public ConcentState ctype;
        public  IM im1;
        #endregion
        public frmAddConcentOrGroup(IM im):this()
        {
            im1 = im;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                switch (ctype)
                {
                    case ConcentState.Concent:
                        //ContactInputDto cInputDto = new ContactInputDto();

                        //ReturnInfo r = AutofacConfig.contactService.AddContact(cInputDto);
                                 
                        //if (r.IsSuccess == true)
                        //{
                           
                        //    this.ShowResult = ShowResult.Yes;
                        //    this.Close();
                        //    Toast("���Ѽ�¼�ύ�ɹ���", ToastLength.SHORT);
                        //}
                        //else
                        //{
                        //    throw new Exception(r.ErrorInfo);
                        //}

                        
                        break;
                    case ConcentState.Group:
                        if (txtName.Text.Trim().Length <= 0)
                        {
                            throw new Exception("������Ⱥ�����ƣ�");
                        }

                        CGroupInputDto cgroupInputDto = new CGroupInputDto();
                        cgroupInputDto.G_NAME = txtName.Text.Trim();
                        string guser = "";
                        foreach (ListViewRow rows in listView1.Rows)
                        {

                            if (Convert.ToBoolean(((Layout.frmAddUserLayout)(rows.Control)).Check.BindDisplayValue) == true)
                            {
                                if (guser.Length <= 0)
                                {
                                    guser = ((Layout.frmAddUserLayout)(rows.Control)).lblUser.BindDataValue .ToString();
                                }
                                else
                                {
                                    guser = guser + "," + ((Layout.frmAddUserLayout)(rows.Control)).lblUser.BindDataValue.ToString();
                                }
                            }
                        }
                        if (guser.Length <= 0)
                        {
                            throw new Exception("��ѡ��Ⱥ���Ա��");
                        }
                        guser= guser+"," + Client.Session["U_ID"].ToString();
                        cgroupInputDto.G_USER = guser;
                        cgroupInputDto.G_CreateUser = Client.Session["U_ID"].ToString();
                        cgroupInputDto.G_UpdateUser  = Client.Session["U_ID"].ToString();
                        ReturnInfo r = AutofacConfig.cGroupService .AddGroup(cgroupInputDto);

                        if (r.IsSuccess == true)
                        {
                            string[] guser1 = guser.Split(',');
                            string groupid = r.ErrorInfo;//������trueʱ������Ⱥ����
                            //����Ⱥ��                    
                            im1.CreateGroup(guser1, groupid, cgroupInputDto.G_NAME);

                            this.ShowResult = ShowResult.Yes;
                            this.Close();
                            Toast("Ⱥ�鴴���ɹ���", ToastLength.SHORT);
                        }
                        else
                        {
                            throw new Exception(r.ErrorInfo);
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
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAddConcentOrGroup_Load(object sender, EventArgs e)
        {
            switch (ctype)
            {
                case ConcentState.Concent:
                    title1.TitleText = "�����ϵ��";
                    pGName.Visible = false;
                    pPost.Visible = false;
                    listView1.TemplateControl = new frmAddUser1Layout();
                    break;
                case ConcentState.Group:
                    title1.TitleText = "����Ⱥ��";
                    pGName.Visible = true ;
                    pPost.Visible = true ;
                    listView1.TemplateControl= new frmAddUserLayout();
                    break;
            }
            Bind();
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void  Bind ()
        {
            try
            {
                List<UserDto> listUser = new List<UserDto>();
                List<UserDto> listCUser = AutofacConfig.userService.GetAllUsers();//��ȡ���䲿����Ա
                List<string> listCUser1 = new List<string>();
                listView1.Rows.Clear();
                switch (ctype)
                {
                    case ConcentState.Concent:

                        //��ȡ��ǰ�û���ϵ��
                        List<ContactDto> listContactDto = AutofacConfig.contactService.GetByCreateUsers(Client.Session["U_ID"].ToString());
                        if (listContactDto.Count > 0)
                        {
                            foreach (ContactDto contact in listContactDto)
                            {
                                listCUser1.Add(contact.C_USER);
                            }
                        }

                        break;

                }
                if (listCUser.Count > 0)
                {
                    listCUser1.Add(Client.Session["U_ID"].ToString());
                    foreach (UserDto user in listCUser)
                    {
                        if (listCUser1.Count <= 0 || (listCUser1.Count > 0 && !listCUser1.Contains(user.U_ID)))
                        {
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
                            listUser.Add(user);
                        }

                    }
                }
                listView1.Rows.Clear();
                if (listUser.Count > 0)
                {
                    listView1.DataSource = listUser;
                    listView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
}


    }
}