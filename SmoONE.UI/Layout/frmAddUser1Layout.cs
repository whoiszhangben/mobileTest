using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using Smobiler.Plugins.RongIM;
using SmoONE.DTOs;
using SmoONE.CommLib;

namespace SmoONE.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmAddUser1Layout : Smobiler.Core.Controls.MobileUserControl
    {
        #region "definition"
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
       
        private void btnAdd_Press(object sender, EventArgs e)
        {
            try
            {
                ContactInputDto cInputDto = new ContactInputDto();
                cInputDto.C_USER = lblUser.BindDataValue.ToString();
                cInputDto.C_CreateUser  = Client.Session["U_ID"].ToString();
                cInputDto.C_UpdateUser  = Client.Session["U_ID"].ToString();
                ReturnInfo r = AutofacConfig.contactService.AddContact(cInputDto);
                if (r.IsSuccess == true)
                {
                    if (((SmoONE.UI.Im.frmAddConcentOrGroup)this.Form).im1 != null)
                    {
                        //�����ϵ��
                        ((SmoONE.UI.Im.frmAddConcentOrGroup)this.Form).im1.CreateUser(cInputDto.C_USER, lblUser.BindDisplayValue.ToString(),System.IO.Path.Combine( MobileResourceManager.DefaultImagePath ,imgPortrait .BindDisplayValue .ToString ()));
                    }
                    this.Form .ShowResult = ShowResult.Yes;
                    this.Form .Close();
                    this.Form .Toast ("��ϵ����ӳɹ���", ToastLength.SHORT);
                }
                else
                {
                    throw new Exception(r.ErrorInfo);
                }
            }
           catch (Exception ex)
            {
                this.Form.Toast(ex.Message , ToastLength.SHORT);
            }
        }
    }
}