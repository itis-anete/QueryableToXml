using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;

namespace qwertyuiop
{
    public class XmlQueryable : IQueryable<D4>
    {
        public static D4 All { get; }

        public XmlQueryable(Expression expression, IQueryProvider provider)
        {
            Expression = expression;
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public XmlQueryable()
        {
            Expression = Expression.Constant(this);
            Provider = new XmlQueryProvider();
        }

        public IEnumerator<D4> GetEnumerator()
        {
            return Provider.Execute<IEnumerable<D4>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType { get; } = typeof(D4);
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }

        public static IQueryable<D4> ReadFromXml()
        {
            var reader = XmlReader.Create(File.OpenRead("../../../file1.xml"));
            var restrictedWords = new[] {"xml", "Root"};
            var list = new List<D4>();

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Whitespace ||
                    restrictedWords.Contains(reader.Name)) continue;
                var name = reader.Name;
                reader.MoveToAttribute("Value");
                list.Add(new D4(name, reader.Value));
            }

            return list.AsQueryable();
        }
    }

    /// <summary>
    /// художественный фильм сп***или
    /// </summary>
    internal static class TypeSystem
    {
        internal static Type GetElementType(Type seqType)
        {
            var ienum = FindIEnumerable(seqType);
            return ienum == null ? seqType : ienum.GetGenericArguments()[0];
        }


        private static Type FindIEnumerable(Type seqType)
        {

            if (seqType == null || seqType == typeof(string))
                return null;

            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());

            if (seqType.IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {

                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);

                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }

            var ifaces = seqType.GetInterfaces();

            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (var iface in ifaces)
                {

                    var ienum = FindIEnumerable(iface);

                    if (ienum != null) return ienum;
                }
            }

            if (seqType.BaseType != null && seqType.BaseType != typeof(object))
            {
                return FindIEnumerable(seqType.BaseType);
            }

            return null;
        }
    }


//    public class FileSystemQueryContext
//    {
//        internal static object Execute(Expression expression, bool isEnumerable)
//        {
//            var queryableElements = GetAllFilesAndFolders();
//
//            // Copy the expression tree that was passed in, changing only the first
//            // argument of the innermost MethodCallExpression.
//            var treeCopier = new Visitor(queryableElements);
//            var newExpressionTree = treeCopier.Visit(expression);
//
//            // This step creates an IQueryable that executes by replacing Queryable methods with Enumerable methods.
//            return isEnumerable
//                ? queryableElements.Provider.CreateQuery(newExpressionTree)
//                : queryableElements.Provider.Execute(newExpressionTree);
//        }
//
//        private static IQueryable<D4> GetAllFilesAndFolders()
//        {
//            var reader = XmlReader.Create(File.OpenRead("file1.xml"));
//            var restrictedWords = new[] {"xml", "Root"};
//            var list = new List<D4>();
//            
//            while (reader.Read())
//            {
//                if (reader.NodeType == XmlNodeType.Whitespace ||
//                    restrictedWords.Contains(reader.Name)) continue;
//                var name = reader.Name;
//                reader.MoveToAttribute("Value");
//                list.Add(new D4(name,reader.Value));
//            }
//
//            var xmlQueryable = new XmlQueryable();
//            foreach (var e in list)
//            {
//                xmlQueryable.Append(e);
//            }
//
//            return xmlQueryable;
//        }
//    }
}