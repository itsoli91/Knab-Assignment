# Answers to technical questions

## Q1

How long did you spend on the coding assignment? What would you add to your solution if you had more time?

## Q1 Answer

About 10 Hours. I did it in 2, 5-hour sessions because I was busy with my current job + I was struggling with chickenpox!!

I would  add these to my solution if I had time:

* I would add [OpenTelemetry](https://opentelemetry.io/) to collect, and export telemetry data (metrics, logs, and traces) to analyze software performance and behavior.
* More Tests!
  * I would write more Unit Tests for ExchangeApi and IdentityApi.
  * I would also write some Integration Tetsts.
  * Since we are using external repositories I would also use [WireMock.Net](https://github.com/WireMock-Net/WireMock.Net) (WireMock.Net is a tool which mimics the behaviour of an HTTP API, it captures the HTTP requests and sends it to WireMock.Net HTTP server, which is started and as a result, we can setup expectations, call the service and then verify its behaviour.)
* I'm using .Net Core's default memory cache to improve the overall performance of the system and help staying within the limits of the third-party API calls quota, but to enable the system to scale-out I would use a distributed cache. (e.g. Redis) if I could spend more time on the project.
* I would add role-based access control (RBAC) to identity.
* To handle business logic validations and errors, I'm throwing custom exceptions and then handling the exceptions by a Filter (HttpGlobalExceptionFilter) working on top of the whole system, to standardize the way errors are communicated with the clients. For now I'm using a common "GeneralApplicationException" class, derived from "IApplicationServiceException" interface for all business logic errors, but with more time I would not only segregate business logic exceptions types per layer (e.g. using IDomainException, IPresentationException, etc.) but also would define more granular exception classes and also exception codes.
* I would also add a means of persisting the data fetched from the third party APIs (e.g. Database)
* I would improve the UI.

## Q2

What was the most useful feature that was added to the latest version of your language of choice?

## Q2 Answer

### Minimal Apis

Minimal APIs are architected to create HTTP APIs with minimal dependencies. They are ideal for microservices and apps that want to include only the minimum files, features, and dependencies in ASP.NET Core.
[Microsoft themselves](https://github.com/DamianEdwards/MinimalApiPlayground/issues/15) have said that they are aiming for Minimal APIs to be used, instead of controllers, for 80-90% of usecases.

```C#
app.MapGet("/", () => Results.LocalRedirect("~/swagger"));
```

### Global using directives and File-scoped namespace declaration (C# 10.0)

You can add the global modifier to any using directive to instruct the compiler that the directive applies to all source files in the compilation. This is typically all source files in a project.

You can use a new form of the namespace declaration to declare that all declarations that follow are members of the declared namespace:

### Init-Only properties (C# 9.0)

Enable developers to create immutable properties in the classes.
I have used it in the "CryptoPriceList" class where exchange rates for the crypto are calculated one in the constructor and should never change after.

```C#
        public double Usd { get; init; }
        public double Eur { get; init; }
```

## Q3

How would you track down a performance issue in production? Have you ever had to do this?

## Q3 Answer

I've done it in a couple of projects.

Most of the time, performance issues are detected/reported from client-side in two ways:

* Load tests
* In actual production by the users.

When the issue is detected we first need to figure out what endpoint is actually causing the latency. This is mostly done by client-side tools such as browser Inspect tool or network monitoring tools.

When the endpoint (or any other entry point to the slow code) is detected, the main part of the job starts. We can use profiling tools to find the bottlenecks.
The first step can always be a simple analysis of whether the result of the function/procedure causing the bottleneck is changing frequently or not. If not, we can always utilize caching mechanisms as a first step to reduce the need of the low-performing code to run.
Next step can be checking the nature of the high-latency codes.

* CPU-bound
  * We need to optimize the code using better algorithms and data-structures, which can indeed lead to less-readable, but faster codes.
* I/O-bound
  * Can vary from calls to slow third-party services to database queries. Depending on the situation we can use a combination of caching, query optimization or even change of tools and technologies to achieve better performance.

## Q4

What was the latest technical book you have read or tech conference you have been to? What did you learn?

## Q4 Answer

I have read “.NET Microservices: Architecture for Containerized .NET Applications” and I learnt about developing microservices-based applications and managing them using containers. I also learnt architectural design and implementation approaches using .NET and Docker containers.

I’m also planning to read the “Accelerate: The Science of Lean Software and DevOps: Building and Scaling High Performing Technology Organizations”

## Q5

What do you think about this technical assessment?

## Q5 Answer

It was challenging. When I started it looked so easy that I started to get scared of the hidden requirements that I may have missed. But later, reading further down the assignment document, I started to realize it's not that easy and there are important stuff that need to be considered while developing the code. The code, combined with the answers to questions, seems to be a good indicator of the developer abilities.

## Q6

Please, describe yourself using JSON.

## Q6 Answer

```Json
{
   "name":{
      "firstName":"MohammadHasan",
      "lastName":"Olfatmiri"
   },
   "contact":{
      "mobile":"+31621353724",
      "email":"m.olfat.miri@gmail.com",
      "stackoverflow":"https://stackoverflow.com/users/1274875/mohammad-olfatmiri",
      "linkedin":"https://www.linkedin.com/in/itsoli/",
      "address":{
         "country":"Netherlands",
         "city":"Amstelveen",
         "postalCode":"1185 SR"
      }
   },
   "occupation":"Software Engineer",
   "interests":[
      "E-Games",
      "Movies",
      "Travel"
   ],
   "skills":[
      {
         "title":"C#",
         "level":5
      },
      {
         "title":".Net Core",
         "level":5
      },
      {
         "title":"Docker",
         "level":5
      }
   ]
}
```
