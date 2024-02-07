using System.Threading.Tasks;

namespace TicTacToe.WinFormsApp.Forms
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            PnlMain = new System.Windows.Forms.TableLayoutPanel();
            PnlPlayerInfo = new System.Windows.Forms.TableLayoutPanel();
            LblRank = new System.Windows.Forms.Label();
            LblUsername = new System.Windows.Forms.Label();
            LblGamesPlayed = new System.Windows.Forms.Label();
            LblGamesWon = new System.Windows.Forms.Label();
            LblGamesLost = new System.Windows.Forms.Label();
            BtnLogOut = new System.Windows.Forms.Button();
            BtnFindGame = new System.Windows.Forms.Button();
            BtnNewGame = new System.Windows.Forms.Button();
            LblGamesTied = new System.Windows.Forms.Label();
            PnlGameBoard = new System.Windows.Forms.TableLayoutPanel();
            Btn11 = new System.Windows.Forms.Button();
            PnlOpponentName = new System.Windows.Forms.TableLayoutPanel();
            LblOpponentName = new System.Windows.Forms.Label();
            Btn01 = new System.Windows.Forms.Button();
            PnlTopLeft = new System.Windows.Forms.TableLayoutPanel();
            Btn00 = new System.Windows.Forms.Button();
            PnlCenterLeft = new System.Windows.Forms.TableLayoutPanel();
            Btn10 = new System.Windows.Forms.Button();
            PnlBottomLeft = new System.Windows.Forms.TableLayoutPanel();
            Btn20 = new System.Windows.Forms.Button();
            PnlBottomCenter = new System.Windows.Forms.TableLayoutPanel();
            Btn21 = new System.Windows.Forms.Button();
            LblTurn = new System.Windows.Forms.Label();
            PnlBottomRight = new System.Windows.Forms.TableLayoutPanel();
            Btn22 = new System.Windows.Forms.Button();
            PnlCenterRight = new System.Windows.Forms.TableLayoutPanel();
            Btn12 = new System.Windows.Forms.Button();
            Btn02 = new System.Windows.Forms.Button();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            PnlMain.SuspendLayout();
            PnlPlayerInfo.SuspendLayout();
            PnlGameBoard.SuspendLayout();
            PnlOpponentName.SuspendLayout();
            PnlTopLeft.SuspendLayout();
            PnlCenterLeft.SuspendLayout();
            PnlBottomLeft.SuspendLayout();
            PnlBottomCenter.SuspendLayout();
            PnlBottomRight.SuspendLayout();
            PnlCenterRight.SuspendLayout();
            SuspendLayout();
            // 
            // PnlMain
            // 
            PnlMain.BackColor = System.Drawing.Color.FromArgb(41, 41, 41);
            PnlMain.ColumnCount = 2;
            PnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            PnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.000008F));
            PnlMain.Controls.Add(PnlPlayerInfo, 0, 0);
            PnlMain.Controls.Add(PnlGameBoard, 1, 0);
            PnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlMain.Location = new System.Drawing.Point(0, 0);
            PnlMain.Name = "PnlMain";
            PnlMain.RowCount = 1;
            PnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlMain.Size = new System.Drawing.Size(982, 703);
            PnlMain.TabIndex = 0;
            // 
            // PnlPlayerInfo
            // 
            PnlPlayerInfo.ColumnCount = 2;
            PnlPlayerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            PnlPlayerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            PnlPlayerInfo.Controls.Add(LblRank, 0, 1);
            PnlPlayerInfo.Controls.Add(LblUsername, 0, 0);
            PnlPlayerInfo.Controls.Add(LblGamesPlayed, 0, 2);
            PnlPlayerInfo.Controls.Add(LblGamesWon, 0, 3);
            PnlPlayerInfo.Controls.Add(LblGamesLost, 0, 4);
            PnlPlayerInfo.Controls.Add(BtnLogOut, 0, 8);
            PnlPlayerInfo.Controls.Add(BtnFindGame, 0, 7);
            PnlPlayerInfo.Controls.Add(BtnNewGame, 0, 6);
            PnlPlayerInfo.Controls.Add(LblGamesTied, 0, 5);
            PnlPlayerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlPlayerInfo.Location = new System.Drawing.Point(3, 3);
            PnlPlayerInfo.Name = "PnlPlayerInfo";
            PnlPlayerInfo.RowCount = 9;
            PnlPlayerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333332F));
            PnlPlayerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333333F));
            PnlPlayerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.333333F));
            PnlPlayerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            PnlPlayerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            PnlPlayerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            PnlPlayerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            PnlPlayerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            PnlPlayerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            PnlPlayerInfo.Size = new System.Drawing.Size(208, 697);
            PnlPlayerInfo.TabIndex = 0;
            // 
            // LblRank
            // 
            LblRank.Anchor = System.Windows.Forms.AnchorStyles.Left;
            LblRank.AutoSize = true;
            LblRank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            LblRank.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            LblRank.Location = new System.Drawing.Point(3, 76);
            LblRank.Name = "LblRank";
            LblRank.Size = new System.Drawing.Size(46, 22);
            LblRank.TabIndex = 1;
            LblRank.Text = "Rank:";
            // 
            // LblUsername
            // 
            LblUsername.Anchor = System.Windows.Forms.AnchorStyles.Left;
            LblUsername.AutoSize = true;
            LblUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            LblUsername.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            LblUsername.Location = new System.Drawing.Point(3, 18);
            LblUsername.Name = "LblUsername";
            LblUsername.Size = new System.Drawing.Size(80, 22);
            LblUsername.TabIndex = 0;
            LblUsername.Text = "Username:";
            // 
            // LblGamesPlayed
            // 
            LblGamesPlayed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            LblGamesPlayed.AutoSize = true;
            LblGamesPlayed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            LblGamesPlayed.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            LblGamesPlayed.Location = new System.Drawing.Point(3, 134);
            LblGamesPlayed.Name = "LblGamesPlayed";
            LblGamesPlayed.Size = new System.Drawing.Size(107, 22);
            LblGamesPlayed.TabIndex = 7;
            LblGamesPlayed.Text = "Games Played:";
            // 
            // LblGamesWon
            // 
            LblGamesWon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            LblGamesWon.AutoSize = true;
            LblGamesWon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            LblGamesWon.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            LblGamesWon.Location = new System.Drawing.Point(3, 206);
            LblGamesWon.Name = "LblGamesWon";
            LblGamesWon.Size = new System.Drawing.Size(93, 22);
            LblGamesWon.TabIndex = 8;
            LblGamesWon.Text = "Games Won:";
            // 
            // LblGamesLost
            // 
            LblGamesLost.Anchor = System.Windows.Forms.AnchorStyles.Left;
            LblGamesLost.AutoSize = true;
            LblGamesLost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            LblGamesLost.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            LblGamesLost.Location = new System.Drawing.Point(3, 293);
            LblGamesLost.Name = "LblGamesLost";
            LblGamesLost.Size = new System.Drawing.Size(90, 22);
            LblGamesLost.TabIndex = 9;
            LblGamesLost.Text = "Games Lost:";
            // 
            // BtnLogOut
            // 
            BtnLogOut.Anchor = System.Windows.Forms.AnchorStyles.Left;
            BtnLogOut.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            BtnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnLogOut.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            BtnLogOut.Location = new System.Drawing.Point(3, 627);
            BtnLogOut.Name = "BtnLogOut";
            BtnLogOut.Size = new System.Drawing.Size(122, 52);
            BtnLogOut.TabIndex = 15;
            BtnLogOut.Text = "Log Out";
            BtnLogOut.UseVisualStyleBackColor = false;
            BtnLogOut.Click += BtnLogOut_Click;
            // 
            // BtnFindGame
            // 
            BtnFindGame.Anchor = System.Windows.Forms.AnchorStyles.Left;
            BtnFindGame.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            BtnFindGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnFindGame.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            BtnFindGame.Location = new System.Drawing.Point(3, 539);
            BtnFindGame.Name = "BtnFindGame";
            BtnFindGame.Size = new System.Drawing.Size(122, 52);
            BtnFindGame.TabIndex = 13;
            BtnFindGame.Text = "Find Game";
            BtnFindGame.UseVisualStyleBackColor = false;
            BtnFindGame.Click += BtnFindGame_Click;
            // 
            // BtnNewGame
            // 
            BtnNewGame.Anchor = System.Windows.Forms.AnchorStyles.Left;
            BtnNewGame.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            BtnNewGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            BtnNewGame.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            BtnNewGame.Location = new System.Drawing.Point(3, 452);
            BtnNewGame.Name = "BtnNewGame";
            BtnNewGame.Size = new System.Drawing.Size(122, 52);
            BtnNewGame.TabIndex = 6;
            BtnNewGame.Text = "New Game";
            BtnNewGame.UseVisualStyleBackColor = false;
            BtnNewGame.Click += BtnNewGame_Click;
            // 
            // LblGamesTied
            // 
            LblGamesTied.Anchor = System.Windows.Forms.AnchorStyles.Left;
            LblGamesTied.AutoSize = true;
            LblGamesTied.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            LblGamesTied.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            LblGamesTied.Location = new System.Drawing.Point(3, 380);
            LblGamesTied.Name = "LblGamesTied";
            LblGamesTied.Size = new System.Drawing.Size(92, 22);
            LblGamesTied.TabIndex = 16;
            LblGamesTied.Text = "Games Tied:";
            // 
            // PnlGameBoard
            // 
            PnlGameBoard.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            PnlGameBoard.ColumnCount = 3;
            PnlGameBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            PnlGameBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            PnlGameBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            PnlGameBoard.Controls.Add(Btn11, 1, 1);
            PnlGameBoard.Controls.Add(PnlOpponentName, 1, 0);
            PnlGameBoard.Controls.Add(PnlTopLeft, 0, 0);
            PnlGameBoard.Controls.Add(PnlCenterLeft, 0, 1);
            PnlGameBoard.Controls.Add(PnlBottomLeft, 0, 2);
            PnlGameBoard.Controls.Add(PnlBottomCenter, 1, 2);
            PnlGameBoard.Controls.Add(PnlBottomRight, 2, 2);
            PnlGameBoard.Controls.Add(PnlCenterRight, 2, 1);
            PnlGameBoard.Controls.Add(Btn02, 2, 0);
            PnlGameBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlGameBoard.Location = new System.Drawing.Point(217, 3);
            PnlGameBoard.Name = "PnlGameBoard";
            PnlGameBoard.RowCount = 3;
            PnlGameBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            PnlGameBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            PnlGameBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            PnlGameBoard.Size = new System.Drawing.Size(762, 697);
            PnlGameBoard.TabIndex = 1;
            // 
            // Btn11
            // 
            Btn11.Anchor = System.Windows.Forms.AnchorStyles.None;
            Btn11.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Btn11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Btn11.Font = new System.Drawing.Font("Arial Black", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Btn11.ForeColor = System.Drawing.Color.Transparent;
            Btn11.Location = new System.Drawing.Point(323, 290);
            Btn11.Name = "Btn11";
            Btn11.Size = new System.Drawing.Size(115, 115);
            Btn11.TabIndex = 4;
            Btn11.UseVisualStyleBackColor = true;
            Btn11.Click += Btn_Board_Click;
            // 
            // PnlOpponentName
            // 
            PnlOpponentName.ColumnCount = 1;
            PnlOpponentName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlOpponentName.Controls.Add(LblOpponentName, 0, 0);
            PnlOpponentName.Controls.Add(Btn01, 0, 1);
            PnlOpponentName.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlOpponentName.Location = new System.Drawing.Point(257, 3);
            PnlOpponentName.Name = "PnlOpponentName";
            PnlOpponentName.RowCount = 2;
            PnlOpponentName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlOpponentName.RowStyles.Add(new System.Windows.Forms.RowStyle());
            PnlOpponentName.Size = new System.Drawing.Size(248, 226);
            PnlOpponentName.TabIndex = 9;
            // 
            // LblOpponentName
            // 
            LblOpponentName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            LblOpponentName.AutoSize = true;
            LblOpponentName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblOpponentName.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            LblOpponentName.Location = new System.Drawing.Point(124, 0);
            LblOpponentName.Name = "LblOpponentName";
            LblOpponentName.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            LblOpponentName.Size = new System.Drawing.Size(0, 36);
            LblOpponentName.TabIndex = 1;
            // 
            // Btn01
            // 
            Btn01.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            Btn01.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Btn01.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Btn01.Font = new System.Drawing.Font("Arial Black", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Btn01.ForeColor = System.Drawing.Color.Transparent;
            Btn01.Location = new System.Drawing.Point(66, 108);
            Btn01.Name = "Btn01";
            Btn01.Size = new System.Drawing.Size(115, 115);
            Btn01.TabIndex = 0;
            Btn01.UseVisualStyleBackColor = false;
            Btn01.Click += Btn_Board_Click;
            // 
            // PnlTopLeft
            // 
            PnlTopLeft.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
            PnlTopLeft.ColumnCount = 1;
            PnlTopLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlTopLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            PnlTopLeft.Controls.Add(Btn00, 0, 0);
            PnlTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlTopLeft.Location = new System.Drawing.Point(3, 3);
            PnlTopLeft.Name = "PnlTopLeft";
            PnlTopLeft.RowCount = 1;
            PnlTopLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlTopLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            PnlTopLeft.Size = new System.Drawing.Size(248, 226);
            PnlTopLeft.TabIndex = 10;
            // 
            // Btn00
            // 
            Btn00.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            Btn00.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Btn00.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Btn00.Font = new System.Drawing.Font("Arial Black", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Btn00.ForeColor = System.Drawing.Color.Transparent;
            Btn00.Location = new System.Drawing.Point(130, 108);
            Btn00.Name = "Btn00";
            Btn00.Size = new System.Drawing.Size(115, 115);
            Btn00.TabIndex = 1;
            Btn00.UseVisualStyleBackColor = true;
            Btn00.Click += Btn_Board_Click;
            // 
            // PnlCenterLeft
            // 
            PnlCenterLeft.ColumnCount = 1;
            PnlCenterLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            PnlCenterLeft.Controls.Add(Btn10, 0, 0);
            PnlCenterLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlCenterLeft.Location = new System.Drawing.Point(3, 235);
            PnlCenterLeft.Name = "PnlCenterLeft";
            PnlCenterLeft.RowCount = 1;
            PnlCenterLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            PnlCenterLeft.Size = new System.Drawing.Size(248, 226);
            PnlCenterLeft.TabIndex = 11;
            // 
            // Btn10
            // 
            Btn10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            Btn10.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Btn10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Btn10.Font = new System.Drawing.Font("Arial Black", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Btn10.ForeColor = System.Drawing.Color.Transparent;
            Btn10.Location = new System.Drawing.Point(130, 55);
            Btn10.Name = "Btn10";
            Btn10.Size = new System.Drawing.Size(115, 115);
            Btn10.TabIndex = 3;
            Btn10.UseVisualStyleBackColor = true;
            Btn10.Click += Btn_Board_Click;
            // 
            // PnlBottomLeft
            // 
            PnlBottomLeft.ColumnCount = 1;
            PnlBottomLeft.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            PnlBottomLeft.Controls.Add(Btn20, 0, 0);
            PnlBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlBottomLeft.Location = new System.Drawing.Point(3, 467);
            PnlBottomLeft.Name = "PnlBottomLeft";
            PnlBottomLeft.RowCount = 1;
            PnlBottomLeft.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            PnlBottomLeft.Size = new System.Drawing.Size(248, 227);
            PnlBottomLeft.TabIndex = 12;
            // 
            // Btn20
            // 
            Btn20.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            Btn20.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Btn20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Btn20.Font = new System.Drawing.Font("Arial Black", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Btn20.ForeColor = System.Drawing.Color.Transparent;
            Btn20.Location = new System.Drawing.Point(130, 3);
            Btn20.Name = "Btn20";
            Btn20.Size = new System.Drawing.Size(115, 115);
            Btn20.TabIndex = 6;
            Btn20.UseVisualStyleBackColor = true;
            Btn20.Click += Btn_Board_Click;
            // 
            // PnlBottomCenter
            // 
            PnlBottomCenter.ColumnCount = 1;
            PnlBottomCenter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlBottomCenter.Controls.Add(Btn21, 0, 0);
            PnlBottomCenter.Controls.Add(LblTurn, 0, 1);
            PnlBottomCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlBottomCenter.Location = new System.Drawing.Point(257, 467);
            PnlBottomCenter.Name = "PnlBottomCenter";
            PnlBottomCenter.RowCount = 2;
            PnlBottomCenter.RowStyles.Add(new System.Windows.Forms.RowStyle());
            PnlBottomCenter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            PnlBottomCenter.Size = new System.Drawing.Size(248, 227);
            PnlBottomCenter.TabIndex = 13;
            // 
            // Btn21
            // 
            Btn21.Anchor = System.Windows.Forms.AnchorStyles.Top;
            Btn21.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Btn21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Btn21.Font = new System.Drawing.Font("Arial Black", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Btn21.ForeColor = System.Drawing.Color.Transparent;
            Btn21.Location = new System.Drawing.Point(66, 3);
            Btn21.Name = "Btn21";
            Btn21.Size = new System.Drawing.Size(115, 115);
            Btn21.TabIndex = 7;
            Btn21.UseVisualStyleBackColor = true;
            Btn21.Click += Btn_Board_Click;
            // 
            // LblTurn
            // 
            LblTurn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            LblTurn.AutoSize = true;
            LblTurn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            LblTurn.ForeColor = System.Drawing.Color.FromArgb(128, 255, 255);
            LblTurn.Location = new System.Drawing.Point(124, 184);
            LblTurn.Name = "LblTurn";
            LblTurn.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            LblTurn.Size = new System.Drawing.Size(0, 43);
            LblTurn.TabIndex = 8;
            // 
            // PnlBottomRight
            // 
            PnlBottomRight.ColumnCount = 1;
            PnlBottomRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            PnlBottomRight.Controls.Add(Btn22, 0, 0);
            PnlBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlBottomRight.Location = new System.Drawing.Point(511, 467);
            PnlBottomRight.Name = "PnlBottomRight";
            PnlBottomRight.RowCount = 1;
            PnlBottomRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            PnlBottomRight.Size = new System.Drawing.Size(248, 227);
            PnlBottomRight.TabIndex = 14;
            // 
            // Btn22
            // 
            Btn22.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Btn22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Btn22.Font = new System.Drawing.Font("Arial Black", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Btn22.ForeColor = System.Drawing.Color.Transparent;
            Btn22.Location = new System.Drawing.Point(3, 3);
            Btn22.Name = "Btn22";
            Btn22.Size = new System.Drawing.Size(115, 115);
            Btn22.TabIndex = 8;
            Btn22.UseVisualStyleBackColor = true;
            Btn22.Click += Btn_Board_Click;
            // 
            // PnlCenterRight
            // 
            PnlCenterRight.ColumnCount = 1;
            PnlCenterRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            PnlCenterRight.Controls.Add(Btn12, 0, 0);
            PnlCenterRight.Dock = System.Windows.Forms.DockStyle.Fill;
            PnlCenterRight.Location = new System.Drawing.Point(511, 235);
            PnlCenterRight.Name = "PnlCenterRight";
            PnlCenterRight.RowCount = 1;
            PnlCenterRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            PnlCenterRight.Size = new System.Drawing.Size(248, 226);
            PnlCenterRight.TabIndex = 15;
            // 
            // Btn12
            // 
            Btn12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            Btn12.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Btn12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Btn12.Font = new System.Drawing.Font("Arial Black", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Btn12.ForeColor = System.Drawing.Color.Transparent;
            Btn12.Location = new System.Drawing.Point(3, 55);
            Btn12.Name = "Btn12";
            Btn12.Size = new System.Drawing.Size(115, 115);
            Btn12.TabIndex = 5;
            Btn12.UseVisualStyleBackColor = true;
            Btn12.Click += Btn_Board_Click;
            // 
            // Btn02
            // 
            Btn02.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            Btn02.BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
            Btn02.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Btn02.Font = new System.Drawing.Font("Arial Black", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Btn02.ForeColor = System.Drawing.Color.Transparent;
            Btn02.Location = new System.Drawing.Point(514, 111);
            Btn02.Margin = new System.Windows.Forms.Padding(6);
            Btn02.Name = "Btn02";
            Btn02.Size = new System.Drawing.Size(115, 115);
            Btn02.TabIndex = 2;
            Btn02.UseVisualStyleBackColor = true;
            Btn02.Click += Btn_Board_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new System.Drawing.Size(200, 100);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(982, 703);
            Controls.Add(PnlMain);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximumSize = new System.Drawing.Size(1000, 750);
            MinimumSize = new System.Drawing.Size(100, 750);
            Name = "FrmMain";
            Text = "Tic-Tac-Toe Online";
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            PnlMain.ResumeLayout(false);
            PnlPlayerInfo.ResumeLayout(false);
            PnlPlayerInfo.PerformLayout();
            PnlGameBoard.ResumeLayout(false);
            PnlOpponentName.ResumeLayout(false);
            PnlOpponentName.PerformLayout();
            PnlTopLeft.ResumeLayout(false);
            PnlCenterLeft.ResumeLayout(false);
            PnlBottomLeft.ResumeLayout(false);
            PnlBottomCenter.ResumeLayout(false);
            PnlBottomCenter.PerformLayout();
            PnlBottomRight.ResumeLayout(false);
            PnlCenterRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel PnlMain;
        private System.Windows.Forms.TableLayoutPanel PnlPlayerInfo;
        private System.Windows.Forms.Label LblRank;
        private System.Windows.Forms.Label LblUsername;
        private System.Windows.Forms.Button BtnNewGame;
        private System.Windows.Forms.Label LblGamesPlayed;
        private System.Windows.Forms.Label LblGamesWon;
        private System.Windows.Forms.Label LblGamesLost;
        private System.Windows.Forms.Button BtnFindGame;
        private System.Windows.Forms.Button BtnLogOut;
        private System.Windows.Forms.TableLayoutPanel PnlGameBoard;
        private System.Windows.Forms.Button Btn22;
        private System.Windows.Forms.Button Btn21;
        private System.Windows.Forms.Button Btn20;
        private System.Windows.Forms.Button Btn12;
        private System.Windows.Forms.Button Btn11;
        private System.Windows.Forms.Button Btn02;
        private System.Windows.Forms.Button Btn00;
        private System.Windows.Forms.Button Btn01;
        private System.Windows.Forms.Button Btn10;
        private System.Windows.Forms.TableLayoutPanel PnlOpponentName;
        private System.Windows.Forms.Label LblOpponentName;
        private System.Windows.Forms.TableLayoutPanel PnlTopLeft;
        private System.Windows.Forms.TableLayoutPanel PnlCenterLeft;
        private System.Windows.Forms.TableLayoutPanel PnlBottomLeft;
        private System.Windows.Forms.TableLayoutPanel PnlBottomCenter;
        private System.Windows.Forms.TableLayoutPanel PnlBottomRight;
        private System.Windows.Forms.TableLayoutPanel PnlCenterRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label LblTurn;
        private System.Windows.Forms.Label LblGamesTied;
    }
}