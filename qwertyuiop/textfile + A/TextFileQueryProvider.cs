using System;
using System.Linq;
using System.Linq.Expressions;

namespace qwertyuiop
{
	class TextFileQueryProvider : IQueryProvider
	{
		public IQueryable CreateQuery(Expression expression) => throw new NotImplementedException();

		public IQueryable<TElement> CreateQuery<TElement>(Expression expression) =>
			new TextFile<TElement>(expression, this);

		public object Execute(Expression expression) => throw new NotImplementedException();

		public TResult Execute<TResult>(Expression expression)
		{
			var constFileArray = Expression.Constant(new A[]
			{
				new A {Id = 50},
				new A {Id = 20},
				new A {Id = -5}
			}.AsQueryable());

			if (!(expression is MethodCallExpression lambdaIf))
				throw new ArgumentException(nameof(expression));

			var whereLambda = lambdaIf.Arguments[1];

			var method = lambdaIf.Method;

			var newExpression = Expression.Call(null, method, constFileArray, whereLambda);

			var compiledResult = Expression.Lambda<Func<IQueryable<A>>>(newExpression)
				.Compile()
				.Invoke();

			return (TResult) compiledResult.GetEnumerator();
		}
	}
}