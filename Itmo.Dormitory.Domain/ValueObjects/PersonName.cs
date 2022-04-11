using System.Collections.Generic;

namespace Itmo.Dormitory.Domain.ValueObjects
{
    public class PersonName : ValueObject
    {
        public string FirstName { get; private init; } = null!;
        public string LastName { get; private init; } = null!;
        public string? MiddleName { get; private init; }

        private PersonName() { }

        public PersonName(string firstName, string lastName, string? middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public string FullName => string.IsNullOrEmpty(MiddleName)
            ? $"{LastName} {FirstName}"
            : $"{LastName} {FirstName} {MiddleName}";

        public string LastNameAndInitials => string.IsNullOrEmpty(MiddleName)
            ? $"{LastName} {Initial(FirstName)}"
            : $"{LastName} {Initial(FirstName)} {Initial(MiddleName)}";

        private static string Initial(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return source;
            }

            if (source.Length == 1)
            {
                return source;
            }

            return string.Concat(source[0].ToString(), ".");
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return MiddleName;
        }
    }
}
