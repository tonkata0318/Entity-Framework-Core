namespace MusicHub
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {

            MusicHubDbContext context = new MusicHubDbContext();

            //DbInitializer.ResetDatabase(context);

            Console.WriteLine(ExportSongsAboveDuration(context, 4));
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb =new StringBuilder();
            var albumInfo=context.Producers
                .Include(x=>x.Albums).ThenInclude(a=>a.Songs).ThenInclude(s=>s.Writer)
                .First(x => x.Id == producerId)
                .Albums.Select(x=> new
                {
                    AlbumName=x.Name,
                    RealeaseDate=x.ReleaseDate,
                    ProducerName=x.Producer.Name,
                    AlbumSongs=x.Songs.Select(x=> new
                    {
                        SongName=x.Name,
                        SongPrice=x.Price,
                        SongWriterName=x.Writer.Name
                    }).OrderByDescending(x=>x.SongName).ThenBy(x=>x.SongWriterName),
                    TotalAlbumPrice=x.Price
                }).OrderByDescending(x=>x.TotalAlbumPrice).AsEnumerable();

            foreach (var album in albumInfo)
            {
                sb.AppendLine($"-AlbumName: {album.AlbumName}");
                sb.AppendLine($"-ReleaseDate: {album.RealeaseDate.ToString("MM/dd/yyyy")}");
                sb.AppendLine($"-ProducerName: {album.ProducerName}");
                sb.AppendLine($"-Songs:");
                if (album.AlbumSongs.Any())
                {
                    int counter = 1;
                    foreach (var song in album.AlbumSongs)
                    {
                        sb.AppendLine($"---#{counter++}");
                        sb.AppendLine($"---SongName: {song.SongName}");
                        sb.AppendLine($"---Price: {song.SongPrice:f2}");
                        sb.AppendLine($"---Writer: {song.SongWriterName}");
                    }
                }
                sb.AppendLine($"-AlbumPrice: {album.TotalAlbumPrice:f2}");
            }
            return sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder sb=new StringBuilder();
            var songInfo = context.Songs.Include(s => s.SongPerformers)
                .ThenInclude(sp => sp.Performer)
                .Include(s => s.Writer)
                .Include(s => s.Album)
                .ThenInclude(a => a.Producer)
                .AsEnumerable()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new 
                { 
                    s.Name,
                    Performers=s.SongPerformers.
                    Select(sp => sp.Performer.FirstName+" "+sp.Performer.LastName).ToList(),
                    WriterName=s.Writer.Name,
                    AlbumProducer=s.Album.Producer.Name,
                    Duration=s.Duration.ToString("c"),
                })
                .OrderBy(s=>s.Name)
                .ThenBy(s=>s.WriterName)
                .ToList();


            var counter = 1;
            foreach (var song in songInfo)
            {
                sb.AppendLine($"-Song #{counter++}");
                sb.AppendLine($"---SongName: {song.Name}");
                sb.AppendLine($"---Writer: {song.WriterName}");
                if (song.Performers.Any())
                {
                    foreach (var performer in song.Performers.OrderBy(p=>p))
                    {
                        sb.AppendLine($"---Performer: {performer}");
                    }
                   
                }
                sb.AppendLine($"---AlbumProducer: {song.AlbumProducer}");
                sb.AppendLine($"---Duration: {song.Duration}");
            }
            return sb.ToString().TrimEnd();
        }
    }
}
