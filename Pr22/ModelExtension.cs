namespace Pr22
{
    using System.Data.Entity;

    public partial class BeautySaloonEntities2 : DbContext
    {
        private static BeautySaloonEntities2 context;

        public static BeautySaloonEntities2 GetContext()
        {
            if (context == null) context = new BeautySaloonEntities2(); 
            return context;
        }
    }

    public partial class Client
    {
        public string PhotoPath
        {
            get
            {
                if (Photo == null) return "";else return Photo;
            }
        }
    }
}
