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
    // ��Ҫ���ݣ�  �Ҵ������б����
    // ******************************************************************
    partial class frmCreated : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCreated_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCreated_Load(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// ��ȡ��ʼ������
        /// </summary>
        public void Bind()
        {
            try
            {
                //��ȡ��ǰ�û��������������
                List<LeaveDto> listLeaveDto = AutofacConfig.leaveService.GetByCreateUsers(Client.Session["U_ID"].ToString());
                //��ȡ��ǰ�û������ı�������
                List<ReimbursementDto> listRBDto = AutofacConfig.rBService.GetByCreateUsers(Client.Session["U_ID"].ToString());
                List<DataGridview> listCreated = new List<DataGridview>();//�ҷ��������
                UserDetails userDetails = new UserDetails();
                //������������������0������ӵ��ҷ��������
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
                                dataGItem.L_StatusDesc = "�ȴ�����";
                                break;
                            case (int)L_Status.������:
                                dataGItem.L_StatusDesc = "����������ɣ�";
                                break;
                            case (int)L_Status.�Ѿܾ�:
                                dataGItem.L_StatusDesc = "���������ܾ���";
                                break;
                        }
                        listCreated.Add(dataGItem);
                    }
                }

                //�������������������0������ӵ��ҷ��������
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
                                dataGItem.L_StatusDesc = "�ȴ�����������";
                                break;
                            case (int)RB_Status.����������:
                                dataGItem.L_StatusDesc = "�ȴ���������";
                                break;
                            case (int)RB_Status.��������:
                                dataGItem.L_StatusDesc = "�ȴ���������";
                                break;
                            case (int)RB_Status.��������:
                                dataGItem.L_StatusDesc = "����������";
                                break;
                            case (int)RB_Status.�Ѿܾ�:
                                dataGItem.L_StatusDesc = "���������ܾ���";
                                break;
                        }
                        listCreated.Add(dataGItem);
                    }
                }
                listCrateData.Rows.Clear();//����ҷ�����б�����
                if (listCreated.Count > 0)
                {
                    listCrateData.DataSource = listCreated;//��gridView����
                    listCrateData.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
    }
}