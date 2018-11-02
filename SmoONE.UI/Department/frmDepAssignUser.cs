using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using SmoONE.CommLib;
using SmoONE.Domain;
using SmoONE.UI.Layout;

namespace SmoONE.UI.Department
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ������Ա�������
    // ******************************************************************
    partial class frmDepAssignUser : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        int selectUserQty = 0;//ѡ����Ա��
         public DepInputDto department;//������Ϣ
         AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
      
        /// <summary>
        /// �ֻ��Դ����˼���ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepAssignUser_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmDepAssignUser_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepAssignUser_Load(object sender, EventArgs e)
        {
            Bind();
           
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void Bind()
        {
            try
            {
                if (department != null)
                {
                    txtDepName.Text = department.Dep_Name;
                    if (string.IsNullOrEmpty(department.Dep_Icon) == false)
                    {
                        imgPortrait.ResourceID = department.Dep_Icon;
                        imgPortrait.Refresh();
                    }
                    btnLeader.Text = AutofacConfig.userService.GetUserByUserID(department.Dep_Leader).U_Name;
                    List<DataGridviewbyUser> listUser = new List<DataGridviewbyUser>();
                    List<UserDto> listDepUser = AutofacConfig.userService.GetAllUsers();//��ȡ���䲿����Ա
                    //���Ŵ���ʱListView������
                    if (string.IsNullOrEmpty(department.Dep_ID) == true )
                    {
                        if (listDepUser.Count > 0)
                        { 
                            //��δ���䲿���Ҳ��ǵ�ǰ���������˵��û�����ӵ�listUser��
                            foreach (UserDto user in listDepUser)
                            {
                                if ((string.IsNullOrEmpty(user.U_DepID) == true) & (!department.Dep_Leader .Equals(user.U_ID)))
                                {
                                    DataGridviewbyUser depUser = new DataGridviewbyUser();
                                    depUser.U_ID = user.U_ID;
                                    depUser.U_Name = user.U_Name;
                                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                    {
                                        switch (user.U_Sex)
                                        {
                                            case (int)Sex.��:
                                                depUser.U_Portrait = "boy";
                                                break;
                                            case (int)Sex.Ů:
                                                depUser.U_Portrait = "girl";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        depUser.U_Portrait = user.U_Portrait;
                                    }
                                    depUser.U_DepID = "";
                                    depUser.U_DepName = "";
                                    depUser.SelectCheck = false;
                                    listUser.Add(depUser);
                                }
                            }
                            //���ѷ��䲿���Ҳ��ǵ�ǰ���������˵��û�����ӵ�listUser��
                            foreach (UserDto user in listDepUser)
                            {
                                if ((string.IsNullOrEmpty(user.U_DepID) == false) & (!department.Dep_Leader.Equals(user.U_ID)))
                                {
                                    DataGridviewbyUser depUser = new DataGridviewbyUser();
                                    depUser.U_ID = user.U_ID;
                                    depUser.U_Name = user.U_Name;
                                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                    {
                                        switch (user.U_Sex)
                                        {
                                            case (int)Sex.��:
                                                depUser.U_Portrait = "boy";
                                                break;
                                            case (int)Sex.Ů:
                                                depUser.U_Portrait = "girl";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        depUser.U_Portrait = user.U_Portrait;
                                    }
                                    depUser.U_DepID = user.U_DepID;
                                    string DepName = "";
                                    if (AutofacConfig.departmentService.GetDepartmentByDepID(user.U_DepID) != null)
                                    {
                                        DepName = AutofacConfig.departmentService.GetDepartmentByDepID(user.U_DepID).Dep_Name;
                                    }      
                                    depUser.U_DepName = DepName;
                                    depUser.SelectCheck = false;
                                    listUser.Add(depUser);
                                }
                            }
                        }
                    }
                    //���ű༭ʱListView������
                    if (string.IsNullOrEmpty(department.Dep_ID) == false )
                    {
                        if (listDepUser.Count > 0)
                        {
                            //����ǰ�����Ҳ��ǵ�ǰ���������˵��û�����ӵ�listUser��
                            foreach (UserDto user in listDepUser)
                            {
                                if ((string.IsNullOrEmpty(user.U_DepID) == false) & (department.Dep_ID.Equals(user.U_DepID)) & (!department.Dep_Leader.Equals(user.U_ID)))
                                {
                                    DataGridviewbyUser depUser = new DataGridviewbyUser();
                                    depUser.U_ID = user.U_ID;
                                    depUser.U_Name = user.U_Name;
                                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                    {
                                        switch (user.U_Sex)
                                        {
                                            case (int)Sex.��:
                                                depUser.U_Portrait = "boy";
                                                break;
                                            case (int)Sex.Ů:
                                                depUser.U_Portrait = "girl";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        depUser.U_Portrait = user.U_Portrait;
                                    }
                                    depUser.U_DepID = department .Dep_ID ;
                                    depUser.U_DepName =department .Dep_Name ;
                                    depUser.SelectCheck = true ;
                                    listUser.Add(depUser);
                                }
                            }
                            //��δ���䲿���Ҳ��ǵ�ǰ���������˵��û�����ӵ�listUser��
                            foreach (UserDto user in listDepUser)
                            {
                                if ((string.IsNullOrEmpty(user.U_DepID) == true) & (!department.Dep_Leader.Equals(user.U_ID)))
                                {
                                    DataGridviewbyUser depUser = new DataGridviewbyUser();
                                    depUser.U_ID = user.U_ID;
                                    depUser.U_Name = user.U_Name;
                                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                    {
                                        switch (user.U_Sex)
                                        {
                                            case (int)Sex.��:
                                                depUser.U_Portrait = "boy";
                                                break;
                                            case (int)Sex.Ů:
                                                depUser.U_Portrait = "girl";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        depUser.U_Portrait = user.U_Portrait;
                                    }
                                    depUser.U_DepID = "";
                                    depUser.U_DepName = "";
                                    depUser.SelectCheck = false;
                                    listUser.Add(depUser);
                                }
                            }
                            //���ѷ��䲿���Ҳ��ǵ�ǰ���ŵ��û�����ӵ�listUser��
                            foreach (UserDto user in listDepUser)
                            {
                                if ((string.IsNullOrEmpty(user.U_DepID) == false) & (!department.Dep_ID.Equals(user.U_DepID)) & (!department.Dep_Leader.Equals(user.U_ID)))
                                {
                                    DataGridviewbyUser depUser = new DataGridviewbyUser();
                                    depUser.U_ID = user.U_ID;
                                    depUser.U_Name = user.U_Name;
                                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                    {
                                        switch (user.U_Sex)
                                        {
                                            case (int)Sex.��:
                                                depUser.U_Portrait = "boy";
                                                break;
                                            case (int)Sex.Ů:
                                                depUser.U_Portrait = "girl";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        depUser.U_Portrait = user.U_Portrait;
                                    }
                                    depUser.U_DepID = user.U_DepID;
                                    string DepName = AutofacConfig.departmentService.GetDepartmentByDepID(user.U_DepID).Dep_Name;
                                    depUser.U_DepName = DepName;
                                    depUser.SelectCheck = false;
                                    listUser.Add(depUser);
                                }
                            }
                         }
                    }
                   
                    gridUserData.Rows.Clear();//�����Ա�б�����
                    if (listUser.Count > 0)
                    {
                        gridUserData.DataSource = listUser; //��ListView����
                        gridUserData.DataBind();
                        upCheckState();
                    }
                   
                }
                else
                {
                    throw new Exception("�����벿����Ϣ��");
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
        private void Checkall()
        {
            switch (checkAll .Checked )
            {
                case true:
                    foreach (ListViewRow rows in gridUserData.Rows)
                    {
                        //rows.Cell.Items["Check"].DefaultValue = true;
                        ((frmDepAssignUserLayout)(rows.Control)).Check.BindDisplayValue = true;

                    }
                    selectUserQty = gridUserData.Rows.Count;
                    break;
                case false:
                    foreach (ListViewRow rows in gridUserData.Rows)
                    {
                        //rows.Cell.Items["Check"].DefaultValue = false;
                        ((frmDepAssignUserLayout)(rows.Control)).Check.BindDisplayValue = false;

                    }
                    selectUserQty = 0;
                    break;
            }
        }
        /// <summary>
        /// ���䲿����Ա
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> listUser = new List<string>(); //�û�����
                string assignUser = "";//�ѷ��䲿���û�
                string depLeader = "";//�����������û�
                department.Dep_Name = txtDepName.Text.Trim();
                listUser.Add(department.Dep_Leader);//��ӵ�ǰ���Ÿ�����
                
               // //��ȡ�����˵Ĳ���
               // UserDetailDto leader = AutofacConfig.userService.GetUserByUserID(department.Dep_Leader);
               ////������ű�Ų�Ϊ���Ҳ����ڵ�ǰ����ʱ������ǰ��������ӵ���ǰ���ŵ��ѷ��䲿����Ա��
               // if (string.IsNullOrEmpty(leader.U_DepID) == false)
               // {   
               //     if (string.IsNullOrEmpty(department.Dep_ID) == false)
               //     {
               //         if (!department.Dep_ID.Equals(leader.U_DepID))
               //         {
               //             assignUser = btnLeader.Text.Trim();
               //         }
               //     }
               // }
                string depuser = null;//ѡ���û������ѷ��䲿�ŵ��û�
                List<string > listselectuserdep =new List<string> ();//��ȡѡ���û��������ѷ��䲿���У��û��Ĳ���
                foreach (ListViewRow rows in gridUserData.Rows)
                {

                    if ((Convert.ToBoolean(((frmDepAssignUserLayout)(rows.Control)).Check.BindDisplayValue) == true) & (!department.Dep_Leader.Equals(((frmDepAssignUserLayout)(rows.Control)).lblUser.BindDataValue.ToString())))
                    {      
                        string user =((frmDepAssignUserLayout)(rows.Control)).lblUser.BindDataValue.ToString();
                        listUser.Add(user);
                       //��ȡѡ���û��е��ѷ��䲿�ŵ��û�                      
                        if (string.IsNullOrEmpty(((frmDepAssignUserLayout)(rows.Control)).lblDep.BindDisplayValue.ToString())==false )
                        {
                            if (string .IsNullOrEmpty(depuser)==true  )
                            {
                                depuser = ((frmDepAssignUserLayout)(rows.Control)).lblUser.BindDataValue.ToString();
                            }
                            else
                            {
                                depuser += "," + ((frmDepAssignUserLayout)(rows.Control)).lblUser.BindDataValue.ToString();
                            }
                            if (listselectuserdep.Count <= 0)
                            {
                                listselectuserdep.Add(((frmDepAssignUserLayout)(rows.Control)).lblDep.BindDisplayValue.ToString());//���ѡ���û��Ĳ���
                            }
                            else
                            {
                                if (listselectuserdep.Contains(((frmDepAssignUserLayout)(rows.Control)).lblDep.BindDisplayValue.ToString()) == false)
                                {
                                    listselectuserdep.Add(((frmDepAssignUserLayout)(rows.Control)).lblDep.BindDisplayValue.ToString());//���ѡ���û��Ĳ���
                                }
                            }
                        }
                           
                          
                            //    //if (string.IsNullOrEmpty(department.Dep_ID) == false & !department.Dep_ID.Equals(((frmDepAssignUserLayout)(rows.Control)).lblDep.BindDisplayValue.ToString()))
                            //    //{
                            //    //    if (!department.Dep_ID.Equals(((frmDepAssignUserLayout)(rows.Control)).lblDep.BindDisplayValue.ToString()))
                            //    //    {
                            //            //����ǲ��������ˣ�����ӵ������������û�depLeader�У�������ӵ��ѷ��䲿���û�assignUser��
                            //            if (AutofacConfig.departmentService.IsLeader(                         ((frmDepAssignUserLayout)(rows.Control)).lblUser.BindDisplayValue.ToString()) == true)
                            //            {
                            //                if (string.IsNullOrEmpty(depLeader) == true)
                            //                {
                            //                    depLeader = rows.Cell.Items["lblUser"].Text;
                            //                }
                            //                else
                            //                {
                            //                    depLeader += "," + rows.Cell.Items["lblUser"].Text;
                            //                }

                            //            }
                            //            else
                            //            {
                            //                if (string.IsNullOrEmpty(assignUser) == true)
                            //                {
                            //                    assignUser = rows.Cell.Items["lblUser"].Text;
                            //                }
                            //                else
                            //                {
                            //                    assignUser += "," + rows.Cell.Items["lblUser"].Text;
                            //                }
                            //            }

                            //    //    }
                            //    //}
                            ////}
                        //}
                    }
                }
                //����ѷ��䲿�ŵ��û���Ϊ��ʱ
                if (string.IsNullOrEmpty(depuser) == false)
                {
                    string[] depusers = depuser.Split(',');
                    //��������ʱ���ж�ѡ���û��Ƿ�Ϊ���������˺��Ƿ�Ϊ�ѷ��䲿�ų�Ա
                    if (string.IsNullOrEmpty(department.Dep_ID) == true)
                    {
                        foreach (string user in depusers)
                         {   
                             //����ǲ��������ˣ�����ӵ������������û�depLeader�У�������ӵ��ѷ��䲿���û�assignUser��
                             if (AutofacConfig.departmentService.IsLeader(user) == true)
                             {
                                 UserDetailDto userd = AutofacConfig.userService.GetUserByUserID(user);
                                 if (string.IsNullOrEmpty(depLeader) == true)
                                 {                  
                                     depLeader = userd.U_Name ;
                                 }
                                 else
                                 {
                                     depLeader += "," + userd.U_Name;
                                 }

                             }
                             else
                             {
                                 if (string.IsNullOrEmpty(assignUser) == true)
                                 {
                                     assignUser = user;
                                 }
                                 else
                                 {
                                     assignUser += "," + user;
                                 }
                             }
                         }
                     }
                    //�༭����ʱ���ж�ѡ���û��Ƿ�Ϊ���������˺��Ƿ�Ϊ�ѷ��䲿�ų�Ա
                    if (string.IsNullOrEmpty(department.Dep_ID) == false )
                    {
                        foreach (string user in depusers)
                        {    
                            UserDetailDto userd = AutofacConfig.userService.GetUserByUserID(user);
                            if (!department.Dep_ID.Equals(userd.U_DepID))
                            {
                                //����ǲ��������ˣ�����ӵ������������û�depLeader�У�������ӵ��ѷ��䲿���û�assignUser��
                                if (AutofacConfig.departmentService.IsLeader(user) == true)
                                {

                                    if (string.IsNullOrEmpty(depLeader) == true)
                                    {
                                        depLeader = userd.U_Name;
                                    }
                                    else
                                    {
                                        depLeader += "," + userd.U_Name;
                                   }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(assignUser) == true)
                                    {
                                        assignUser = user;
                                    }
                                    else
                                    {
                                        assignUser += "," + user;
                                    }
                                }
                            }
                      }
                    }
                    if (listselectuserdep.Count >0 & string.IsNullOrEmpty(assignUser) == false)
                    {

                        string[] assignUsers = assignUser.Split(',');
                        string assignUser1 = "";
                        foreach (string depNO in listselectuserdep)
                        {
                            string assignU = "";
                            foreach (string user in assignUsers)
                           {
                               UserDetailDto userd = AutofacConfig.userService.GetUserByUserID(user);
                               if (user != null)
                               {
                                   if (userd.U_DepID.Equals(depNO))
                                   {
                                       if (string.IsNullOrEmpty(assignU) == true)
                                       {
                                           assignU = userd.U_Name ;
                                       }
                                       else
                                       {
                                           assignU += "," + userd.U_Name;
                                       }
                                   }
                               }
                           }
                            if (string.IsNullOrEmpty(assignU) == false)
                            {
                                if (string.IsNullOrEmpty(assignU) == false)
                                {
                                    assignUser1 = assignU + "����" + AutofacConfig.departmentService.GetDepartmentByDepID(depNO).Dep_Name + "���ų�Ա";
                                }
                                else
                                {
                                    assignUser1 +="��"+ assignU + "����" + AutofacConfig.departmentService.GetDepartmentByDepID(depNO).Dep_Name + "���ų�Ա";
                                }
                            }
                        }
                        assignUser = assignUser1;
                    }
                }
                if (string.IsNullOrEmpty(depLeader) == false)
                {
                    throw new Exception(depLeader+"���ǲ��������ˣ����Ƚ�ɢ���ţ�");
                }
                //bool isUPdateDep = false; //�Ƿ���²�����Ա
                ReturnInfo result;
                if (string.IsNullOrEmpty(assignUser) == false)
                {
                    MessageBox.Show(assignUser+"�Ƿ���䣿", "������Ա", MessageBoxButtons.YesNo, (Object s, MessageBoxHandlerArgs args) =>
                    {
                        if (args.Result ==  Smobiler.Core.Controls .ShowResult.Yes)
                        {
                            //isUPdateDep = true;
                            department.UserIDs = listUser;
                            if (department.Dep_ID != null)
                            {
                              
                                result = AutofacConfig.departmentService.UpdateDepartment(department);
                            }
                            else
                            {
                              
                                result = AutofacConfig.departmentService.AddDepartment(department);
                            }
                            if (result.IsSuccess == false)
                            {
                                throw new Exception(result.ErrorInfo);
                            }
                            else
                            {
                                ShowResult = ShowResult.Yes;
                                Close();
                                Toast("������Ա����ɹ���", ToastLength.SHORT);
                            }
                        }
                    }
                      );
                }
                else
                {
                 
                    department.UserIDs = listUser;
                    if (department.Dep_ID != null)
                    {
                     
                        result = AutofacConfig.departmentService.UpdateDepartment(department);
                    }
                    else
                    {
                     
                        result = AutofacConfig.departmentService.AddDepartment(department);
                    }
                    if (result.IsSuccess == false)
                    {
                        throw new Exception(result.ErrorInfo);
                    }
                    else
                    {
                        ShowResult = ShowResult.Yes;
                        Close();
                        Toast("������Ա����ɹ���", ToastLength.SHORT);
                    }
                }
              
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
       
        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            if (checkAll.Checked)
            {
                checkAll.Checked = false;
            }
            else
            {
                checkAll.Checked = true ;
            }
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
        ///// <summary>
        ///// ListView����¼�
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gridView1_ItemClick(object sender, GridViewCellItemEventArgs e)
        //{
        //    upCheckState();
        //}
        /// <summary>
        /// ����ȫѡ״̬
        /// </summary>
        private void upCheckState()
        {
             selectUserQty = 0;
            foreach (ListViewRow rows in gridUserData.Rows)
            {
                if (Convert.ToBoolean(((frmDepAssignUserLayout)(rows.Control)).Check.BindDisplayValue) == true)
                {
                    selectUserQty += 1;
                }
            }
            //��ListView����ѡ����������ListView����ʱ��Ϊȫѡ״̬������Ϊ��ѡ״̬��
            if (selectUserQty == gridUserData.Rows.Count)
            {
                checkAll.Checked = true;
            }
            else
            {
                checkAll.Checked = false;
            }
        }
        ///// <summary>
        ///// ListView����¼�
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void gridView1_CellClick(object sender, GridViewCellEventArgs e)
        //{
        //    switch (Convert .ToBoolean (e.Cell.Items["Check"].DefaultValue))
        //    {
        //        case true :
        //            e.Cell.Items["Check"].DefaultValue = false;
        //            break;
        //        case false :
        //            e.Cell.Items["Check"].DefaultValue = true;
        //            break;
        //    }
        //    upCheckState();
        //}

        /// <summary>
        /// �ϴ�����ͷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            cameraPortrait.GetPhoto();
        }

        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeader_Click(object sender, EventArgs e)
        {
            popLeader.Groups.Clear();
            PopListGroup poli = new PopListGroup();
            popLeader.Groups.Add(poli);
            poli.Title = "������ѡ��";
            List<UserDto> listuser = AutofacConfig.userService.GetAllUsers();
            foreach (UserDto user in listuser)
            {
                poli.AddListItem(user.U_Name, user.U_ID);
                if (string .IsNullOrEmpty (department .Dep_Leader)==false )
                {
                    if (department.Dep_Leader.Trim().Equals(user.U_ID))
                    {
                        popLeader.SetSelections(poli.Items[(poli.Items.Count - 1)]);
                    }
                }
            }
            popLeader.Show();
        }
        /// <summary>
        /// �����˸�ֵ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popLeader_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popLeader.Selection != null)
                {
                    //��ѯ��ѡ�е��û��Ƿ��Ѿ��ǲ���������
                    bool isLeader = AutofacConfig.departmentService.IsLeader(popLeader.Selection.Value);
                    //�����ѡ�����������ǲ��������ˣ��򱨴�
                    if (isLeader == true)
                    {
                        throw new Exception(popLeader.Selection.Text + "���ǲ��������ˣ����Ƚ�ɢ���ţ�");
                    }
                    //
                    UserDepDto userdep = AutofacConfig.userService.GetUseDepByUserID(popLeader.Selection.Value);
                    //���ѡ���û����ǲ��ų�Ա�Ҳ��ǲ��������ˣ������ѡ���Ƿ�ȷ��Ϊ���������ˣ���ȷ����Ϊ�ò��Ÿ�����
                    if (userdep != null & string.IsNullOrEmpty(userdep.Dep_ID) == false & isLeader == false)
                        //if (AutofacConfig.userService.GetAllUsers().Count > 0 & isLeader== false)
                        //{
                        MessageBox.Show(popLeader.Selection.Text + "���ǲ��ų�Ա���Ƿ�ȷ��Ϊ�ò��������ˣ�", MessageBoxButtons.YesNo, (Object s1, MessageBoxHandlerArgs args) =>
                        {
                            //��ί��Ϊ�첽ί���¼�
                            if (args.Result == Smobiler.Core.Controls .ShowResult.Yes)
                            {
                                department.Dep_Leader = popLeader.Selection.Value;
                                btnLeader.Text = popLeader.Selection.Text + "   > ";
                            }
                        });
                    //}
                    //���ѡ���û����ǲ����������Ҳ��ǲ��ų�Ա����Ϊ�ò��Ÿ�����
                    if (isLeader == false & userdep != null & string.IsNullOrEmpty(userdep.Dep_ID) == true)
                    {
                        department.Dep_Leader = popLeader.Selection.Value;
                        btnLeader.Text = popLeader.Selection.Text + "   > ";
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }

        /// <summary>
        /// ���沢��ֵͷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cameraPortrait_ImageCaptured(object sender, BinaryResultArgs e)
        {
            if (string.IsNullOrEmpty(e.error))
            {

                if (imgPortrait.ResourceID.Trim().Length > 0 & string.IsNullOrEmpty(department.Dep_Icon) == false)
                {
                    e.SaveFile(department.Dep_Icon);
                    imgPortrait.ResourceID = department.Dep_Icon;
                    imgPortrait.Refresh();
                }
                else
                {
                    e.SaveFile(e.ResourceID);
                    department.Dep_Icon = e.ResourceID;
                    imgPortrait.ResourceID = e.ResourceID;
                    imgPortrait.Refresh();
                }
            }
        }

        private void title1_Load(object sender, EventArgs e)
        {

        }
    }
}