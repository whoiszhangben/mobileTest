using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;

namespace SmoONE.UI.Attendance
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.1
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2017/2
    // ��Ҫ���ݣ� �û�������־������Աͳ�ƽ���
    // ******************************************************************
    partial class frmAttendanceStatUser : Smobiler.Core.Controls.MobileForm
    {

        #region "definition"
        public  string atType;         //����
        public string atDate;//�����·�
        public string atTypeUser;//�û�
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��ʼ��ҳ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatUser_Load(object sender, EventArgs e)
        {
            lblATMonth.Text = Convert.ToDateTime(atDate).ToString("yyyy��  M��");
            GetATUser();
        }
        /// <summary>
        /// ��ȡ�û�
        /// </summary>
        private void GetATUser()
        {
            try
            {
                gridATUserData.Rows.Clear();//�����Ա�б�����
                //����п���ѡ���û�������ӵ������û�����listATUser����Ĭ���û�״̬Ϊѡ��
                if (string.IsNullOrEmpty(atType) == false )
                {
                        if (string.IsNullOrEmpty(atTypeUser ) == false)
                        {
                             List<UserDto> listUser = new List<UserDto> ();
                             string[] atTypeUsers = atTypeUser.Split(',');
                             foreach (string user in atTypeUsers)
                            {
                                UserDetails userDetails = new UserDetails();
                                UserDetailDto userDetailDto = userDetails.getUser(user);
                                if (userDetailDto != null)
                                {
                                    UserDto userDto = new UserDto();
                                    userDto.U_ID = userDetailDto.U_ID;
                                    userDto.U_Name = userDetailDto.U_Name;
                                    userDto.U_Portrait = userDetailDto.U_Portrait;
                                    listUser.Add(userDto);
                                }
                            }
                            gridATUserData.DataSource = listUser; //��ListView����
                            gridATUserData.DataBind();
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        
      

        /// <summary>
        /// �ֻ��Դ����ؼ����˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatUser_KeyDown(object sender, KeyDownEventArgs e)
        {

            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }

        /// <summary>
        /// ���Ͻǰ�ť���˳�ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAttendanceStatUser_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }

      
    }
}