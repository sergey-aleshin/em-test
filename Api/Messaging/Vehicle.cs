﻿namespace Api.Messaging
{
    public class Vehicle(Guid id, string name)
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
    }
}
