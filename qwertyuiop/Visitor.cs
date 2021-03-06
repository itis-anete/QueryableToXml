﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace qwertyuiop
{
	internal class Visitor : ExpressionVisitor
	{
		public IEnumerable<D4> Execute(Expression expression)
		{
			var visitationResult = (ConstantExpression) Visit(expression) ?? throw new ArgumentNullException(nameof(expression));
			var returnedTokens = (IEnumerable<D4>) visitationResult.Value;
			return returnedTokens
				.Select(x => x);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			if (!(node.Value is XmlQueryable))
				throw new NotSupportedException($"Value of type {typeof(XmlQueryable)} expected");

			var d4S = XmlQueryable.ReadFromXml();
			return Expression.Constant(d4S);
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			switch (node.Method.Name)
			{
				case "Select":
					return HandleSelectMethod(node);
				case "Where":
					return HandleWhereMethod(node);
				default:
					throw new NotSupportedException($"Method \"{node.Method.Name}\" is not supported");
			}
		}

		private Expression HandleSelectMethod(MethodCallExpression node)
		{
			var argument = (ConstantExpression) Visit(node.Arguments[0]) ?? throw new ArgumentNullException(nameof(node.Arguments));
			var d4S = (IEnumerable<D4>) argument.Value;

			var lambda = GetLambda(node.Arguments[1]);
			var selectOption = ((PropertyInfo) ((MemberExpression) lambda.Body).Member).Name;
			switch (selectOption)
			{
				case "All":
					break;
				default:
					throw new NotSupportedException("Unknown option in Select method");
			}

			return Expression.Constant(d4S);
		}

		private Expression HandleWhereMethod(MethodCallExpression node)
		{
			var argument = (ConstantExpression) Visit(node.Arguments[0]) ?? throw new ArgumentNullException(nameof(node.Arguments));

			var d4S = (IEnumerable<D4>) argument.Value;

			var lambda = (Expression<Func<D4, bool>>)
				GetLambda(node.Arguments[1]);
			var predicate = lambda.Compile();
			d4S = d4S.Where(x => predicate(x));
			return Expression.Constant(d4S);
		}

		private static LambdaExpression GetLambda(Expression expression)
		{
			var lambdaExpression = expression.NodeType == ExpressionType.Quote
				? ((UnaryExpression) expression).Operand
				: expression;

			return (LambdaExpression) lambdaExpression;
		}
	}
}