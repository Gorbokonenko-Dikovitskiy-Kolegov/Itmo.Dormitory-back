using System;
using System.Collections.Generic;

namespace Itmo.Dormitory_backend.Domain.ValueObjects
{
    public class Information : ValueObject
    {
        public string? Title { get; private init; }
        public string Description { get; private init; } = null!;

        private Information() { }
        public Information(string? title, string description)
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
