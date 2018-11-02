using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.ComponentModel;
using System.Drawing;

namespace SmoONE.UI.Layout
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ImageButton : Smobiler.Core.Controls.MobileUserControl
    {
       
            /// <summary>
            /// ͼ����Դ����
            /// </summary>
            [Browsable(true), Category("Appearance"), DefaultValue(""), Description("ͼ����Դ����")]
            public string ResourceID
            {
                get
                {
                    return this.imageEx1.ResourceID;
                }
                set
                {
                    this.imageEx1.ResourceID = value;
                }
            }
       
            /// <summary>
            /// ͼ������
            /// </summary>
            [Browsable(true), Category("Appearance"), DefaultValue(""), Description("ͼ������")]
            public ImageEx.ImageStyle ImageType
            {
                get
                {
                    return this.imageEx1.ImageType;
                }
                set
                {
                    this.imageEx1.ImageType = value;
                }
            }

            /// <summary>
            /// ͼ��ģʽ
            /// </summary>
            [Browsable(true), Category("Appearance"), DefaultValue(""), Description("ͼ��ģʽ")]
            public ImageSizeMode SizeMode
            {
                get
                {
                    return this.imageEx1.SizeMode;
                }
                set
                {
                    this.imageEx1.SizeMode = value;
                }
            }
          
       

            /// <summary>
            /// �ı�
            /// </summary>
            [Browsable(true), Category("Appearance"), DefaultValue(""), Description("�����ı�")]
            public string DisplayMember
            {
                get
                {
                    return this.label1.DisplayMember;
                }
                set
                {
                    this.label1 .DisplayMember = value;
                }
            }
            /// <summary>
            /// �ı�
            /// </summary>
            [Browsable(true), Category("Appearance"), DefaultValue(""), Description("�����ı�")]
            public string Text
            {
                get
                {
                    return this.label1.Text;
                }
                set
                {
                    this.label1.Text  = value;
                }
            }

            /// <summary>
            /// �ı���ɫ
            /// </summary>
            [Browsable(true), Category("Appearance"), DefaultValue(""), Description("�����ı���ɫ")]
            public Color ForeColor
            {
                get
                {
                    return this.label1.ForeColor;
                }
                set
                {
                    this.label1.ForeColor = value;
                }
            }

            /// <summary>
            /// �ı������С
            /// </summary>
            [Browsable(true), Category("Appearance"), DefaultValue(""), Description("�����ı������С")]
            public float FontSize
            {
                get
                {
                    return this.label1.FontSize;
                }
                set
                {
                    this.label1.FontSize = value;
                }
            }

        /// <summary>
        /// ͼ����Դ��ɫ
        /// </summary>
        [Browsable(true), Category("Appearance"), DefaultValue(""), Description("ͼ����Դ��ɫ")]
        public System.Drawing.Color IconColor
        {
            get
            {
                return this.imageEx1.IconColor;
            }
            set
            {
                this.imageEx1.IconColor = value;
            }
        }

        /// <summary>
        /// �ı�ˮƽ��ʽ
        /// </summary>
        [Browsable(true), Category("Appearance"), DefaultValue(""), Description("�����ı���ˮƽ����")]
            public HorizontalAlignment HorizontalAlignment
            {
                get
                {
                    return this.label1.HorizontalAlignment;
                }
                set
                {
                    this.label1.HorizontalAlignment = value;
                }
            }

            /// <summary>
            /// �ڴ�����������ʱ����
            /// </summary>
            [Browsable(true), Category("Appearance"), DefaultValue(""), Description("�ڴ�����������ʱ����")]
            public event EventHandler Press;

         
           
            private void panel1_Press(object sender, EventArgs e)
            {
                if (Press != null) Press(this, EventArgs.Empty);
            }
        }

       
  

}