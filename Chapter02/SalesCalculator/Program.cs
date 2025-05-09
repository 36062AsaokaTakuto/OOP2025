namespace SalesCalculator {
    internal class Program {
        static void Main(string[] args) {
            var sales = new SalesCounter(@"data\sales.csv");//右辺で型が確定するならvarでOK
            var amountsPerStore = sales.GetPerStoreSales();
            foreach (var obj in amountsPerStore){
                Console.WriteLine($"{obj.Key} {obj.Value}");
            }


        }

        

    }
}
