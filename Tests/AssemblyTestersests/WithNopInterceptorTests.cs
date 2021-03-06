﻿using System;
using System.IO;
using NUnit.Framework;

[TestFixture]
public class WithNopInterceptorTests
{
    AssemblyWeaver assemblyWeaver;

    public WithNopInterceptorTests()
    {
        var assemblyPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\AssemblyWithNopInterceptor\bin\Debug\AssemblyWithNopInterceptor.dll");
        assemblyWeaver = new AssemblyWeaver(assemblyPath);
    }

    [Test]
    public void AssertAttributeIsRemoved()
    {
        var type = assemblyWeaver.Assembly.GetType("TimeAttribute");
        Assert.IsNull(type);
    }

    [Test]
    public void ClassWithMethod()
    {
        var type = assemblyWeaver.Assembly.GetType("ClassWithMethod");
        var instance = (dynamic) Activator.CreateInstance(type);
        instance.Method();
    }

    [Test]
    public void PeVerify()
    {
        Verifier.Verify(assemblyWeaver.Assembly.CodeBase.Remove(0, 8));
    }

}