using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace qwertyuiop
{
	public class XmlQueryProvider : IQueryProvider
	{
		public IQueryable CreateQuery(Expression expression)
		{
			var elementType = TypeSystem.GetElementType(expression.Type);

			try
			{
				return (IQueryable) Activator
					.CreateInstance(typeof(XmlQueryable)
						.MakeGenericType(elementType), this, expression);
			}

			catch (TargetInvocationException tie)
			{
				throw tie.InnerException;
			}
		}

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			if (typeof(TElement) != typeof(D4))
				throw new NotSupportedException("Element type must be " +
				                                typeof(D4));

			return (IQueryable<TElement>) new XmlQueryable(expression, this);
		}

		public object Execute(Expression expression)
		{
			if (!(expression is MethodCallExpression))
				throw new NotImplementedException();

			return new Visitor().Execute(expression);
		}

		public TResult Execute<TResult>(Expression expression) => (TResult) Execute(expression);
	}
}