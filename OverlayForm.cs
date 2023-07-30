using AssaltCubeH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssaltCubeH
{
    public partial class OverlayForm : Form
    {
        Graphics g;
        Pen myPen = new Pen(Color.Red);
        IntPtr hAssualtCube; //핸들 정의
        PosEnemy[] posEnemy = new PosEnemy[30];

        public const string WINDOWNAME = "AssaultCube";
        RECT rect;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        public struct PosEnemy
        {
            public float x;
            public float y;
            public float size;
            public int health;
        }

        // SetWindowLong 함수는 윈도우 핸들의 속성을 변경하는 데 사용됩니다.
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        // GetWindowLong 함수는 윈도우 핸들의 속성을 가져오는 데 사용됩니다.
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        // GetWindowRect 함수는 윈도우의 위치와 크기 정보를 가져오는 데 사용됩니다.
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        // FindWindow 함수는 윈도우의 클래스 이름 또는 윈도우 제목을 기준으로 해당 윈도우 핸들을 검색하는 데 사용됩니다.
        [DllImport("user32")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

       
        public OverlayForm()
        {
            InitializeComponent();
        }

        private void OverlayForm_Load(object sender, EventArgs e)
        {
            /* 창에 대한 속성 조절 */
            this.BackColor = Color.Wheat; // 배경 색 없음
            this.TransparencyKey = Color.Wheat; // 투명한 영역에다가 이미지 업데이트
            this.TopMost = true; // 가장 상단 노출
            this.FormBorderStyle = FormBorderStyle.None; // 창의 틀이 완전히 삭제

            int presnetStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, presnetStyle | 0x80000 | 0x20); // 마우스 이벤트 뒤로 전달 속성 추가

            /* 창에 대한 위치와 크기 조절 */
            hAssualtCube = FindWindow(null, WINDOWNAME); // AssaultCube 창을 찾아 핸들을 저장 
            GetWindowRect(hAssualtCube, out rect); //윈도우 모양이 어떻게 생겼는지 찾기

            // 창 사이즈
            int height = rect.Bottom - rect.Top;
            int width = rect.Right - rect.Left;
            this.Size = new Size(width, height);

            // 창 위치
            this.Top = rect.Top;
            this.Left = rect.Left;


            timer1.Enabled = true;
        }



        // 모든 창이 초기화된 뒤에 사용되도록 로드되는 마지막에 타이머 온
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            GetWindowRect(hAssualtCube, out rect);

            // 창 사이즈
            int height = rect.Bottom - rect.Top;
            int width = rect.Right - rect.Left;
            this.Size = new Size(width, height);

            // 창 위치
            this.Top = rect.Top;
            this.Left = rect.Left;
        }

        private void OverlayForm_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            //g.DrawRectangle(myPen, 100, 100, 150, 150);

            for (int i = 0; i < 30; i++)
            {
                if (posEnemy[i].x != -1234)
                {
                    //캐릭터를 표시할 사각형 그리기
                    g.DrawRectangle(myPen, posEnemy[i].x - posEnemy[i].size / 2, posEnemy[i].y - posEnemy[i].size / 2, posEnemy[i].size, posEnemy[i].size * 2);
                    //g.DrawRectangle(myPen, (rect.Right - rect.Left)/ 2, (rect.Bottom - rect.Top) / 2, 10,10);

                    //캐릭터의 체력을 표시
                    Font font = new Font("Arial", 9); // 폰트 지정
                    SolidBrush brush = new SolidBrush(Color.Red);
                    string text = "체력 : " + posEnemy[i].health;
                    PointF location = new PointF(posEnemy[i].x - posEnemy[i].size / 2, posEnemy[i].y - posEnemy[i].size / 2 - 20);
                    g.DrawString(text, font, brush, location);
                }
            }
        }

        
        internal void wallHack(PlayerData mainPlayer, PlayerData[] enemyPlayer)
        {
            for (int i = 0; i < 30; i++) // 30명의 플레이어를 검사
            {
                //각도 계산
                float x_angle_pos = mainPlayer.x_angle - Double2Float(enemyPlayer[i].head_x_angle);
                float y_angle_pos = mainPlayer.y_angle - Double2Float(enemyPlayer[i].head_y_angle);

                // 실제 각도와 다르게 측정되는 경우, 실제 각도로 보정
                // 예: 359 - 1 = 358 --> -2도
                // 예: 1 - 359 = -358 --> 2도
                if (360 - 45 <= Math.Abs(x_angle_pos) && Math.Abs(x_angle_pos) <= 360)
                {
                    if (x_angle_pos > 0) // 360 - 0
                        x_angle_pos -= 360; // 예: 359 - 1 = 358 --> -2도
                    else
                        x_angle_pos += 360; // 예: 1 - 359 = -358 --> 2도
                }

                // 내 시야에 있는 것 체크
                if ((Math.Abs(x_angle_pos) <= 45) && enemyPlayer[i].health > 0) //길이가 총 90도, 체력이 0이상인 것만 표시
                {
                    float x_corr = (rect.Right - rect.Left) / 90 * x_angle_pos;
                    float y_corr = (rect.Bottom - rect.Top) / 60 * y_angle_pos;

                    posEnemy[i].x = ((rect.Right - rect.Left) / 2) - x_corr;
                    posEnemy[i].y = ((rect.Bottom - rect.Top) / 2) + y_corr;
                    posEnemy[i].size = Double2Float(1800 / enemyPlayer[i].distance);
                    posEnemy[i].health = enemyPlayer[i].health;
                }

                else
                {
                    posEnemy[i].x = -1234;
                    posEnemy[i].y = -1234;
                }
            }
            this.Invalidate(); // 페인트 초기화
        }

        private float Double2Float(double input)
        {
            float result = (float)input;
            if (float.IsPositiveInfinity(result))
            {
                result = float.MaxValue;
            }
            else if (float.IsNegativeInfinity(result))
            {
                result = float.MinValue;
            }
            return result;
        }
    }
}

