namespace CSharpVAST
{
    class Node
    {
        public ulong id { get; set; }
        public string socketip { get; set; }
        public int socketport { get; set; }
        public string name { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public int direct { get; set; }  // 方向
        public double distance { get; set; }

        public Node(ulong id, string name, string socketip, int socketport, float x, float y, int direct)
        {
            this.id = id;
            this.socketport = socketport;
            this.socketip = socketip;
            this.name = name;
            this.x = x;
            this.y = y;
            this.direct = direct;
            distance = 0;
        }
    }
}
