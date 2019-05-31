# Asp.Net
.Net Core 2.1,MVC, WebApi,EntityFrameWork Core,bootstrap4,jquery,Restful,前后端分离，javascript原型链，Dapper，自定义UI框架,SwaggerUI,Linq表达式树，T4模板,NUnit单元测试
后端：WebApi+EfCore+Dapper兼具开发速度和性能，批处理可以使用dapper 
前端：基于bootstrap4封装的一套组件，GridSearchFilter，Dialog，RowContextMenu，
一些表单提交，做BS开发不用重复造轮子，js采用严格模式，兼容性好，代码量少，不用写开发人员不用写html，统一了UI，开发人员只需要写业务

Example of the using grid search filters
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
```

Example of the using RowContextMenu
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

Example of the using DialogConfiguration
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

Example of the using GridConfiguration
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

Example of the using SearchFilterConfiguration
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

Example of the using index.js
```C#
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

Example of the using SearchGridPage
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