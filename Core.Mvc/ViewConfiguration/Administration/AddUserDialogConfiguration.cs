using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Extension.Expression;
using Core.Model;
using Core.Model.Administration.User;
using Core.Web.ViewConfiguration;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.Mvc.ViewConfiguration.Administration
{
    public class AddUserDialogConfiguration : DialogConfiguration<User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entity"></param>
        public AddUserDialogConfiguration(ResponseModel entity) : base(entity)
        {
        }

        public override string Title
        {
            get { return "添加用户"; }
        }


        public override string Footer
        {
            get
            {
                return "<button type=\"submit\" class=\"btn btn-primary\">提交</button>" +
                       "<button type=\"button\" class=\"btn btn-secondary\" data-dismiss=\"modal\">关闭</button>";
            }
        }


        public override string Body
        {
            get
            {
                List<TextBox<UserPostModel>> list = new List<TextBox<UserPostModel>>();
                list.Add(new TextBox<UserPostModel>(o => o.LoginName, "登录名"));
                list.Add(new TextBox<UserPostModel>(o => o.DisplayName, "显示名"));
                list.Add(new TextBox<UserPostModel>(o => o.Password, "密码"));

                string html = default;
                foreach (var VARIABLE in list)
                {
                    html += VARIABLE.Render();
                }
                return html;
            }
        }
    }



    public class TextBox<TPostModel>
    {
        private readonly Expression<Func<TPostModel, string>> _expression;
        private string lable;

        public TextBox(Expression<Func<TPostModel, string>> _expression, string label)
        {
            this._expression = _expression;
            this.lable = label;
        }

        public string Render()
        {
            string name = _expression.GetPropertyInfo().Name;
            string html = $"<div class=\"form-group\">" +
                          $"<label for=\"pwd\">登录名:</label>" +
                          $"<input type=\"password\" name=\"{name}\" class=\"form-control\" id=\"pwd\">" +
                          $"</div>";
            return "";
        }
    }
}