using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shared.Utilities.Functional
{
    delegate Func<T1, TResult> Recursive<T1, TResult>(Recursive<T1, TResult> r);
    delegate Func<T1, T2, TResult> Recursive<T1, T2, TResult>(Recursive<T1, T2, TResult> r);
    delegate Func<T1, T2, T3, TResult> Recursive<T1, T2, T3, TResult>(Recursive<T1, T2, T3, TResult> r);
    delegate Func<T1, T2, T3, T4, TResult> Recursive<T1, T2, T3, T4, TResult>(Recursive<T1, T2, T3, T4, TResult> r);
    delegate Func<T1, T2, T3, T4, T5, TResult> Recursive<T1, T2, T3, T4, T5, TResult>(Recursive<T1, T2, T3, T4, T5, TResult> r);
    delegate Func<T1, T2, T3, T4, T5, T6, TResult> Recursive<T1, T2, T3, T4, T5, T6, TResult>(Recursive<T1, T2, T3, T4, T5, T6, TResult> r);
    delegate Func<T1, T2, T3, T4, T5, T6, T7, TResult> Recursive<T1, T2, T3, T4, T5, T6, T7, TResult>(Recursive<T1, T2, T3, T4, T5, T6, T7, TResult> r);
    delegate Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Recursive<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(Recursive<T1, T2, T3, T4, T5, T6, T7, T8, TResult> r);
}
