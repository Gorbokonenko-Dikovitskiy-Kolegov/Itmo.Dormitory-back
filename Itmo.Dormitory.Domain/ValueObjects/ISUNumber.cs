using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Itmo.Dormitory.Domain.ValueObjects
{
    public class ISUNumber : ValueObject
    {
        public string Value { get; private init; } = null!;

        private ISUNumber() { }
        public ISUNumber(string value)
        {
            if (!Regex.IsMatch(value, @"^\d+$"))
                throw new ArgumentException("Wrong ISU number format");
            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
