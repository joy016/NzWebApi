﻿namespace NzApi.Models.Domain
{
    public class Walks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid RegionId { get; set; }
        public Guid DiffultyId { get; set; }


        //Navigation properties

        public Difficulty Difficulty  { get; set; }
        public Regions Region { get; set; }
    }
}
