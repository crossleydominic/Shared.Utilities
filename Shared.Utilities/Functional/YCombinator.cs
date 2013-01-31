using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Functional
{
    public static class YCombinator
    {
        public static Func<T1, TResult> Apply<T1, TResult>(Func<Func<T1, TResult>, Func<T1, TResult>> func)
        {
            Recursive<T1, TResult> rec = r => a => func(r(r))(a);
            return rec(rec);
        }

        public static Func<T1, T2, TResult> Apply<T1, T2, TResult>(Func<Func<T1, T2, TResult>, Func<T1, T2, TResult>> func)
        {
            Recursive<T1, T2, TResult> rec = r => (a, b) => func(r(r))(a, b);
            return rec(rec);
        }

        public static Func<T1, T2, T3, TResult> Apply<T1, T2, T3, TResult>(Func<Func<T1, T2, T3, TResult>, Func<T1, T2, T3, TResult>> func)
        {
            Recursive<T1, T2, T3, TResult> rec = r => (a, b, c) => func(r(r))(a, b, c);
            return rec(rec);
        }

        public static Func<T1, T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(Func<Func<T1, T2, T3, T4, TResult>, Func<T1, T2, T3, T4, TResult>> func)
        {
            Recursive<T1, T2, T3, T4, TResult> rec = r => (a, b, c, d) => func(r(r))(a, b, c, d);
            return rec(rec);
        }

        public static Func<T1, T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(Func<Func<T1, T2, T3, T4, T5, TResult>, Func<T1, T2, T3, T4, T5, TResult>> func)
        {
            Recursive<T1, T2, T3, T4, T5, TResult> rec = r => (a, b, c, d, e) => func(r(r))(a, b, c, d, e);
            return rec(rec);
        }

        public static Func<T1, T2, T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(Func<Func<T1, T2, T3, T4, T5, T6, TResult>, Func<T1, T2, T3, T4, T5, T6, TResult>> func)
        {
            Recursive<T1, T2, T3, T4, T5, T6, TResult> rec = r => (a, b, c, d, e, f) => func(r(r))(a, b, c, d, e, f);
            return rec(rec);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<Func<T1, T2, T3, T4, T5, T6, T7, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, TResult>> func)
        {
            Recursive<T1, T2, T3, T4, T5, T6, T7, TResult> rec = r => (a, b, c, d, e, f, g) => func(r(r))(a, b, c, d, e, f, g);
            return rec(rec);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>, Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> func)
        {
            Recursive<T1, T2, T3, T4, T5, T6, T7, T8, TResult> rec = r => (a, b, c, d, e, f, g, h) => func(r(r))(a, b, c, d, e, f, g, h);
            return rec(rec);
        }
    }
}
