﻿using System;

namespace ImportCsvData.RavenEntities
{
    public class Brewery
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Code { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }
        public LocationCode Location { get; set; }

        public class LocationCode
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public string Accuracy { get; set; }
        }
    }
}