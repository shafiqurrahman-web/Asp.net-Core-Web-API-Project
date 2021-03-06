using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Provenance.DataLayer.Base
{

	public class WhereBuilder
	{
		// private readonly IProvider _provider;
		// private TableDefinition _tableDef;

		public WhereBuilder () //IProvider provider
		{
			// _provider = provider;
		}

		public WherePart ToSql<T> (Expression<Func<T, bool>> expression)
		{
			//_tableDef = _provider.GetTableDefinitionFor<T>();
			var i = 1;
			return Recurse(ref i, expression.Body, isUnary: true);
		}

		public string ToRawSql<T> (Expression<Func<T, bool>> expression)
		{
			//_tableDef = _provider.GetTableDefinitionFor<T>();
			var i = 1;
			var whereParts = Recurse(ref i, expression.Body, isUnary: true);

			if (!whereParts.Parameters.Any())
				return whereParts.Sql;

			StringBuilder finalQuery = new StringBuilder();
			finalQuery.Append(whereParts.Sql);
			foreach (var p in whereParts.Parameters)
			{
				var val = "@" + p.Key;
				finalQuery = finalQuery.Replace(val, p.Value.ToString());
			}
			return finalQuery.ToString();
		}

		private WherePart Recurse (ref int i, Expression expression, bool isUnary = false, string prefix = null, string postfix = null)
		{
			if (expression is UnaryExpression)
			{
				var unary = (UnaryExpression)expression;
				return WherePart.Concat(NodeTypeToString(unary.NodeType), Recurse(ref i, unary.Operand, true));
			}
			if (expression is BinaryExpression)
			{
				var body = (BinaryExpression)expression;
				return WherePart.Concat(Recurse(ref i, body.Left), NodeTypeToString(body.NodeType), Recurse(ref i, body.Right));
			}
			if (expression is ConstantExpression)
			{
				var constant = (ConstantExpression)expression;
				var value = constant.Value;
				if (value is int)
				{
					return WherePart.IsSql(value.ToString());
				}
				if (value is string)
				{
					if (prefix == null && postfix == null)
						value = "'" + (string) value + "'";
					else
						value = prefix + (string) value + postfix;
				}
				if (value is DateTime)
				{
					value = "'" + value + "'";
				}
				if (value is DateTime?)
				{
					value = "'" + (string) value + "'";
				}
				if (value is TimeSpan)
				{
					value = "'" + value + "'";
				}
				if (value is TimeSpan?)
				{
					value = "'" + (string) value + "'";
				}
				if (value is bool && isUnary)
				{
					return WherePart.Concat(WherePart.IsParameter(i++, value), "=", WherePart.IsSql("1"));
				}
				return WherePart.IsParameter(i++, value);
			}
			if (expression is MemberExpression)
			{
				var member = (MemberExpression)expression;

				if (member.Member is PropertyInfo)
				{
					var property = (PropertyInfo)member.Member;
					//var colName = _tableDef.GetColumnNameFor(property.Name);
					var colName = property.Name;
					if (isUnary && member.Type == typeof(bool))
					{
						return WherePart.Concat(Recurse(ref i, expression), "=", WherePart.IsParameter(i++, true));
					}
					return WherePart.IsSql("[" + colName + "]");
				}
				if (member.Member is FieldInfo)
				{
					var value = GetValue(member);
					if (value is string)
					{
						value = prefix + (string) value + postfix;
					}
					return WherePart.IsParameter(i++, value);
				}
				throw new Exception($"Expression does not refer to a property or field: {expression}");
			}
			if (expression is MethodCallExpression)
			{
				var methodCall = (MethodCallExpression)expression;
				// LIKE queries:
				if (methodCall.Method == typeof(string).GetMethod("Contains", new[] { typeof(string) }))
				{
					return WherePart.Concat(Recurse(ref i, methodCall.Object), "LIKE", Recurse(ref i, methodCall.Arguments[0], prefix: "'%", postfix: "%'"));
				}
				if (methodCall.Method == typeof(string).GetMethod("StartsWith", new[] { typeof(string) }))
				{
					return WherePart.Concat(Recurse(ref i, methodCall.Object), "LIKE", Recurse(ref i, methodCall.Arguments[0], prefix: "'", postfix: "%'"));
				}
				if (methodCall.Method == typeof(string).GetMethod("EndsWith", new[] { typeof(string) }))
				{
					return WherePart.Concat(Recurse(ref i, methodCall.Object), "LIKE", Recurse(ref i, methodCall.Arguments[0], prefix: "'%", postfix: "'"));
				}
				// IN queries:
				if (methodCall.Method.Name == "Contains")
				{
					Expression collection;
					Expression property;
					if (methodCall.Method.IsDefined(typeof(ExtensionAttribute)) && methodCall.Arguments.Count == 2)
					{
						collection = methodCall.Arguments[0];
						property = methodCall.Arguments[1];
					}
					else if (!methodCall.Method.IsDefined(typeof(ExtensionAttribute)) && methodCall.Arguments.Count == 1)
					{
						collection = methodCall.Object;
						property = methodCall.Arguments[0];
					}
					else
					{
						throw new Exception("Unsupported method call: " + methodCall.Method.Name);
					}
					var values = (IEnumerable)GetValue(collection);
					return WherePart.Concat(Recurse(ref i, property), "IN", WherePart.IsCollection(ref i, values));
				}
				throw new Exception("Unsupported method call: " + methodCall.Method.Name);
			}
			throw new Exception("Unsupported expression: " + expression.GetType().Name);
		}

		public string ValueToString (object value, bool isUnary, bool quote)
		{
			if (value is bool)
			{
				if (isUnary)
				{
					return (bool) value ? "(1=1)" : "(1=0)";
				}
				return (bool) value ? "1" : "0";
			}
			//return _provider.ValueToString(value, quote);
			return value.ToString();
		}

		private static object GetValue (Expression member)
		{
			// source: http://stackoverflow.com/a/2616980/291955
			var objectMember = Expression.Convert(member, typeof(object));
			var getterLambda = Expression.Lambda<Func<object>>(objectMember);
			var getter = getterLambda.Compile();
			return getter();
		}

		private static string NodeTypeToString (ExpressionType nodeType)
		{
			switch (nodeType)
			{
				case ExpressionType.Add:
					return "+";
				case ExpressionType.And:
					return "&";
				case ExpressionType.AndAlso:
					return "AND";
				case ExpressionType.Divide:
					return "/";
				case ExpressionType.Equal:
					return "=";
				case ExpressionType.ExclusiveOr:
					return "^";
				case ExpressionType.GreaterThan:
					return ">";
				case ExpressionType.GreaterThanOrEqual:
					return ">=";
				case ExpressionType.LessThan:
					return "<";
				case ExpressionType.LessThanOrEqual:
					return "<=";
				case ExpressionType.Modulo:
					return "%";
				case ExpressionType.Multiply:
					return "*";
				case ExpressionType.Negate:
					return "-";
				case ExpressionType.Not:
					return "NOT";
				case ExpressionType.NotEqual:
					return "<>";
				case ExpressionType.Or:
					return "|";
				case ExpressionType.OrElse:
					return "OR";
				case ExpressionType.Subtract:
					return "-";
			}
			throw new Exception($"Unsupported node type: {nodeType}");
		}
	}

	public class WherePart
	{
		public string Sql { get; set; }
		public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

		public static WherePart IsSql (string sql)
		{
			return new WherePart()
			{
				Parameters = new Dictionary<string, object>(),
				Sql = sql
			};
		}

		public static WherePart IsParameter (int count, object value)
		{
			return new WherePart()
			{
				Parameters = { { count.ToString(), value } },
				Sql = $"@{count}"
			};
		}

		public static WherePart IsCollection (ref int countStart, IEnumerable values)
		{
			var parameters = new Dictionary<string, object>();
			var sql = new StringBuilder("(");
			foreach (var value in values)
			{
				parameters.Add(countStart.ToString(), value);
				sql.Append($"@{countStart},");
				countStart++;
			}
			if (sql.Length == 1)
			{
				sql.Append("null,");
			}
			sql[sql.Length - 1] = ')';
			return new WherePart()
			{
				Parameters = parameters,
				Sql = sql.ToString()
			};
		}

		public static WherePart Concat (string @operator, WherePart operand)
		{
			return new WherePart()
			{
				Parameters = operand.Parameters,
				Sql = $"({@operator} {operand.Sql})"
			};
		}

		public static WherePart Concat (WherePart left, string @operator, WherePart right)
		{
			return new WherePart()
			{
				Parameters = left.Parameters.Union(right.Parameters).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
				Sql = $"({left.Sql} {@operator} {right.Sql})"
			};
		}
	}

}