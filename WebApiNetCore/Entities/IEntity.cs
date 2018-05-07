﻿namespace WebApiNetCore.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; set; }
        byte[] Timestamp { get; set; }
    }
}