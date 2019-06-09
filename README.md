# Asp.Net
.Net Core 2.1,MVC, WebApi,EntityFrameWork Core,bootstrap4,jquery,Restful,前后端分离，javascript原型链，Dapper，自定义UI框架,SwaggerUI,Linq表达式树，T4模板,NUnit单元测试
后端：WebApi+EfCore+Dapper兼具开发速度和性能，批处理可以使用dapper 
前端：基于bootstrap4封装的一套组件，GridSearchFilter，Dialog，RowContextMenu，
一些表单提交，做BS开发不用重复造轮子，js采用严格模式，兼容性好，代码量少，不用写开发人员不用写html，统一了UI，开发人员只需要写业务

## Code Analysis
* [Microsoft.AspNetCore.Mvc.Analyzers]
* [Microsoft.CodeAnalysis.CSharp.Analyzers]
* [Microsoft.CodeQuality.CSharp.Analyzers]
* [Microsoft.VisualStudio.Threading.Analyzers]
* [StyleCop.Analyzers]

## Technologies

* [.NET Core 2.1](https://dotnet.microsoft.com/download)
* [ASP.NET Core 2.1](https://docs.microsoft.com/en-us/aspnet/core)
* [Entity Framework Core 3.0](https://docs.microsoft.com/en-us/ef/core)
* [C# 7.3](https://docs.microsoft.com/en-us/dotnet/csharp)
* [HTML](https://www.w3schools.com/html)
* [CSS](https://www.w3schools.com/css)
* [JWT](https://jwt.io)
* [Swagger](https://swagger.io)
* [Bootstrap v4.3.1](https://www.bootcss.com)
* [Dapper](https://github.com/StackExchange/Dapper)
* [NUnit](https://github.com/nunit/nunit)
* [JQuery](http://jquery.com/)

## Practices

* Clean Code
* Code Analysis
* Inversion of Control
* Logging
* T4模板引擎(Text Template Transformation Toolkit)
* TDD(Test-Driven Development)

## Unit Test

* [Dapper Extension](https://github.com/188867052/Asp.Net/blob/master/UnitTest/Dapper/UnitTest.cs)
* [Queryable Extension](https://github.com/188867052/Asp.Net/blob/master/UnitTest/Filter/UnitTest.cs)
* [Web Api](https://github.com/188867052/Asp.Net/blob/master/UnitTest/Api/UnitTest.cs)
* [Code Generator](https://github.com/188867052/Asp.Net/blob/master/UnitTest/CodeGenerator/UnitTest.cs)

## Examples of Front End

* [Grid Search Filter](https://github.com/188867052/Asp.Net/blob/37fc5e0a587a0dc6feb816d3b462864b0153cc43/Core.Models/Administration/User/UserPostModel.cs)
* [Row Context Menu](https://github.com/188867052/Asp.Net/blob/01cdb009e38bc051669fb74e65f6700d9a0571c5/Core.Mvc/Areas/Administration/ViewConfiguration/Role/RoleRowContextMenu.cs)
* [Dialog Configuration](https://github.com/188867052/Asp.Net/blob/df337c0918b7b7f8c0a6e7e9d853cc2f7f0ebb8b/Core.Mvc/Areas/Administration/ViewConfiguration/User/EditUserDialogConfiguration.cs)
* [Grid Configuration](https://github.com/188867052/Asp.Net/blob/df337c0918b7b7f8c0a6e7e9d853cc2f7f0ebb8b/Core.Mvc/Areas/Administration/ViewConfiguration/Role/RoleViewConfiguration.cs)
* [Search Filter Configuration](https://github.com/188867052/Asp.Net/blob/df337c0918b7b7f8c0a6e7e9d853cc2f7f0ebb8b/Core.Mvc/Areas/Administration/SearchFilterConfigurations/UserSearchFilterConfiguration.cs)
* [Index Javascript](https://github.com/188867052/Asp.Net/blob/master/Core.Mvc/wwwroot/js/User/index.js)
* [Search Grid Page](https://github.com/188867052/Asp.Net/blob/d7d949f8974b3dc4b8d37ae67123c4dc581c6ed3/Core.Mvc/Areas/Administration/ViewConfiguration/User/UserIndex.cs)

## Code Generators

* Javascript&CSS
* [Generators](https://github.com/188867052/Asp.Net/blob/master/Core.Mvc/Resource.Generated.tt)
* [Generated File](https://github.com/188867052/Asp.Net/blob/master/Core.Mvc/Resource.Generated.cs)
* Resources
* [Generators](https://github.com/188867052/Asp.Net/blob/master/Core.Resource/Resource.Generated.tt)
* [Generated File](https://github.com/188867052/Asp.Net/blob/master/Core.Resource/Resource.Generated.cs)
* EntityFrameworkCore.Generator
* [Generators](https://github.com/188867052/Asp.Net/tree/master/EntityFrameworkCore.Generator)
* [Generated File](https://github.com/188867052/Asp.Net/tree/master/Core.Entity/Data)

## Extensions

* [Queryable Extension](https://github.com/188867052/Asp.Net/blob/c601b87ed46d60a5989bc66539d9553d2ca7a4a8/Core.Extension/QueryableExtension.cs)
* [Dapper Extension](https://github.com/188867052/Asp.Net/tree/master/Core.Extension/Dapper)

## Install EntityFrameworkCore Generator

PM>
 dotnet pack .\EntityFrameworkCore.Generator\EntityFrameworkCore.Generator.csproj -o ..s\..\..\.nuget\localpackages -c Release
 dotnet tool uninstall --global EFCore.Generator
 dotnet tool install --global EFCore.Generator
 cd .\core.Entity
 efg generate -c "Data Source=.;App=EntityFrameworkCore;Initial Catalog=Core;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"