using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Core.Extension
{
  internal static class CachedReflectionInfo
  {
    private static MethodInfo s_Aggregate_TSource_2;
    private static MethodInfo s_Aggregate_TSource_TAccumulate_3;
    private static MethodInfo s_Aggregate_TSource_TAccumulate_TResult_4;
    private static MethodInfo s_All_TSource_2;
    private static MethodInfo s_Any_TSource_1;
    private static MethodInfo s_Any_TSource_2;
    private static MethodInfo s_Average_Int32_1;
    private static MethodInfo s_Average_NullableInt32_1;
    private static MethodInfo s_Average_Int64_1;
    private static MethodInfo s_Average_NullableInt64_1;
    private static MethodInfo s_Average_Single_1;
    private static MethodInfo s_Average_NullableSingle_1;
    private static MethodInfo s_Average_Double_1;
    private static MethodInfo s_Average_NullableDouble_1;
    private static MethodInfo s_Average_Decimal_1;
    private static MethodInfo s_Average_NullableDecimal_1;
    private static MethodInfo s_Average_Int32_TSource_2;
    private static MethodInfo s_Average_NullableInt32_TSource_2;
    private static MethodInfo s_Average_Single_TSource_2;
    private static MethodInfo s_Average_NullableSingle_TSource_2;
    private static MethodInfo s_Average_Int64_TSource_2;
    private static MethodInfo s_Average_NullableInt64_TSource_2;
    private static MethodInfo s_Average_Double_TSource_2;
    private static MethodInfo s_Average_NullableDouble_TSource_2;
    private static MethodInfo s_Average_Decimal_TSource_2;
    private static MethodInfo s_Average_NullableDecimal_TSource_2;
    private static MethodInfo s_Cast_TResult_1;
    private static MethodInfo s_Concat_TSource_2;
    private static MethodInfo s_Contains_TSource_2;
    private static MethodInfo s_Contains_TSource_3;
    private static MethodInfo s_Count_TSource_1;
    private static MethodInfo s_Count_TSource_2;
    private static MethodInfo s_DefaultIfEmpty_TSource_1;
    private static MethodInfo s_DefaultIfEmpty_TSource_2;
    private static MethodInfo s_Distinct_TSource_1;
    private static MethodInfo s_Distinct_TSource_2;
    private static MethodInfo s_ElementAt_TSource_2;
    private static MethodInfo s_ElementAtOrDefault_TSource_2;
    private static MethodInfo s_Except_TSource_2;
    private static MethodInfo s_Except_TSource_3;
    private static MethodInfo s_First_TSource_1;
    private static MethodInfo s_First_TSource_2;
    private static MethodInfo s_FirstOrDefault_TSource_1;
    private static MethodInfo s_FirstOrDefault_TSource_2;
    private static MethodInfo s_GroupBy_TSource_TKey_2;
    private static MethodInfo s_GroupBy_TSource_TKey_3;
    private static MethodInfo s_GroupBy_TSource_TKey_TElement_3;
    private static MethodInfo s_GroupBy_TSource_TKey_TElement_4;
    private static MethodInfo s_GroupBy_TSource_TKey_TResult_3;
    private static MethodInfo s_GroupBy_TSource_TKey_TResult_4;
    private static MethodInfo s_GroupBy_TSource_TKey_TElement_TResult_4;
    private static MethodInfo s_GroupBy_TSource_TKey_TElement_TResult_5;
    private static MethodInfo s_GroupJoin_TOuter_TInner_TKey_TResult_5;
    private static MethodInfo s_GroupJoin_TOuter_TInner_TKey_TResult_6;
    private static MethodInfo s_Intersect_TSource_2;
    private static MethodInfo s_Intersect_TSource_3;
    private static MethodInfo s_Join_TOuter_TInner_TKey_TResult_5;
    private static MethodInfo s_Join_TOuter_TInner_TKey_TResult_6;
    private static MethodInfo s_Last_TSource_1;
    private static MethodInfo s_Last_TSource_2;
    private static MethodInfo s_LastOrDefault_TSource_1;
    private static MethodInfo s_LastOrDefault_TSource_2;
    private static MethodInfo s_LongCount_TSource_1;
    private static MethodInfo s_LongCount_TSource_2;
    private static MethodInfo s_Max_TSource_1;
    private static MethodInfo s_Max_TSource_TResult_2;
    private static MethodInfo s_Min_TSource_1;
    private static MethodInfo s_Min_TSource_TResult_2;
    private static MethodInfo s_OfType_TResult_1;
    private static MethodInfo s_OrderBy_TSource_TKey_2;
    private static MethodInfo s_OrderBy_TSource_TKey_3;
    private static MethodInfo s_OrderByDescending_TSource_TKey_2;
    private static MethodInfo s_OrderByDescending_TSource_TKey_3;
    private static MethodInfo s_Reverse_TSource_1;
    private static MethodInfo s_Select_TSource_TResult_2;
    private static MethodInfo s_Select_Index_TSource_TResult_2;
    private static MethodInfo s_SelectMany_TSource_TResult_2;
    private static MethodInfo s_SelectMany_Index_TSource_TResult_2;
    private static MethodInfo s_SelectMany_Index_TSource_TCollection_TResult_3;
    private static MethodInfo s_SelectMany_TSource_TCollection_TResult_3;
    private static MethodInfo s_SequenceEqual_TSource_2;
    private static MethodInfo s_SequenceEqual_TSource_3;
    private static MethodInfo s_Single_TSource_1;
    private static MethodInfo s_Single_TSource_2;
    private static MethodInfo s_SingleOrDefault_TSource_1;
    private static MethodInfo s_SingleOrDefault_TSource_2;
    private static MethodInfo s_Skip_TSource_2;
    private static MethodInfo s_SkipWhile_TSource_2;
    private static MethodInfo s_SkipWhile_Index_TSource_2;
    private static MethodInfo s_Sum_Int32_1;
    private static MethodInfo s_Sum_NullableInt32_1;
    private static MethodInfo s_Sum_Int64_1;
    private static MethodInfo s_Sum_NullableInt64_1;
    private static MethodInfo s_Sum_Single_1;
    private static MethodInfo s_Sum_NullableSingle_1;
    private static MethodInfo s_Sum_Double_1;
    private static MethodInfo s_Sum_NullableDouble_1;
    private static MethodInfo s_Sum_Decimal_1;
    private static MethodInfo s_Sum_NullableDecimal_1;
    private static MethodInfo s_Sum_NullableDecimal_TSource_2;
    private static MethodInfo s_Sum_Int32_TSource_2;
    private static MethodInfo s_Sum_NullableInt32_TSource_2;
    private static MethodInfo s_Sum_Int64_TSource_2;
    private static MethodInfo s_Sum_NullableInt64_TSource_2;
    private static MethodInfo s_Sum_Single_TSource_2;
    private static MethodInfo s_Sum_NullableSingle_TSource_2;
    private static MethodInfo s_Sum_Double_TSource_2;
    private static MethodInfo s_Sum_NullableDouble_TSource_2;
    private static MethodInfo s_Sum_Decimal_TSource_2;
    private static MethodInfo s_Take_TSource_2;
    private static MethodInfo s_TakeWhile_TSource_2;
    private static MethodInfo s_TakeWhile_Index_TSource_2;
    private static MethodInfo s_ThenBy_TSource_TKey_2;
    private static MethodInfo s_ThenBy_TSource_TKey_3;
    private static MethodInfo s_ThenByDescending_TSource_TKey_2;
    private static MethodInfo s_ThenByDescending_TSource_TKey_3;
    private static MethodInfo s_Union_TSource_2;
    private static MethodInfo s_Union_TSource_3;
    private static MethodInfo s_Where_TSource_2;
    private static MethodInfo s_Where_Index_TSource_2;
    private static MethodInfo s_Zip_TFirst_TSecond_TResult_3;
    private static MethodInfo s_SkipLast_TSource_2;
    private static MethodInfo s_TakeLast_TSource_2;
    private static MethodInfo s_Append_TSource_2;
    private static MethodInfo s_Prepend_TSource_2;

    public static MethodInfo Aggregate_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Aggregate_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Aggregate_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, object, object>>, object>(Queryable.Aggregate<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Aggregate_TSource_TAccumulate_3(
      Type TSource,
      Type TAccumulate)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_3 = new Func<IQueryable<object>, object, Expression<Func<object, object, object>>, object>(Queryable.Aggregate<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TAccumulate);
    }

    public static MethodInfo Aggregate_TSource_TAccumulate_TResult_4(
      Type TSource,
      Type TAccumulate,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_TResult_4;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Aggregate_TSource_TAccumulate_TResult_4 = new Func<IQueryable<object>, object, Expression<Func<object, object, object>>, Expression<Func<object, object>>, object>(Queryable.Aggregate<object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TAccumulate, TResult);
    }

    public static MethodInfo All_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_All_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_All_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, bool>(Queryable.All<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Any_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Any_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Any_TSource_1 = new Func<IQueryable<object>, bool>(Queryable.Any<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Any_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Any_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Any_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, bool>(Queryable.Any<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Average_Int32_1
    {
      get
      {
        MethodInfo averageInt321 = CachedReflectionInfo.s_Average_Int32_1;
        if ((object) averageInt321 != null)
          return averageInt321;
        return CachedReflectionInfo.s_Average_Int32_1 = new Func<IQueryable<int>, double>(Queryable.Average).GetMethodInfo();
      }
    }

    public static MethodInfo Average_NullableInt32_1
    {
      get
      {
        MethodInfo averageNullableInt321 = CachedReflectionInfo.s_Average_NullableInt32_1;
        if ((object) averageNullableInt321 != null)
          return averageNullableInt321;
        return CachedReflectionInfo.s_Average_NullableInt32_1 = new Func<IQueryable<int?>, double?>(Queryable.Average).GetMethodInfo();
      }
    }

    public static MethodInfo Average_Int64_1
    {
      get
      {
        MethodInfo averageInt641 = CachedReflectionInfo.s_Average_Int64_1;
        if ((object) averageInt641 != null)
          return averageInt641;
        return CachedReflectionInfo.s_Average_Int64_1 = new Func<IQueryable<long>, double>(Queryable.Average).GetMethodInfo();
      }
    }

    public static MethodInfo Average_NullableInt64_1
    {
      get
      {
        MethodInfo averageNullableInt641 = CachedReflectionInfo.s_Average_NullableInt64_1;
        if ((object) averageNullableInt641 != null)
          return averageNullableInt641;
        return CachedReflectionInfo.s_Average_NullableInt64_1 = new Func<IQueryable<long?>, double?>(Queryable.Average).GetMethodInfo();
      }
    }

    public static MethodInfo Average_Single_1
    {
      get
      {
        MethodInfo averageSingle1 = CachedReflectionInfo.s_Average_Single_1;
        if ((object) averageSingle1 != null)
          return averageSingle1;
        return CachedReflectionInfo.s_Average_Single_1 = new Func<IQueryable<float>, float>(Queryable.Average).GetMethodInfo();
      }
    }

    public static MethodInfo Average_NullableSingle_1
    {
      get
      {
        MethodInfo averageNullableSingle1 = CachedReflectionInfo.s_Average_NullableSingle_1;
        if ((object) averageNullableSingle1 != null)
          return averageNullableSingle1;
        return CachedReflectionInfo.s_Average_NullableSingle_1 = new Func<IQueryable<float?>, float?>(Queryable.Average).GetMethodInfo();
      }
    }

    public static MethodInfo Average_Double_1
    {
      get
      {
        MethodInfo averageDouble1 = CachedReflectionInfo.s_Average_Double_1;
        if ((object) averageDouble1 != null)
          return averageDouble1;
        return CachedReflectionInfo.s_Average_Double_1 = new Func<IQueryable<double>, double>(Queryable.Average).GetMethodInfo();
      }
    }

    public static MethodInfo Average_NullableDouble_1
    {
      get
      {
        MethodInfo averageNullableDouble1 = CachedReflectionInfo.s_Average_NullableDouble_1;
        if ((object) averageNullableDouble1 != null)
          return averageNullableDouble1;
        return CachedReflectionInfo.s_Average_NullableDouble_1 = new Func<IQueryable<double?>, double?>(Queryable.Average).GetMethodInfo();
      }
    }

    public static MethodInfo Average_Decimal_1
    {
      get
      {
        MethodInfo averageDecimal1 = CachedReflectionInfo.s_Average_Decimal_1;
        if ((object) averageDecimal1 != null)
          return averageDecimal1;
        return CachedReflectionInfo.s_Average_Decimal_1 = new Func<IQueryable<Decimal>, Decimal>(Queryable.Average).GetMethodInfo();
      }
    }

    public static MethodInfo Average_NullableDecimal_1
    {
      get
      {
        MethodInfo nullableDecimal1 = CachedReflectionInfo.s_Average_NullableDecimal_1;
        if ((object) nullableDecimal1 != null)
          return nullableDecimal1;
        return CachedReflectionInfo.s_Average_NullableDecimal_1 = new Func<IQueryable<Decimal?>, Decimal?>(Queryable.Average).GetMethodInfo();
      }
    }

    public static MethodInfo Average_Int32_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Average_Int32_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Average_Int32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int>>, double>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Average_NullableInt32_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Average_NullableInt32_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Average_NullableInt32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int?>>, double?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Average_Single_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Average_Single_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Average_Single_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float>>, float>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Average_NullableSingle_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Average_NullableSingle_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Average_NullableSingle_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float?>>, float?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Average_Int64_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Average_Int64_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Average_Int64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long>>, double>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Average_NullableInt64_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Average_NullableInt64_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Average_NullableInt64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long?>>, double?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Average_Double_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Average_Double_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Average_Double_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double>>, double>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Average_NullableDouble_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Average_NullableDouble_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Average_NullableDouble_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double?>>, double?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Average_Decimal_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Average_Decimal_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Average_Decimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, Decimal>>, Decimal>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Average_NullableDecimal_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Average_NullableDecimal_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Average_NullableDecimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, Decimal?>>, Decimal?>(Queryable.Average<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Cast_TResult_1(Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Cast_TResult_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Cast_TResult_1 = new Func<IQueryable, IQueryable<object>>(Queryable.Cast<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TResult);
    }

    public static MethodInfo Concat_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Concat_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Concat_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Concat<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Contains_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Contains_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Contains_TSource_2 = new Func<IQueryable<object>, object, bool>(Queryable.Contains<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Contains_TSource_3(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Contains_TSource_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Contains_TSource_3 = new Func<IQueryable<object>, object, IEqualityComparer<object>, bool>(Queryable.Contains<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Count_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Count_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Count_TSource_1 = new Func<IQueryable<object>, int>(Queryable.Count<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Count_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Count_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Count_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, int>(Queryable.Count<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo DefaultIfEmpty_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_DefaultIfEmpty_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_DefaultIfEmpty_TSource_1 = new Func<IQueryable<object>, IQueryable<object>>(Queryable.DefaultIfEmpty<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo DefaultIfEmpty_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_DefaultIfEmpty_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_DefaultIfEmpty_TSource_2 = new Func<IQueryable<object>, object, IQueryable<object>>(Queryable.DefaultIfEmpty<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Distinct_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Distinct_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Distinct_TSource_1 = new Func<IQueryable<object>, IQueryable<object>>(Queryable.Distinct<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Distinct_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Distinct_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Distinct_TSource_2 = new Func<IQueryable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Distinct<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo ElementAt_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_ElementAt_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_ElementAt_TSource_2 = new Func<IQueryable<object>, int, object>(Queryable.ElementAt<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo ElementAtOrDefault_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_ElementAtOrDefault_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_ElementAtOrDefault_TSource_2 = new Func<IQueryable<object>, int, object>(Queryable.ElementAtOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Except_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Except_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Except_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Except<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Except_TSource_3(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Except_TSource_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Except_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Except<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo First_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_First_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_First_TSource_1 = new Func<IQueryable<object>, object>(Queryable.First<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo First_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_First_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_First_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.First<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo FirstOrDefault_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_FirstOrDefault_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_FirstOrDefault_TSource_1 = new Func<IQueryable<object>, object>(Queryable.FirstOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo FirstOrDefault_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_FirstOrDefault_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_FirstOrDefault_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.FirstOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo GroupBy_TSource_TKey_2(Type TSource, Type TKey)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey);
    }

    public static MethodInfo GroupBy_TSource_TKey_3(Type TSource, Type TKey)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, IEqualityComparer<object>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey);
    }

    public static MethodInfo GroupBy_TSource_TKey_TElement_3(
      Type TSource,
      Type TKey,
      Type TElement)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey, TElement);
    }

    public static MethodInfo GroupBy_TSource_TKey_TElement_4(
      Type TSource,
      Type TKey,
      Type TElement)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_4;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_4 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, IEqualityComparer<object>, IQueryable<IGrouping<object, object>>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey, TElement);
    }

    public static MethodInfo GroupBy_TSource_TKey_TResult_3(
      Type TSource,
      Type TKey,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IQueryable<object>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey, TResult);
    }

    public static MethodInfo GroupBy_TSource_TKey_TResult_4(
      Type TSource,
      Type TKey,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_4;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TResult_4 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.GroupBy<object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey, TResult);
    }

    public static MethodInfo GroupBy_TSource_TKey_TElement_TResult_4(
      Type TSource,
      Type TKey,
      Type TElement,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_4;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_4 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IQueryable<object>>(Queryable.GroupBy<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey, TElement, TResult);
    }

    public static MethodInfo GroupBy_TSource_TKey_TElement_TResult_5(
      Type TSource,
      Type TKey,
      Type TElement,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_5;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_GroupBy_TSource_TKey_TElement_TResult_5 = new Func<IQueryable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.GroupBy<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey, TElement, TResult);
    }

    public static MethodInfo GroupJoin_TOuter_TInner_TKey_TResult_5(
      Type TOuter,
      Type TInner,
      Type TKey,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_5;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_5 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IQueryable<object>>(Queryable.GroupJoin<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TOuter, TInner, TKey, TResult);
    }

    public static MethodInfo GroupJoin_TOuter_TInner_TKey_TResult_6(
      Type TOuter,
      Type TInner,
      Type TKey,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_6;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_GroupJoin_TOuter_TInner_TKey_TResult_6 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, IEnumerable<object>, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.GroupJoin<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TOuter, TInner, TKey, TResult);
    }

    public static MethodInfo Intersect_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Intersect_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Intersect_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Intersect<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Intersect_TSource_3(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Intersect_TSource_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Intersect_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Intersect<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Join_TOuter_TInner_TKey_TResult_5(
      Type TOuter,
      Type TInner,
      Type TKey,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_5;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_5 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.Join<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TOuter, TInner, TKey, TResult);
    }

    public static MethodInfo Join_TOuter_TInner_TKey_TResult_6(
      Type TOuter,
      Type TInner,
      Type TKey,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_6;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Join_TOuter_TInner_TKey_TResult_6 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object>>, Expression<Func<object, object>>, Expression<Func<object, object, object>>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Join<object, object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TOuter, TInner, TKey, TResult);
    }

    public static MethodInfo Last_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Last_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Last_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Last<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Last_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Last_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Last_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.Last<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo LastOrDefault_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_LastOrDefault_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_LastOrDefault_TSource_1 = new Func<IQueryable<object>, object>(Queryable.LastOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo LastOrDefault_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_LastOrDefault_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_LastOrDefault_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.LastOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo LongCount_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_LongCount_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_LongCount_TSource_1 = new Func<IQueryable<object>, long>(Queryable.LongCount<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo LongCount_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_LongCount_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_LongCount_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, long>(Queryable.LongCount<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Max_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Max_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Max_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Max<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Max_TSource_TResult_2(Type TSource, Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Max_TSource_TResult_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Max_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, object>(Queryable.Max<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TResult);
    }

    public static MethodInfo Min_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Min_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Min_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Min<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Min_TSource_TResult_2(Type TSource, Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Min_TSource_TResult_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Min_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, object>(Queryable.Min<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TResult);
    }

    public static MethodInfo OfType_TResult_1(Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_OfType_TResult_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_OfType_TResult_1 = new Func<IQueryable, IQueryable<object>>(Queryable.OfType<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TResult);
    }

    public static MethodInfo OrderBy_TSource_TKey_2(Type TSource, Type TKey)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_OrderBy_TSource_TKey_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_OrderBy_TSource_TKey_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.OrderBy<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey);
    }

    public static MethodInfo OrderBy_TSource_TKey_3(Type TSource, Type TKey)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_OrderBy_TSource_TKey_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_OrderBy_TSource_TKey_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.OrderBy<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey);
    }

    public static MethodInfo OrderByDescending_TSource_TKey_2(Type TSource, Type TKey)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_OrderByDescending_TSource_TKey_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_OrderByDescending_TSource_TKey_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.OrderByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey);
    }

    public static MethodInfo OrderByDescending_TSource_TKey_3(Type TSource, Type TKey)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_OrderByDescending_TSource_TKey_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_OrderByDescending_TSource_TKey_3 = new Func<IQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.OrderByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey);
    }

    public static MethodInfo Reverse_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Reverse_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Reverse_TSource_1 = new Func<IQueryable<object>, IQueryable<object>>(Queryable.Reverse<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Select_TSource_TResult_2(Type TSource, Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Select_TSource_TResult_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Select_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, object>>, IQueryable<object>>(Queryable.Select<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TResult);
    }

    public static MethodInfo Select_Index_TSource_TResult_2(Type TSource, Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Select_Index_TSource_TResult_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Select_Index_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, int, object>>, IQueryable<object>>(Queryable.Select<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TResult);
    }

    public static MethodInfo SelectMany_TSource_TResult_2(Type TSource, Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SelectMany_TSource_TResult_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SelectMany_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, IEnumerable<object>>>, IQueryable<object>>(Queryable.SelectMany<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TResult);
    }

    public static MethodInfo SelectMany_Index_TSource_TResult_2(
      Type TSource,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SelectMany_Index_TSource_TResult_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SelectMany_Index_TSource_TResult_2 = new Func<IQueryable<object>, Expression<Func<object, int, IEnumerable<object>>>, IQueryable<object>>(Queryable.SelectMany<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TResult);
    }

    public static MethodInfo SelectMany_Index_TSource_TCollection_TResult_3(
      Type TSource,
      Type TCollection,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SelectMany_Index_TSource_TCollection_TResult_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SelectMany_Index_TSource_TCollection_TResult_3 = new Func<IQueryable<object>, Expression<Func<object, int, IEnumerable<object>>>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.SelectMany<object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TCollection, TResult);
    }

    public static MethodInfo SelectMany_TSource_TCollection_TResult_3(
      Type TSource,
      Type TCollection,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SelectMany_TSource_TCollection_TResult_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SelectMany_TSource_TCollection_TResult_3 = new Func<IQueryable<object>, Expression<Func<object, IEnumerable<object>>>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.SelectMany<object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TCollection, TResult);
    }

    public static MethodInfo SequenceEqual_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SequenceEqual_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SequenceEqual_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, bool>(Queryable.SequenceEqual<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo SequenceEqual_TSource_3(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SequenceEqual_TSource_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SequenceEqual_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, bool>(Queryable.SequenceEqual<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Single_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Single_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Single_TSource_1 = new Func<IQueryable<object>, object>(Queryable.Single<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Single_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Single_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Single_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.Single<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo SingleOrDefault_TSource_1(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SingleOrDefault_TSource_1;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SingleOrDefault_TSource_1 = new Func<IQueryable<object>, object>(Queryable.SingleOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo SingleOrDefault_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SingleOrDefault_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SingleOrDefault_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, object>(Queryable.SingleOrDefault<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Skip_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Skip_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Skip_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.Skip<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo SkipWhile_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SkipWhile_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SkipWhile_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.SkipWhile<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo SkipWhile_Index_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SkipWhile_Index_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SkipWhile_Index_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int, bool>>, IQueryable<object>>(Queryable.SkipWhile<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Sum_Int32_1
    {
      get
      {
        MethodInfo sumInt321 = CachedReflectionInfo.s_Sum_Int32_1;
        if ((object) sumInt321 != null)
          return sumInt321;
        return CachedReflectionInfo.s_Sum_Int32_1 = new Func<IQueryable<int>, int>(Queryable.Sum).GetMethodInfo();
      }
    }

    public static MethodInfo Sum_NullableInt32_1
    {
      get
      {
        MethodInfo sumNullableInt321 = CachedReflectionInfo.s_Sum_NullableInt32_1;
        if ((object) sumNullableInt321 != null)
          return sumNullableInt321;
        return CachedReflectionInfo.s_Sum_NullableInt32_1 = new Func<IQueryable<int?>, int?>(Queryable.Sum).GetMethodInfo();
      }
    }

    public static MethodInfo Sum_Int64_1
    {
      get
      {
        MethodInfo sumInt641 = CachedReflectionInfo.s_Sum_Int64_1;
        if ((object) sumInt641 != null)
          return sumInt641;
        return CachedReflectionInfo.s_Sum_Int64_1 = new Func<IQueryable<long>, long>(Queryable.Sum).GetMethodInfo();
      }
    }

    public static MethodInfo Sum_NullableInt64_1
    {
      get
      {
        MethodInfo sumNullableInt641 = CachedReflectionInfo.s_Sum_NullableInt64_1;
        if ((object) sumNullableInt641 != null)
          return sumNullableInt641;
        return CachedReflectionInfo.s_Sum_NullableInt64_1 = new Func<IQueryable<long?>, long?>(Queryable.Sum).GetMethodInfo();
      }
    }

    public static MethodInfo Sum_Single_1
    {
      get
      {
        MethodInfo sumSingle1 = CachedReflectionInfo.s_Sum_Single_1;
        if ((object) sumSingle1 != null)
          return sumSingle1;
        return CachedReflectionInfo.s_Sum_Single_1 = new Func<IQueryable<float>, float>(Queryable.Sum).GetMethodInfo();
      }
    }

    public static MethodInfo Sum_NullableSingle_1
    {
      get
      {
        MethodInfo sumNullableSingle1 = CachedReflectionInfo.s_Sum_NullableSingle_1;
        if ((object) sumNullableSingle1 != null)
          return sumNullableSingle1;
        return CachedReflectionInfo.s_Sum_NullableSingle_1 = new Func<IQueryable<float?>, float?>(Queryable.Sum).GetMethodInfo();
      }
    }

    public static MethodInfo Sum_Double_1
    {
      get
      {
        MethodInfo sumDouble1 = CachedReflectionInfo.s_Sum_Double_1;
        if ((object) sumDouble1 != null)
          return sumDouble1;
        return CachedReflectionInfo.s_Sum_Double_1 = new Func<IQueryable<double>, double>(Queryable.Sum).GetMethodInfo();
      }
    }

    public static MethodInfo Sum_NullableDouble_1
    {
      get
      {
        MethodInfo sumNullableDouble1 = CachedReflectionInfo.s_Sum_NullableDouble_1;
        if ((object) sumNullableDouble1 != null)
          return sumNullableDouble1;
        return CachedReflectionInfo.s_Sum_NullableDouble_1 = new Func<IQueryable<double?>, double?>(Queryable.Sum).GetMethodInfo();
      }
    }

    public static MethodInfo Sum_Decimal_1
    {
      get
      {
        MethodInfo sumDecimal1 = CachedReflectionInfo.s_Sum_Decimal_1;
        if ((object) sumDecimal1 != null)
          return sumDecimal1;
        return CachedReflectionInfo.s_Sum_Decimal_1 = new Func<IQueryable<Decimal>, Decimal>(Queryable.Sum).GetMethodInfo();
      }
    }

    public static MethodInfo Sum_NullableDecimal_1
    {
      get
      {
        MethodInfo nullableDecimal1 = CachedReflectionInfo.s_Sum_NullableDecimal_1;
        if ((object) nullableDecimal1 != null)
          return nullableDecimal1;
        return CachedReflectionInfo.s_Sum_NullableDecimal_1 = new Func<IQueryable<Decimal?>, Decimal?>(Queryable.Sum).GetMethodInfo();
      }
    }

    public static MethodInfo Sum_NullableDecimal_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Sum_NullableDecimal_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Sum_NullableDecimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, Decimal?>>, Decimal?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Sum_Int32_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Sum_Int32_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Sum_Int32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int>>, int>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Sum_NullableInt32_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Sum_NullableInt32_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Sum_NullableInt32_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int?>>, int?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Sum_Int64_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Sum_Int64_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Sum_Int64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long>>, long>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Sum_NullableInt64_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Sum_NullableInt64_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Sum_NullableInt64_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, long?>>, long?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Sum_Single_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Sum_Single_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Sum_Single_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float>>, float>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Sum_NullableSingle_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Sum_NullableSingle_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Sum_NullableSingle_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, float?>>, float?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Sum_Double_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Sum_Double_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Sum_Double_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double>>, double>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Sum_NullableDouble_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Sum_NullableDouble_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Sum_NullableDouble_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, double?>>, double?>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Sum_Decimal_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Sum_Decimal_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Sum_Decimal_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, Decimal>>, Decimal>(Queryable.Sum<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Take_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Take_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Take_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.Take<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo TakeWhile_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_TakeWhile_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_TakeWhile_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.TakeWhile<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo TakeWhile_Index_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_TakeWhile_Index_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_TakeWhile_Index_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int, bool>>, IQueryable<object>>(Queryable.TakeWhile<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo ThenBy_TSource_TKey_2(Type TSource, Type TKey)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_ThenBy_TSource_TKey_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_ThenBy_TSource_TKey_2 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.ThenBy<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey);
    }

    public static MethodInfo ThenBy_TSource_TKey_3(Type TSource, Type TKey)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_ThenBy_TSource_TKey_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_ThenBy_TSource_TKey_3 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.ThenBy<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey);
    }

    public static MethodInfo ThenByDescending_TSource_TKey_2(Type TSource, Type TKey)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_ThenByDescending_TSource_TKey_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_ThenByDescending_TSource_TKey_2 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IOrderedQueryable<object>>(Queryable.ThenByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey);
    }

    public static MethodInfo ThenByDescending_TSource_TKey_3(Type TSource, Type TKey)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_ThenByDescending_TSource_TKey_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_ThenByDescending_TSource_TKey_3 = new Func<IOrderedQueryable<object>, Expression<Func<object, object>>, IComparer<object>, IOrderedQueryable<object>>(Queryable.ThenByDescending<object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource, TKey);
    }

    public static MethodInfo Union_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Union_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Union_TSource_2 = new Func<IQueryable<object>, IEnumerable<object>, IQueryable<object>>(Queryable.Union<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Union_TSource_3(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Union_TSource_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Union_TSource_3 = new Func<IQueryable<object>, IEnumerable<object>, IEqualityComparer<object>, IQueryable<object>>(Queryable.Union<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Where_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Where_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Where_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, bool>>, IQueryable<object>>(Queryable.Where<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Where_Index_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Where_Index_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Where_Index_TSource_2 = new Func<IQueryable<object>, Expression<Func<object, int, bool>>, IQueryable<object>>(Queryable.Where<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Zip_TFirst_TSecond_TResult_3(
      Type TFirst,
      Type TSecond,
      Type TResult)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Zip_TFirst_TSecond_TResult_3;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Zip_TFirst_TSecond_TResult_3 = new Func<IQueryable<object>, IEnumerable<object>, Expression<Func<object, object, object>>, IQueryable<object>>(Queryable.Zip<object, object, object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TFirst, TSecond, TResult);
    }

    public static MethodInfo SkipLast_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_SkipLast_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_SkipLast_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.SkipLast<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo TakeLast_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_TakeLast_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_TakeLast_TSource_2 = new Func<IQueryable<object>, int, IQueryable<object>>(Queryable.TakeLast<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Append_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Append_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Append_TSource_2 = new Func<IQueryable<object>, object, IQueryable<object>>(Queryable.Append<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }

    public static MethodInfo Prepend_TSource_2(Type TSource)
    {
      MethodInfo methodInfo = CachedReflectionInfo.s_Prepend_TSource_2;
      if ((object) methodInfo == null)
        methodInfo = CachedReflectionInfo.s_Prepend_TSource_2 = new Func<IQueryable<object>, object, IQueryable<object>>(Queryable.Prepend<object>).GetMethodInfo().GetGenericMethodDefinition();
      return methodInfo.MakeGenericMethod(TSource);
    }
  }
}
