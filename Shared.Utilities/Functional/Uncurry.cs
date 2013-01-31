using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Functional
{
    public static class Uncurry
    {
        public static Func<T1, T2, TResult> Apply<T1, T2, TResult>(Func<T1, Func<T2, TResult>> func)
        {
            return (a, b) => func(a)(b);
        }

        public static Func<T1, T2, T3, TResult> Apply<T1, T2, T3, TResult>(Func<T1, Func<T2, Func<T3, TResult>>> func)
        {
            return (a, b, c) => func(a)(b)(c);
        }

        public static Func<T1, T2, T3, T4, TResult> Apply<T1, T2, T3, T4, TResult>(Func<T1, Func<T2, Func<T3, Func<T4, TResult>>>> func)
        {
            return (a, b, c, d) => func(a)(b)(c)(d);
        }

        public static Func<T1, T2, T3, T4, T5, TResult> Apply<T1, T2, T3, T4, T5, TResult>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, TResult>>>>> func)
        {
            return (a, b, c, d, e) => func(a)(b)(c)(d)(e);
        }

        public static Func<T1, T2, T3, T4, T5, T6, TResult> Apply<T1, T2, T3, T4, T5, T6, TResult>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, TResult>>>>>> func)
        {
            return (a, b, c, d, e, f) => func(a)(b)(c)(d)(e)(f);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, TResult>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, TResult>>>>>>> func)
        {
            return (a, b, c, d, e, f, g) => func(a)(b)(c)(d)(e)(f)(g);
        }

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Apply<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, TResult>>>>>>>> func)
        {
            return (a, b, c, d, e, f, g, h) => func(a)(b)(c)(d)(e)(f)(g)(h);
        }
    }
}
