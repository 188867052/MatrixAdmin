namespace Core.Console
{

    class Program
    {
        static void Main(string[] args)
        {
            Store store = new Store() { Name = "store111", Book = new Book { BookName = "bookname222" } };
            var a = new TextColumn<Store>(o => o.Name+"ffffffff");
            var value = a.GetValue(store);
        }
    }
}
