# Furlong
Furlong provides several implementations of the Chain of Responsibility pattern, supporting sync, async-await, request, request-response, and local delegates (also using async-await, et al).

![build](https://github.com/dungeym/Furlong/workflows/build/badge.svg) 
[![code coverage](https://codecov.io/gh/dungeym/Furlong/branch/master/graph/badge.svg)](https://codecov.io/gh/dungeym/Furlong) 
[![codacy](https://app.codacy.com/project/badge/Grade/9cf7e0c25cc441a3a386f9adb0d46403)](https://www.codacy.com/manual/dungeym/Furlong?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=dungeym/Furlong&amp;utm_campaign=Badge_Grade) 
[![NuGet Badge](https://buildstats.info/nuget/Furlong)](https://www.nuget.org/packages/Furlong/)

## Introduction
This ```netstandard2.0``` library contains several implementations of the *Chain of Responsibility* pattern, one of the Behavioural Patterns from the [Gang of Four](http://wiki.c2.com/?GangOfFour).  

Each implementation has the following characteristics.
-  A **chain** represents two or more **links**.
-  A **chain** can only be accessed by the first **link**.
-  Each **link** determines if the **request** should be passed to the next **link** in the **chain**.

### Installation
Install the library from <a target="_blank" href="https://www.nuget.org/packages/Furlong/" title="Furlong on NuGet">NuGet</a> using:
``` csharp
dotnet add package Furlong
```

### Basic Examples
Simple chain with no response.
```csharp
var chain = ChainFactory<MyRequest>
		.Initialize()
		.StartWith(new MyLink1())
		.FollowedBy(new MyLink2())
		.FollowedBy(new MyLink3())
		.Build();

var request = new MyRequest();

chain.Handle(request);
```

Asynchronous chain returning a response.
```csharp
var chain = AsyncChainFactory<MyRequest>
		.Initialize()
		.StartWith(new MyLink1())
		.FollowedBy(new MyLink2())
		.FollowedBy(new MyLink3())
		.Build();

var request = new MyRequest();

var source = new CancellationTokenSource();

var response = await chain.HandleAsync(request, source.Token);
```

Synchronous chain using local delegates, with no response.
```csharp
var chain = LocalChainFactory<MyRequest>
		.Initialize()
		.StartWith(Handle1)
		.FollowedBy(Handle2)
		.FollowedBy(Handle3)
		.Build();

var request = new MyRequest();

chain.Handle(request);

private void Handle1(MyRequest request, out bool cancel)
{
	cancel = false;
	
	// Do something..
}
```

### Further Examples 
Further examples can be found in the Unit Tests project, these include:
1. [Simple Dependency Injection](https://github.com/dungeym/Furlong/blob/master/src/tests/Furlong.UnitTests/DependencyInjection/FurlongInterface/FurlongInterface_Tests.cs)
1. [Dependency Injection using a Context Resolver](https://github.com/dungeym/Furlong/blob/master/src/tests/Furlong.UnitTests/DependencyInjection/ContextResolver/ContextDriven_Tests.cs)
1. [Using a custom interface as the request definition](https://github.com/dungeym/Furlong/blob/master/src/tests/Furlong.UnitTests/DependencyInjection/CustomInterface/CustomInterface_Tests.cs)

### Feedback
Please add any feedback or issue to the [Issues](https://github.com/dungeym/Furlong/issues) section in this project.

### License
This project is licensed under the [MIT](https://choosealicense.com/licenses/mit/) license.

### Copyright and Trademarks
This library is copyright (©) Mark Dungey 2000-2020.
