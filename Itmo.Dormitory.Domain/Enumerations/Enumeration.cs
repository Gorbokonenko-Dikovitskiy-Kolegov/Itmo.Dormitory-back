using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Itmo.Dormitory.Domain.Enumerations
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private init; }

        public int Id { get; private init; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Id);
        }

        public int CompareTo(object? other) => Id.CompareTo((other as Enumeration)?.Id);
    }
}
