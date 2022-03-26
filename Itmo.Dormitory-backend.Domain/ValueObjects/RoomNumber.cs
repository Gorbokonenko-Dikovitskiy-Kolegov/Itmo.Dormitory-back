using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Itmo.Dormitory_backend.Domain.ValueObjects
{
    public class RoomNumber : ValueObject
    {
        public string Value { get; private init; } = null!;

        private RoomNumber() { }
        public RoomNumber(string value)
        {
            if (!Regex.IsMatch(value, @"^\d+$"))
                throw new ArgumentException("Wrong Room number format");
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
