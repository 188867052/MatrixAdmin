using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Web.Grid
{
    public class Column<T>
    {
        public IList<T> EntityList;
        public List<TextColumn<T>> TextColumns;
        public List<BooleanColumn<T>> BooleanColumns;
        public List<DateTimeColumn<T>> DateTimeColumns;
        public List<EnumColumn<T>> EnumColumns;
        public Column(IList<T> list)
        {
            this.TextColumns = new List<TextColumn<T>>();
            this.BooleanColumns = new List<BooleanColumn<T>>();
            this.DateTimeColumns = new List<DateTimeColumn<T>>();
            this.EnumColumns = new List<EnumColumn<T>>();
            this.EntityList = list;
        }

        public void AddBooleanColumn(BooleanColumn<T> column)
        {
            this.BooleanColumns.Add(column);
        }

        public void AddTextColumn(TextColumn<T> column)
        {
            this.TextColumns.Add(column);
        }

        public void AddDateTimeColumn(DateTimeColumn<T> column)
        {
            this.DateTimeColumns.Add(column);
        }
        public void AddEnumColumn(EnumColumn<T> column)
        {
            this.EnumColumns.Add(column);
        }

        public string Render()
        {
            string thead = default;
            foreach (var item in TextColumns)
            {
                thead += $"<th>{item.Thead}</th>";
            }
            foreach (var item in DateTimeColumns)
            {
                thead += $"<th>{item.Thead}</th>";
            }
            foreach (var item in EnumColumns)
            {
                thead += $"<th>{item.Thead}</th>";
            }

            string tbody = default;
            foreach (var entity in EntityList)
            {
                foreach (var item in TextColumns)
                {
                    string value = this.GetStringValue(item.Expression, entity);
                    tbody += $"<td>{value}</td>";
                }

                foreach (var item in DateTimeColumns)
                {
                    DateTime value = this.GetDateTimeValue(item.Expression, entity);
                    tbody += $"<td>{value}</td>";
                }

                foreach (var item in EnumColumns)
                {
                    Enum value = this.GetEnumValue(item.Expression, entity);
                    tbody += $"<td>{value}</td>";
                }

                tbody += $"<tr>{tbody}</tr>";
            }

            string table = $"<table class=\"table table-bordered data-table\"><thead><tr>{thead}</tr></thead><tbody>{tbody}</tbody></table>";
            return table;
        }

        private string GetStringValue(Expression<Func<T, string>> expression, object instance)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            string propertyName = memberExpression.Member.Name;
            var property = typeof(T).GetProperties().First(l => l.Name == propertyName);
            var obj = property.GetValue(instance);
            return obj is null ? default : property.GetValue(instance).ToString();
        }

        private bool GetBooleanValue(Expression<Func<T, bool>> expression, object instance)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            string propertyName = memberExpression.Member.Name;
            var property = typeof(T).GetProperties().First(l => l.Name == propertyName);
            return (bool)property.GetValue(instance);
        }

        private DateTime GetDateTimeValue(Expression<Func<T, DateTime>> expression, object instance)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            string propertyName = memberExpression.Member.Name;
            var property = typeof(T).GetProperties().First(o => o.Name == propertyName);
            return (DateTime)property.GetValue(instance);
        }

        private Enum GetEnumValue(Expression<Func<T, Enum>> expression, object instance)
        {
            string memberExpression = expression.Body.ToString();
            string propertyName = memberExpression.Split('.', ',')[1];
            var property = typeof(T).GetProperties().First(l => l.Name == propertyName);
            return (Enum)property.GetValue(instance);
        }
    }
}