namespace TicTacToe.WinFormsApp.Forms
{
    partial class FrmSignIn
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSignIn));
            PnlMain = new System.Windows.Forms.TableLayoutPanel();
            BtnSignIn = new System.Windows.Forms.Button();
            TxbPassword = new System.Windows.Forms.TextBox();
            LblUsername = new System.Windows.Forms.Label();
            LblPassword = new System.Windows.Forms.Label();
            TxbUsername = new System.Windows.Forms.TextBox();
            BtnRegister = new System.Windows.Forms.Button();
            PnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // PnlMain
            // 
            PnlMain.ColumnCount = 2;
            PnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            PnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlMain.Controls.Add(BtnSignIn, 1, 2);
            PnlMain.Controls.Add(TxbPassword, 1, 1);
            PnlMain.Controls.Add(LblUsername, 0, 0);
            PnlMain.Controls.Add(LblPassword, 0, 1);
            PnlMain.Controls.Add(TxbUsername, 1, 0);
            PnlMain.Controls.Add(BtnRegister, 0, 2);
            PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlMain.Location = new System.Drawing.Point(0, 0);
            PnlMain.Name = "PnlMain";
            PnlMain.RowCount = 3;
            PnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            PnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            PnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlMain.Size = new System.Drawing.Size(262, 118);
            PnlMain.TabIndex = 0;
            // 
            // BtnSignIn
            // 
            BtnSignIn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            BtnSignIn.Location = new System.Drawing.Point(89, 73);
            BtnSignIn.Name = "BtnSignIn";
            BtnSignIn.Size = new System.Drawing.Size(90, 37);
            BtnSignIn.TabIndex = 5;
            BtnSignIn.Text = "Sign In";
            BtnSignIn.UseVisualStyleBackColor = true;
            BtnSignIn.Click += BtnSignIn_Click;
            // 
            // TxbPassword
            // 
            TxbPassword.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TxbPassword.Location = new System.Drawing.Point(89, 36);
            TxbPassword.Name = "TxbPassword";
            TxbPassword.Size = new System.Drawing.Size(170, 27);
            TxbPassword.TabIndex = 3;
            // 
            // LblUsername
            // 
            LblUsername.Anchor = System.Windows.Forms.AnchorStyles.Right;
            LblUsername.AutoSize = true;
            LblUsername.Location = new System.Drawing.Point(5, 6);
            LblUsername.Name = "LblUsername";
            LblUsername.Size = new System.Drawing.Size(78, 20);
            LblUsername.TabIndex = 0;
            LblUsername.Text = "Username:";
            // 
            // LblPassword
            // 
            LblPassword.Anchor = System.Windows.Forms.AnchorStyles.Right;
            LblPassword.AutoSize = true;
            LblPassword.Location = new System.Drawing.Point(10, 39);
            LblPassword.Name = "LblPassword";
            LblPassword.Size = new System.Drawing.Size(73, 20);
            LblPassword.TabIndex = 1;
            LblPassword.Text = "Password:";
            // 
            // TxbUsername
            // 
            TxbUsername.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TxbUsername.Location = new System.Drawing.Point(89, 3);
            TxbUsername.Name = "TxbUsername";
            TxbUsername.Size = new System.Drawing.Size(170, 27);
            TxbUsername.TabIndex = 2;
            // 
            // BtnRegister
            // 
            BtnRegister.Anchor = System.Windows.Forms.AnchorStyles.None;
            BtnRegister.Location = new System.Drawing.Point(3, 73);
            BtnRegister.Name = "BtnRegister";
            BtnRegister.Size = new System.Drawing.Size(80, 37);
            BtnRegister.TabIndex = 4;
            BtnRegister.Text = "Register";
            BtnRegister.UseVisualStyleBackColor = true;
            BtnRegister.Click += BtnRegister_Click;
            // 
            // FrmSignIn
            // 
            AcceptButton = BtnSignIn;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(262, 118);
            Controls.Add(PnlMain);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximumSize = new System.Drawing.Size(280, 165);
            MinimumSize = new System.Drawing.Size(280, 165);
            Name = "FrmSignIn";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Sign In";
            FormClosing += FrmSignIn_FormClosing;
            Load += FrmSignIn_Load;
            PnlMain.ResumeLayout(false);
            PnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel PnlMain;
        private System.Windows.Forms.Button BtnSignIn;
        private System.Windows.Forms.TextBox TxbPassword;
        private System.Windows.Forms.Label LblUsername;
        private System.Windows.Forms.Label LblPassword;
        private System.Windows.Forms.TextBox TxbUsername;
        private System.Windows.Forms.Button BtnRegister;
    }
}