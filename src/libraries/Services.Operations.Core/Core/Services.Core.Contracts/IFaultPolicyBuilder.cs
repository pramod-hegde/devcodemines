using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Core.Contracts
{
    public interface IFaultPolicyBuilder : ICompositionPart
    {
        IFaultPolicyBuilder Init ();
        IFaultPolicyBuilder Handle<T> () where T : Exception;
        IFaultPolicyBuilder Handle<T> (Func<T, bool> exceptionPredicate) where T : Exception;
        IFaultPolicyBuilder Or<T> () where T : Exception;
        IFaultPolicyBuilder Or<T> (Func<T, bool> exceptionPredicate) where T : Exception;
        IFaultPolicyBuilder ForTransientIf<T> (Expression<Func<T, bool>> condition, Expression<Action<T>> action);
        IFaultPolicyBuilder ForTransientIf<T> (Expression<Func<T, bool>> condition, Expression<Action> action);
        IFaultPolicyBuilder ForTransientElseIf<T> (Expression<Func<T, bool>> condition, Expression<Action> action);
        IFaultPolicyBuilder ForTransientElseIf<T> (Expression<Func<T, bool>> condition, Expression<Action<T>> action);
        IFaultPolicyBuilder ForTransientElse (Expression<Action> action);
        IFaultPolicyBuilder ForTransientElse<T> (Expression<Action<T>> action);
        IFaultPolicyBuilder Unhandled<T> (Expression<Action<T>> action);
        IFaultPolicyBuilder WithData<T> (T data);       
        void Run (Exception ex);
    }
}
