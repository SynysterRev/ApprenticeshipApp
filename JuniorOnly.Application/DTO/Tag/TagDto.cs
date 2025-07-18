using System;
using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Application.DTO.Tag
{
    public class TagDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public static Domain.Entities.Tag ToEntity(string name)
        {
            return new Domain.Entities.Tag
            {
                Name = name
            };
        }
    }
}
