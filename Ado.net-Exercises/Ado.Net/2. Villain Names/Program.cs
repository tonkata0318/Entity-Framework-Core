using System.Data.Common;
using System.Data.SqlClient;

namespace _2._Villain_Names
{
    internal class Program
    {
        //Server name=DESKTOP-D6UISOH\SQLEXPRESS
        //Database=MinionsDB
        //3.
        //const string connectionString = @"Server=DESKTOP-D6UISOH\SQLEXPRESS;Database=MinionsDB;Integrated Security=True";
        //static SqlConnection connection;
        //static async Task Main(string[] args)
        //{
        //    connection = new SqlConnection(connectionString);
        //    connection.Open();
        //    await GetOrderMinionsByVillianId(1);

        //}
        //static async Task GetOrderMinionsByVillianId(int id)
        //{
        //    using SqlCommand command = new SqlCommand(SqlQueries.GetVilianById, connection);
        //    command.Parameters.AddWithValue("@Id", id);
        //    var result= await command.ExecuteScalarAsync();
        //    if (result is null)
        //    {
        //        await Console.Out.WriteLineAsync($"No villain with ID {id} exists in the database.");
        //    }
        //    else
        //    {
        //        await Console.Out.WriteLineAsync($"Vilian: {result}");

        //        using SqlCommand commandGetMinionsData = new SqlCommand(SqlQueries.GetOrderMinionsByVillianId, connection);
        //        commandGetMinionsData.Parameters.AddWithValue("@Id", id);
        //        var minionsReader = await commandGetMinionsData.ExecuteReaderAsync();
        //        while (await minionsReader.ReadAsync())
        //        {
        //            await Console.Out.WriteLineAsync($"{minionsReader["RowNum"]}. " +
        //                $"{minionsReader["Name"]} {minionsReader["Age"]}");
        //        }
        //    }

        //4.
        const string connectionString = @"Server=DESKTOP-D6UISOH\SQLEXPRESS;Database=MinionsDB;Integrated Security=True";
        static SqlConnection connection;
        static async Task Main(string[] args)
        {
            try
            {
                connection= new SqlConnection(connectionString);
                connection.Open();
                string minionInfoRaw = Console.ReadLine();
                string villainInfoRaw=Console.ReadLine();

                string minionInfo = minionInfoRaw.Substring(minionInfoRaw.IndexOf(":")+1).Trim();
                string villainName = villainInfoRaw.Substring(villainInfoRaw.IndexOf(":") + 1).Trim();
                await AddMinion(minionInfo, villainName);
            }
            finally
            {
                connection?.Dispose();
            }
           
        }   
        static async Task AddMinion(string minionInfo,string vilainName)
        {
            string[] minionData = minionInfo.Split(' ');
            string minionName = minionData[0];
            int minionAge = int.Parse(minionData[1]);
            string minionTown = minionData[2];


            #region Town
            SqlCommand cmdGetTownId = new SqlCommand(SqlQueries.GetTownByName, connection);
            cmdGetTownId.Parameters.AddWithValue("@townName", minionTown);

            var townResult=await cmdGetTownId.ExecuteScalarAsync();

            int townId = -1;
            if (townResult is null)
            {
                SqlCommand createTown = new SqlCommand(SqlQueries.InsertNewTown,connection);
                cmdGetTownId.Parameters.AddWithValue("@townName", minionTown);
                townId =Convert.ToInt32(await createTown.ExecuteScalarAsync());
                Console.Out.WriteLineAsync($"Town ${minionTown} was added to the database.");
            }
            else
            {
                townId=(int)townResult;
            }
            #endregion

            #region Vilian

            SqlCommand cmdGetVilian = new SqlCommand(SqlQueries.GetVilianByName, connection);
            cmdGetVilian.Parameters.AddWithValue("@vilianName", vilainName);
            var vilianResult= await cmdGetVilian.ExecuteScalarAsync();
            int vilianid = -1;
            if (vilianResult is null)
            {
                SqlCommand cmdInsertNewVilian = new SqlCommand(SqlQueries.InsertNewVilian, connection);
                cmdInsertNewVilian.Parameters.AddWithValue("@vilianName", vilainName);
                cmdInsertNewVilian.Parameters.AddWithValue("@evilnessFactorID", 4);
                vilianid = Convert.ToInt32(await cmdInsertNewVilian.ExecuteScalarAsync());
                await Console.Out.WriteLineAsync($"Villain {vilainName} was added to the database.");
            }
            else
            {
                vilianid=(int)vilianResult;
            }
            #endregion

            #region Minion

            using SqlCommand cmdInsertMinion=new SqlCommand(SqlQueries.InsertNewMinion, connection);
            cmdInsertMinion.Parameters.AddWithValue("@minionName", minionName);
            cmdInsertMinion.Parameters.AddWithValue("@minionAge", minionAge);
            cmdInsertMinion.Parameters.AddWithValue("@townId", townId);
            await Console.Out.WriteLineAsync($"Minion {minionName} was added to database");
            int minionId = Convert.ToInt32(await cmdInsertMinion.ExecuteScalarAsync());
            using SqlCommand cmdInsertMinionVillain=new SqlCommand(SqlQueries.InsertIntoMinionsVilians, connection);
            await cmdInsertMinion.ExecuteNonQueryAsync();
            await Console.Out.WriteLineAsync($"Successfully added {minionName} was added as servant to {vilainName}");
            #endregion
        }
    }
}