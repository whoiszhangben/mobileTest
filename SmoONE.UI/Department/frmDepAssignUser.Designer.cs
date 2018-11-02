using System;
using Smobiler.Core;
namespace SmoONE.UI.Department
{
    partial class frmDepAssignUser : Smobiler.Core.Controls.MobileForm
    {
        #region "SmobilerForm generated code "

        public frmDepAssignUser()
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
            this.checkAll = new Smobiler.Core.Controls.CheckBox();
            this.popLeader = new Smobiler.Core.Controls.PopList();
            this.cameraPortrait = new Smobiler.Core.Controls.Camera();
            this.title1 = new SmoONE.UI.Layout.Title();
            this.plContent = new Smobiler.Core.Controls.Panel();
            this.plTop = new Smobiler.Core.Controls.Panel();
            this.label3 = new Smobiler.Core.Controls.Label();
            this.txtDepName = new Smobiler.Core.Controls.TextBox();
            this.btnAll = new Smobiler.Core.Controls.Button();
            this.label1 = new Smobiler.Core.Controls.Label();
            this.label5 = new Smobiler.Core.Controls.Label();
            this.btnUp = new Smobiler.Core.Controls.Button();
            this.label6 = new Smobiler.Core.Controls.Label();
            this.imgPortrait = new Smobiler.Core.Controls.Image();
            this.btnLeader = new Smobiler.Core.Controls.Button();
            this.gridUserData = new Smobiler.Core.Controls.ListView();
            this.btnSave = new Smobiler.Core.Controls.Button();
            // 
            // checkAll
            // 
            this.checkAll.Location = new System.Drawing.Point(270, 144);
            this.checkAll.Name = "checkAll";
            this.checkAll.Size = new System.Drawing.Size(20, 20);
            this.checkAll.TintColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(146)))), ((int)(((byte)(223)))));
            this.checkAll.CheckedChanged += new System.EventHandler(this.checkAll_CheckChanged);
            // 
            // popLeader
            // 
            this.popLeader.Name = "popLeader";
            this.popLeader.Selected += new System.EventHandler(this.popLeader_Selected);
            // 
            // cameraPortrait
            // 
            this.cameraPortrait.Name = "cameraPortrait";
            // 
            // title1
            // 
            this.title1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.title1.Direction = Smobiler.Core.Controls.LayoutDirection.Row;
            this.title1.Dock = System.Windows.Forms.DockStyle.Top;
            this.title1.Location = new System.Drawing.Point(111, 36);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(100, 50);
            this.title1.TitleText = "������Ա����";
            this.title1.Load += new System.EventHandler(this.title1_Load);
            // 
            // plContent
            // 
            this.plContent.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.plTop,
            this.gridUserData});
            this.plContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plContent.Location = new System.Drawing.Point(0, 50);
            this.plContent.Name = "plContent";
            this.plContent.Size = new System.Drawing.Size(300, 405);
            // 
            // plTop
            // 
            this.plTop.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.label3,
            this.txtDepName,
            this.btnAll,
            this.label1,
            this.label5,
            this.btnUp,
            this.label6,
            this.imgPortrait,
            this.checkAll,
            this.btnLeader});
            this.plTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTop.Name = "plTop";
            this.plTop.Size = new System.Drawing.Size(300, 170);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Border = new Smobiler.Core.Controls.Border(0F, 0F, 0F, 1F);
            this.label3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.label3.Name = "label3";
            this.label3.Padding = new Smobiler.Core.Controls.Padding(4F, 0F, 0F, 0F);
            this.label3.Size = new System.Drawing.Size(66, 35);
            this.label3.Text = "����";
            // 
            // txtDepName
            // 
            this.txtDepName.Border = new Smobiler.Core.Controls.Border(0F, 0F, 0F, 1F);
            this.txtDepName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtDepName.FontSize = 12F;
            this.txtDepName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.txtDepName.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.txtDepName.Location = new System.Drawing.Point(66, 0);
            this.txtDepName.Name = "txtDepName";
            this.txtDepName.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 4F, 0F);
            this.txtDepName.Size = new System.Drawing.Size(234, 35);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.White;
            this.btnAll.BorderRadius = 0;
            this.btnAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(146)))), ((int)(((byte)(223)))));
            this.btnAll.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.btnAll.Location = new System.Drawing.Point(0, 135);
            this.btnAll.Name = "btnAll";
            this.btnAll.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 40F, 0F);
            this.btnAll.Size = new System.Drawing.Size(300, 35);
            this.btnAll.Text = "ȫѡ";
            this.btnAll.Press += new System.EventHandler(this.btnAll_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 1F);
            this.label1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.label1.Location = new System.Drawing.Point(0, 90);
            this.label1.Name = "label1";
            this.label1.Padding = new Smobiler.Core.Controls.Padding(4F, 0F, 0F, 0F);
            this.label1.Size = new System.Drawing.Size(66, 35);
            this.label1.Text = "������";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 1F);
            this.label5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.label5.Location = new System.Drawing.Point(0, 45);
            this.label5.Name = "label5";
            this.label5.Padding = new Smobiler.Core.Controls.Padding(4F, 0F, 0F, 0F);
            this.label5.Size = new System.Drawing.Size(66, 35);
            this.label5.Text = "����ͷ��";
            this.label5.ZIndex = 3;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.White;
            this.btnUp.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 1F);
            this.btnUp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnUp.BorderRadius = 0;
            this.btnUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.btnUp.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.btnUp.Location = new System.Drawing.Point(275, 45);
            this.btnUp.Name = "btnUp";
            this.btnUp.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 4F, 0F);
            this.btnUp.Size = new System.Drawing.Size(25, 35);
            this.btnUp.Text = ">";
            this.btnUp.Press += new System.EventHandler(this.btnUp_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 1F);
            this.label6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.label6.Location = new System.Drawing.Point(66, 45);
            this.label6.Name = "label6";
            this.label6.Padding = new Smobiler.Core.Controls.Padding(4F, 0F, 0F, 0F);
            this.label6.Size = new System.Drawing.Size(209, 35);
            this.label6.ZIndex = 4;
            // 
            // imgPortrait
            // 
            this.imgPortrait.BorderRadius = 10;
            this.imgPortrait.Location = new System.Drawing.Point(242, 47);
            this.imgPortrait.Name = "imgPortrait";
            this.imgPortrait.ResourceID = "bumenicon";
            this.imgPortrait.Size = new System.Drawing.Size(31, 31);
            this.imgPortrait.SizeMode = Smobiler.Core.Controls.ImageSizeMode.Stretch;
            this.imgPortrait.ZIndex = 5;
            // 
            // btnLeader
            // 
            this.btnLeader.BackColor = System.Drawing.Color.White;
            this.btnLeader.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 1F);
            this.btnLeader.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnLeader.BorderRadius = 0;
            this.btnLeader.FontSize = 12F;
            this.btnLeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.btnLeader.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.btnLeader.Location = new System.Drawing.Point(66, 90);
            this.btnLeader.Name = "btnLeader";
            this.btnLeader.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 4F, 0F);
            this.btnLeader.Size = new System.Drawing.Size(234, 35);
            this.btnLeader.Text = "ѡ�񣨱��   > ";
            this.btnLeader.Press += new System.EventHandler(this.btnLeader_Click);
            // 
            // gridUserData
            // 
            this.gridUserData.BackColor = System.Drawing.Color.White;
            this.gridUserData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridUserData.Location = new System.Drawing.Point(0, 170);
            this.gridUserData.Name = "gridUserData";
            this.gridUserData.PageSize = 10;
            this.gridUserData.PageSizeTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(145)))), ((int)(((byte)(145)))));
            this.gridUserData.PageSizeTextSize = 11F;
            this.gridUserData.ShowSplitLine = true;
            this.gridUserData.Size = new System.Drawing.Size(300, 226);
            this.gridUserData.SplitLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.gridUserData.TemplateControlName = "frmDepAssignUserLayout";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(146)))), ((int)(((byte)(223)))));
            this.btnSave.BorderRadius = 4;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSave.FontSize = 17F;
            this.btnSave.Location = new System.Drawing.Point(8, 465);
            this.btnSave.Margin = new Smobiler.Core.Controls.Margin(10F, 0F, 10F, 0F);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(280, 35);
            this.btnSave.Text = "�ύ";
            this.btnSave.Press += new System.EventHandler(this.btnSave_Click);
            // 
            // frmDepAssignUser
            // 
            this.Components.AddRange(new Smobiler.Core.Controls.MobileComponent[] {
            this.cameraPortrait,
            this.popLeader});
            this.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.title1,
            this.btnSave,
            this.plContent});
            this.Orientation = Smobiler.Core.Controls.FormOrientation.Portrait;
            this.KeyDown += new Smobiler.Core.Controls.KeyDownEventHandler(this.frmDepAssignUser_KeyDown);
            this.Load += new System.EventHandler(this.frmDepAssignUser_Load);
            this.Name = "frmDepAssignUser";

        }
        #endregion
        private Smobiler.Core.Controls.CheckBox checkAll;
        private Smobiler.Core.Controls.PopList popLeader;
        private Smobiler.Core.Controls.Camera cameraPortrait;
        private SmoONE.UI.Layout.Title title1;
        private Smobiler.Core.Controls.Panel plContent;
        private Smobiler.Core.Controls.Button btnSave;
        private Smobiler.Core.Controls.Panel plTop;
        internal Smobiler.Core.Controls.Label label3;
        internal Smobiler.Core.Controls.TextBox txtDepName;
        private Smobiler.Core.Controls.Button btnAll;
        internal Smobiler.Core.Controls.Label label1;
        internal Smobiler.Core.Controls.Label label5;
        internal Smobiler.Core.Controls.Button btnUp;
        internal Smobiler.Core.Controls.Label label6;
        private Smobiler.Core.Controls.Image imgPortrait;
        internal Smobiler.Core.Controls.Button btnLeader;
        private Smobiler.Core.Controls.ListView gridUserData;
    }
}