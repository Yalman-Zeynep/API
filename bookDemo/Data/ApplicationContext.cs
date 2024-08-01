using bookDemo.Models; //book için 

namespace bookDemo.Data
{
    public static class ApplicationContext
    {
        public static List<Book> Books { get; set; } //books adinda kitap listesi olusturuldu
        static ApplicationContext()
        {
            Books = new List<Book>()
            {
                new Book() { Id=1, Title="Karagöz ve hacivat", Price=75 },
                new Book() { Id=2, Title="Mesnevi", Price =150 },
                new Book() { Id=3, Title="Dede Korkut" , Price =75 }
            };  
            
        }
    }
}
