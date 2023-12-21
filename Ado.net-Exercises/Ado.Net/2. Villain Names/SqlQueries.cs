using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2._Villain_Names
{
    public static class SqlQueries
    {
        public const string GetVilliansWithNumberOfMinions = @"SELECT [Name],COUNT(*) AS [TotalMinions]
                                                        FROM Villains AS v
                                                        JOIN MinionsVillains AS mv ON mv.VillainId=v.Id
                                                        GROUP BY [Name]
                                                        HAVING COUNT(*)>3
                                                        ORDER BY TotalMinions";

        public const string GetVilianById = @"SELECT Name FROM Villains WHERE Id = @Id";

        public const string GetOrderMinionsByVillianId = "SELECT ROW_NUMBER() OVER (ORDER BY m.Name) AS RowNum,\r\n                                         m.Name, \r\n                                         m.Age\r\n                                    FROM MinionsVillains AS mv\r\n                                    JOIN Minions As m ON mv.MinionId = m.Id\r\n      "         +                    "WHERE mv.VillainId = @Id\r\n                                ORDER BY m.Name";
        public const string GetTownByName = @"SELECT Id FROM Towns WHERE Name = @townName";
        public const string InsertNewTown = @"INSERT INTO Towns ([Name]) OUTPUT inserted.Id VALUES (@townName)";
        public const string GetVilianByName = @"SELECT Id FROM Villians WHERE Name = @vilianName";
        public const string InsertNewVilian = @"INSERT INTO Vilians([Name],EvilnessFactorId) OUTPUT inserted.Id VALUES (@vilianName,@evilnessFactorID)";
        public const string InsertNewMinion = @"INSERT INTO Minions([Name],Age,TownId) OUTPUT inserted.Id VALUES(@minionName,@minionAge,@townId)";
        public const string InsertIntoMinionsVilians = @"INSERT INTO MinionsVillians(MinionId,VillainId) VALUES (@minionId,@villianId)";
    }
}
