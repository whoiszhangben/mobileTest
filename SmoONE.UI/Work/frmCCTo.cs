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
    // ��Ҫ���ݣ�  �����ҵ��б����
    // ******************************************************************
    partial class frmCCTo : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCCTo_Load(object sender, EventArgs e)
        {
            Bind();
        }/// <summary>
         /// ��ȡ��ʼ������
         /// </summary>
        public void Bind()
        {
            try
            {
                List<DataGridview> listCCTo = new List<DataGridview>();//�����ҵ�����
                //��ȡ���͸���ǰ�û����������
                List<LeaveDto> listLeaveDto = AutofacConfig.leaveService.GetByCCTo(Client.Session["U_ID"].ToString());
                //������������������0������ӵ������ҵ�����
                if (listLeaveDto.Count > 0)
                {
                    foreach (LeaveDto leave in listLeaveDto)
                    {
                        DataGridview dataGItem = new DataGridview();
                        dataGItem.ID = leave.L_ID;
                        if (string.IsNullOrEmpty(leave.U_Portrait) == true)
                        {
                            UserDetails userDetails = new UserDetails();
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
                        listCCTo.Add(dataGItem);
                    }
                }

                listCCData.Rows.Clear();//��������ҵ��б�����
                if (listCCTo.Count > 0)
                {
                    //��gridView����
                    listCCData.DataSource = listCCTo;
                    listCCData.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCCTo_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
    }
}