namespace AssaltCubeH
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Title = new System.Windows.Forms.Label();
            this.HealthHack = new System.Windows.Forms.Button();
            this.BulletHack = new System.Windows.Forms.Button();
            this.SelectProcess = new System.Windows.Forms.ComboBox();
            this.PlayerData = new System.Windows.Forms.GroupBox();
            this.Grenade = new System.Windows.Forms.Label();
            this.Angle = new System.Windows.Forms.Label();
            this.Postion = new System.Windows.Forms.Label();
            this.Armor = new System.Windows.Forms.Label();
            this.Bullet = new System.Windows.Forms.Label();
            this.Health = new System.Windows.Forms.Label();
            this.ProcessSelect = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.healthHackDuring = new System.Windows.Forms.Label();
            this.bulletHackDuring = new System.Windows.Forms.Label();
            this.GrenadeHack = new System.Windows.Forms.Button();
            this.grenadeHackDuring = new System.Windows.Forms.Label();
            this.armorHackDuring = new System.Windows.Forms.Label();
            this.ArmorHack = new System.Windows.Forms.Button();
            this.healthsetlabel = new System.Windows.Forms.Label();
            this.HealthSet = new System.Windows.Forms.TextBox();
            this.ArmorSet = new System.Windows.Forms.TextBox();
            this.armorsetlabel = new System.Windows.Forms.Label();
            this.BulletSet = new System.Windows.Forms.TextBox();
            this.bulletsetlabel = new System.Windows.Forms.Label();
            this.GrenadeSet = new System.Windows.Forms.TextBox();
            this.grenadesetlabel = new System.Windows.Forms.Label();
            this.OverlayCB = new System.Windows.Forms.CheckBox();
            this.JumpCB = new System.Windows.Forms.CheckBox();
            this.RecoilCB = new System.Windows.Forms.CheckBox();
            this.PlayerData.SuspendLayout();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("맑은 고딕", 31.8F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point);
            this.Title.Location = new System.Drawing.Point(85, 23);
            this.Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Title.Name = "Title";
            this.Title.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Title.Size = new System.Drawing.Size(832, 71);
            this.Title.TabIndex = 0;
            this.Title.Text = "AssaultCube 1.3.0.2 Game Hack";
            // 
            // HealthHack
            // 
            this.HealthHack.Location = new System.Drawing.Point(322, 187);
            this.HealthHack.Margin = new System.Windows.Forms.Padding(4);
            this.HealthHack.Name = "HealthHack";
            this.HealthHack.Size = new System.Drawing.Size(100, 31);
            this.HealthHack.TabIndex = 1;
            this.HealthHack.Text = "체력 핵";
            this.HealthHack.UseVisualStyleBackColor = true;
            this.HealthHack.Click += new System.EventHandler(this.HealthHack_Click);
            // 
            // BulletHack
            // 
            this.BulletHack.Location = new System.Drawing.Point(322, 259);
            this.BulletHack.Margin = new System.Windows.Forms.Padding(4);
            this.BulletHack.Name = "BulletHack";
            this.BulletHack.Size = new System.Drawing.Size(100, 31);
            this.BulletHack.TabIndex = 2;
            this.BulletHack.Text = "탄창 핵";
            this.BulletHack.UseVisualStyleBackColor = true;
            this.BulletHack.Click += new System.EventHandler(this.BulletHack_Click);
            // 
            // SelectProcess
            // 
            this.SelectProcess.FormattingEnabled = true;
            this.SelectProcess.Location = new System.Drawing.Point(85, 148);
            this.SelectProcess.Margin = new System.Windows.Forms.Padding(4);
            this.SelectProcess.Name = "SelectProcess";
            this.SelectProcess.Size = new System.Drawing.Size(405, 28);
            this.SelectProcess.TabIndex = 3;
            this.SelectProcess.SelectedIndexChanged += new System.EventHandler(this.SelectProcess_SelectedIndexChanged);
            this.SelectProcess.Click += new System.EventHandler(this.SelectProcess_Clieck);
            // 
            // PlayerData
            // 
            this.PlayerData.Controls.Add(this.Grenade);
            this.PlayerData.Controls.Add(this.Angle);
            this.PlayerData.Controls.Add(this.Postion);
            this.PlayerData.Controls.Add(this.Armor);
            this.PlayerData.Controls.Add(this.Bullet);
            this.PlayerData.Controls.Add(this.Health);
            this.PlayerData.Location = new System.Drawing.Point(533, 123);
            this.PlayerData.Margin = new System.Windows.Forms.Padding(4);
            this.PlayerData.Name = "PlayerData";
            this.PlayerData.Padding = new System.Windows.Forms.Padding(4);
            this.PlayerData.Size = new System.Drawing.Size(397, 233);
            this.PlayerData.TabIndex = 4;
            this.PlayerData.TabStop = false;
            this.PlayerData.Text = "Player Data";
            // 
            // Grenade
            // 
            this.Grenade.AutoSize = true;
            this.Grenade.Location = new System.Drawing.Point(8, 184);
            this.Grenade.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Grenade.Name = "Grenade";
            this.Grenade.Size = new System.Drawing.Size(67, 20);
            this.Grenade.TabIndex = 11;
            this.Grenade.Text = "수류탄 : ";
            // 
            // Angle
            // 
            this.Angle.AutoSize = true;
            this.Angle.Location = new System.Drawing.Point(158, 95);
            this.Angle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Angle.Name = "Angle";
            this.Angle.Size = new System.Drawing.Size(52, 20);
            this.Angle.TabIndex = 10;
            this.Angle.Text = "앵글 : ";
            // 
            // Postion
            // 
            this.Postion.AutoSize = true;
            this.Postion.Location = new System.Drawing.Point(158, 48);
            this.Postion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Postion.Name = "Postion";
            this.Postion.Size = new System.Drawing.Size(52, 20);
            this.Postion.TabIndex = 9;
            this.Postion.Text = "위치 : ";
            // 
            // Armor
            // 
            this.Armor.AutoSize = true;
            this.Armor.Location = new System.Drawing.Point(8, 95);
            this.Armor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Armor.Name = "Armor";
            this.Armor.Size = new System.Drawing.Size(67, 20);
            this.Armor.TabIndex = 7;
            this.Armor.Text = "방어구 : ";
            // 
            // Bullet
            // 
            this.Bullet.AutoSize = true;
            this.Bullet.Location = new System.Drawing.Point(8, 139);
            this.Bullet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Bullet.Name = "Bullet";
            this.Bullet.Size = new System.Drawing.Size(52, 20);
            this.Bullet.TabIndex = 6;
            this.Bullet.Text = "탄창 : ";
            // 
            // Health
            // 
            this.Health.AutoSize = true;
            this.Health.Location = new System.Drawing.Point(8, 48);
            this.Health.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Health.Name = "Health";
            this.Health.Size = new System.Drawing.Size(52, 20);
            this.Health.TabIndex = 5;
            this.Health.Text = "체력 : ";
            // 
            // ProcessSelect
            // 
            this.ProcessSelect.AutoSize = true;
            this.ProcessSelect.Location = new System.Drawing.Point(85, 123);
            this.ProcessSelect.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ProcessSelect.Name = "ProcessSelect";
            this.ProcessSelect.Size = new System.Drawing.Size(167, 20);
            this.ProcessSelect.TabIndex = 11;
            this.ProcessSelect.Text = "프로세스 선택해주세요.";
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(768, 401);
            this.Exit.Margin = new System.Windows.Forms.Padding(4);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(121, 31);
            this.Exit.TabIndex = 12;
            this.Exit.Text = "종료";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // healthHackDuring
            // 
            this.healthHackDuring.AutoSize = true;
            this.healthHackDuring.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.healthHackDuring.Location = new System.Drawing.Point(429, 189);
            this.healthHackDuring.Name = "healthHackDuring";
            this.healthHackDuring.Size = new System.Drawing.Size(61, 23);
            this.healthHackDuring.TabIndex = 13;
            this.healthHackDuring.Text = "미동작";
            // 
            // bulletHackDuring
            // 
            this.bulletHackDuring.AutoSize = true;
            this.bulletHackDuring.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.bulletHackDuring.Location = new System.Drawing.Point(429, 261);
            this.bulletHackDuring.Name = "bulletHackDuring";
            this.bulletHackDuring.Size = new System.Drawing.Size(61, 23);
            this.bulletHackDuring.TabIndex = 14;
            this.bulletHackDuring.Text = "미동작";
            // 
            // GrenadeHack
            // 
            this.GrenadeHack.Location = new System.Drawing.Point(322, 298);
            this.GrenadeHack.Margin = new System.Windows.Forms.Padding(4);
            this.GrenadeHack.Name = "GrenadeHack";
            this.GrenadeHack.Size = new System.Drawing.Size(100, 31);
            this.GrenadeHack.TabIndex = 15;
            this.GrenadeHack.Text = "수류탄 핵";
            this.GrenadeHack.UseVisualStyleBackColor = true;
            this.GrenadeHack.Click += new System.EventHandler(this.GrenadeHack_Click);
            // 
            // grenadeHackDuring
            // 
            this.grenadeHackDuring.AutoSize = true;
            this.grenadeHackDuring.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grenadeHackDuring.Location = new System.Drawing.Point(429, 300);
            this.grenadeHackDuring.Name = "grenadeHackDuring";
            this.grenadeHackDuring.Size = new System.Drawing.Size(61, 23);
            this.grenadeHackDuring.TabIndex = 16;
            this.grenadeHackDuring.Text = "미동작";
            // 
            // armorHackDuring
            // 
            this.armorHackDuring.AutoSize = true;
            this.armorHackDuring.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.armorHackDuring.Location = new System.Drawing.Point(429, 224);
            this.armorHackDuring.Name = "armorHackDuring";
            this.armorHackDuring.Size = new System.Drawing.Size(61, 23);
            this.armorHackDuring.TabIndex = 18;
            this.armorHackDuring.Text = "미동작";
            // 
            // ArmorHack
            // 
            this.ArmorHack.Location = new System.Drawing.Point(322, 222);
            this.ArmorHack.Margin = new System.Windows.Forms.Padding(4);
            this.ArmorHack.Name = "ArmorHack";
            this.ArmorHack.Size = new System.Drawing.Size(100, 31);
            this.ArmorHack.TabIndex = 17;
            this.ArmorHack.Text = "방어구 핵";
            this.ArmorHack.UseVisualStyleBackColor = true;
            this.ArmorHack.Click += new System.EventHandler(this.ArmorHack_Click);
            // 
            // healthsetlabel
            // 
            this.healthsetlabel.AutoSize = true;
            this.healthsetlabel.Location = new System.Drawing.Point(84, 189);
            this.healthsetlabel.Name = "healthsetlabel";
            this.healthsetlabel.Size = new System.Drawing.Size(59, 20);
            this.healthsetlabel.TabIndex = 19;
            this.healthsetlabel.Text = "설정 값";
            // 
            // HealthSet
            // 
            this.HealthSet.Location = new System.Drawing.Point(149, 186);
            this.HealthSet.Name = "HealthSet";
            this.HealthSet.Size = new System.Drawing.Size(166, 27);
            this.HealthSet.TabIndex = 20;
            this.HealthSet.Text = "1000";
            this.HealthSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ArmorSet
            // 
            this.ArmorSet.Location = new System.Drawing.Point(150, 223);
            this.ArmorSet.Name = "ArmorSet";
            this.ArmorSet.Size = new System.Drawing.Size(166, 27);
            this.ArmorSet.TabIndex = 22;
            this.ArmorSet.Text = "1000";
            this.ArmorSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // armorsetlabel
            // 
            this.armorsetlabel.AutoSize = true;
            this.armorsetlabel.Location = new System.Drawing.Point(85, 226);
            this.armorsetlabel.Name = "armorsetlabel";
            this.armorsetlabel.Size = new System.Drawing.Size(59, 20);
            this.armorsetlabel.TabIndex = 21;
            this.armorsetlabel.Text = "설정 값";
            // 
            // BulletSet
            // 
            this.BulletSet.Location = new System.Drawing.Point(149, 260);
            this.BulletSet.Name = "BulletSet";
            this.BulletSet.Size = new System.Drawing.Size(166, 27);
            this.BulletSet.TabIndex = 24;
            this.BulletSet.Text = "1000";
            this.BulletSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // bulletsetlabel
            // 
            this.bulletsetlabel.AutoSize = true;
            this.bulletsetlabel.Location = new System.Drawing.Point(84, 263);
            this.bulletsetlabel.Name = "bulletsetlabel";
            this.bulletsetlabel.Size = new System.Drawing.Size(59, 20);
            this.bulletsetlabel.TabIndex = 23;
            this.bulletsetlabel.Text = "설정 값";
            // 
            // GrenadeSet
            // 
            this.GrenadeSet.Location = new System.Drawing.Point(149, 299);
            this.GrenadeSet.Name = "GrenadeSet";
            this.GrenadeSet.Size = new System.Drawing.Size(166, 27);
            this.GrenadeSet.TabIndex = 26;
            this.GrenadeSet.Text = "1000";
            this.GrenadeSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grenadesetlabel
            // 
            this.grenadesetlabel.AutoSize = true;
            this.grenadesetlabel.Location = new System.Drawing.Point(84, 302);
            this.grenadesetlabel.Name = "grenadesetlabel";
            this.grenadesetlabel.Size = new System.Drawing.Size(59, 20);
            this.grenadesetlabel.TabIndex = 25;
            this.grenadesetlabel.Text = "설정 값";
            // 
            // OverlayCB
            // 
            this.OverlayCB.AutoSize = true;
            this.OverlayCB.Location = new System.Drawing.Point(85, 363);
            this.OverlayCB.Name = "OverlayCB";
            this.OverlayCB.Size = new System.Drawing.Size(149, 24);
            this.OverlayCB.TabIndex = 27;
            this.OverlayCB.Text = "WallHack On/Off";
            this.OverlayCB.UseVisualStyleBackColor = true;
            this.OverlayCB.CheckedChanged += new System.EventHandler(this.OverlayCB_CheckedChanged);
            // 
            // JumpCB
            // 
            this.JumpCB.AutoSize = true;
            this.JumpCB.Location = new System.Drawing.Point(306, 363);
            this.JumpCB.Name = "JumpCB";
            this.JumpCB.Size = new System.Drawing.Size(155, 24);
            this.JumpCB.TabIndex = 28;
            this.JumpCB.Text = "JumpHack On/Off";
            this.JumpCB.UseVisualStyleBackColor = true;
            this.JumpCB.CheckedChanged += new System.EventHandler(this.JumpCB_CheckedChanged);
            // 
            // RecoilCB
            // 
            this.RecoilCB.AutoSize = true;
            this.RecoilCB.Location = new System.Drawing.Point(84, 405);
            this.RecoilCB.Name = "RecoilCB";
            this.RecoilCB.Size = new System.Drawing.Size(130, 24);
            this.RecoilCB.TabIndex = 29;
            this.RecoilCB.Text = "무반동 On/Off";
            this.RecoilCB.UseVisualStyleBackColor = true;
            this.RecoilCB.CheckedChanged += new System.EventHandler(this.RecoilCB_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 481);
            this.Controls.Add(this.RecoilCB);
            this.Controls.Add(this.JumpCB);
            this.Controls.Add(this.OverlayCB);
            this.Controls.Add(this.GrenadeSet);
            this.Controls.Add(this.grenadesetlabel);
            this.Controls.Add(this.BulletSet);
            this.Controls.Add(this.bulletsetlabel);
            this.Controls.Add(this.ArmorSet);
            this.Controls.Add(this.armorsetlabel);
            this.Controls.Add(this.HealthSet);
            this.Controls.Add(this.healthsetlabel);
            this.Controls.Add(this.armorHackDuring);
            this.Controls.Add(this.ArmorHack);
            this.Controls.Add(this.grenadeHackDuring);
            this.Controls.Add(this.GrenadeHack);
            this.Controls.Add(this.bulletHackDuring);
            this.Controls.Add(this.healthHackDuring);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.ProcessSelect);
            this.Controls.Add(this.PlayerData);
            this.Controls.Add(this.SelectProcess);
            this.Controls.Add(this.BulletHack);
            this.Controls.Add(this.HealthHack);
            this.Controls.Add(this.Title);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "AssaultCube 1.3.0.2 Game Hack";
            this.PlayerData.ResumeLayout(false);
            this.PlayerData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label Title;
        private Button HealthHack;
        private Button BulletHack;
        private ComboBox SelectProcess;
        private GroupBox PlayerData;
        private Label Armor;
        private Label Bullet;
        private Label Health;
        private Label Angle;
        private Label Postion;
        private Label ProcessSelect;
        private Button Exit;
        private System.Windows.Forms.Timer timer1;
        private Label Grenade;
        private Label healthHackDuring;
        private Label bulletHackDuring;
        private Button GrenadeHack;
        private Label grenadeHackDuring;
        private Label armorHackDuring;
        private Button ArmorHack;
        private Label healthsetlabel;
        private TextBox HealthSet;
        private TextBox ArmorSet;
        private Label armorsetlabel;
        private TextBox BulletSet;
        private Label bulletsetlabel;
        private TextBox GrenadeSet;
        private Label grenadesetlabel;
        private CheckBox OverlayCB;
        private CheckBox JumpCB;
        private CheckBox RecoilCB;
    }
}