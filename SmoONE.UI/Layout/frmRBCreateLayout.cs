using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.UI.RB;

namespace SmoONE.UI.Layout
{
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmRBCreateLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ����ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ((frmRBCreate)(this.Form)).upCheckState();    //����ȫѡ��״̬
                ((frmRBCreate)(this.Form)).getAmount();         //���㵱ǰѡ�������ܽ��
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }
        /// <summary>
        /// ��ȡ��ǰ���Ƿ�ѡ��
        /// </summary>
        public int checkNum()
        {
            if (Check.Checked == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// ��ȡ��ǰѡ���б�����ϸID
        /// </summary>
        /// <returns></returns>
        public int getID()
        {
            return Convert.ToInt32(imgType.BindDataValue);
        }
        /// <summary>
        /// ��ȡѡ��������
        /// </summary>
        /// <returns></returns>
        public decimal getNum()
        {
            if (Check.Checked == true)
            {
                return Convert.ToDecimal(lblMoney.BindDataValue);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// ����ÿ��checkBox״̬
        /// </summary>
        public void setCheck(bool state)
        {
            Check.Checked = state;
        }
        /// <summary>
        /// �������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plContent_Press(object sender, EventArgs e)
        {
            try
            {
                if (Check.Checked == true)
                {
                    Check.Checked = false;
                }
                else
                {
                    Check.Checked = true;
                }
                ((frmRBCreate)(this.Form)).upCheckState();          //����ȫѡ��״̬
                ((frmRBCreate)(this.Form)).getAmount();         //���㵱ǰѡ�������ܽ�� 
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message);
            }
        }
    }
}