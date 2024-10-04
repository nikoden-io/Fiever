// -----------------------------------------------------------------------
// <copyright>
//     Copyright 2024, Nikoden.IO All rights reserved.
//     Author: Nicolas Denoël
// </copyright>
// -----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Fiever.Domain.Entities;

public class Country
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    [JsonIgnore] public ICollection<Student> Students { get; set; } = new List<Student>();
}