using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Core.Api.ExpressionBuilder.Resources;

namespace Core.Api.ExpressionBuilder.Interfaces
{
    /// <summary>
    /// Collection of <see cref="Property" />.
    /// </summary>
    public interface IPropertyCollection : ICollection
    {
        /// <summary>
        /// Type from which the properties are loaded.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// ResourceManager which the properties descriptions should be gotten from.
        /// </summary>
        ResourceManager ResourceManager { get; }

        /// <summary>
        /// Retrieves a property based on its Id.
        /// </summary>
        /// <param name="propertyId">Property conventionalized <see cref="Property.Id" />.</param>
        /// <returns></returns>
        Property this[string propertyId] { get; }

        /// <summary>
        /// Loads the properties names from the specified ResourceManager.
        /// </summary>
        /// <param name="resourceManager">resourceManager.</param>
        /// <returns>The LoadProperties.</returns>
        List<Property> LoadProperties(ResourceManager resourceManager);

        /// <summary>
        /// Converts the collection into a list.
        /// </summary>
        /// <returns>A list.</returns>
        IList<Property> ToList();
    }
}