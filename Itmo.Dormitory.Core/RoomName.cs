using System;
using System.Linq;
using System.Reflection;

namespace Itmo.Dormitory.Web.Models
{
    public static class RoomName
    {
        public const string Billiard = "billiard";

        public const string Tennis = "tennis";

        public const string Study = "study";
        
        public static string[] All { get; } = GetAll(typeof(RoomName));
        
        public static string[] GetAll(Type type) => type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Select(p => p.GetValue(null))
            .Where(f => f is string)
            .Cast<string>()
            .ToArray();

    }
}