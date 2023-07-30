using System;
using System.Diagnostics;
using ProcessMemoryReaderLib;

namespace AssaltCubeH
{
    public partial class Form1 : Form
    {
        Process[] MyProcess; //���μ��� ����� ������ ���
        ProcessMemoryReader mem = new ProcessMemoryReader();
        Process attachProc;
        OverlayForm overlayForm = new OverlayForm();

        bool attach = false;
        bool healthHack = false;
        bool armorHack = false;
        bool bulletHack = false;
        bool grenadeHack = false;
        bool wallHack = false;
        bool jumpHack = false;
        bool recoilHack = false;

        PlayerData mainPlayer;
        PlayerData[] enemyPlayer = new PlayerData[30];

        public Form1()
        {
            InitializeComponent();
        }

        //���� ��ư Ŭ�� �� ���� �Լ�
        private void Exit_Click(object sender, EventArgs e)
        {
            DialogResult result; //���� Ŭ�� �� �޽��� ����
            result = MessageBox.Show("�����Ͻðڽ��ϱ�?", "����޽���", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                this.DialogResult = DialogResult.Abort;
                Application.Exit();
            }
        }

        // Ŭ���Ͽ��� �� ���μ��� ���
        private void SelectProcess_Clieck(object sender, EventArgs e)
        {
            SelectProcess.Items.Clear(); //������ ���μ��� ����� �ʱ�ȭ
            MyProcess = Process.GetProcesses(); //���μ��� ����� �ҷ��´�.

            //���μ��� ��� ��ü ã�� (Process ��ü ���̸�ŭ �ݺ�)
            for (int i = 0; i < MyProcess.Length; i++)
            {
                String text = MyProcess[i].ProcessName + "-" + MyProcess[i].Id;
                SelectProcess.Items.Add(text);
            }
        }

        // SelectProcess �޴��߿� � �׸��� Ŭ������ �� ������ ����
        private void SelectProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Ŭ�� �� �ش� ���μ����� ���� ������ �����´�.
                //���μ��� �޸� �б�/����
                if (SelectProcess.SelectedIndex != -1) //���μ��� ����� �����ߴٸ�
                {
                    String selectedItem = SelectProcess.SelectedItem.ToString();
                    int pid = int.Parse(selectedItem.Split('-')[selectedItem.Split('-').Length - 1]);
                    attachProc = Process.GetProcessById(pid);

                    //���μ����� ����� �� (������ ��� ����)
                    mem.ReadProcess = attachProc; //� ���μ����� ������ ����
                    mem.OpenProcess(); //���μ��� ����

                    MessageBox.Show("���μ��� ���� ����!" + attachProc.ProcessName);
                    int base_ptr = attachProc.MainModule.BaseAddress.ToInt32() + 0x0017E0A8;

                    int player_base = mem.ReadInt(base_ptr);


                    mainPlayer = new PlayerData(player_base);

                    attach = true; //���μ��� ������ �����Ͽ��ٸ� attach�� true�� ����
                }
            }
            catch (Exception ex) // ���μ��� ���� ���� �ߴٸ�..
            {
                attach = false;
                MessageBox.Show("���μ��� ���� ����" + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) // 0.001�ʸ��� ����
        {
            if (attach)
            {
                try
                {
                    mainPlayer.SetPlayerData(mem); // ������ ����͸�

                    //ü�� �� ��ư Ŭ�� �� ü�� �� �۵�
                    if (healthHack)
                    {
                        mainPlayer.healthHack(mem, int.Parse(HealthSet.Text != "" ? HealthSet.Text : "1000")); //����� �Է°��� ���� �� �⺻�� 1000
                    }

                    //�� �� ��ư Ŭ�� �� ü�� �� �۵�
                    if (armorHack)
                    {
                        mainPlayer.armorhHack(mem, int.Parse(ArmorSet.Text != "" ? ArmorSet.Text : "1000")); //����� �Է°��� ���� �� �⺻�� 1000
                    }

                    //źâ �� ��ư Ŭ�� �� ü�� �� �۵�
                    if (bulletHack)
                    {
                        mainPlayer.bulletHack(mem, int.Parse(BulletSet.Text != "" ? BulletSet.Text : "1000")); //����� �Է°��� ���� �� �⺻�� 1000
                    }

                    //����ź �� ��ư Ŭ�� �� ü�� �� �۵�
                    if (grenadeHack)
                    {
                        mainPlayer.grenadeHack(mem, int.Parse(GrenadeSet.Text != "" ? GrenadeSet.Text : "1000")); //����� �Է°��� ���� �� �⺻�� 1000
                    }

                    //�����̽� �ٿ� ���� ���¸� Ȯ��
                    int hotkey_space = ProcessMemoryReaderApi.GetKeyState(0x20);
                    // ���� �� üũ �ڽ� üũ �� �����̽� �� ��ư Ŭ�� �� ���� �� �۵�
                    if (jumpHack && (hotkey_space & 0x8000) != 0)
                    {
                        mainPlayer.jumpHack(mem); //���� �ɷ�ġ 2�� ���
                    }

                    //���ݵ� �� üũ �� ����
                    if (recoilHack)
                    {
                        mainPlayer.recoilHack(mem);
                    }
                    else // ���ݵ� �� üũ ���� �� ����
                    {
                        mainPlayer.uncoilHack(mem);
                    }

                    //���콺 ������Ű�� ���� ���¸� Ȯ��
                    int hotkey = ProcessMemoryReaderApi.GetKeyState(0x02);

                    if (wallHack || (hotkey & 0x8000) != 0)
                    {
                        GetEnemyState(mem); // ���鿡 ���� ���� ����
                    }

                    //�� �� �۵�
                    if (wallHack)
                    {
                        overlayForm.wallHack(mainPlayer, enemyPlayer);
                    }

                    if ((hotkey & 0x8000) != 0) // Ű�� �����ִٸ�...
                    {
                        float min_err = 100000; // ������ ������ ū ������ �ʱ�ȭ
                        float err = 0;
                        double min_x_angle = 0;
                        double min_y_angle = 0;


                        for (int i = 0; i < 30; i++)
                        {
                            // aim hack algorithm
                            err = mainPlayer.getAimErr(mem, enemyPlayer[i].head_x_angle, enemyPlayer[i].head_y_angle);
                            if (min_err > err)
                            {
                                min_err = err;
                                min_x_angle = enemyPlayer[i].head_x_angle;
                                min_y_angle = enemyPlayer[i].head_y_angle;
                            }
                        }
                        mainPlayer.AimHack(mem, min_x_angle, min_y_angle);
                    }

                    Health.Text = "ü�� : " + mainPlayer.health;
                    Armor.Text = "�� : " + mainPlayer.armor;
                    Bullet.Text = "źâ : " + mainPlayer.bullet + "/" + mainPlayer.bullet_case;
                    Grenade.Text = "����ź : " + mainPlayer.grenade;
                    Postion.Text = "��ġ : " + (mainPlayer.x_pos != 0 ? mainPlayer.x_pos.ToString("#.##") : "0") + ", " + (mainPlayer.y_pos != 0 ? mainPlayer.y_pos.ToString("#.##") : "0") + ", " + (mainPlayer.z_pos != 0 ? mainPlayer.z_pos.ToString("#.##") : "0");
                    Angle.Text = "�ޱ� : " + (mainPlayer.x_angle != 0 ? mainPlayer.x_angle.ToString("#.##") : "0") + ", " + (mainPlayer.y_angle != 0 ? mainPlayer.y_angle.ToString("#.##") : "0");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }

            }
        }

        //Z_angle ���� ���ϱ�
        private double GetZDegree(PlayerData mainPlayer, PlayerData enemyPlayer)
        {
            double xy_distance = Math.Sqrt(Math.Pow(mainPlayer.x_pos - enemyPlayer.x_pos, 2) + Math.Pow(mainPlayer.z_pos - enemyPlayer.z_pos, 2));
            double z = mainPlayer.z_pos - enemyPlayer.z_pos;
            double correction = 1;

            if (z > 0)
            {
                correction = -1;
            }


            return correction * Math.Abs(Math.Atan(z / xy_distance) * 180 / Math.PI);
        }

        //X_angle ���� ���ϱ�
        private double Get2DDegree(PlayerData mainPlayer, PlayerData enemyPlayer)
        {
            double x = mainPlayer.x_pos - enemyPlayer.x_pos;
            double y = mainPlayer.y_pos - enemyPlayer.y_pos;
            
            double correction = 270;

            if (x < 0) correction = 90;

            return correction + Math.Atan(y / x) * 180 / Math.PI;
        }

        //���� ĳ���Ϳ� ������ �Ÿ� ���ϱ�
        private double GetDistance(PlayerData mainPlayer, PlayerData enemyPlayer)
        {
            //��Ÿ����� ������ ����� xy�� �Ÿ��� ����
            double xy_distance = Math.Sqrt(Math.Pow(mainPlayer.x_pos - enemyPlayer.x_pos, 2) + Math.Pow(mainPlayer.y_pos - enemyPlayer.y_pos, 2));
            //��Ÿ����� ������ ����� �Ÿ��� ����
            double distance = Math.Sqrt(Math.Pow(xy_distance, 2) + Math.Pow(mainPlayer.z_pos - enemyPlayer.z_pos, 2));

            return distance;
        }

        private void GetEnemyState(ProcessMemoryReader mem)
        {
            try
            {
                int base_ptr = attachProc.MainModule.BaseAddress.ToInt32() + 0x00191FCC;
            
                for (int i=0; i< 30; i++)
                {
                
                        int[] offsetArray = { i * 4, 0 }; // 0, 4, 8, 12 �� 30���� �÷��̾� �����͸� �ҷ���
                        int player_base = mem.ReadMultiLevelPointer(base_ptr, 4, offsetArray);
                        enemyPlayer[i] = new PlayerData(player_base);
                        enemyPlayer[i].SetPlayerData(mem);
                        enemyPlayer[i].distance = GetDistance(mainPlayer, enemyPlayer[i]);
                        enemyPlayer[i].head_x_angle = Get2DDegree(mainPlayer, enemyPlayer[i]);
                        enemyPlayer[i].head_y_angle = GetZDegree(mainPlayer, enemyPlayer[i]);
                
                }
            }
            catch (Exception e) { }

        }


        //ü�� �� ��ư Ŭ�� �� �۵� 
        private void HealthHack_Click(object sender, EventArgs e)
        {

            if (healthHack)
            {
                healthHack = false;
                healthHackDuring.Text = "�̵���";
                HealthSet.ReadOnly = false;
            }
            else
            {
                healthHack = true;
                healthHackDuring.Text = "������...";
                HealthSet.ReadOnly = true;
            }
        }


        //�� �� ��ư Ŭ�� �� �۵� 
        private void ArmorHack_Click(object sender, EventArgs e)
        {
            if (armorHack)
            {
                armorHack = false;
                armorHackDuring.Text = "�̵���";
                ArmorSet.ReadOnly = false;
            }
            else
            {
                armorHack = true;
                armorHackDuring.Text = "������...";
                ArmorSet.ReadOnly = true;   
            }
        }


        //źâ �� ���
        //źâ �� ��ư Ŭ�� �� �۵� 
        private void BulletHack_Click(object sender, EventArgs e)
        {

            if (bulletHack)
            {
                bulletHack = false;
                bulletHackDuring.Text = "�̵���";
                BulletSet.ReadOnly = false; 
            }
            else
            {
                bulletHack = true;
                bulletHackDuring.Text = "������...";
                BulletSet.ReadOnly = true;
            }
        }


        //����ź �� ����
        //����ź �� ��ư Ŭ�� �� �۵� 
        private void GrenadeHack_Click(object sender, EventArgs e)
        {
            if (grenadeHack)
            {
                grenadeHack = false;
                grenadeHackDuring.Text = "�̵���";
                GrenadeSet.ReadOnly = false;    
            }
            else
            {
                grenadeHack = true;
                grenadeHackDuring.Text = "������...";
                GrenadeSet.ReadOnly = true;
            }
        }

        //���� ����
        //üũ ������ ������� Ŭ���ϰ� �Ǹ� ����
        private void OverlayCB_CheckedChanged(object sender, EventArgs e)
        {
            if (OverlayCB.Checked)
            {
                // ���࿡ üũ�ڽ��� üŷ�� �ִٸ� -> overlayform�� �����ش�.
                overlayForm.Show();
                wallHack = true;
            }
            else
            {
                // ���࿡ üũ�ڽ��� üŷ�� ���� �ʴٸ� -> overlayform�� �����.
                overlayForm.Hide();
                wallHack = false;
            }
        }

        private void JumpCB_CheckedChanged(object sender, EventArgs e)
        {
            if (JumpCB.Checked)
            {
                // ���࿡ üũ�ڽ��� üŷ�� �ִٸ� -> Jump �ɷ�ġ�� �ø���.
                jumpHack = true;
            }
            else
            {
                // ���࿡ üũ�ڽ��� üŷ�� ���� �ʴٸ� -> Jump �ɷ�ġ�� ������ ������
                jumpHack = false;
            }
        }

        private void RecoilCB_CheckedChanged(object sender, EventArgs e)
        {
            if (RecoilCB.Checked)
            {
                // ���࿡ üũ�ڽ��� üŷ�� �ִٸ� -> Jump �ɷ�ġ�� �ø���.
                recoilHack = true;
            }
            else
            {
                // ���࿡ üũ�ڽ��� üŷ�� ���� �ʴٸ� -> Jump �ɷ�ġ�� ������ ������
                recoilHack = false;
            }
        }
    }
}
