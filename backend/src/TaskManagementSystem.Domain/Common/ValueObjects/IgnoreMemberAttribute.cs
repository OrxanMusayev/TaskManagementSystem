using System;

namespace TaskManagementSystem.Domain.Common.ValueObjects
{
    // I got this from this source: https://github.com/jhewlett/ValueObject
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}
