using System;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class ShowIfAttribute : Attribute
{
    public string TargetVar { get; }
    public string TargetValue { get; }

    public ShowIfAttribute(string targetVar, string targetValue)
    {
        TargetVar = targetVar;
        TargetValue = targetValue;
    }
}