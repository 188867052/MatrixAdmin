using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Tools.ExpressionExtension;

namespace Core.Web.Grid
{
    public class ColumnConfiguration<T>
    {
        public IList<T> EntityList;
        public List<TextColumn<T>> TextColumns;
        public List<BooleanColumn<T>> BooleanColumns;
        public List<DateTimeColumn<T>> DateTimeColumns;
        public List<EnumColumn<T>> EnumColumns;
        public ColumnConfiguration(IList<T> list)
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
                    string value = item.Expression.GetValue(entity);
                    tbody += $"<td>{value}</td>";
                }

                foreach (var item in DateTimeColumns)
                {
                    DateTime value = item.Expression.GetValue(entity);
                    tbody += $"<td>{value}</td>";
                }

                foreach (var item in EnumColumns)
                {
                    Enum value = item.Expression.GetValue(entity);
                    tbody += $"<td>{value}</td>";
                }

                tbody += $"<tr>{tbody}</tr>";
            }

            string table = $"<table class=\"table table-bordered data-table\"><thead><tr>{thead}</tr></thead><tbody>{tbody}</tbody></table>";
            return table;
        }


    }
}