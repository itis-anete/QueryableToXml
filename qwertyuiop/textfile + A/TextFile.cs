using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace qwertyuiop
{
	class TextFile<T> : IQueryable<T>
	{
		public TextFile()
		{
			Expression = Expression.Constant(this);
			Provider = new TextFileQueryProvider();
		}

		public TextFile(Expression expression, IQueryProvider queryProvider)
		{
			Expression = expression;
			Provider = queryProvider;
		}

		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public Type ElementType { get; }
		public Expression Expression { get; }
		public IQueryProvider Provider { get; }
	}
}