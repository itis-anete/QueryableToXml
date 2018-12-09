using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace qwertyuiop
{
	static class ExpressionLearning
	{
		internal static void ExpressionMain()
		{
			var aInstance = Expression.New(typeof(A).GetConstructors()[0]);
			var constant = Expression.Constant("00.0");
			var call = Expression.Call(aInstance, ToStringMethod, constant);
			var cw = Expression.Call(MethodWriteLine, call);
			Expression.Lambda<Action>(cw).Compile().DynamicInvoke();
		}

		private static MethodInfo ToStringMethod
		{
			get
			{
				return typeof(A).GetMethods()
					.First(x => x.Name == "ToString" &&
					            x.GetParameters()[0]
						            .ParameterType ==
					            typeof(string));
			}
		}

		private static MethodInfo MethodWriteLine
		{
			get
			{
				return typeof(Console).GetMethods()
					.First(x => x.Name == "WriteLine" &&
					            x.GetParameters()[0].ParameterType == typeof(string));
			}
		}
	}
}


//            IQueryable<A> aFile = new TextFile<A>();
//
//            var data1 = aFile
//                .Where(x => x.Id > 0)
//                .Select(x => x.Id);
//                //.ToList();
//
//            Expression<Func<A, bool>> lambda1 = x => x.Id > 0;
//            var whereAFile = Expression.Call(aFile.Expression, null /* where */, lambda1);
//            aFile = aFile.Provider.CreateQuery<A>(whereAFile);
//
//            var d4Xml = XmlQueryable<D4>.ReadFromXml();
//            ShowXml(d4Xml);
//
//            var data = d4Xml
//                .Where(x => x.Name == "d4n0n_myself");
//
//            var list = data
//                .Select(x => x.Name)
//                .ToList();
//
//            ShowXml(data);
//
//            foreach (var e in list)
//            {
//                Console.WriteLine(e);
//            }

//            Expression<Func<D4, bool>> lambda = x => x.Name == "d4n0n_myself";
//            Expression<Func<D4, string>> selectLambda = x => x.Name;
////            var methodInfo = SymbolExtensions.GetMethodInfo<IQueryable<D4>, IQueryable<D4>>(z => z
////                .Where(x => x.Name == "d4n0n_myself"));
//            var selectMethodInfo =
//                SymbolExtensions.GetMethodInfo<IQueryable<D4>, IQueryable<string>>(z => z.Select(x => x.Name));
////            var a213 = (MethodInfo)MethodBase.GetCurrentMethod();
//            var selectd4Xml = Expression.Call(d4Xml.Expression, selectMethodInfo /*methodInfo*/, selectLambda);
//            d4Xml = d4Xml.Provider.CreateQuery<D4>(selectd4Xml);


//
//public static void CompareCompilatorAndManual()
//{
//Console.WriteLine(new A().ToString("00.0")); // 1 
//
//ExpressionLearning.ExpressionMain(); // 2
//}