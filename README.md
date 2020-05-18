# Furlong
Library containing several implementations of the Chain of Responsibility pattern supporting sync, async-await, request, request-response and local delegates.

![build](https://github.com/dungeym/Furlong/workflows/build/badge.svg)</br>
![codecov](https://codecov.io/gh/dungeym/Furlong/branch/master/graph/badge.svg)](https://codecov.io/gh/dungeym/Furlong)</br>
![codacy](https://app.codacy.com/project/badge/Grade/9cf7e0c25cc441a3a386f9adb0d46403)](https://www.codacy.com/manual/dungeym/Furlong?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=dungeym/Furlong&amp;utm_campaign=Badge_Grade)</br>
![nuget](https://github.com/dungeym/Furlong/workflows/Publish%20to%20Nuget/badge.svg)

## Introduction
This library contains several implementations of the *Chain of Responsibility*, one of the Behavioural Patterns from the [Gang of Four](http://wiki.c2.com/?GangOfFour).  Each of the implementations has the following characteristics.

-  A **chain** represents two or more **links**.
-  A **chain** can only be accessed by the first **link**.
-  Each **link** determines if it should pass the **request** to the next **link** in the **chain**.

### Installation
Install the library from <a target="_blank" href="https://www.nuget.org/packages/Furlong/" title="Furlong on NuGet">NuGet</a> using:
``` csharp
dotnet add package Furlong
```

### Basic Examples
Simple chain with no response.
```csharp
// using Furlong.Synchronous;

var link = ChainFactory<MyRequest>
	  .Initialize()
	  .StartWith(new MyLink1())
	  .FollowedBy(new MyLink2())
	  .FollowedBy(new MyLink3())
	  .Build();

var request = new MyRequest();

link.Handle(request);
```

Asynchronous chain returning a response.
```csharp
// using Furlong.Asynchronous;

var link = ChainFactory<MyRequest>
	  .Initialize()
	  .StartWith(new MyLink1())
	  .FollowedBy(new MyLink2())
	  .FollowedBy(new MyLink3())
	  .Build();

var request = new MyRequest();

var response = await link.HandleAsync(request);
```

Further examples can be found in the Unit Tests project, these include:
1. [Simple Dependency Injection](https://github.com/dungeym/Furlong/blob/master/src/Furlong.UnitTests/DependencyInjection/FurlongInterface/FurlongInterface_Tests.cs)
1. [Dependency Injection using a Context Resolver](https://github.com/dungeym/Furlong/blob/master/src/Furlong.UnitTests/DependencyInjection/ContextResolver/ContextDriven_Tests.cs)
1. [Using a custom interface as the request definition](https://github.com/dungeym/Furlong/blob/master/src/Furlong.UnitTests/DependencyInjection/CustomInterface/CustomInterface_Tests.cs)
1. [Inline links (delegates)](https://github.com/dungeym/Furlong/blob/master/src/Furlong.UnitTests/Synchronous/LocalChainFactoryRequestResponse/LocalChainFactory_RequestResponse_Tests.cs)
