namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using System.ComponentModel.DataAnnotations;
    using Helpers;
    using Footballers.DataProcessor.ImportDto;
    using System.Text;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using System.Globalization;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            var CoachesDTO = XmlSerializationHelper.Deserialize<ImportCoachesDTO[]>(xmlString, "Coaches");
            StringBuilder sb=new StringBuilder();
            List<Coach> coaches= new List<Coach>();

            foreach (var coachDTO in CoachesDTO)
            {
                if (!IsValid(coachDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                var coach = new Coach()
                { 
                    Name= coachDTO.Name,
                    Nationality= coachDTO.Nationality,
                };
                foreach (var footbaler in coachDTO.Footballers)
                {
                    if (!IsValid(footbaler))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    string format = "dd/MM/yyyy";
                    DateTime startedContract= DateTime.ParseExact(footbaler.ContractStartDate, format, CultureInfo.InvariantCulture);
                    DateTime endedContract=DateTime.ParseExact(footbaler.ContractEndDate,format,CultureInfo.InvariantCulture);
                    if (startedContract>endedContract)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    coach.Footballers.Add(new Footballer()
                    {
                        Name = footbaler.Name,
                        ContractStartDate = startedContract,
                        ContractEndDate = endedContract,
                        BestSkillType = (BestSkillType)footbaler.BestSkillType,
                        PositionType = (PositionType)footbaler.PositionType
                    });
                }
                coaches.Add(coach);
                sb.AppendLine(string.Format(SuccessfullyImportedCoach,coach.Name,coach.Footballers.Count));
            }

            context.Coaches.AddRange(coaches);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            StringBuilder stringBuilder= new StringBuilder();
            var teamsDTO = JsonConvert.DeserializeObject<ImportTeamDTO[]>(jsonString);
            var existingFootballersIDs = context.Footballers
                .Select(f => f.Id)
                .ToArray();
            List<Team> teams= new List<Team>();

            foreach (ImportTeamDTO teamDTO in teamsDTO)
            {
                if (!IsValid(teamDTO))
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }
                if (teamDTO.Trophies==0)
                {
                    stringBuilder.AppendLine(ErrorMessage);
                    continue;
                }
                Team teamtoAdd = new Team()
                {
                    Name= teamDTO.Name,
                    Nationality= teamDTO.Nationality,
                    Trophies= teamDTO.Trophies,

                };
                foreach (var footbalerid in teamDTO.FootbollersIds.Distinct())
                {
                    if (!existingFootballersIDs.Contains(footbalerid))
                    {
                        stringBuilder.AppendLine(ErrorMessage);
                        continue;
                    }
                    TeamFootballer teamFootballer = new TeamFootballer()
                    {
                        FootballerId = footbalerid,
                        Team = teamtoAdd
                    };
                    teamtoAdd.TeamsFootballers.Add(teamFootballer);
                }
                teams.Add(teamtoAdd);
                stringBuilder.AppendLine(string.Format(SuccessfullyImportedTeam,teamtoAdd.Name,teamtoAdd.TeamsFootballers.Count()));
            }
            context.Teams.AddRange(teams);
            context.SaveChanges();

            return stringBuilder.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
