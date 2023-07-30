using ProcessMemoryReaderLib;
using System.Diagnostics;
using System.Net;
using static System.Windows.Forms.Design.AxImporter;

namespace AssaltCubeH
{
    internal class PlayerData
    {
        int base_addr; //"ac_client.exe"+0017E0A8 구조체의 위치
        
        //구조체의 모양
        int health_offset = 0xEC; //체력
        int bullet_offset = 0x140; //탄창
        int bullet_case_offset = 0x11C; //예비 탄창
        int grenade_offset = 0x144; // 수류탄
        int armor_offset = 0xF0; //방어구
        int x_pos_offset = 0x28; //x 위치
        int y_pos_offset = 0x2c; //y 위치
        int z_pos_offset = 0x30; //z 위치
        int x_angle_offset = 0x34; //x 시선
        int y_angle_offset = 0x38; //y 시선
        int jump_offset = 0x18; //점프 높이
        int name_offset = 0x205; //이름

        // 캐릭터 정보
        public int health;
        public int bullet;
        public int bullet_case;
        public int grenade;
        public int armor;
        public float x_pos;
        public float y_pos;
        public float z_pos;
        public float x_angle;
        public float y_angle;
        public double distance;
        public double head_x_angle;
        public double head_y_angle;

        //초기화 함수
        public PlayerData(int player_base)
        {
            // mainPlayer 또는 적 플레이어 구조체의 위치
            base_addr = player_base;

            health = 0;
            bullet = 0;
            bullet_case = 0;
            grenade = 0;
            armor = 0;
            x_pos = 0;
            y_pos = 0;
            z_pos = 0;
            x_angle = 0;
            y_angle = 0;
            distance = 0;
            head_x_angle = 0;
            head_y_angle = 0;
        }

        internal void SetPlayerData(ProcessMemoryReader mem)
        {
            health = mem.ReadInt(base_addr + health_offset);
            bullet = mem.ReadInt(base_addr + bullet_offset);
            bullet_case = mem.ReadInt(base_addr + bullet_case_offset);
            grenade = mem.ReadInt(base_addr + grenade_offset);
            armor = mem.ReadInt(base_addr + armor_offset);
            x_pos = mem.ReadFloat(base_addr + x_pos_offset);
            y_pos = mem.ReadFloat(base_addr + y_pos_offset);
            z_pos = mem.ReadFloat(base_addr + z_pos_offset);
            x_angle = mem.ReadFloat(base_addr + x_angle_offset);
            y_angle = mem.ReadFloat(base_addr + y_angle_offset);
        }

        internal void healthHack(ProcessMemoryReader mem, int OptionSet)
        {
            mem.WriteInt(base_addr + health_offset, OptionSet); // 체력을 사용자 입력값으로 (메모리에 쓰기)
            mem.WriteInt(base_addr + health_offset, OptionSet); // 체력을 사용자 입력값으로 (메모리에 쓰기)
            mem.WriteByte(base_addr - 0xBBC1D, 80);
            
        }

        internal void bulletHack(ProcessMemoryReader mem, int OptionSet)
        {
            mem.WriteInt(base_addr + bullet_offset, OptionSet); // 탄약을 사용자 입력값으로 (메모리에 쓰기)
            mem.WriteInt(base_addr + bullet_case_offset, OptionSet); // 탄창을 사용자 입력값으로 (메모리에 쓰기)
        }

        internal void grenadeHack(ProcessMemoryReader mem, int OptionSet)
        {
            mem.WriteInt(base_addr + grenade_offset, OptionSet); // 수류탄을 사용자 입력값으로 (메모리에 쓰기)
        }

        internal void armorhHack(ProcessMemoryReader mem, int OptionSet)
        {
            mem.WriteInt(base_addr + armor_offset, OptionSet); // 방어구를 사용자 입력값으로 (메모리에 쓰기)
        }

        internal void AimHack(ProcessMemoryReader mem, double x_angel, double y_angel)
        {
            float _x = Double2Float(x_angel);
            float _y = Double2Float(y_angel);
          
            mem.WriteFloat(base_addr + x_angle_offset, _x); // X angle을 메모리에 쓰기
            mem.WriteFloat(base_addr + y_angle_offset, _y); // Y angle을 메모리에 쓰기
        }

        // Double 형태를 Float 형태로 변환
        private float Double2Float(double input)
        {
            float result = (float)input;
            if (float.IsPositiveInfinity(result))
            {
                result = float.MaxValue;
            }
            else if(float.IsNegativeInfinity(result))
            {
                result = float.MinValue;
            }
            return result;
        }

        internal float getAimErr(ProcessMemoryReader mem, double _x_angle, double _y_angle)
        {
            return Double2Float(Math.Pow(x_angle - _x_angle, 2) + Math.Pow(y_angle - _y_angle, 2));
        }

        //점프 능력치 2배 향상 (점프 높이를 정하는 메모리 값 변조) 
        internal void jumpHack(ProcessMemoryReader mem)
        {
            mem.WriteFloat(base_addr + jump_offset, 4);
        }
         
        // 무반동으로 메모리 변조 (ReadOnly 여서 안되는듯...ㅠㅠ)
        internal void recoilHack(ProcessMemoryReader mem)
        {
            byte[] nope = { 0x90, 0x90, 0x90, 0x90, 0x90 };
            mem.WriteMem(base_addr - 0xBB1E5, nope);
            /*
            mem.WriteByte(base_addr - 0xBB1E5, 144);
            mem.WriteByte(base_addr - 0xBB1E4, 144);
            mem.WriteByte(base_addr - 0xBB1E3, 144);
            mem.WriteByte(base_addr - 0xBB1E2, 144);
            mem.WriteByte(base_addr - 0xBB1E1, 144);*/
        }

        // 원래 값으로 되돌리기
        internal void uncoilHack(ProcessMemoryReader mem)
        {
            mem.WriteByte(base_addr - 0xBB1E5, 243);
            mem.WriteByte(base_addr - 0xBB1E4, 15);
            mem.WriteByte(base_addr - 0xBB1E3, 17);
            mem.WriteByte(base_addr - 0xBB1E2, 86);
            mem.WriteByte(base_addr - 0xBB1E1, 56);
        }
    }
}