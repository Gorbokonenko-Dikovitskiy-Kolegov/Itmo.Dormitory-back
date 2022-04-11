using System;
using System.Collections.Generic;

namespace Itmo.Dormitory.Domain.ValueObjects
{
    public class AttachedInformation : ValueObject
    {
        public string? Title { get; private init; }
        public string Description { get; private init; } = null!;

        private AttachedInformation() { }
        public AttachedInformation(string? title, string description)
        {
            Title = title;
            Description = description;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Title;
            yield return Description;
        }

    }
}
