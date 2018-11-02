using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.Data;

namespace SmoONE.UI.FileUp
{
    partial class frmFile : Smobiler.Core.Controls.MobileForm
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFile_Load(object sender, EventArgs e)
        {
            Bind();
        }

        private void Bind()
        {
            try
            {
                listView1.Rows.Clear();//��������
                DataTable table = new DataTable();
                table.Columns.Add("FImg", typeof(System.String));
                table.Columns.Add("FileName", typeof(System.String));
                table.Rows.Add("word", "�ɼ���.doc");
                table.Rows.Add("Excel", "�ʲ�.xls");
                table.Rows.Add("ppt", "SombilerApp.ppt");
                table.Rows.Add("zip", "1.rar");
                listView1.DataSource = table;
                listView1.DataBind ();

            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }

        /// <summary>
        /// �ֻ����ļ��嵥
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFile_Press(object sender, EventArgs e)
        {
            try
            {
                //�����ļ��嵥
                frmFileDetail frmFileDetail = new frmFileDetail();
                this.Show(frmFileDetail);

            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
    }
}