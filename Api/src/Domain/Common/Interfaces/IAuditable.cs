﻿namespace HeroApi.Domain.Common.Interfaces;

public interface IAuditable
{
    DateTimeOffset CreatedAt { get; }
    string CreatedBy { get; }
    DateTimeOffset? UpdatedAt { get; }
    string? UpdatedBy { get; }

    void SetCreated(TimeProvider timeProvider, string? createdBy = null);

    void SetUpdated(TimeProvider timeProvider, string? updatedBy = null);
}