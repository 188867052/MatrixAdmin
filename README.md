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

## Practices

* Clean Code
* Code Analysis
* Inversion of Control
* Logging

## Example of using grid search filters 
[See Code](https://github.com/188867052/Asp.Net/blob/37fc5e0a587a0dc6feb816d3b462864b0153cc43/Core.Models/Administration/User/UserPostModel.cs)
```C#
public IQueryable<Entity.User> GenerateQuery(IQueryable<Entity.User> query)
{
    query = query.AddFilter(o => o.UserRoleMapping.Any(x => x.RoleId == this.RoleId), this.RoleId);
    query = query.AddFilter(o => o.IsEnable == this.IsEnable, this.IsEnable);
    query = query.AddFilter(o => o.Status == (int?)this.ForbiddenStatus, this.ForbiddenStatus);
    query = query.AddFilter(o => o.DisplayName.Contains(this.DisplayName), this.DisplayName);
    query = query.AddFilter(o => o.LoginName.Contains(this.LoginName), this.LoginName);
    query = query.AddDateTimeBetweenFilter(this.StartCreateTime, this.EndCreateTime, o => o.CreateTime);

    query = query.OrderByDescending(o => o.CreateTime);
    return query;
}

 IQueryable<Role> query = this.DbContext.Role;
 query = query.AddStringContainsFilter(o => o.Name, model.RoleName);
 query = query.AddStringContainsFilter(o => o.Description, model.Description);
 query = query.AddDateTimeBetweenFilter(model.StartCreateTime, model.EndCreateTime, o => o.CreateTime);
 query = query.OrderBy(o => o.IsForbidden).ThenByDescending(o => o.CreateTime);

```

## Example of using row context menu
```C#
public class RoleRowContextMenu : RowContextMenu<RoleModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRowContextMenu"/> class.
    /// </summary>
    /// <param name="model">A model.</param>
    public RoleRowContextMenu(RoleModel model) : base(model)
    {
    }

    protected override void CreateMenu(IList<RowContextMenuLink> links)
    {
        Url editUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.EditDialog));
        Url recoverUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.Recover));
        Url deleteUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.Delete));
        Url forbiddenUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.Forbidden));
        Url normalUrl = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.Normal));
        links.Add(new RowContextMenuLink("编辑", "core.editDialog", editUrl));
        links.Add(this.Model.IsDeleted
            ? new RowContextMenuLink("恢复", "index.recover", recoverUrl)
            : new RowContextMenuLink("删除", "index.delete", deleteUrl));
        links.Add(this.Model.IsForbidden == ForbiddenStatusEnum.Normal
            ? new RowContextMenuLink("禁用", "index.forbidden", forbiddenUrl)
            : new RowContextMenuLink("启用", "index.normal", normalUrl));
    }
}
```

## Example of using dialog configuration
```C#
public class EditUserDialogConfiguration<TPostModel, TModel> : DialogConfiguration<TPostModel, TModel>
    where TPostModel : UserEditPostModel
    where TModel : UserModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EditUserDialogConfiguration{TPostModel, TModel}"/> class.
    /// </summary>
    /// <param name="user">The user.</param>
    public EditUserDialogConfiguration(TModel user) : base(UserIdentifiers.EditUserDialogIdentifier, user)
    {
    }

    public override string Title => Resources.Title;

    protected override void CreateHiddenValues(IList<ITextRender<TPostModel, TModel>> textBoxes)
    {
        textBoxes.Add(new HiddenValue<TPostModel, TModel>(o => o.Id, this.Model.Id));
    }

    protected override void CreateBody(IList<ITextRender<TPostModel, TModel>> textBoxes)
    {
        textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.LoginName, o => o.LoginName, o => o.LoginName));
        textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.DisplayName, o => o.DisplayName, o => o.DisplayName));

        var dropDown = new DropDownTextBox<TPostModel, TModel>(Resources.UserRole, o => o.UserRole, false);
        dropDown.AddOption((int)UserRoleEnum.GeneralUser, Resources.GeneralUser, this.Model.UserRole == UserRoleEnum.GeneralUser);
        dropDown.AddOption((int)UserRoleEnum.Admin, Resources.Admin, this.Model.UserRole == UserRoleEnum.Admin);
        dropDown.AddOption((int)UserRoleEnum.SuperAdministrator, Resources.SuperAdministrator, this.Model.UserRole == UserRoleEnum.SuperAdministrator);
        textBoxes.Add(dropDown);
        textBoxes.Add(new LabeledTextBox<TPostModel, TModel>(Resources.Password, o => o.Password, o => o.Password, TextBoxTypeEnum.Password));
    }

    protected override void CreateButtons(IList<StandardButton> buttons)
    {
        Url saveEditUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.SaveEdit));

        buttons.Add(new StandardButton(Resources.Submit, "index.submit", saveEditUrl));
        buttons.Add(new StandardButton(Resources.Cancel, "core.cancel"));
    }
}
```

## Example of using grid configuration
```C#
public class RoleViewConfiguration<T> : GridConfiguration<T>
     where T : RoleModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleViewConfiguration{T}"/> class.
    /// </summary>
    /// <param name="entity">The. </param>
    public RoleViewConfiguration(ResponseModel entity) : base(entity)
    {
    }

    public override void CreateGridColumn(IList<BaseGridColumn<T>> gridColumns)
    {
        Url url = new Url(nameof(Administration), typeof(RoleController), nameof(RoleController.RowContextMenu));
        gridColumns.Add(new RowContextMenuColumn<T>(o => o.Id, "操作", url));
        gridColumns.Add(new TextGridColumn<T>(o => o.Name, Resources.Name));
        gridColumns.Add(new EnumGridColumn<T>(o => o.IsForbidden, "禁用状态"));
        gridColumns.Add(new TextGridColumn<T>(o => o.Description, "描述"));
        gridColumns.Add(new BooleanGridColumn<T>(o => o.IsSuperAdministrator, Resources.IsSuperAdministrator));
        gridColumns.Add(new DateTimeGridColumn<T>(o => o.CreateTime, Resources.CreatedOn));
        gridColumns.Add(new TextGridColumn<T>(o => o.CreatedByUserName, Resources.CreatedByUserName));
        gridColumns.Add(new DateTimeGridColumn<T>(o => o.UpdateTime, "更新时间"));
    }
}
```

## Example of using search filter configuration
```C#
 public class UserSearchFilterConfiguration<T> : SearchFilterConfiguration
        where T : UserPostModel
{
    protected override void CreateSearchFilter(IList<BaseGridFilter> searchFilter)
    {
        var dropDown = new DropDownGridFilter<T, ForbiddenStatusEnum>(o => (ForbiddenStatusEnum)o.ForbiddenStatus, Resources.ForbiddenStatus, tooltip: Resources.ForbiddenStatus);
        dropDown.AddOption(ForbiddenStatusEnum.Normal, Resources.Normal);
        dropDown.AddOption(ForbiddenStatusEnum.Forbidden, Resources.Forbidden);

        searchFilter.Add(new TextGridFilter<T>(o => o.DisplayName, Resources.DisplayName, Resources.DisplayName));
        searchFilter.Add(new TextGridFilter<T>(o => o.LoginName, Resources.LoginName, Resources.LoginName));
        searchFilter.Add(new DateTimeGridFilter<T>(o => o.StartCreateTime, Resources.StartCreateTime, Resources.StartCreateTime));
        searchFilter.Add(new DateTimeGridFilter<T>(o => o.EndCreateTime, Resources.EndCreateTime, Resources.EndCreateTime));
        searchFilter.Add(dropDown);
        searchFilter.Add(AdvancedDropDown.RoleAdvancedDropDown<T>(o => o.RoleId));
        searchFilter.Add(AdvancedDropDown.UserAdvancedDropDown<T>(o => o.Id));
        searchFilter.Add(AdvancedDropDown.MenuAdvancedDropDown<T>(o => o.Id));
    }

    protected override void CreateButton(IList<StandardButton> buttons)
    {
        Url searchUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.GridStateChange));
        Url addDialogUrl = new Url(nameof(Administration), typeof(UserController), nameof(UserController.AddDialog));

        buttons.Add(new StandardButton(Resources.SearchButtonLabel, "index.search", searchUrl, Resources.SearchButtonLabel));
        buttons.Add(new StandardButton(Resources.AddButtonLabel, "core.dialog", addDialogUrl, Resources.AddButtonLabel));
        buttons.Add(new StandardButton(Resources.ClearButtonLabel, "core.clear", tooltip: Resources.ClearButtonLabel));
    }
}
```

## Example of using index.js
```javascript
(function () {
    'use strict';

    window.Index = function () {
    };

    window.Index.prototype = {

        // Private Fields

        _searchUrl: null,

        // Private Event Delegates  

        _onSuccess: function () {
            this.initialize();
        },

        // Public Properties

        // Public Methods

        initialize: function () {
            $(".page-link").on('click', $.proxy(this.search, this));
        },

        submit: function () {
            var url = event.currentTarget.dataset.url;
            window.core.submit(url, this._dialogInstance, $.proxy(this.search, this));
        },

        search: function () {
            window.core.setSuccessPointer($.proxy(this._onSuccess, this));
            window.core.gridSearch(this._searchUrl);
        },

        delete: function () {
            window.core.update($.proxy(this.search, this));
        },

        recover: function () {
            window.core.update($.proxy(this.search, this));
        },

        forbidden: function () {
            window.core.update($.proxy(this.search, this));
        },

        normal: function () {
            window.core.update($.proxy(this.search, this));
        }
        // Private Methods
    };
})();
```

## example of using search grid page
```C#
public class UserIndex<TModel, TPostModel> : SearchGridPage<TModel>
     where TModel : UserModel
     where TPostModel : UserPostModel
{
    private readonly ResponseModel _response;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserIndex{TModel, TPostModel}"/> class.
    /// </summary>
    /// <param name="response">The response.</param>
    public UserIndex(ResponseModel response)
    {
        this._response = response;
    }

    public override IList<string> Css()
    {
        return new List<string>();
    }

    protected override IList<string> JavaScript()
    {
        return new List<string>
        {
            "/js/User/user.js",
        };
    }

    /// <summary>
    /// Content header.
    /// </summary>
    /// <returns>The string.</returns>
    protected override string ContentHeader()
    {
        ContentHeader contentHeader = new ContentHeader("用户管理");
        contentHeader.AddAnchor(new Anchor(new Url(typeof(RedirectController), nameof(RedirectController.Index)), "首页", "返回首页", "icon-home", "tip-bottom"));
        string html = contentHeader.Render();
        return html;
    }

    /// <summary>
    /// Create view instance constructions.
    /// </summary>
    /// <returns>The string.</returns>
    protected override IList<ViewInstanceConstruction> CreateViewInstanceConstructions()
    {
        return new List<ViewInstanceConstruction>
        {
            new IndexViewInstance(),
            new UserViewInstance()
        };
    }

    protected override SearchFilterConfiguration SearchFilterConfiguration()
    {
        return new UserSearchFilterConfiguration<TPostModel>();
    }

    protected override GridConfiguration<TModel> GridConfiguration()
    {
        return new UserViewConfiguration<TModel>(this._response);
    }
}
```

## Unit test of queryable extension
```C#
public class UnitTest
{
    [Test]
    public void TestStringContainsFilter()
    {
        using (var context = new CoreContext())
        {
            IQueryable<User> query = context.User;
            query = query.AddStringContainsFilter(o => o.LoginName, "a");
            var a = context.User.Where(o => o.LoginName.Contains("a")).Expression.ToString();
            var b = query.Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestStringContainsFilter);
        }
    }

    [Test]
    public void TestAddStringIsNullFilter()
    {
        using (var context = new CoreContext())
        {
            IQueryable<User> query = context.User;
            query = query.AddStringIsNullFilter(o => o.LoginName);
            var a = context.User.Where(o => o.LoginName == null).Expression.ToString();
            var b = query.Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringIsNullFilter);
        }
    }

    [Test]
    public void TestAddStringIsEmptyFilter()
    {
        using (var context = new CoreContext())
        {
            var a = context.User.Where(o => o.LoginName == string.Empty).Expression.ToString();
            var b = context.User.AddStringIsEmptyFilter(o => o.LoginName).Expression.ToString();

            Assert.AreEqual(a.Replace("String.Empty", "\"\""), b, UnitTestResource.TestAddStringIsEmptyFilter);
        }
    }

    [Test]
    public void TestAddIntegerEqualFilter()
    {
        using (var context = new CoreContext())
        {
            IQueryable<User> query = context.User;
            query = query.AddIntegerEqualFilter(1, o => o.Id);
            var a = context.User.Where(o => o.Id == 1).Expression.ToString();
            var b = query.Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddIntegerEqualFilter);
        }
    }

    [Test]
    public void TestAddIntegerInArrayFilter()
    {
        using (var context = new CoreContext())
        {
            var list = context.User.Take(10).Select(o => o.Id).ToArray();
            var a = context.User.Where(o => list.Contains(o.Id)).ToList();
            var b = context.User.AddIntegerInArrayFilter(o => o.Id, list).ToList();

            Assert.AreEqual(a.Count, b.Count, UnitTestResource.TestAddIntegerInArrayFilter);
        }
    }

    [Test]
    public void TestAddStringInArrayFilter()
    {
        using (var context = new CoreContext())
        {
            var list = context.Role.Take(10).Select(o => o.Name).ToArray();
            var a = context.Role.Where(o => list.Contains(o.Name)).ToList();
            var b = context.Role.AddStringInArrayFilter(o => o.Name, list).ToList();

            Assert.AreEqual(a.Count, b.Count, UnitTestResource.TestAddStringInArrayFilter);
        }
    }

    [Test]
    public void TestAddStringEndsWithFilter()
    {
        using (var context = new CoreContext())
        {
            var a = context.User.Where(o => o.LoginName.EndsWith("a")).Expression.ToString();
            var b = context.User.AddStringEndsWithFilter("a", o => o.LoginName).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringEndsWithFilter);
        }
    }

    [Test]
    public void TestAddDateTimeBetweenFilter()
    {
        using (var context = new CoreContext())
        {
            var list = context.User.OrderBy(o => o.CreateTime).Take(10).Select(o => o.CreateTime).ToList();
            var a = context.User.Where(o => o.CreateTime >= list.FirstOrDefault() && o.CreateTime <= list.LastOrDefault()).ToList();
            var b = context.User.AddDateTimeBetweenFilter(list.FirstOrDefault(), list.LastOrDefault(), o => o.CreateTime).ToList();

            Assert.AreEqual(a.Count, b.Count);
        }
    }

    [Test]
    public void TestAddIntegerBetweenFilter()
    {
        using (var context = new CoreContext())
        {
            var list = context.User.OrderBy(o => o.CreateTime).Take(10).Select(o => o.Id).ToList();
            var a = context.User.Where(o => o.Id >= list.FirstOrDefault() && o.Id <= list.LastOrDefault()).ToList();
            var b = context.User.AddIntegerBetweenFilter(list.FirstOrDefault(), list.LastOrDefault(), o => o.Id).ToList();

            Assert.AreEqual(a.Count, b.Count);
        }
    }

    [Test]
    public void TestAddStringStartsWithFilter()
    {
        using (var context = new CoreContext())
        {
            var a = context.User.Where(o => o.LoginName.StartsWith("a")).Expression.ToString();
            var b = context.User.AddStringStartsWithFilter("a", o => o.LoginName).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringStartsWithFilter);
        }
    }

    [Test]
    public void TestAddStringEqualFilter()
    {
        using (var context = new CoreContext())
        {
            var a = context.User.Where(o => o.LoginName.Equals("a")).Expression.ToString();
            var b = context.User.AddStringEqualFilter("a", o => o.LoginName).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringEqualFilter);
        }
    }

    [Test]
    public void TestAddDateTimeGreaterThanOrEqualFilters()
    {
        using (var context = new CoreContext())
        {
            var a = context.User.Where(o => o.CreateTime >= DateTime.Today).Expression.ToString().Replace("DateTime.Today", DateTime.Today.ToString());
            var b = context.User.AddDateTimeGreaterThanOrEqualFilter(DateTime.Today, o => o.CreateTime).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddDateTimeGreaterThanOrEqualFilters);
        }
    }

    [Test]
    public void TestAddDateTimeLessThanOrEqualFilter()
    {
        using (var context = new CoreContext())
        {
            var a = context.User.Where(o => o.CreateTime <= DateTime.Today).Expression.ToString().Replace("DateTime.Today", DateTime.Today.ToString());
            var b = context.User.AddDateTimeLessThanOrEqualFilter(DateTime.Today, o => o.CreateTime).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddDateTimeLessThanOrEqualFilter);
        }
    }

    [Test]
    public void TestAddStringNotNullFilter()
    {
        using (var context = new CoreContext())
        {
            var a = context.User.Where(o => o.LoginName != null).Expression.ToString();
            var b = context.User.AddStringNotNullFilter(o => o.LoginName).Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddStringEqualFilter);
        }
    }

    [Test]
    public void TestAddBooleanFilter()
    {
        using (var context = new CoreContext())
        {
            IQueryable<User> query = context.User;
            query = query.AddBooleanFilter(o => o.IsEnable, false);
            var a = context.User.Where(o => o.IsEnable == false).Expression.ToString();
            var b = query.Expression.ToString();

            Assert.AreEqual(a, b, UnitTestResource.TestAddBooleanFilter);
        }
    }
}
```

## QueryableExtension
```C#
public static IQueryable<T> AddStringContainsFilter<T>(this IQueryable<T> query, Expression<Func<T, string>> expression, string value)
{
    return query.AddStringFilter(value, expression, stringContainsMethod);
}

public static IQueryable<T> AddStringEqualFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression)
{
    return query.AddStringFilter(value, expression, stringEqualsMethod);
}

public static IQueryable<T> AddStringEndsWithFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression)
{
    return query.AddStringFilter(value, expression, stringEndsWithMethod);
}

public static IQueryable<T> AddStringStartsWithFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression)
{
    return query.AddStringFilter(value, expression, stringStartsWithMethod);
}

public static IQueryable<T> AddStringIsNullFilter<T>(this IQueryable<T> query, Expression<Func<T, string>> expression)
{
    return query.AddIsNullFilter(expression.GetPropertyName());
}

public static IQueryable<T> AddStringIsEmptyFilter<T>(this IQueryable<T> query, Expression<Func<T, string>> expression)
{
    return query.AddIsEmptyFilter(expression.GetPropertyName());
}

public static IQueryable<T> AddStringNotNullFilter<T>(this IQueryable<T> query, Expression<Func<T, string>> expression)
{
    BinaryExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.NotEqual(a, b);
    return query.CreateQuery(null, expression.GetPropertyName(), Predicate);
}

private static IQueryable<T> AddStringFilter<T>(this IQueryable<T> query, string value, Expression<Func<T, string>> expression, MethodInfo method)
{
    if (!string.IsNullOrWhiteSpace(value))
    {
        MethodCallExpression Predicate(MemberExpression a, ConstantExpression b) => Expression.Call(a, method, b);
        return query.CreateQuery(value, expression.GetPropertyName(), Predicate);
    }

    return query;
}
```
## DapperExtension
```C#
 [Test]
public void TestUpdateWithSpecifiedColumnName()
{
    var log = DapperExtension.Connection.FindAll<Log>().FirstOrDefault();
    if (log != null)
    {
        int count = DapperExtension.Connection.Delete<Log>(log.Id);
        Assert.AreEqual(count, 1);
    }
}

[Test]
public void TestFindAllByExpression()
{
    using (var dapper = DapperExtension.Connection)
    {
        User user = DapperExtension.Connection.QueryFirst<User>();
        if (user != null)
        {
            var users = dapper.FindAll<User>(o => o.Id, user.Id);
            Assert.AreEqual(users.Count, 1);
            user = dapper.Find<User>(user.Id);
            Assert.IsNotNull(user);
            var roles = dapper.FindAll<Role>();
            Assert.IsNotNull(roles);
        }
    }
}

 [Test]
public async Task TestRecordCount()
{
    User user = DapperExtension.Connection.QueryFirst<User>();
    if (user != null)
    {
        int count = await DapperExtension.Connection.RecordCountAsync<User>();
        Assert.GreaterOrEqual(count, 0);
    }
}


  [Test]
public void TestDeleteById()
{
    Log log = DapperExtension.Connection.QueryFirst<Log>();
    if (log != null)
    {
        var count = DapperExtension.Connection.Delete<Log>(log.Id);
        Assert.AreEqual(count, 1);
    }

    log = DapperExtension.Connection.QueryFirst<Log>();
    if (log != null)
    {
        int count = DapperExtension.Connection.DeleteAsync<Log>(log.Id).Result;
        Assert.AreEqual(count, 1);
    }
}


[Test]
public void TestDeleteByObject()
{
    Log log = DapperExtension.Connection.QueryFirst<Log>();
    if (log != null)
    {
        var count = DapperExtension.Connection.Delete(log);
        Assert.AreEqual(count, 1);
    }
}

[Test]
public void TestDeleteByObjectAsync()
{
    Log log = DapperExtension.Connection.QueryFirst<Log>();
    if (log != null)
    {
        int count = DapperExtension.Connection.DeleteAsync(log).Result;
        Assert.AreEqual(count, 1);
    }
}

[Test]
public void TestDeleteByMultipleKeyObjectAsync()
{
    MultiplePrimaryKeyTable entity = DapperExtension.Connection.QueryFirst<MultiplePrimaryKeyTable>();
    if (entity != null)
    {
        var count = DapperExtension.Connection.DeleteAsync(entity).Result;
        Assert.AreEqual(count, 1);
    }
}

[Test]
public void TestDeleteByMultipleKeyObject()
{
    MultiplePrimaryKeyTable entity = DapperExtension.Connection.QueryFirst<MultiplePrimaryKeyTable>();
    if (entity != null)
    {
        var count = DapperExtension.Connection.Delete(entity);
        Assert.AreEqual(count, 1);
    }
}

 [Test]
public async Task TestInsertAsyncWithSpecifiedPrimaryKeyAsync()
{
    var log = new Log { LogLevel = (int)LogLevel.Information, CreateTime = DateTime.Now, Message = "TestInsertWithSpecifiedPrimaryKey" };
    var idTask = DapperExtension.Connection.InsertAsync(log);
    var id = await idTask;
    Assert.Greater(id, 0);
}

[Test]
public void TestInsertWithMultiplePrimaryKeys()
{
    var keyMaster = new MultiplePrimaryKeyTable { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N") };
    string id = DapperExtension.Connection.InsertReturnKey(keyMaster);
    Assert.IsNotNull(id);
}

[Test]
public async Task TestInsertAsyncWithMultiplePrimaryKeysAsync()
{
    Task<dynamic> task = DapperExtension.Connection.InsertAsync(new MultiplePrimaryKeyTable { Id = Guid.NewGuid().ToString("N"), Name = Guid.NewGuid().ToString("N") });
    var result = await task;
    Assert.IsNotNull(result);
}

 [Test]
public void TestUpdate()
{
    var message = Guid.NewGuid().ToString();
    var log = DapperExtension.Connection.QueryFirst<Log>();
    log.Message = message;
    DapperExtension.Connection.Update(log);
    var logFind = DapperExtension.Connection.Find<Log>(log.Id);

    Assert.AreEqual
```

PM>
 dotnet pack .\EntityFrameworkCore.Generator\EntityFrameworkCore.Generator.csproj -o ..s\..\..\.nuget\localpackages -c Release
 dotnet tool uninstall --global EFCore.Generator
 dotnet tool install --global EFCore.Generator
 cd .\core.Entity
 efg generate -c "Data Source=.;App=EntityFrameworkCore;Initial Catalog=Core;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"






