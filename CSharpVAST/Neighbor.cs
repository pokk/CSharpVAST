using System;
using System.Collections.Generic;

namespace CSharpVAST
{
    class Neighbor
    {
        public class IcpDistance : IComparer<Node>  // 拿來比較距離的類別, 從小排到大
        {
            // 按距離排序
            public int Compare(Node x, Node y)
            {
                return x.distance.CompareTo(y.distance);
            }
        }

        private List<Node> list;

        public Neighbor()  // constructer
        {
            list = new List<Node>();
        }

        public bool Add(ulong id, string name, string socketip, int socketport, float x, float y, int direct)
        {
            list.Add(new Node(id, name, socketip, socketport, x, y, direct));
            return true;
        }

        public bool Remove(ulong id, List<VoiceNetwork> voicelist)
        {
            for (int i = 0; i < list.Count; i++ )
                if (list[i].id == id)
                {
                    list.RemoveAt(i);
                    voicelist[i].Stop_Thread();
                    voicelist.RemoveAt(i);
                    return true;
                }
            return false;
        }

        public bool Find(ulong id)
        {
            foreach (Node n in list)
                if (n.id == id)
                    return true;
            return false;
        }

        public int FindIndex(ulong id)
        {
            for (int i = 0; i < list.Count; i++)
                if (list[i].id == id)
                    return i;
            return -1;
        }

        public void Update(ulong id, float x, float y, int direct, UInt16 aoi_radius, float self_x, float self_y, List<VoiceNetwork> voicelist)  // 接收到訊息的時候
        {
            for (int i = 0; i < list.Count; i++)
            {
                // 若距離有在 aoi 範圍內就更新
                if (list[i].id == id)
                    if (list[i].x != x || list[i].y != y)
                    {
                        list[i].x = x;
                        list[i].y = y;
                        list[i].direct = direct;
                    }
                
                double x_dis = list[i].x - self_x, y_dis = list[i].y - self_y;
                double distance = Math.Sqrt(Math.Pow(x_dis, 2.0) + Math.Pow(y_dis, 2.0));
                list[i].distance = distance;  // 記錄離本身距離多遠

                // 若距離超過 aoi 範圍就從鄰居名單中踢除
                if (distance > aoi_radius)  
                {
                    list.RemoveAt(i);
                    voicelist[i].Stop_Thread();
                    voicelist.RemoveAt(i);
                }
            }
        }

        public void Update(UInt16 aoi_radius, float self_x, float self_y, List<VoiceNetwork> voicelist)  // 自己在移動時
        {
            for (int i = 0; i < list.Count; i++)
            {
                double x_dis = list[i].x - self_x, y_dis = list[i].y - self_y;
                double distance = Math.Sqrt(Math.Pow(x_dis, 2.0) + Math.Pow(y_dis, 2.0));
                list[i].distance = distance;  // 記錄離本身距離多遠

                // 若距離超過 aoi 範圍就從鄰居名單中踢除
                if (distance > aoi_radius)
                {
                    list.RemoveAt(i);
                    voicelist[i].Stop_Thread();
                    voicelist.RemoveAt(i);
                }
            }
        }

        public void Update_SocketPort(ulong id, int port)
        {
            int index = FindIndex(id);
            if (index != -1)
                list[index].socketport = port;
        }

        static public List<Node> GetSortDistance(List<Node> list)  // 取得距離大小 sorting 之後並只回傳 id 順序
        {
            List<Node> temp_list = list;  // 暫存要 sorting

            temp_list.Sort(new IcpDistance());  // 以距離大小來作 sorting

            return temp_list;
        }

        public List<Node> GetList()
        {
            return list;
        }
    }
}