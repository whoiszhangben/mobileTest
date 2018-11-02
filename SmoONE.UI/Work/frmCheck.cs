using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.UI;
using SmoONE.DTOs;

namespace SmoONE.UI.Work
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ���������б����
    // ******************************************************************
    partial class frmCheck : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        internal string type = "";//����
        internal string state = "";//״̬
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCheck_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            
            }
        }

        private void frmCheck_Load(object sender, EventArgs e)
        {
            type = "";
            state = "";
            Bind();
        }
        /// <summary>
        /// ѡ��������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void segmentedControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            type = "";
            state = "";
            Bind();
        }
        /// <summary>
        /// ��ȡ��ʼ������
        /// </summary>
        public void Bind()
        {
            try
            {
                List<LeaveDto> listLeaveDto = new List<LeaveDto>();
                List<ReimbursementDto> listRBDto = new List<ReimbursementDto>();
                switch (segmentedControl1.SelectedIndex)
                {
                    case 0:
                        //��ȡ��ǰ�û����������������
                        listLeaveDto = AutofacConfig.leaveService.GetNewByCheckUsers(Client.Session["U_ID"].ToString());
                        //��ȡ��ǰ�û��������ı�������
                        listRBDto = AutofacConfig.rBService.GetNewByCheckUsers(Client.Session["U_ID"].ToString());
                        break;
                    case 1:
                        //��ȡ��ǰ�û����������������
                        listLeaveDto = AutofacConfig.leaveService.GetCheckedByCheckUsers(Client.Session["U_ID"].ToString());
                        //��ȡ��ǰ�û��������ı�������
                        listRBDto = AutofacConfig.rBService.GetCheckedByCheckUsers(Client.Session["U_ID"].ToString());
                        break;
                }
                List<DataGridview> listCheck = new List<DataGridview>();//��������
                UserDetails userDetails = new UserDetails();
                //������������������������������������0������ӵ���������                
                if (listLeaveDto.Count > 0)
                {
                    foreach (LeaveDto leave in listLeaveDto)
                    {
                        DataGridview dataGItem = new DataGridview();
                        dataGItem.ID = leave.L_ID;
                        if (string.IsNullOrEmpty(leave.U_Portrait) == true)
                        {
                            UserDetailDto user = userDetails.getUser(leave.U_ID);
                            if (user != null)
                            {
                                dataGItem.U_Portrait = user.U_Portrait;
                            }
                        }
                        else
                        {
                            dataGItem.U_Portrait = leave.U_Portrait;
                        }
                        dataGItem.Name = leave.U_Name + "��" + DataGridviewType.���;
                        dataGItem.Type = ((int)Enum.Parse(typeof(DataGridviewType), DataGridviewType.���.ToString())).ToString();
                        dataGItem.CreateDate = leave.L_CreateDate.ToString("yyyy/MM/dd");
                        switch (leave.L_Status)
                        {
                            case (int)L_Status.�½�:
                                if (string.IsNullOrEmpty(leave.L_CheckUsers) == false)
                                {
                                    dataGItem.L_StatusDesc = "�ȴ�������";
                                }
                                else
                                {
                                    dataGItem.L_StatusDesc = "�ȴ�������";
                                }
                                break;
                            case (int)L_Status.������:
                                dataGItem.L_StatusDesc = "����������ɣ�";
                                break;
                            case (int)L_Status.�Ѿܾ�:
                                dataGItem.L_StatusDesc = "���������ܾ���";
                                break;
                        }
                        listCheck.Add(dataGItem);
                    }
                }

                //��������������������ı���������������0������ӵ���������     
                if (listRBDto.Count > 0)
                {
                    foreach (ReimbursementDto reimbursement in listRBDto)
                    {
                        DataGridview dataGItem = new DataGridview();
                        dataGItem.ID = reimbursement.RB_ID;
                        if (string.IsNullOrEmpty(reimbursement.U_Portrait) == true)
                        {
                            UserDetailDto user = userDetails.getUser(reimbursement.U_ID);
                            if (user != null)
                            {
                                dataGItem.U_Portrait = user.U_Portrait;
                            }
                        }
                        else
                        {
                            dataGItem.U_Portrait = reimbursement.U_Portrait;
                        }
                        dataGItem.Name = reimbursement.U_Name + "��" + DataGridviewType.����;
                        dataGItem.Type = ((int)Enum.Parse(typeof(DataGridviewType), DataGridviewType.����.ToString())).ToString();
                        dataGItem.CreateDate = reimbursement.RB_CreateDate.ToString("yyyy/MM/dd");
                        switch (reimbursement.RB_Status)
                        {
                            case (int)RB_Status.�½�:
                                switch (segmentedControl1.SelectedIndex)
                                {
                                    case 0:
                                        dataGItem.L_StatusDesc = "�ȴ�����������";
                                        break;
                                }
                                break;
                            case (int)RB_Status.����������:
                                switch (segmentedControl1.SelectedIndex)
                                {
                                    case 0:
                                        dataGItem.L_StatusDesc = "�ȴ���������";
                                        break;
                                    case 1:
                                        dataGItem.L_StatusDesc = "������������";
                                        break;
                                }
                                break;
                            case (int)RB_Status.��������:
                                switch (segmentedControl1.SelectedIndex)
                                {
                                    case 0:
                                        dataGItem.L_StatusDesc = "�ȴ���������";
                                        break;
                                    case 1:
                                        dataGItem.L_StatusDesc = "����������";
                                        break;
                                }
                                break;
                            case (int)RB_Status.��������:
                                switch (segmentedControl1.SelectedIndex)
                                {
                                    case 1:
                                        dataGItem.L_StatusDesc = "����������";
                                        break;
                                }
                                break;
                            case (int)RB_Status.�Ѿܾ�:
                                dataGItem.L_StatusDesc = "���������ܾ���";
                                break;
                        }
                        listCheck.Add(dataGItem);
                    }
                }
                listCheckData.Rows.Clear();//����������б�����
                if (listCheck.Count > 0)
                {
                    listCheckData.DataSource = listCheck;//��gridview����
                    listCheckData.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// ��ȡɸѡ����
        /// </summary>
        private void GetCheckData()
        {
            List<LeaveDto> listLeaveDto = new List<LeaveDto>();
            List<ReimbursementDto> listRBDto = new List<ReimbursementDto>();
            if (string.IsNullOrWhiteSpace(type) == false & string.IsNullOrWhiteSpace(state) == false)
            {
                switch (Convert.ToInt32(type))
                {
                    case (int)DataGridviewType.���:
                        switch (segmentedControl1.SelectedIndex)
                        {
                            case 0:
                                //��ȡ��ǰ�û����������������
                                listLeaveDto = AutofacConfig.leaveService.GetNewByCheckUsers(Client.Session["U_ID"].ToString());
                                break;
                            case 1:
                                //�������״̬��ȡ��ǰ�û����������������
                                listLeaveDto = AutofacConfig.leaveService.QueryCheckedByCheckUsers(Client.Session["U_ID"].ToString(), Convert.ToInt32(state));
                                break;
                        }
                        break;
                    case (int)DataGridviewType.����:
                        switch (segmentedControl1.SelectedIndex)
                        {
                            case 0:
                                //���ݱ���״̬��ȡ��ǰ�û��������ı�������
                                listRBDto = AutofacConfig.rBService.QueryNewByCheckUsers(Client.Session["U_ID"].ToString(), Convert.ToInt32(state));
                                break;
                            case 1:
                                //���ݱ���״̬��ȡ��ǰ�û��������ı�������
                                listRBDto = AutofacConfig.rBService.QueryCheckedByCheckUsers(Client.Session["U_ID"].ToString(), Convert.ToInt32(state));
                                break;
                        }

                        break;
                }
            }
            List<DataGridview> listCheck = new List<DataGridview>();//��������

            //������������������������������������0������ӵ���������     
            if (listLeaveDto.Count > 0)
            {
                foreach (LeaveDto leave in listLeaveDto)
                {
                    DataGridview dataGItem = new DataGridview();
                    dataGItem.ID = leave.L_ID;
                    if (string.IsNullOrEmpty(leave.U_Portrait) == true)
                    {
                        UserDetailDto user = AutofacConfig.userService.GetUserByUserID(leave.U_ID);
                        switch (user.U_Sex)
                        {
                            case (int)Sex.��:
                                dataGItem.U_Portrait = "boy";
                                break;
                            case (int)Sex.Ů:
                                dataGItem.U_Portrait = "girl";
                                break;
                        }
                    }
                    else
                    {
                        dataGItem.U_Portrait = leave.U_Portrait;
                    }
                    dataGItem.Name = leave.U_Name + "��" + DataGridviewType.���;
                    dataGItem.Type = ((int)Enum.Parse(typeof(DataGridviewType), DataGridviewType.���.ToString())).ToString();
                    switch (leave.L_Status)
                    {
                        case (int)L_Status.�½�:
                            dataGItem.L_StatusDesc = "�ȴ�����";
                            break;
                        case (int)L_Status.������:
                            dataGItem.L_StatusDesc = "����������ɣ�";
                            break;
                        case (int)L_Status.�Ѿܾ�:
                            dataGItem.L_StatusDesc = "���������ܾ���";
                            break;
                    }
                    listCheck.Add(dataGItem);
                }
            }

            //��������������������ı���������������0������ӵ���������     
            if (listRBDto.Count > 0)
            {
                foreach (ReimbursementDto reimbursement in listRBDto)
                {
                    DataGridview dataGItem = new DataGridview();
                    dataGItem.ID = reimbursement.RB_ID;
                    if (string.IsNullOrEmpty(reimbursement.U_Portrait) == true)
                    {
                        UserDetailDto user = AutofacConfig.userService.GetUserByUserID(reimbursement.U_ID);
                        switch (user.U_Sex)
                        {
                            case (int)Sex.��:
                                dataGItem.U_Portrait = "boy";
                                break;
                            case (int)Sex.Ů:
                                dataGItem.U_Portrait = "girl";
                                break;
                        }
                    }
                    else
                    {
                        dataGItem.U_Portrait = reimbursement.U_Portrait;
                    }
                    dataGItem.Name = reimbursement.U_Name + "��" + DataGridviewType.����;
                    dataGItem.Type = ((int)Enum.Parse(typeof(DataGridviewType), DataGridviewType.����.ToString())).ToString();
                    switch (reimbursement.RB_Status)
                    {
                        case (int)RB_Status.�½�:
                            switch (segmentedControl1.SelectedIndex)
                            {
                                case 0:
                                    dataGItem.L_StatusDesc = "�ȴ�����������";
                                    break;
                            }
                            break;
                        case (int)RB_Status.����������:
                            switch (segmentedControl1.SelectedIndex)
                            {
                                case 0:
                                    dataGItem.L_StatusDesc = "�ȴ���������";
                                    break;
                                case 1:
                                    dataGItem.L_StatusDesc = "������������";
                                    break;
                            }
                            break;
                        case (int)RB_Status.��������:
                            switch (segmentedControl1.SelectedIndex)
                            {
                                case 0:
                                    dataGItem.L_StatusDesc = "�ȴ���������";
                                    break;
                                case 1:
                                    dataGItem.L_StatusDesc = "����������";
                                    break;
                            }
                            break;
                        case (int)RB_Status.��������:
                            switch (segmentedControl1.SelectedIndex)
                            {
                                case 1:
                                    dataGItem.L_StatusDesc = "����������";
                                    break;
                            }
                            break;
                        case (int)RB_Status.�Ѿܾ�:
                            dataGItem.L_StatusDesc = "���������ܾ���";
                            break;
                    }
                    listCheck.Add(dataGItem);
                }
            }
            listCheckData.Rows.Clear();//����б�����
            if (listCheck.Count > 0)
            {
                listCheckData.DataSource = listCheck;
                listCheckData.DataBind();
            }
        }
        /// <summary>
        /// ɸѡ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popList1_Selected(object sender, EventArgs e)
        {
            if (popList1.Selection != null)
            {
                foreach (PopListItem poitem in popList1.Selections)
                {

                    type = (popList1.Selection.Value.ToString()).Split('/')[0].ToString();
                    state = (popList1.Selection.Value.ToString()).Split('/')[1].ToString();
                }
                GetCheckData();
            }
        }
       
        /// <summary>
        /// ɸѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tpSearch_Press(object sender, EventArgs e)
        {
            popList1.Groups.Clear();
            DataCheckPoplist poplist = new DataCheckPoplist();
            System.Data.DataTable grouptable = poplist.GetPopGroup();
            if (grouptable.Rows.Count > 0)
            {
                foreach (System.Data.DataRow Grow in grouptable.Rows)
                {
                    PopListGroup poli = new PopListGroup();
                    popList1.Groups.Add(poli);
                    poli.Title  = Grow["GroupName"].ToString();
                    System.Data.DataTable table = new System.Data.DataTable();
                    switch (segmentedControl1.SelectedIndex)
                    {
                        case 0:
                            table = poplist.GetPendingCheckPopItem();
                            break;
                        case 1:
                            table = poplist.GetCheckPopItem();
                            break;
                    }
                    if (table.Rows.Count > 0)
                    {
                        foreach (System.Data.DataRow rowli in table.Rows)
                        {
                            if (rowli["ParentID"].ToString().Equals(Grow["GroupID"].ToString()))
                            {
                                poli.AddListItem(rowli["PopItemName"].ToString(), rowli["ParentID"].ToString() + "/" + rowli["Status"].ToString());
                                if (type.Trim().Length > 0 & state.Trim().Length > 0)
                                {
                                    if ((type.Trim() + "/" + state.Trim()).Equals(rowli["ParentID"].ToString() + "/" + rowli["Status"].ToString()))
                                    {
                                        popList1.SetSelections(poli.Items[(poli.Items.Count - 1)]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            popList1.Show();
        }
    }
}