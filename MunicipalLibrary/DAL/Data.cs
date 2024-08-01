namespace MunicipalLibrary.DAL
{
    public class Data
    {

        static Data GetData;

        string ConnectionString = "server = ELIEZER\\SQLEXPRESS; initial catalog = Libraries; user id = sa; password = 1234; TrustServerCertificate = Yes";

        private Data()
        {
            Layer = new DataLayer(ConnectionString);
        }

        public static DataLayer Get
        {
            get
            {
                if (GetData == null)
                {
                    GetData = new Data();
                }
                return GetData.Layer;
            }
        }
        public DataLayer Layer { get; set; }
    }
}
