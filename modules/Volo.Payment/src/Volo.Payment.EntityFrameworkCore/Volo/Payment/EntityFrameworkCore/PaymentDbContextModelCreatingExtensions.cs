﻿using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Payment.Plans;
using Volo.Payment.Requests;

namespace Volo.Payment.EntityFrameworkCore
{
    public static class PaymentDbContextModelCreatingExtensions
    {
        public static void ConfigurePayment(
            this ModelBuilder builder,
            Action<PaymentModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new PaymentModelBuilderConfigurationOptions(
                PaymentDbProperties.DefaultDbTablePrefix,
                PaymentDbProperties.DefaultDbSchema
            );

            optionsAction?.Invoke(options);

            if (builder.IsTenantOnlyDatabase())
            {
                return;
            }

            builder.Entity<PaymentRequest>(b =>
            {
                b.ToTable(options.TablePrefix + "PaymentRequests", options.Schema);

                b.ConfigureByConvention();

                b.Property(pr => pr.State).IsRequired();
                b.Property(pr => pr.FailReason);

                b.HasMany(pr => pr.Products).WithOne().HasForeignKey(qt => qt.PaymentRequestId).IsRequired();

                b.HasIndex(pr => pr.CreationTime);

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<PaymentRequestProduct>(b =>
            {
                b.ToTable(options.TablePrefix + "PaymentRequestProducts", options.Schema);

                b.ConfigureByConvention();

                b.HasKey(prp => new { prp.PaymentRequestId, prp.Code });

                b.HasOne<PaymentRequest>().WithMany(u => u.Products).HasForeignKey(ur => ur.PaymentRequestId).IsRequired();

                b.Property(prp => prp.Code).IsRequired();
                b.Property(prp => prp.Name).IsRequired();
                b.Property(prp => prp.UnitPrice).IsRequired();
                b.Property(prp => prp.Count).IsRequired();
                b.Property(prp => prp.TotalPrice);

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<Plan>(b =>
            {
                b.ToTable(options.TablePrefix + "Plans", options.Schema);

                b.ConfigureByConvention();

                b.Property(prp => prp.Name).HasMaxLength(PlanConsts.MaxNameLength).IsRequired();

                b.HasMany(pr => pr.GatewayPlans).WithOne().HasForeignKey(ep => ep.PlanId);
            });

            builder.Entity<GatewayPlan>(b =>
            {
                b.ToTable(options.TablePrefix + "GatewayPlans", options.Schema);

                b.ConfigureByConvention();

                b.HasKey(prp => new { prp.PlanId, prp.Gateway });

                b.Property(prp => prp.Gateway).IsRequired();
                b.Property(prp => prp.ExternalId).IsRequired();
            });
            
            builder.TryConfigureObjectExtensions<PaymentDbContext>();
        }
    }
}
