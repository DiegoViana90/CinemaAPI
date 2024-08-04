using System;
using System.Collections.Generic;

namespace CinemaApi.DTOs.Response
{
    public class MovieResponse
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public TimeSpan Duration { get; set; }
        public List<string> RoomNumbers { get; set; }
        public List<string> Descriptions { get; set; }
    }
}
