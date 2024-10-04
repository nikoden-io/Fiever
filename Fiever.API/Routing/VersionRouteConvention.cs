// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace GoldenBack.API.Routing;

/// <summary>
///     Applies a versioned route convention to controllers based on their API version.
/// </summary>
public class VersionRouteConvention : IApplicationModelConvention
{
    /// <summary>
    ///     Applies the versioned route convention to the specified application model.
    /// </summary>
    /// <param name="application">The application model.</param>
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            var controllerVersion = controller.Attributes
                .OfType<ApiVersionAttribute>()
                .SelectMany(attr => attr.Versions)
                .FirstOrDefault();

            if (controllerVersion != null)
            {
                var versionedRoute = $"v{controllerVersion.ToString()}/[controller]";
                controller.Selectors[0].AttributeRouteModel =
                    new AttributeRouteModel(new RouteAttribute(versionedRoute));
            }
        }
    }
}