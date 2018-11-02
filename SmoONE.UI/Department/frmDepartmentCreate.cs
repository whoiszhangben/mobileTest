using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.CommLib;
using SmoONE.DTOs;
using SmoONE.Domain;

namespace SmoONE.UI.Department
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ���Ŵ�����༭����
    // ******************************************************************
    partial class frmDepartmentCreate : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        public string D_ID;//���ű��
        string leader="";//������
        string D_Portrait="";//����ͷ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDep_Name.Text .Trim ().Length <= 0)
                {
                    throw new Exception("�����벿�����ƣ�");
                }
              
                if (leader.Length <= 0)
                {
                    throw new Exception("�����������ˣ�");
                }
                DepInputDto department = new DepInputDto();
                department.Dep_Name = txtDep_Name.Text;
                department.Dep_UpdateUser = Client.Session["U_ID"].ToString();
                department.Dep_Leader = leader;
                if (string.IsNullOrEmpty(D_Portrait) == false)
                {
                    department.Dep_Icon = D_Portrait;
                }
                else
                {
                    department.Dep_Icon = "";
                }
                if (string.IsNullOrEmpty(D_ID) == false)
                {
                    department.Dep_ID = D_ID;
                    List<UserDto> listuserDto=  AutofacConfig .userService  .GetUserByDepID(D_ID);
                    List <string > listUser=new List<string> ();
                    foreach (UserDto user in listuserDto)
                    {
                        listUser.Add (user.U_ID);
                    }
                    department.UserIDs=listUser;
                    ReturnInfo result = AutofacConfig.departmentService.UpdateDepartment(department);
                    if (result.IsSuccess == false)
                    {
                        throw new Exception(result.ErrorInfo);
                    }
                    else
                    {
                        ShowResult = ShowResult.Yes;
                        Close();
                        Toast("�����ύ�ɹ���", ToastLength.SHORT);
                    }
                }
                else
                {
                    //ShowResult = ShowResult.Yes;
                  
                    frmDepAssignUser frmDepAssignUser = new frmDepAssignUser();
                    frmDepAssignUser.department = department;
                    //Show(frmDepAssignUser);
                    Show(frmDepAssignUser, (MobileForm form, object args) =>
                        {
                            if (frmDepAssignUser.ShowResult == ShowResult.Yes)
                            {
                                ShowResult = ShowResult.Yes;
                                Close();
                            }
                        });
                }
                
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
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
                if (leader.Trim().Length > 0)
                {
                    if (leader.Trim().Equals(user.U_ID))
                    {
                        popLeader.SetSelections(poli.Items[(poli.Items.Count - 1)]);
                    }
                }
            }
            popLeader.Show();
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepartmentCreate_Load(object sender, EventArgs e)
        {
            Bind();
        }

        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        private void Bind()
        {
            try 
            {
                if (D_ID != null)
                {
                    DepDetailDto dep = AutofacConfig.departmentService.GetDepartmentByDepID(D_ID);
                    if (dep == null)
                    {
                        throw new Exception("����" + D_ID + "�����ڣ����飡");
                    }
                    txtDep_Name.Text = dep.Dep_Name;
                    leader = dep.Dep_Leader;
                    if (string.IsNullOrEmpty(dep.Dep_Icon) == false)
                    {
                        D_Portrait = dep.Dep_Icon;
                        imgPortrait.ResourceID = dep.Dep_Icon;
                    }
                    else
                    {
                        imgPortrait.ResourceID = "bumenicon";
                    }
                    UserDetailDto userinfo = AutofacConfig.userService.GetUserByUserID(leader);
                    btnLeader.Text = userinfo.U_Name;
                    btnSave.Text = "�ύ";
                    btnAssignUser.Visible = true;
                   // btnSave.Top = 190;
                    btnAssignUser.Top = btnLeader.Top + btnLeader.Height + 10;
                    btnSave.Top = btnAssignUser.Top + btnAssignUser.Height + 10;
                }
                else
                {
                    btnAssignUser.Visible =false ;
                   // btnSave.Top = 145;
                    btnSave.Top = btnLeader.Top + btnLeader.Height + 10;
                }
            }
                catch (Exception ex)
            {
                    MessageBox .Show (ex.Message );
             }
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
                    bool isLeader=AutofacConfig.departmentService.IsLeader(popLeader.Selection.Value);
                    //�����ѡ�����������ǲ��������ˣ��򱨴�
                    if (isLeader == true )
                    {
                        throw new Exception (popLeader.Selection.Text + "���ǲ��������ˣ����Ƚ�ɢ���ţ�");
                    }
                    //
                  UserDepDto userdep=  AutofacConfig.userService.GetUseDepByUserID(popLeader.Selection.Value);
                  //���ѡ���û����ǲ��ų�Ա�Ҳ��ǲ��������ˣ������ѡ���Ƿ�ȷ��Ϊ���������ˣ���ȷ����Ϊ�ò��Ÿ�����
                    if ( userdep !=null & string.IsNullOrEmpty (userdep.Dep_ID)==false & isLeader== false )
                    //if (AutofacConfig.userService.GetAllUsers().Count > 0 & isLeader== false)
                    //{
                        MessageBox.Show(popLeader.Selection.Text+"���ǲ��ų�Ա���Ƿ�ȷ��Ϊ�ò��������ˣ�", MessageBoxButtons.YesNo, (Object s1, MessageBoxHandlerArgs args) =>
                        {
                            //��ί��Ϊ�첽ί���¼�
                            if (args.Result == Smobiler.Core.Controls .ShowResult.Yes)
                            {
                                leader = popLeader.Selection.Value;
                                btnLeader.Text = popLeader.Selection.Text + "   > ";
                            }
                        });
                    //}
                    //���ѡ���û����ǲ����������Ҳ��ǲ��ų�Ա����Ϊ�ò��Ÿ�����
                   if (isLeader == false & userdep != null & string.IsNullOrEmpty(userdep.Dep_ID) == true )
                   {
                       leader = popLeader.Selection.Value;
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
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepartmentCreate_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmDepartmentCreate_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
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
        /// ���沢��ֵͷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cameraPortrait_ImageCaptured(object sender, BinaryResultArgs e)
        {
            if (string.IsNullOrEmpty(e.error ))
            {
                if (imgPortrait.ResourceID.Trim().Length > 0 & string.IsNullOrEmpty(D_Portrait)==false )
                {
                    e.SaveFile(D_Portrait);
                    imgPortrait.ResourceID = D_Portrait;
                    imgPortrait.Refresh();
                }
                else
                {
                    e.SaveFile(e.ResourceID);
                    D_Portrait = e.ResourceID;
                    imgPortrait.ResourceID = e.ResourceID;
                    imgPortrait.Refresh();
                }
            }
        }
        /// <summary>
        /// ��ת�����䲿�Ž���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAssignUser_Click(object sender, EventArgs e)
        {
            if (D_ID != null)
            {
                DepDetailDto dep = AutofacConfig.departmentService.GetDepartmentByDepID(D_ID);
                if (dep != null)
                {
                    DepInputDto department = new DepInputDto();
                    department.Dep_ID = dep.Dep_ID;
                    department.Dep_Name = dep.Dep_Name;
                    department.Dep_Leader = dep.Dep_Leader;
                    department.Dep_Icon = dep.Dep_Icon;
                    frmDepAssignUser frmDepAssignUser = new frmDepAssignUser();
                    frmDepAssignUser.department = department;
                    Show(frmDepAssignUser, (MobileForm form, object args) =>
                    {
                        if (frmDepAssignUser.ShowResult == ShowResult.Yes)
                        {
                            ShowResult = ShowResult.Yes;
                            Close();
                        }
                    });
                }

            }
        }
    }
}