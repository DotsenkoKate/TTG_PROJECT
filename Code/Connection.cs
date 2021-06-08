using System;
using Npgsql;

namespace Connection
{
    public interface IConnection<TConnectionClass>
    {
        TConnectionClass Db { get; set; }
        void Open();
        void Close();
    }
    public class ConnectionPSQL : IConnection<NpgsqlConnection>
    {
        public NpgsqlConnection Db { get; set; }

        public ConnectionPSQL(String connectionString)
        {
            Db = new NpgsqlConnection(connectionString);
        }
        public void Close()
        {
            try
            {
                //Открываем соединение.
                Db.Close();
                Console.WriteLine("ttg_static_1 closed");
            }
            catch (Exception ex)
            {
                //Код обработки ошибок
                Console.WriteLine("Static BD is not closed!");
            }
        }

        public void Open()
        {
            try
            {
                //Открываем соединение.
                Db.Open();
                Console.WriteLine("ttg_static_1 opened");
            }
            catch (Exception ex)
            {
                //Код обработки ошибок
                Console.WriteLine("Static BD is not opened!");
            }
        }
    }
}
