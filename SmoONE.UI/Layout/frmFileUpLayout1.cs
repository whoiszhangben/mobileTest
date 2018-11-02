using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;

namespace SmoONE.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmFileUpLayout1 : Smobiler.Core.Controls.MobileUserControl
    {
        private void tpFileOpen_Press(object sender, EventArgs e)
        {
            //�ļ���
            this.Client.File.Open(lblFile.BindDisplayValue.ToString(), MobileResourceManager.DefaultDocumentResourceName, (obj1, args1) => {
                if (args1.isError == true)
                {
                    throw new Exception(args1.error);
                }
            });
        }
        
        
        /// <summary>
        /// �ļ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Press(object sender, EventArgs e)
        {
            try
            {
                //�ļ���
                this.Client.File.Open(lblFile.BindDisplayValue.ToString(), MobileResourceManager.DefaultDocumentResourceName, (obj1, args1) => {
                    if (args1.isError == true)
                    {
                        throw new Exception(args1.error);
                    }
                });

            }

            catch (Exception ex)
            {
                this.Form.Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// �ļ��ϴ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Press(object sender, EventArgs e)
        {
            try
            {
                //�ļ��ϴ�
                this.Client.File.Upload((obj, args) => {
                    if (args.isError == false)
                    {
                        try
                        {
                            args.SaveFile(args.ResourceID, MobileResourceManager.DefaultDocumentPath);
                            this.Form.Toast("�ϴ��ɹ���",ToastLength.SHORT );
                        }
                        catch (Exception ex)
                        {
                            this.Form.Toast(ex.Message, ToastLength.SHORT);
                        }
                    }
                }
                );
            }
            catch (Exception ex)
            {
                this.Form.Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// �ļ�ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Press(object sender, EventArgs e)
        {
            try
            {

                this.Client.File.Exists(lblFile.BindDisplayValue.ToString(), MobileResourceManager.DefaultDocumentResourceName, (obj, args) => {
                    if (args.Exists == true)
                    {
                        //ɾ���ֻ����ļ�
                        this.Client.File.Delete(lblFile.BindDisplayValue.ToString(), MobileResourceManager.DefaultDocumentResourceName, (obj1, args1) => {
                            if (args1.isError == true  )
                            {
                                throw new Exception(args1.error);
                            }
                            else
                            {
                                ListViewRow row = this.Tag as ListViewRow;
                                ((FileUp.frmFileDetail)(this.Form)).RemoveRow(row);//ɾ����ǰ�б�����
                               

                            }
                        });
                    }
                    else
                    {
                        this.Form.Toast("�ļ������ڣ��������ļ����ֻ���");
                    }
                });

            }

            catch (Exception ex)
            {
                this.Form.Toast(ex.Message, ToastLength.SHORT);
            }
        }
    }
}