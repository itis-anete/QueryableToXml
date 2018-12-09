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
				                                typeof(D4).ToString());

			return (IQueryable<TElement>) new XmlQueryable(expression, this);
//            Execute(expression);
//            return null;
		}

		public object Execute(Expression expression)
		{
			if (!(expression is MethodCallExpression lambdaIf))
				throw new NotImplementedException();

//            var argument = (UnaryExpression) lambdaIf.Arguments[1];
//            var operand = (LambdaExpression) argument.Operand;
//            var body = (BinaryExpression)operand.Body;
//            var field = (MemberExpression) body.Left;
			//return Execute<D4>(expression);
			return new Visitor().Execute(expression);
		}

		public TResult Execute<TResult>(Expression expression)
		{
//            var isEnumerable = (typeof(TResult).Name == "IEnumerable`1");
//            return (TResult) FileSystemQueryContext.Execute(expression, isEnumerable);
			return (TResult) Execute(expression);
		}
	}
}