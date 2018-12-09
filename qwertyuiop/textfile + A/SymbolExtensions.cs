using System;
using System.Linq.Expressions;
using System.Reflection;

namespace qwertyuiop
{
	public static class SymbolExtensions
	{
		public static MethodInfo GetMethodInfo<T, TResult>(Expression<Func<T, TResult>> expression)
		{
			var newExpression = (LambdaExpression) expression;

			if (!(newExpression.Body is MethodCallExpression outermostExpression))
			{
				throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
			}

			return outermostExpression.Method;
		}
	}
}