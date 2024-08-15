﻿namespace TestFrameWork.Abstractions
{
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
    public sealed class TestGroupAttribute : Attribute
    {
        public string? Title { get; set; }
    }
}
