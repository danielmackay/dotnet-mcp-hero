﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeroApi.Domain.Heroes;

namespace HeroApi.Infrastructure.Persistence.Configuration;

public class HeroConfiguration : AuditableConfiguration<Hero>
{
    public override void PostConfigure(EntityTypeBuilder<Hero> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(Hero.NameMaxLength)
            .IsRequired();

        builder.Property(t => t.Alias)
            .HasMaxLength(Hero.AliasMaxLength)
            .IsRequired();

        // This is to highlight that we can also serialise to JSON for simple content instead of adding a new table 
        builder.OwnsMany(t => t.Powers, b =>
        {
            b.ToJson();
            b.Property(t => t.Name)
                .HasMaxLength(Power.NameMaxLength)
                .IsRequired();
        });
    }
}