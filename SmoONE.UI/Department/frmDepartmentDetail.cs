using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Domain;
using SmoONE.DTOs;
using SmoONE.CommLib;

namespace SmoONE.UI.Department
{

    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  �����������
    // ******************************************************************
    partial class frmDepartmentDetail : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        /// <summary>
        /// ���ű��
        /// </summary>
        public string D_ID;
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion

        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepartmentDetail_Load(object sender, EventArgs e)
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
            
                //���ݲ��ű�Ż�ȡ��������
                DepDetailDto department = AutofacConfig.departmentService.GetDepartmentByDepID(D_ID);
               lblName.Text = department.Dep_Name;
                //��ȡ������Ա����
                if (string .IsNullOrEmpty (department.Dep_Leader)==false )
                {
                      UserDetailDto user = AutofacConfig.userService.GetUserByUserID(department.Dep_Leader);
                      if (user != null )
                      {
                          lblLeader.Text = user.U_Name;
                      }
                }
               if (string.IsNullOrEmpty(department.Dep_Icon) == false)
               {
                   imgPortrait.ResourceID = department.Dep_Icon;
               }
               else
               {
                   imgPortrait.ResourceID = "bumenicon";
               }
               List<UserDto> listDepUser = AutofacConfig.userService.GetUserByDepID(D_ID);
               if (listDepUser.Count > 0)
               {
                   gridUserData.Rows.Clear();
                   foreach (UserDto userinfo in listDepUser)
                   {
                       if (userinfo.U_ID.Equals(department.Dep_Leader))
                       {
                           listDepUser.Remove(userinfo);
                           break;
                       }
                   }
                   gridUserData.Rows.Clear();//��ղ�����Ա�б�����
                   if (listDepUser.Count > 0)
                   {
                       foreach (UserDto userinfo in listDepUser)
                       {
                               if (string.IsNullOrEmpty(userinfo.U_Portrait) == true)
                               {
                                   switch (userinfo.U_Sex)
                                   {
                                       case (int)Sex.��:
                                           userinfo.U_Portrait = "boy";
                                           break;
                                       case (int)Sex.Ů:
                                           userinfo.U_Portrait = "girl";
                                           break;
                                   }
                               }
                               //else
                               //{
                               //    userinfo.U_Portrait = userinfo.U_Portrait;
                               //}
                           }
                   }
                   gridUserData.DataSource = listDepUser;
                   gridUserData.DataBind();
               }
               
            }
            catch (Exception ex)
            {
                MessageBox .Show (ex.Message);
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepartmentDetail_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmDepartmentDetail_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// ��ת���༭����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmDepartmentCreate frm = new frmDepartmentCreate();
            frm.D_ID = D_ID;
            Show(frm, (MobileForm form, object args) =>
            {
                if (frm.ShowResult == ShowResult.Yes)
                {
                    ShowResult = ShowResult.Yes;
                    Bind();
                }
            });
     
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
          
            //bool isDelDep = false;//�Ƿ�ɾ������
            MessageBox.Show("�Ƿ�ȷ��ɾ�����ţ�", "����", MessageBoxButtons.YesNo, (Object s1, MessageBoxHandlerArgs args1) =>
               {
                   if (args1.Result == Smobiler.Core.Controls .ShowResult.Yes)
                   {
                       //���������Ա��������0���򵯳���ʾ����ɾ�����ţ�����ֱ��ɾ��
                       if (gridUserData.Rows.Count > 0)
                       {
                           MessageBox.Show(lblName.Text + "�ѷ��䲿����Ա�Ƿ�ɾ����", "ɾ������", MessageBoxButtons.YesNo, (Object s, MessageBoxHandlerArgs args) =>
                           {
                               if (args.Result ==Smobiler.Core.Controls .ShowResult.Yes)
                               {

                                   //isDelDep = true;
                                   try
                                   {
                                       ReturnInfo result = AutofacConfig.departmentService.DeleteDepartment(D_ID);
                                       if (result.IsSuccess == true)
                                       {
                                           ShowResult = ShowResult.Yes;
                                           Close();
                                           Toast("������ɾ����", ToastLength.SHORT);
                                       }
                                       else
                                       {
                                           throw new Exception(result.ErrorInfo);
                                       }
                                   }
                                   catch (Exception ex)
                                   {
                                       Toast (ex.Message , ToastLength.SHORT);
                                   }
                               }
                           });
                       }
                       else
                       {
                           //isDelDep = true;
                           ReturnInfo result = AutofacConfig.departmentService.DeleteDepartment(D_ID);
                           if (result.IsSuccess == true)
                           {
                               ShowResult = ShowResult.Yes;
                               Close();
                               Toast("������ɾ����", ToastLength.SHORT);
                           }
                           else
                           {
                               Toast(result.ErrorInfo, ToastLength.SHORT);
                           }
                       }
                       //if (isDelDep == true)
                       //{
                       //    ReturnInfo result = AutofacConfig.departmentService.DeleteDepartment(D_ID);
                       //    if (result.IsSuccess == true)
                       //    {
                       //        ShowResult = ShowResult.Yes;
                       //        Close();
                       //        Toast("������ɾ����", ToastLength.SHORT);
                       //    }
                       //    else
                       //    {
                       //        throw new Exception(result.ErrorInfo);
                       //    }
                       //}
                   }
               });
        }
    }
}