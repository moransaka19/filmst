using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Infrastructure;
using Newtonsoft.Json;
using DAL.Models;

namespace BLL_Frame_
{
    class TrackHelper
    {
        private string connectionString;

        public TrackHelper(string connection)
        {
            connectionString = connection;
        }

        [HttpGet]
        public string GetTrackTable()
        {
            IEnumerable<Track> tracks;
            using (var uof = new UnitOfWork(connectionString))
            {
                tracks = uof.Tracks.GetAll();
            }
            string tracksTable = JsonConvert.SerializeObject(tracks);

            return tracksTable;
        }
    }
}
