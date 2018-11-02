using System;
using Smobiler.Core;
using SmoONE.UI.Layout;

namespace SmoONE.UI.UserInfo
{
    partial class frmRegister : Smobiler.Core.Controls.MobileForm
    {
        #region "SmobilerForm generated code "

        public frmRegister()
            : base()
        {
            //This call is required by the SmobilerForm.
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
        }

        //SmobilerForm overrides dispose to clean up the component list.
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


        //NOTE: The following procedure is required by the SmobilerForm
        //It can be modified using the SmobilerForm.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.plName = new Smobiler.Core.Controls.Panel();
            this.label1 = new Smobiler.Core.Controls.Label();
            this.txtName = new Smobiler.Core.Controls.TextBox();
            this.plPwd1 = new Smobiler.Core.Controls.Panel();
            this.label2 = new Smobiler.Core.Controls.Label();
            this.txtPwd1 = new Smobiler.Core.Controls.TextBox();
            this.tpPwd1 = new Smobiler.Core.Controls.Panel();
            this.fontPwd1 = new Smobiler.Core.Controls.FontIcon();
            this.plPwd2 = new Smobiler.Core.Controls.Panel();
            this.label3 = new Smobiler.Core.Controls.Label();
            this.txtPwd2 = new Smobiler.Core.Controls.TextBox();
            this.tpPwd2 = new Smobiler.Core.Controls.Panel();
            this.fontPwd2 = new Smobiler.Core.Controls.FontIcon();
            this.label4 = new Smobiler.Core.Controls.Label();
            this.btnSave = new Smobiler.Core.Controls.Button();
            this.radioGroup1 = new Smobiler.Core.Controls.RadioGroup();
            this.menuTitle1 = new SmoONE.UI.Layout.MenuTitle();
            this.spContent = new Smobiler.Core.Controls.Panel();
            // 
            // plName
            // 
            this.plName.BackColor = System.Drawing.Color.White;
            this.plName.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.plName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.plName.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.label1,
            this.txtName});
            this.plName.Name = "plName";
            this.plName.Size = new System.Drawing.Size(300, 35);
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.label1.Name = "label1";
            this.label1.Padding = new Smobiler.Core.Controls.Padding(4F, 0F, 0F, 0F);
            this.label1.Size = new System.Drawing.Size(66, 35);
            this.label1.Text = "�ǳ�";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.Transparent;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.txtName.Location = new System.Drawing.Point(66, 0);
            this.txtName.Name = "txtName";
            this.txtName.ReturnKeyType = Smobiler.Core.Controls.ReturnKeyType.Done;
            this.txtName.Size = new System.Drawing.Size(234, 35);
            this.txtName.WaterMarkText = "�������ǳ�";
            // 
            // plPwd1
            // 
            this.plPwd1.BackColor = System.Drawing.Color.White;
            this.plPwd1.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.plPwd1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.plPwd1.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.label2,
            this.txtPwd1,
            this.tpPwd1});
            this.plPwd1.Location = new System.Drawing.Point(0, 35);
            this.plPwd1.Name = "plPwd1";
            this.plPwd1.Size = new System.Drawing.Size(300, 35);
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.label2.Name = "label2";
            this.label2.Padding = new Smobiler.Core.Controls.Padding(4F, 0F, 0F, 0F);
            this.label2.Size = new System.Drawing.Size(66, 35);
            this.label2.Text = "������";
            // 
            // txtPwd1
            // 
            this.txtPwd1.BackColor = System.Drawing.Color.Transparent;
            this.txtPwd1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.txtPwd1.KeyboardType = Smobiler.Core.Controls.KeyboardType.Numeric;
            this.txtPwd1.Location = new System.Drawing.Point(66, 0);
            this.txtPwd1.MaxLength = 12;
            this.txtPwd1.Name = "txtPwd1";
            this.txtPwd1.SecurityMode = true;
            this.txtPwd1.Size = new System.Drawing.Size(189, 35);
            this.txtPwd1.WaterMarkText = "������6-12λ����";
            // 
            // tpPwd1
            // 
            this.tpPwd1.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.fontPwd1});
            this.tpPwd1.Location = new System.Drawing.Point(255, 0);
            this.tpPwd1.Name = "tpPwd1";
            this.tpPwd1.Size = new System.Drawing.Size(45, 35);
            this.tpPwd1.Touchable = true;
            this.tpPwd1.Press += new System.EventHandler(this.tpPwd1_Press);
            // 
            // fontPwd1
            // 
            this.fontPwd1.ForeColor = System.Drawing.Color.Gray;
            this.fontPwd1.Margin = new Smobiler.Core.Controls.Margin(5F, 0F, 0F, 0F);
            this.fontPwd1.Name = "fontPwd1";
            this.fontPwd1.Padding = new Smobiler.Core.Controls.Padding(8F);
            this.fontPwd1.ResourceID = "eye-slash";
            this.fontPwd1.Size = new System.Drawing.Size(35, 35);
            // 
            // plPwd2
            // 
            this.plPwd2.BackColor = System.Drawing.Color.White;
            this.plPwd2.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 1F);
            this.plPwd2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.plPwd2.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.label3,
            this.txtPwd2,
            this.tpPwd2});
            this.plPwd2.Location = new System.Drawing.Point(0, 70);
            this.plPwd2.Name = "plPwd2";
            this.plPwd2.Size = new System.Drawing.Size(300, 35);
            // 
            // label3
            // 
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.label3.Name = "label3";
            this.label3.Padding = new Smobiler.Core.Controls.Padding(4F, 0F, 0F, 0F);
            this.label3.Size = new System.Drawing.Size(66, 35);
            this.label3.Text = "ȷ������";
            // 
            // txtPwd2
            // 
            this.txtPwd2.BackColor = System.Drawing.Color.Transparent;
            this.txtPwd2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.txtPwd2.KeyboardType = Smobiler.Core.Controls.KeyboardType.Numeric;
            this.txtPwd2.Location = new System.Drawing.Point(66, 0);
            this.txtPwd2.MaxLength = 12;
            this.txtPwd2.Name = "txtPwd2";
            this.txtPwd2.SecurityMode = true;
            this.txtPwd2.Size = new System.Drawing.Size(189, 35);
            this.txtPwd2.WaterMarkText = "������6-12λ����";
            // 
            // tpPwd2
            // 
            this.tpPwd2.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.fontPwd2});
            this.tpPwd2.Location = new System.Drawing.Point(255, 0);
            this.tpPwd2.Name = "tpPwd2";
            this.tpPwd2.Size = new System.Drawing.Size(45, 35);
            this.tpPwd2.Touchable = true;
            this.tpPwd2.Press += new System.EventHandler(this.tpPwd2_Press);
            // 
            // fontPwd2
            // 
            this.fontPwd2.ForeColor = System.Drawing.Color.Gray;
            this.fontPwd2.Margin = new Smobiler.Core.Controls.Margin(5F, 0F, 0F, 0F);
            this.fontPwd2.Name = "fontPwd2";
            this.fontPwd2.Padding = new Smobiler.Core.Controls.Padding(8F);
            this.fontPwd2.ResourceID = "eye-slash";
            this.fontPwd2.Size = new System.Drawing.Size(35, 35);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.label4.Location = new System.Drawing.Point(0, 105);
            this.label4.Name = "label4";
            this.label4.Padding = new Smobiler.Core.Controls.Padding(4F, 0F, 0F, 0F);
            this.label4.Size = new System.Drawing.Size(300, 35);
            this.label4.Text = "��ɫѡ��";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(146)))), ((int)(((byte)(223)))));
            this.btnSave.BorderRadius = 4;
            this.btnSave.FontSize = 15F;
            this.btnSave.Location = new System.Drawing.Point(10, 250);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(280, 35);
            this.btnSave.Text = "��ʼʹ��";
            this.btnSave.Press += new System.EventHandler(this.btnSave_Press);
            // 
            // radioGroup1
            // 
            this.radioGroup1.BackColor = System.Drawing.Color.White;
            this.radioGroup1.FontSize = 12F;
            this.radioGroup1.GridLines = true;
            this.radioGroup1.GridLinesColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.radioGroup1.Location = new System.Drawing.Point(0, 140);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Padding = new Smobiler.Core.Controls.Padding(4F, 0F, 0F, 0F);
            this.radioGroup1.Size = new System.Drawing.Size(300, 100);
            // 
            // menuTitle1
            // 
            this.menuTitle1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuTitle1.Location = new System.Drawing.Point(53, 68);
            this.menuTitle1.Name = "menuTitle1";
            this.menuTitle1.Size = new System.Drawing.Size(100, 50);
            this.menuTitle1.TitleText = "���õ�¼��Ϣ";
            // 
            // spContent
            // 
            this.spContent.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 1F);
            this.spContent.BorderColor = System.Drawing.Color.LightGray;
            this.spContent.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.plName,
            this.plPwd1,
            this.plPwd2,
            this.label4,
            this.btnSave,
            this.radioGroup1});
            this.spContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spContent.Flex = 10000;
            this.spContent.Location = new System.Drawing.Point(120, 142);
            this.spContent.Margin = new Smobiler.Core.Controls.Margin(0F, 10F, 0F, 0F);
            this.spContent.Name = "spContent";
            this.spContent.Scrollable = true;
            this.spContent.Size = new System.Drawing.Size(100, 30);
            // 
            // frmRegister
            // 
            this.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.menuTitle1,
            this.spContent});
            this.Orientation = Smobiler.Core.Controls.FormOrientation.Portrait;
            this.Load += new System.EventHandler(this.frmRegister_Load);
            this.Name = "frmRegister";

        }
        #endregion
        private MenuTitle menuTitle1;
        private Smobiler.Core.Controls.Panel spContent;
        private Smobiler.Core.Controls.Panel plName;
        private Smobiler.Core.Controls.Label label1;
        private Smobiler.Core.Controls.TextBox txtName;
        private Smobiler.Core.Controls.Panel plPwd1;
        private Smobiler.Core.Controls.Label label2;
        private Smobiler.Core.Controls.TextBox txtPwd1;
        private Smobiler.Core.Controls.Panel tpPwd1;
        private Smobiler.Core.Controls.FontIcon fontPwd1;
        private Smobiler.Core.Controls.Panel plPwd2;
        private Smobiler.Core.Controls.Label label3;
        private Smobiler.Core.Controls.TextBox txtPwd2;
        private Smobiler.Core.Controls.Panel tpPwd2;
        private Smobiler.Core.Controls.FontIcon fontPwd2;
        private Smobiler.Core.Controls.Label label4;
        private Smobiler.Core.Controls.Button btnSave;
        private Smobiler.Core.Controls.RadioGroup radioGroup1;
    }
}