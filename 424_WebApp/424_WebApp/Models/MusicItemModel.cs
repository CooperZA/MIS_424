using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace _424_WebApp.Models
{
    public class MusicItemModel
    {
        // asin id
        [Key]
        public string ASIN { get; set; }

        // Title
        public string Title { get; set; }

        // Artist
        public string Artists { get; set; }

        // Release date
        public string ReleaseDate { get; set; }

        // Release date DT
        public DateTime ReleaseDateTime { get; set; }

        // List Price
        public double ListPrice { get; set; }

        // Label
        public string Label { get; set; }

        // Editorial review
        public string EditorialReviews { get; set; }

        // number of discs
        public string NumDiscs { get; set; }

        // Number of tracks
        public string NumTracks { get; set; }

        // Detail page url
        public string DetailUrl { get; set; }

        // Availability
        public string Availability { get; set; }
    }
}