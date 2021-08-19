namespace CSharpFeatures
{
    internal class Coffee
    {
        public Coffee()
        {
        }
        public Coffee(string name)
        {
            CoffeeType = name;
        }

        public string CoffeeType { get; set; }

        public override string ToString()
        {
            return CoffeeType;
        }
    }
}