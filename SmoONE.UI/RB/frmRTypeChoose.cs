using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.Data;
using SmoONE.UI;

namespace SmoONE.UI.RB
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 2.0
    // Copyright  (c)  2017-2018 Smobiler 
    // ����ʱ�䣺 2017/07
    // ��Ҫ���ݣ�  ��������ѡ���б�
    // ******************************************************************
    partial class frmRTypeChoose : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        internal string TYPEID;                //�������ͱ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRTypeChoose_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                this.Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRTypeChoose_Load(object sender, EventArgs e)
        {
            Bind();//�����ݿ����ݲ���
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <remarks></remarks>
        private void Bind()
        {
            try
            {
                //��ʾ���ݱ�������
                List<SmoONE.Domain.RB_RType> Types = AutofacConfig.rBService.GetAllType();
                DataTable typetable = new DataTable();
                typetable.Columns.Add("TYPE", typeof(System.String));          //�������ͱ��
                typetable.Columns.Add("TYPENAME", typeof(System.String));      //������������
                foreach (SmoONE.Domain.RB_RType row in Types)
                {
                    typetable.Rows.Add(row.RB_RT_ID, row.RB_RT_Name);
                }
                this.listRBRowTypeData.Rows.Clear();//�����������ѡ���б�����
                if (typetable.Rows.Count > 0)
                {
                    this.listRBRowTypeData.DataSource = typetable;
                    this.listRBRowTypeData.DataBind();
                }

            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}