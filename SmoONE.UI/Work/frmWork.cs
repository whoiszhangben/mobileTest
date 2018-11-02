using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.UI;
using SmoONE.Domain;
using SmoONE.UI.UserInfo;
using SmoONE.DTOs;

namespace SmoONE.UI.Work
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ��������
    // ******************************************************************
    partial class frmWork : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        /// <summary>
        /// �˵����ֵ�
        /// </summary>
        /// <remarks></remarks>
        private Dictionary<string, IconMenuViewGroup> MenuGroupDict;//�����˵�
        private DateTime toasttime;//toastʱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion

        /// <summary>
        /// IconMenuDate����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iconMenuData_ItemPress(object sender, IconMenuViewItemPressEventArgs e)
        {
            MenuItem(e.Item.ID);
           
        }
        /// <summary>
        /// �˵�����¼�����
        /// </summary>
        /// <param name="id"></param>
        private void MenuItem(string id)
        {
            if (MenuGroupDict.ContainsKey(id) == true)
            {
                //��ʾ��ǰ�˵��Ķ����˵�
                this.iconMenuData.ShowDialogMenu(MenuGroupDict[id]);
            }
            else
            {
                switch (id)
                {
                    ////�������
                    //case "Leave":
                    //    Leave.frmLeaveCreate frmLeaveCreate = new Leave.frmLeaveCreate();
                    //    //��ٴ���������Ӳ໬�رչ��ܣ���Show�н�����moveCloseΪTrue
                    //    Show(frmLeaveCreate,true);
                    //    break;
                    ////��������
                    //case "Reimbursement":
                    //    RB.frmRBCreate frmRBCreate = new RB.frmRBCreate();
                    //    Show(frmRBCreate);
                    //    break;
                    ////�������Ѽ�¼
                    //case "RB_Rows":
                    //    RB.frmRBRows frmRBRows = new RB.frmRBRows();
                    //    Show(frmRBRows);
                    //    break;
                    ////�������Ѽ�¼ģ��
                    //case "RB_RType_Template":
                    //    RB.frmRTypeTemplate frmRTypeTemplate = new RB.frmRTypeTemplate();
                    //    Show(frmRTypeTemplate);
                    //    break;
                    ////��������
                    //case "Department":
                    //    Department.frmDepartment frmDepartment = new Department.frmDepartment();
                    //    Show(frmDepartment);
                    //    break;
                    ////�����ɱ�����
                    //case "CostCenter":
                    //    CostCenter.frmCostCenter frmCostCenter = new CostCenter.frmCostCenter();
                    //    Show(frmCostCenter);
                    //    break;
                    //    //�ɱ����ķ���
                    //case "CCFX":
                    //    CostCenter.frmCostCenterFX frmCostCenterFX = new CostCenter.frmCostCenterFX();
                    //    Show(frmCostCenterFX);
                    //    break;
                    ////�����ɱ�����ģ��
                    //case "CC_Type_Template":
                    //    CostCenter.frmCostTemplet frmCostTemplet = new CostCenter.frmCostTemplet();
                    //    Show(frmCostTemplet);
                    //    break;
                    ////���ڹ���ģ��
                    //case "AttendanceManagement":
                    //    Attendance.frmAttendanceManager frmAttendanceManager = new Attendance.frmAttendanceManager();
                    //    Show(frmAttendanceManager);
                    //    break;
                    ////����
                    //case "AttendanceInfo":
                    //    Attendance.frmAttendanceMain frmAttendanceMain = new Attendance.frmAttendanceMain();
                    //    frmAttendanceMain.enter = (int)Enum.Parse(typeof(ATMainState), ATMainState.����ǩ��.ToString());
                    //    Show(frmAttendanceMain);
                    //    break;
                    ////�ҵĿ�����ʷ
                    //case "MyAttendanceHistory":
                    //    Attendance.frmAttendanceStatSelfDay frmAttendanceStatSelfDay = new Attendance.frmAttendanceStatSelfDay();
                    //    Show(frmAttendanceStatSelfDay);
                    //    break;
                    ////����ͳ��
                    //case "AttendanceStatistics":
                    //    Attendance.frmAttendanceStatistics frmAttendanceStatistics = new Attendance.frmAttendanceStatistics();
                    //    Show(frmAttendanceStatistics);
                    //    break;
                    ////IM
                    //case "IM":
                    //    SmoONE.UI.Im.frmConcent frmConcent = new SmoONE.UI.Im.frmConcent();
                    //    Show(frmConcent);
                    //    break;
                    ////�ļ��ϴ�
                    //case "FileUp":
                    //    SmoONE.UI.FileUp.frmFile frmFile = new SmoONE.UI.FileUp.frmFile ();
                    //    Show(frmFile);
                    //    break;
                }
            }
        }
        /// <summary>
        /// Toolbar����
        /// </summary>
        /// <param name="toolbarItemName"></param>
        private void ProcessToolbarFormName(string toolbarItemName)
        {
            try
            {
                switch (toolbarItemName)
                {
                    case "":
                        this.Close();
                        break;
                    case "Me":
                        frmUser frm = new frmUser();
                        this.Show(frm, (MobileForm sender1, object args) =>
                        {
                            ProcessToolbarFormName(frm.toolbarItemName);
                            UI.Layout.LeftPage lp = this.Drawer as UI.Layout.LeftPage;
                            lp.getUser();
                        }
                        );
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Toolbar����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolBar1_ToolbarItemClick(object sender, ToolbarClickEventArgs e)
        {
            ProcessToolbarFormName(e.Name);
            
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWork_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                HandleToast();
            }
        }
        /// <summary>
        /// Toast
        /// </summary>
        private void HandleToast()
        {
            if (toasttime.AddSeconds(3) >= DateTime.Now)
            {
                this.Client.Exit();
            }
            else
            {
                toasttime = DateTime.Now;
                this.Toast("�ٰ�һ���˳�ϵͳ", ToastLength.SHORT);
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWork_Load(object sender, EventArgs e)
        {
           
          //  this.DrawerWidth = (int)Math.Floor(260 * (float)((this.Client.ScreenSize.Width / this.Client.ScreenDensity) / this.Width));
            MenuGroupDict = new Dictionary<string, IconMenuViewGroup>();
            //��ȡ�˵�
            MenuGroup();
           CreateScreenGestures();
        }
        /// <summary>
        ///��ȡ�˵�
        /// </summary>
        private void MenuGroup()
        {
            try
            {
                List<Menu> listmenu = AutofacConfig.userService.GetMenuByUserID(Client.Session["U_ID"].ToString());
                this.iconMenuData.Groups.Clear();
                MenuGroupDict.Clear();
                IconMenuViewGroup grp = new IconMenuViewGroup("");
                //��ȡ���в˵���
                foreach (Menu menu in listmenu)
                {
                    if (string.IsNullOrWhiteSpace(menu.M_ParentID) == true)
                    {
                        //���һ���˵�
                        grp.Items.Add(new IconMenuViewItem(menu.M_MenuID, menu.M_Portrait, menu.M_Description, menu.M_MenuID, ""));
                        //��Ӷ����˵�
                        List<Menu> listsecondMenu = AutofacConfig.userService.GetSubMenuByUserID(Client.Session["U_ID"].ToString(), menu.M_MenuID);
                        if (listsecondMenu.Count > 0)
                        {
                            Menu menuItem = AutofacConfig.userService.GetMenuByMenuID(menu.M_MenuID);
                            IconMenuViewGroup mvGroupItem = new IconMenuViewGroup(menuItem.M_Description);
                            foreach (Menu secondMenu in listsecondMenu)
                            {
                                mvGroupItem.Items.Add(new IconMenuViewItem(secondMenu.M_MenuID, secondMenu.M_Portrait, secondMenu.M_Description, secondMenu.M_MenuID, ""));
                                if (MenuGroupDict.ContainsKey(menu.M_MenuID) == false)
                                    MenuGroupDict.Add(menu.M_MenuID, mvGroupItem);
                            }
                        }
                    }
                }
                this.iconMenuData.Groups.Add(grp);
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private void plShenPi_Press(object sender, EventArgs e)
        {
            //frmCheck frmCheck = new frmCheck();
            //Show(frmCheck);
        }

        private void plFaQi_Press(object sender, EventArgs e)
        {
            //frmCreated frmCreated = new frmCreated();
            //Show(frmCreated);
        }

        private void plChaoSong_Press(object sender, EventArgs e)
        {
            //frmCCTo frmCCTo = new frmCCTo();
            //Show(frmCCTo);
        }


        /// <summary>
        /// ��������
        /// </summary>
        /// <remarks></remarks>
        private void CreateScreenGestures()
        {
            this.Client.Pattern.Password = null;
            UserDetailDto user = AutofacConfig.userService.GetUserByUserID(Client.Session["U_ID"].ToString());
            if (user != null)
            {
                if (string.IsNullOrEmpty(user.U_Gestures) == false)
                {
                    this.Client.Pattern.Password = user.U_Gestures;
                }
            }
            else
            {
                Toast("�û�" + Client.Session["U_ID"].ToString() + "�����ڣ�", ToastLength.SHORT);
            }


            if (string .IsNullOrEmpty (this.Client.Pattern.Password ) == true || string.IsNullOrWhiteSpace(this.Client.Pattern.Password) == true)
            {
                this.Client.Pattern.Create((object s1, Smobiler.Core.RPC .PatternCreatedEventArgs ee) =>
                {
                    if (ee.isError == true)
                    {
                        Toast (ee.error,ToastLength.SHORT);
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(ee.Password) == false)
                        {
                            try
                            {
                                this.Client.Pattern.Password = ee.Password;
                                //���ݿ⸳ֵ
                                UserInputDto upuser = new UserInputDto();
                                upuser.U_ID = Client.Session["U_ID"].ToString();
                                upuser.U_Gestures = Client.Pattern.Password;
                                CommLib.ReturnInfo result = AutofacConfig.userService.UpdateUser(upuser);
                                if (result.IsSuccess == false )
                                {
                                    Toast(result.ErrorInfo, ToastLength.SHORT);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                        }
                    }
                });
            }
           
        }
        /// <summary>
        /// ��ʾ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void title1_ImagePress(object sender, EventArgs e)
        {
            OpenDrawer();
        }
        /// <summary>
        /// action�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmWork_ActionButtonPress(object sender, ActionButtonPressEventArgs e)
        {
            try
            {
                switch (e.Index )
                {
                    //�������
                    case 0:
                        Leave.frmLeaveCreate frmLeaveCreate = new Leave.frmLeaveCreate();
                        Show(frmLeaveCreate);
                        break;
                    //�������Ѽ�¼
                    case 1:
                        RB.frmRBRows frmRBRows = new RB.frmRBRows();
                        Show(frmRBRows);
                        break;
                    //��������
                    case 2:
                        RB.frmRBCreate frmRBCreate = new RB.frmRBCreate();
                        Show(frmRBCreate);
                        break;                   
                  
                }
            }
            catch(Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
    }
}