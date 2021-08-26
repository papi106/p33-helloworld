// <copyright file="TeamInfo.cs" company="Principal 33">
// Copyright (c) Principal 33. All rights reserved.
// </copyright>

using System;
using System.Diagnostics;
using HelloWorldWebApp.Services;

namespace HelloWorldWebApp.Models
{
    [DebuggerDisplay("{Name}, {Id}")]
    public class TeamMember
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public int GetAge()
        {
            var age = DateTime.Now.Subtract(Birthday).Days;
            return age / 365;
        }

        //public override string ToString()
        //{
        //    return $"Id:{Id}, Name:{Name}, Birthday:{Birthday}";
        //}
    }
}