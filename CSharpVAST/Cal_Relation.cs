using System;
using System.Collections.Generic;

namespace CSharpVAST
{
    class Vector
    {
        public double x { get; set; }
        public double y { get; set; }  // 兩維向量

        public Vector(int x, int y)  // 建構子
        {
            this.x = x;
            this.y = y;
        }
        public static double dot(Vector v1, Vector v2)  // 兩個向量 Dot 起來
        {
            return v1.x * v2.x + v1.y * v2.y;
        }
        public static double len(Vector v)  // 算向量長度
        {
            return Math.Sqrt(Math.Pow(v.x, 2.0) + Math.Pow(v.y, 2.0));
        }
    }

    class Cal_Relation
    {
        
        #region 移動方向之類型
        const int LEFT_DOWN = 1;
        const int DOWN = 2;
        const int RIGHT_DOWN = 3;
        const int LEFT = 4;
        const int RIGHT = 6;
        const int LEFT_UP = 7;
        const int UP = 8;
        const int RIGHT_UP = 9;
        #endregion

        private List<Vector> DIRECT;  // 裡面儲存每個方向的向量

        private float self_x;  // 本身節點之x座標
        private float self_y;  // 本身節點之y座標
        private float max_distance;
        private List<Relation> relationlist;
        private List<Node> neighborlist;

        public Cal_Relation(float x, float y, float dis)
        {
            int[] _temp = { -1, 0, 1 };  // 用來給定向量使用
            
            self_x = x;
            self_y = y;
            max_distance = dis;
            relationlist = new List<Relation>();
            DIRECT = new List<Vector>();

            for (int i = 0; i < _temp.Length; i++)                          // 7(-1, 1)    8( 0, 1)    9( 1, 1)         每個按鍵的方向向量
                for (int j = 0; j < _temp.Length; j++ )                     // 4(-1, 0)    5( 0, 0)    6( 1, 0)
                    DIRECT.Add(new Vector(_temp[j], _temp[i]));             // 1(-1,-1)    2( 0,-1)    3( 1,-1)
        }

        public void Set_NeighborList(List<Node> list)  // 設定鄰居之後馬上更新鄰居之關係值
        {
            neighborlist = list;
        }

        public void Update_RelationList(int direct)  // 可能會有新的節點加入，所以要重新計算   parameter : direct 自己的面向
        {
            relationlist.RemoveRange(0, relationlist.Count);  // 先清空裡面所有物件
            foreach (Node n in neighborlist)   
            {
                double rel = Calculate_Distance(n.distance) + Calculate_Relation(direct, n.direct);  // 重新計算關係值  距離 + 面向
                relationlist.Add(new Relation(n.id, n.name, rel));  // 相對的新增一個關係度進去
            }
        }

        public double Calculate_Distance(double relation_distance)  // 計算"距離"上的關係度  parameter : relation_distance 在鄰居資訊裡面就有記錄
        {
            return 1 - (relation_distance / max_distance);
        }

        public double Calculate_Relation(int self_direct, int other_direct)  // 計算"面向"上的關係度  parameter : self_direct 自己的面向   other_direct 別人的面向
        {
            Vector v_self = DIRECT[self_direct - 1];
            Vector v_other = DIRECT[other_direct - 1];

            // 公式為 (acos((A‧B)/(A長度*B長度)) <-- 此算出為弧度, 因此需 * 180 / PI)     <--- 此算出度數
            double relation = (Math.Acos(Vector.dot(v_self, v_other) / (Vector.len(v_self) * Vector.len(v_other))) * 180 / 3.14) / 180;
            if (relation < 0)  // 恆為正
                relation *= -1;
            return relation;
        }

        public Node Get_Listener()  // 取得 Listener
        {
            double max = 0;
            ulong who = 0;
            foreach (Relation r in relationlist)
                if (r.relationship > max)
                {
                    max = r.relationship;
                    who = r.id;
                }
            foreach (Node n in neighborlist)
                if (n.id == who)
                    return n;
            return new Node(0, "0", null, 0, 0, 0, 0);
        }

        public List<Relation> Get_RelationList()
        {
            return relationlist;
        }
    }
}
