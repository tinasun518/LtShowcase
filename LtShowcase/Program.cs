using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Script.Serialization;

namespace LtShowcase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var photos = new List<Photo>();
                using (var client = new WebClient())
                {
                    var json = client.DownloadString("https://jsonplaceholder.typicode.com/photos");
                    photos = new JavaScriptSerializer().Deserialize<List<Photo>>(json);
                }
                var input = string.Empty;
                do
                {
                    input = Console.ReadLine();
                    var command = input?.Split();
                    if (string.Equals(input.ToLower(), "quit"))
                    {
                        break;
                    }
                    if (command.Length != 2)
                    {
                        Console.WriteLine("Command not valid.");
                        continue;
                    }
                    if (string.Equals(command[0].ToLower(), "photo-album"))
                    {
                        var selectedPhotos = photos.FindAll(photo => photo.albumId == command[1]);
                        foreach (var photo in selectedPhotos)
                        {
                            Console.WriteLine($"[{photo.id}] {photo.title.ToString()}");
                        }
                    }
                }
                while (!string.Equals(input.ToLower(), "quit"));
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }
    }
}
