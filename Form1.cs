using System;
using System.Diagnostics;
using ProcessMemoryReaderLib;

namespace AssaltCubeH
{
    public partial class Form1 : Form
    {
        Process[] MyProcess; //프로세스 목록을 저장할 장소
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

        //종료 버튼 클릭 시 실행 함수
        private void Exit_Click(object sender, EventArgs e)
        {
            DialogResult result; //종료 클릭 시 메시지 띄우기
            result = MessageBox.Show("종료하시겠습니까?", "종료메시지", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                this.DialogResult = DialogResult.Abort;
                Application.Exit();
            }
        }

        // 클릭하였을 때 프로세스 목록
        private void SelectProcess_Clieck(object sender, EventArgs e)
        {
            SelectProcess.Items.Clear(); //기존의 프로세스 목록을 초기화
            MyProcess = Process.GetProcesses(); //프로세스 목록을 불러온다.

            //프로세스 목록 전체 찾기 (Process 전체 길이만큼 반복)
            for (int i = 0; i < MyProcess.Length; i++)
            {
                String text = MyProcess[i].ProcessName + "-" + MyProcess[i].Id;
                SelectProcess.Items.Add(text);
            }
        }

        // SelectProcess 메뉴중에 어떤 항목을 클릭했을 때 동작할 내용
        private void SelectProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //클릭 시 해당 프로세스에 대한 권한을 가져온다.
                //프로세스 메모리 읽기/수정
                if (SelectProcess.SelectedIndex != -1) //프로세스 목록을 선택했다면
                {
                    String selectedItem = SelectProcess.SelectedItem.ToString();
                    int pid = int.Parse(selectedItem.Split('-')[selectedItem.Split('-').Length - 1]);
                    attachProc = Process.GetProcessById(pid);

                    //프로세스를 열어야 함 (권한을 얻기 위함)
                    mem.ReadProcess = attachProc; //어떤 프로세스를 열지를 저장
                    mem.OpenProcess(); //프로세스 열기

                    MessageBox.Show("프로세스 열기 성공!" + attachProc.ProcessName);
                    int base_ptr = attachProc.MainModule.BaseAddress.ToInt32() + 0x0017E0A8;

                    int player_base = mem.ReadInt(base_ptr);


                    mainPlayer = new PlayerData(player_base);

                    attach = true; //프로세스 열기의 성공하였다면 attach를 true로 변경
                }
            }
            catch (Exception ex) // 프로세스 열기 실패 했다면..
            {
                attach = false;
                MessageBox.Show("프로세스 열기 실패" + ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) // 0.001초마다 동작
        {
            if (attach)
            {
                try
                {
                    mainPlayer.SetPlayerData(mem); // 데이터 모니터링

                    //체력 핵 버튼 클릭 시 체력 핵 작동
                    if (healthHack)
                    {
                        mainPlayer.healthHack(mem, int.Parse(HealthSet.Text != "" ? HealthSet.Text : "1000")); //사용자 입력값이 없을 때 기본값 1000
                    }

                    //방어구 핵 버튼 클릭 시 체력 핵 작동
                    if (armorHack)
                    {
                        mainPlayer.armorhHack(mem, int.Parse(ArmorSet.Text != "" ? ArmorSet.Text : "1000")); //사용자 입력값이 없을 때 기본값 1000
                    }

                    //탄창 핵 버튼 클릭 시 체력 핵 작동
                    if (bulletHack)
                    {
                        mainPlayer.bulletHack(mem, int.Parse(BulletSet.Text != "" ? BulletSet.Text : "1000")); //사용자 입력값이 없을 때 기본값 1000
                    }

                    //수류탄 핵 버튼 클릭 시 체력 핵 작동
                    if (grenadeHack)
                    {
                        mainPlayer.grenadeHack(mem, int.Parse(GrenadeSet.Text != "" ? GrenadeSet.Text : "1000")); //사용자 입력값이 없을 때 기본값 1000
                    }

                    //스페이스 바에 대한 상태를 확인
                    int hotkey_space = ProcessMemoryReaderApi.GetKeyState(0x20);
                    // 점프 핵 체크 박스 체크 후 스페이스 바 버튼 클릭 시 점프 핵 작동
                    if (jumpHack && (hotkey_space & 0x8000) != 0)
                    {
                        mainPlayer.jumpHack(mem); //점프 능력치 2배 향상
                    }

                    //무반동 핵 체크 시 동작
                    if (recoilHack)
                    {
                        mainPlayer.recoilHack(mem);
                    }
                    else // 무반동 핵 체크 해제 시 동작
                    {
                        mainPlayer.uncoilHack(mem);
                    }

                    //마우스 오른쪽키에 대한 상태를 확인
                    int hotkey = ProcessMemoryReaderApi.GetKeyState(0x02);

                    if (wallHack || (hotkey & 0x8000) != 0)
                    {
                        GetEnemyState(mem); // 적들에 대한 정보 습득
                    }

                    //월 핵 작동
                    if (wallHack)
                    {
                        overlayForm.wallHack(mainPlayer, enemyPlayer);
                    }

                    if ((hotkey & 0x8000) != 0) // 키가 눌려있다면...
                    {
                        float min_err = 100000; // 에러를 굉장히 큰 값으로 초기화
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

                    Health.Text = "체력 : " + mainPlayer.health;
                    Armor.Text = "방어구 : " + mainPlayer.armor;
                    Bullet.Text = "탄창 : " + mainPlayer.bullet + "/" + mainPlayer.bullet_case;
                    Grenade.Text = "수류탄 : " + mainPlayer.grenade;
                    Postion.Text = "위치 : " + (mainPlayer.x_pos != 0 ? mainPlayer.x_pos.ToString("#.##") : "0") + ", " + (mainPlayer.y_pos != 0 ? mainPlayer.y_pos.ToString("#.##") : "0") + ", " + (mainPlayer.z_pos != 0 ? mainPlayer.z_pos.ToString("#.##") : "0");
                    Angle.Text = "앵글 : " + (mainPlayer.x_angle != 0 ? mainPlayer.x_angle.ToString("#.##") : "0") + ", " + (mainPlayer.y_angle != 0 ? mainPlayer.y_angle.ToString("#.##") : "0");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }

            }
        }

        //Z_angle 각도 구하기
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

        //X_angle 각도 구하기
        private double Get2DDegree(PlayerData mainPlayer, PlayerData enemyPlayer)
        {
            double x = mainPlayer.x_pos - enemyPlayer.x_pos;
            double y = mainPlayer.y_pos - enemyPlayer.y_pos;
            
            double correction = 270;

            if (x < 0) correction = 90;

            return correction + Math.Atan(y / x) * 180 / Math.PI;
        }

        //현재 캐릭터와 적과의 거리 구하기
        private double GetDistance(PlayerData mainPlayer, PlayerData enemyPlayer)
        {
            //피타고라스의 정리를 사용해 xy의 거리를 구함
            double xy_distance = Math.Sqrt(Math.Pow(mainPlayer.x_pos - enemyPlayer.x_pos, 2) + Math.Pow(mainPlayer.y_pos - enemyPlayer.y_pos, 2));
            //피타고라스의 정리를 사용해 거리를 구함
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
                
                        int[] offsetArray = { i * 4, 0 }; // 0, 4, 8, 12 총 30명의 플레이어 데이터를 불러옴
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


        //체력 핵 버튼 클릭 시 작동 
        private void HealthHack_Click(object sender, EventArgs e)
        {

            if (healthHack)
            {
                healthHack = false;
                healthHackDuring.Text = "미동작";
                HealthSet.ReadOnly = false;
            }
            else
            {
                healthHack = true;
                healthHackDuring.Text = "동작중...";
                HealthSet.ReadOnly = true;
            }
        }


        //방어구 핵 버튼 클릭 시 작동 
        private void ArmorHack_Click(object sender, EventArgs e)
        {
            if (armorHack)
            {
                armorHack = false;
                armorHackDuring.Text = "미동작";
                ArmorSet.ReadOnly = false;
            }
            else
            {
                armorHack = true;
                armorHackDuring.Text = "동작중...";
                ArmorSet.ReadOnly = true;   
            }
        }


        //탄창 핵 사용
        //탄창 핵 버튼 클릭 시 작동 
        private void BulletHack_Click(object sender, EventArgs e)
        {

            if (bulletHack)
            {
                bulletHack = false;
                bulletHackDuring.Text = "미동작";
                BulletSet.ReadOnly = false; 
            }
            else
            {
                bulletHack = true;
                bulletHackDuring.Text = "동작중...";
                BulletSet.ReadOnly = true;
            }
        }


        //수류탄 핵 실행
        //수류탄 핵 버튼 클릭 시 작동 
        private void GrenadeHack_Click(object sender, EventArgs e)
        {
            if (grenadeHack)
            {
                grenadeHack = false;
                grenadeHackDuring.Text = "미동작";
                GrenadeSet.ReadOnly = false;    
            }
            else
            {
                grenadeHack = true;
                grenadeHackDuring.Text = "동작중...";
                GrenadeSet.ReadOnly = true;
            }
        }

        //월핵 실행
        //체크 유무와 상관없이 클릭하게 되면 실행
        private void OverlayCB_CheckedChanged(object sender, EventArgs e)
        {
            if (OverlayCB.Checked)
            {
                // 만약에 체크박스가 체킹돼 있다면 -> overlayform을 보여준다.
                overlayForm.Show();
                wallHack = true;
            }
            else
            {
                // 만약에 체크박스가 체킹돼 있지 않다면 -> overlayform을 숨긴다.
                overlayForm.Hide();
                wallHack = false;
            }
        }

        private void JumpCB_CheckedChanged(object sender, EventArgs e)
        {
            if (JumpCB.Checked)
            {
                // 만약에 체크박스가 체킹돼 있다면 -> Jump 능력치를 올린다.
                jumpHack = true;
            }
            else
            {
                // 만약에 체크박스가 체킹돼 있지 않다면 -> Jump 능력치를 원래의 값으로
                jumpHack = false;
            }
        }

        private void RecoilCB_CheckedChanged(object sender, EventArgs e)
        {
            if (RecoilCB.Checked)
            {
                // 만약에 체크박스가 체킹돼 있다면 -> Jump 능력치를 올린다.
                recoilHack = true;
            }
            else
            {
                // 만약에 체크박스가 체킹돼 있지 않다면 -> Jump 능력치를 원래의 값으로
                recoilHack = false;
            }
        }
    }
}
