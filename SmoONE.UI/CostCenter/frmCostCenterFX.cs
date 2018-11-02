using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;

namespace SmoONE.UI.CostCenter
{
        // ******************************************************************
        // �ļ��汾�� SmoONE 1.0
        // Copyright  (c)  2016-2017 Smobiler
        // ����ʱ�䣺 2016/11
        // ��Ҫ���ݣ� �ɱ������б����
        // ******************************************************************
        partial class frmCostCenterFX : Smobiler.Core.Controls.MobileForm
        {
            #region "definition"
            AutofacConfig AutofacConfig = new AutofacConfig();//����������
            #endregion

            /// <summary>
            /// ��ʼ���¼�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void frmCostCenterFX_Load(object sender, EventArgs e)
            {
                Bind();
            }
            /// <summary>
            /// ��ʼ������
            /// </summary>
            public void Bind()
            {
                //��ȡ���гɱ���������
                List<CCDto> listCC = AutofacConfig.costCenterService.GetAllCC();

                gridCCData.Rows.Clear();//��ճɱ������б�����
                if (listCC.Count > 0)
                {
                    foreach (CCDto cc in listCC)
                    {
                        UserDetailDto user = AutofacConfig.userService.GetUserByUserID(cc.CC_LiableMan);
                        cc.CC_LiableMan = user.U_Name;

                    }
                  
                    gridCCData.DataSource = listCC;
                    gridCCData.DataBind();
                }
                
            }
            /// <summary>
            /// �ֻ��Դ����˰�ť�¼�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void frmCostCenterFX_KeyDown(object sender, KeyDownEventArgs e)
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
            private void frmCostCenterFX_TitleImageClick(object sender, EventArgs e)
            {
                Close();
            }
        }
    }
