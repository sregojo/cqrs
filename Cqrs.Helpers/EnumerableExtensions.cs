﻿namespace Cqrs.Helpers
{
    public static class EnumerableExtensions
    {
        public static T[] ToEnumerable<T>(this T @object)
        {
            return new[] {@object};
        }
    }
}