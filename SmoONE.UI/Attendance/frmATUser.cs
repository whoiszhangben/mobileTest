using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using SmoONE.UI.Attendance;
using SmoONE.UI.Layout;

namespace SmoONE.UI.Attendance
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ� �����û�ѡ�н���
    // ******************************************************************
    partial class frmATUser : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        public  int selectUserQty = 0;//ѡ����Ա��
        public string ATNo;//����ģ����
        public string  selectUser;//ѡ����Ա
        #endregion
        /// <summary>
        /// ListView����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void gridATData_CellClick(object sender, GridViewCellEventArgs e)
        //{
        //    switch (Convert.ToBoolean(e.Cell.Items["Check"].DefaultValue))
        //    {
        //        case true:
        //            e.Cell.Items["Check"].DefaultValue = false;
        //            break;
        //        case false:
        //            e.Cell.Items["Check"].DefaultValue = true;
        //            break;
        //    }
        //    upCheckState();
        //}
        /// <summary>
        /// ��ȡ�û�
        /// </summary>
        private void GetATUser()
        {
            try
            {         
                List<DataGridviewbyUser> listATUser = new List<DataGridviewbyUser>();//�����û�����
                //����п���ѡ���û�������ӵ������û�����listATUser����Ĭ���û�״̬Ϊѡ��
                if (string.IsNullOrEmpty(selectUser) == false)
                {
                    string[] selectUsers = selectUser.Split(',');
                    foreach (string user in selectUsers)
                    {
                        //UserDetailDto userdto = AutofacConfig.userService.GetUserByUserID(user);
                        UserDetails userDetails = new UserDetails();
                        UserDetailDto userdto = userDetails.getUser(user);
                        if (userdto != null)
                        {
                            DataGridviewbyUser atUser = new DataGridviewbyUser();
                            atUser.U_ID = userdto.U_ID;
                            atUser.U_Name = userdto.U_Name;
                            atUser.U_Portrait = userdto.U_Portrait;
                            string depID = "";
                            string depName = "";
                            if (string.IsNullOrEmpty(userdto.U_DepID))
                            {
                                DepDetailDto department = AutofacConfig.departmentService.GetDepartmentByDepID(userdto.U_DepID);
                                if (department != null)
                                {
                                    depID = department.Dep_ID;
                                    depID = department.Dep_Name;
                                }
                            }
                            atUser.U_DepID = depID;
                            atUser.U_DepName = depName;
                            atUser.SelectCheck = true;
                            listATUser.Add(atUser);
                        }
                    }
                }
              
                //�༭����ģ�����ֿ���ѡ���û���������ǰ���ڳ�Աʱ����ӿ����û�������listATUser�������û�״̬Ĭ��Ϊδѡ��
                if (string.IsNullOrEmpty(ATNo) == false)
                {
                    List<UserDto> listUser = AutofacConfig.attendanceService.GetATUser(ATNo);
                    if (listUser.Count > 0)
                    {
                        foreach (UserDto user in listUser)
                        {
                            if (!(string.IsNullOrEmpty(selectUser) == false & selectUser.Split(',').Contains(user.U_ID) == true))
                            {
                           
                                DataGridviewbyUser atUser = new DataGridviewbyUser();
                                atUser.U_ID = user.U_ID;
                                atUser.U_Name = user.U_Name;
                                if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                {
                                    switch (user.U_Sex)
                                    {
                                        case (int)Sex.��:
                                            atUser.U_Portrait = "boy";
                                            break;
                                        case (int)Sex.Ů:
                                            atUser.U_Portrait = "girl";
                                            break;
                                    }
                                }
                                else
                                {
                                    atUser.U_Portrait = user.U_Portrait;
                                }
                                string depID = "";
                                string depName = "";
                                if (string.IsNullOrEmpty(user.U_DepID))
                                {
                                    DepDetailDto department = AutofacConfig.departmentService.GetDepartmentByDepID(user.U_DepID);
                                    if (department != null)
                                    {
                                        depID = department.Dep_ID;
                                        depID = department.Dep_Name;
                                    }
                                }
                                atUser.U_DepID = depID;
                                atUser.U_DepName = depName;
                                atUser.SelectCheck = false;
                                listATUser.Add(atUser);
                            }
                        }
                    }
                }
               

                //���δ���俼��ģ����û�����0ʱ����ӵ������û�����listATUser     
                List<UserDto> listNoATUser = AutofacConfig.attendanceService.GetNoATUser();
                if (listNoATUser.Count > 0)
                {
                    foreach (UserDto user in listNoATUser)
                    {
                        if ((string.IsNullOrEmpty(selectUser) == true )|| (string.IsNullOrEmpty(selectUser) == false & selectUser.Split(',').Contains(user.U_ID)==false ))
                        {
                            DataGridviewbyUser atUser = new DataGridviewbyUser();
                            atUser.U_ID = user.U_ID;
                            atUser.U_Name = user.U_Name;
                            if (string.IsNullOrEmpty(user.U_Portrait) == true)
                            {
                                switch (user.U_Sex)
                                {
                                    case (int)Sex.��:
                                        atUser.U_Portrait = "boy";
                                        break;
                                    case (int)Sex.Ů:
                                        atUser.U_Portrait = "girl";
                                        break;
                                }
                            }
                            else
                            {
                                atUser.U_Portrait = user.U_Portrait;
                            }
                            string depID = "";
                            string depName = "";
                            if (string.IsNullOrEmpty(user.U_DepID))
                            {
                                DepDetailDto department = AutofacConfig.departmentService.GetDepartmentByDepID(user.U_DepID);
                                if (department != null)
                                {
                                    depID = department.Dep_ID;
                                    depID = department.Dep_Name;
                                }
                            }
                            atUser.U_DepID = depID;
                            atUser.U_DepName = depName;
                            atUser.SelectCheck = false;
                            listATUser.Add(atUser);
                        }
                    }
                }
                gridATUserData.Rows.Clear();//�����Ա�б�����
                if (listATUser.Count > 0)
                {
                   
                    gridATUserData.DataSource = listATUser; //��ListView����
                    gridATUserData.DataBind();
                    upCheckState();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            Checkall();
        }
        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkAll_CheckChanged(object sender, EventArgs e)
        {
            Checkall();
        }
        /// <summary>
        /// ȫѡ����
        /// </summary>
        private void Checkall()
        {
            switch (checkAll1.Checked)
            {
                case true:
                    foreach (ListViewRow rows in gridATUserData.Rows)
                    {
                       // rows.Cell.Items["Check"].DefaultValue = true;
                        ((frmATUserLayout)(rows.Control)).Check.BindDisplayValue = true;

                    }
                    selectUserQty = gridATUserData.Rows.Count;
                    break;
                case false:
                    foreach (ListViewRow rows in gridATUserData.Rows)
                    {
                      //  rows.Cell.Items["Check"].DefaultValue = false;
                        ((frmATUserLayout)(rows.Control)).Check.BindDisplayValue =false ;

                    }
                    selectUserQty = 0;
                    break;
            }
            //����ѡ����Ա������
            upSelectUserFoot();
        }
        /// <summary>
        /// ����ȫѡ״̬
        /// </summary>
        public  void upCheckState()
        {
            selectUserQty = 0;
            foreach (ListViewRow rows in gridATUserData.Rows)
            {


                if (Convert.ToBoolean(((frmATUserLayout)(rows.Control)).Check.BindDisplayValue) == true)
                {
                    selectUserQty += 1;
                }
            }
            //��ListView����ѡ����������ListView����ʱ��Ϊȫѡ״̬������Ϊ��ѡ״̬��
            if (selectUserQty == gridATUserData.Rows.Count)
            {
                checkAll1.Checked = true;
            }
            else
            {
                checkAll1.Checked = false;
            }
            //����ѡ����Ա������
            upSelectUserFoot();
        }
        /// <summary>
        /// ����ѡ����Ա������
        /// </summary>
        private void upSelectUserFoot()
        {
            try
            {
                if (selectUserQty > 0)
                {
                   // FooterBarLayoutData.Items["btnSave"].BackColor = System.Drawing.Color.FromArgb(43, 146, 223);
                    //FooterBarLayoutData.Items["btnSave"].Enabled = true;
                    frmATFootLayout1.btnSave.BackColor = System.Drawing.Color.FromArgb(43, 146, 223);
                    frmATFootLayout1.btnSave.Enabled = true;

                }
                else
                {
                   // FooterBarLayoutData.Items["btnSave"].BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
                   // FooterBarLayoutData.Items["btnSave"].Enabled = false;

                    frmATFootLayout1.btnSave.BackColor = System.Drawing.Color.FromArgb(45, 45, 45);
                    frmATFootLayout1.btnSave.Enabled = false;
                }
                //FooterBarLayoutData.Items["lblDesc"].DefaultValue = "��ѡ�� " + selectUserQty.ToString() + " ��";
                frmATFootLayout1.lblDesc.Text = "��ѡ�� " + selectUserQty.ToString() + " ��";

            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /////// <summary>
        /////// ListView����¼�
        /////// </summary>
        /////// <param name="sender"></param>
        /////// <param name="e"></param>
        ////private void gridATData_ItemClick(object sender, GridViewCellItemEventArgs e)
        ////{
        ////    upCheckState();
        ////}

      
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmATUser_Load(object sender, EventArgs e)
        {
            GetATUser();
        }

        private void frmATUser_KeyDown(object sender, KeyDownEventArgs e)
        {
            Close();
        }

        private void frmATUser_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}