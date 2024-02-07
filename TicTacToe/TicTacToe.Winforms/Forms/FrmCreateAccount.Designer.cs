namespace TicTacToe.WinFormsApp.Forms
{
    partial class FrmCreateAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateAccount));
            PnlMain = new System.Windows.Forms.TableLayoutPanel();
            BtnCreateAccount = new System.Windows.Forms.Button();
            TxbConfirmNewPassword = new System.Windows.Forms.TextBox();
            TxbNewPassword = new System.Windows.Forms.TextBox();
            LblNewUsername = new System.Windows.Forms.Label();
            LblNewPassword = new System.Windows.Forms.Label();
            LblConfirmPassword = new System.Windows.Forms.Label();
            TxbNewUsername = new System.Windows.Forms.TextBox();
            PnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // PnlMain
            // 
            PnlMain.ColumnCount = 2;
            PnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            PnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlMain.Controls.Add(BtnCreateAccount, 1, 3);
            PnlMain.Controls.Add(TxbConfirmNewPassword, 1, 2);
            PnlMain.Controls.Add(TxbNewPassword, 1, 1);
            PnlMain.Controls.Add(LblNewUsername, 0, 0);
            PnlMain.Controls.Add(LblNewPassword, 0, 1);
            PnlMain.Controls.Add(LblConfirmPassword, 0, 2);
            PnlMain.Controls.Add(TxbNewUsername, 1, 0);
            PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlMain.Location = new System.Drawing.Point(0, 0);
            PnlMain.Name = "PnlMain";
            PnlMain.RowCount = 4;
            PnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            PnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            PnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            PnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlMain.Size = new System.Drawing.Size(347, 153);
            PnlMain.TabIndex = 1;
            // 
            // BtnCreateAccount
            // 
            BtnCreateAccount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            BtnCreateAccount.Location = new System.Drawing.Point(173, 108);
            BtnCreateAccount.Name = "BtnCreateAccount";
            BtnCreateAccount.Size = new System.Drawing.Size(118, 35);
            BtnCreateAccount.TabIndex = 6;
            BtnCreateAccount.Text = "Create Account";
            BtnCreateAccount.UseVisualStyleBackColor = true;
            BtnCreateAccount.Click += BtnCreateAccount_Click;
            // 
            // TxbConfirmNewPassword
            // 
            TxbConfirmNewPassword.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TxbConfirmNewPassword.Location = new System.Drawing.Point(173, 69);
            TxbConfirmNewPassword.Name = "TxbConfirmNewPassword";
            TxbConfirmNewPassword.Size = new System.Drawing.Size(171, 27);
            TxbConfirmNewPassword.TabIndex = 5;
            // 
            // TxbNewPassword
            // 
            TxbNewPassword.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TxbNewPassword.Location = new System.Drawing.Point(173, 36);
            TxbNewPassword.Name = "TxbNewPassword";
            TxbNewPassword.Size = new System.Drawing.Size(171, 27);
            TxbNewPassword.TabIndex = 4;
            // 
            // LblNewUsername
            // 
            LblNewUsername.Anchor = System.Windows.Forms.AnchorStyles.Right;
            LblNewUsername.AutoSize = true;
            LblNewUsername.Location = new System.Drawing.Point(17, 6);
            LblNewUsername.Name = "LblNewUsername";
            LblNewUsername.Size = new System.Drawing.Size(150, 20);
            LblNewUsername.TabIndex = 0;
            LblNewUsername.Text = "Enter New Username:";
            // 
            // LblNewPassword
            // 
            LblNewPassword.Anchor = System.Windows.Forms.AnchorStyles.Right;
            LblNewPassword.AutoSize = true;
            LblNewPassword.Location = new System.Drawing.Point(22, 39);
            LblNewPassword.Name = "LblNewPassword";
            LblNewPassword.Size = new System.Drawing.Size(145, 20);
            LblNewPassword.TabIndex = 1;
            LblNewPassword.Text = "Enter New Password:";
            // 
            // LblConfirmPassword
            // 
            LblConfirmPassword.Anchor = System.Windows.Forms.AnchorStyles.Right;
            LblConfirmPassword.AutoSize = true;
            LblConfirmPassword.Location = new System.Drawing.Point(3, 72);
            LblConfirmPassword.Name = "LblConfirmPassword";
            LblConfirmPassword.Size = new System.Drawing.Size(164, 20);
            LblConfirmPassword.TabIndex = 2;
            LblConfirmPassword.Text = "Confirm New Password:";
            // 
            // TxbNewUsername
            // 
            TxbNewUsername.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            TxbNewUsername.Location = new System.Drawing.Point(173, 3);
            TxbNewUsername.Name = "TxbNewUsername";
            TxbNewUsername.Size = new System.Drawing.Size(171, 27);
            TxbNewUsername.TabIndex = 3;
            // 
            // FrmCreateAccount
            // 
            AcceptButton = BtnCreateAccount;
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(347, 153);
            Controls.Add(PnlMain);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximumSize = new System.Drawing.Size(365, 200);
            MinimumSize = new System.Drawing.Size(365, 200);
            Name = "FrmCreateAccount";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Create Account";
            Load += FrmCreateAccount_Load;
            PnlMain.ResumeLayout(false);
            PnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel PnlMain;
        private System.Windows.Forms.Button BtnCreateAccount;
        private System.Windows.Forms.TextBox TxbConfirmNewPassword;
        private System.Windows.Forms.TextBox TxbNewPassword;
        private System.Windows.Forms.Label LblNewUsername;
        private System.Windows.Forms.Label LblNewPassword;
        private System.Windows.Forms.Label LblConfirmPassword;
        private System.Windows.Forms.TextBox TxbNewUsername;
    }
}