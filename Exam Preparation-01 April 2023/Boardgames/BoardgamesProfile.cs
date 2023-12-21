namespace Boardgames
{
    using AutoMapper;
    using Boardgames.Data.Models;
    using Boardgames.DataProcessor.ImportDto;

    public class BoardgamesProfile : Profile
    {
        // DO NOT CHANGE OR RENAME THIS CLASS!
        public BoardgamesProfile()
        {
            CreateMap<ImportCreatorDTO, Creator>();
            CreateMap<ImportBoardgameDTO, Boardgame>();
        }
    }
}