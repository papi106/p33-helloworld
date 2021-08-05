// <copyright file="TeamInfo.cs" company="Principal 33">
// Copyright (c) Principal 33. All rights reserved.
// </copyright>

namespace HelloWorldWebApp.Models
{
    public class TeamMember
    {
        public TeamMember(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}