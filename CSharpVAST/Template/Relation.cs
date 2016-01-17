namespace CSharpVAST
{
    class Relation
    {
        public ulong id { get; set; }
        public string name { get; set; }
        public double relationship { get; set; }

        public Relation(ulong _id, string _name, double _relationship)
        {
            id = _id;
            name = _name;
            relationship = _relationship;
        }
    }
}
