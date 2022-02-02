// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore.Cosmos.Metadata.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Property extension methods for Cosmos metadata.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see>, and
///     <see href="https://aka.ms/efcore-docs-cosmos">Accessing Azure Cosmos DB with EF Core</see> for more information and examples.
/// </remarks>
public static class CosmosNavigationExtensions
{
    /// <summary>
    ///     Returns the property name that the property is mapped to when targeting Cosmos.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>Returns the property name that the property is mapped to when targeting Cosmos.</returns>
    public static string GetJsonPropertyName(this IReadOnlyNavigation property)
        => (string?)property[CosmosAnnotationNames.PropertyName]
            ?? GetDefaultJsonPropertyName(property);

    private static string GetDefaultJsonPropertyName(IReadOnlyNavigation property)
    {
        var entityType = property.TargetEntityType;
        return entityType.GetContainingPropertyName()!;
    }

    /// <summary>
    ///     Sets the property name that the property is mapped to when targeting Cosmos.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <param name="name">The name to set.</param>
    public static void SetJsonPropertyName(this IMutableNavigation property, string? name)
        => property.SetOrRemoveAnnotation(
            CosmosAnnotationNames.PropertyName,
            name);

    /// <summary>
    ///     Gets the <see cref="ConfigurationSource" /> the property name that the property is mapped to when targeting Cosmos.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>
    ///     The <see cref="ConfigurationSource" /> the property name that the property is mapped to when targeting Cosmos.
    /// </returns>
    public static ConfigurationSource? GetJsonPropertyNameConfigurationSource(this IConventionProperty property)
        => property.FindAnnotation(CosmosAnnotationNames.PropertyName)?.GetConfigurationSource();
}
