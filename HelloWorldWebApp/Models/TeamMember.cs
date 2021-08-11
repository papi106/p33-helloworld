// <copyright file="TeamInfo.cs" company="Principal 33">
// Copyright (c) Principal 33. All rights reserved.
// </copyright>

using System;

namespace HelloWorldWebApp.Models
{
    public class TeamMember
    {
        private static int idGenerator = 0;

        public TeamMember(string name)
        {
            Id = idGenerator;
            Name = name;
            idGenerator++;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public int GetAge()
        {
            var age = DateTime.Now.Subtract(Birthday).Days;
            return age / 365;
        }
    }
}