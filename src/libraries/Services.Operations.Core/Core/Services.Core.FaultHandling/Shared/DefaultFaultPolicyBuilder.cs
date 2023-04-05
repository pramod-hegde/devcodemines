using Services.Core.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Core.FaultHandling.Shared
{
    public delegate Exception ExceptionPredicate (Exception ex);

    [Export(typeof(ICompositionPart))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed partial class DefaultFaultPolicyBuilder : IFaultPolicyBuilder
    {
        IList<ExceptionPredicate> ExceptionPredicates { get; }

        IList<ConditionalContext> ConditionalContexts { get; }

        Delegate UnhandledContext { get; set; }

        public DefaultFaultPolicyBuilder ()
        {
            ExceptionPredicates = new List<ExceptionPredicate>();
            ConditionalContexts = new List<ConditionalContext>();
        }

        string ICompositionPart.Id => "DefaulFaultHandler";

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString ()
        {
            return base.ToString();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals (object obj)
        {
            return base.Equals(obj);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode ()
        {
            return base.GetHashCode();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType ()
        {
            return base.GetType();
        }

        object _data;

        IFaultPolicyBuilder IFaultPolicyBuilder.Init ()
        {
            return new DefaultFaultPolicyBuilder();
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.Handle<T> ()
        {
            ExceptionPredicates.Add(exception => exception is T ? exception : null);
            return this;
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.Handle<T> (Func<T, bool> exceptionPredicate)
        {
            ExceptionPredicates.Add(exception => exception is T texception && exceptionPredicate(texception) ? exception : null);
            return this;
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.Or<T> ()
        {
            ExceptionPredicates.Add(exception => exception is T ? exception : null);
            return this;
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.Or<T> (Func<T, bool> exceptionPredicate)
        {
            ExceptionPredicates.Add(exception => exception is T texception && exceptionPredicate(texception) ? exception : null);
            return this;
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.ForTransientIf<T> (Expression<Func<T, bool>> condition, Expression<Action<T>> action)
        {
            ConditionalContexts.Add(new ConditionalContext
            {
                Condition = condition.Compile(),
                Action = action.Compile(),
                ConditionType = Condition.If
            });
            return this;
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.ForTransientIf<T> (Expression<Func<T, bool>> condition, Expression<Action> action)
        {
            ConditionalContexts.Add(new ConditionalContext
            {
                Condition = condition.Compile(),
                Action = action.Compile(),
                ConditionType = Condition.If
            });
            return this;
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.ForTransientElseIf<T> (Expression<Func<T, bool>> condition, Expression<Action> action)
        {
            ConditionalContexts.Add(new ConditionalContext
            {
                Condition = condition.Compile(),
                Action = action.Compile(),
                ConditionType = Condition.ElseIf
            });
            return this;
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.ForTransientElseIf<T> (Expression<Func<T, bool>> condition, Expression<Action<T>> action)
        {
            ConditionalContexts.Add(new ConditionalContext
            {
                Condition = condition.Compile(),
                Action = action.Compile(),
                ConditionType = Condition.ElseIf
            });
            return this;
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.ForTransientElse (Expression<Action> action)
        {
            ConditionalContexts.Add(new ConditionalContext
            {
                Action = action.Compile(),
                ConditionType = Condition.Else
            });
            return this;
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.ForTransientElse<T> (Expression<Action<T>> action)
        {
            ConditionalContexts.Add(new ConditionalContext
            {
                Action = action.Compile(),
                ConditionType = Condition.Else
            });
            return this;
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.WithData<T> (T data)
        {
            _data = data;
            return this;
        }

        void IFaultPolicyBuilder.Run (Exception ex)
        {
            Exception handledException = ExceptionPredicates
                        .Select(predicate => predicate(ex))
                        .FirstOrDefault(e => e != null);

            if (handledException == null)
            {
                UnhandledContext.DynamicInvoke(_data);
            }
            else
            {
                if (ConditionalContexts.Count == 0)
                {
                    return;
                }
                HandleFault();
            }
        }

        private void HandleFault ()
        {
            for (int i = 0; i < ConditionalContexts.Count; i++)
            {
                var current = ConditionalContexts.ElementAtOrDefault(i);
                var next = ConditionalContexts.ElementAtOrDefault(i + 1);

                IsConditionalFork(current, next, out bool _fork);

                if (ExecCondition(current))
                {
                    ExecAction(current);
                    if (_fork)
                        break;
                }
            }
        }

        IFaultPolicyBuilder IFaultPolicyBuilder.Unhandled<T> (Expression<Action<T>> action)
        {
            UnhandledContext = action.Compile();
            return this;
        }

        private void ExecAction (ConditionalContext current)
        {
            if (current.Action == null)
            {
                return;
            }

            current.Action.DynamicInvoke();
        }

        bool ExecCondition (ConditionalContext current)
        {
            if (current.ConditionType == Condition.Else || current.Condition == null)
            {
                return true;
            }

            return Convert.ToBoolean(current.Condition.DynamicInvoke(_data));
        }

        void IsConditionalFork (ConditionalContext current, ConditionalContext next, out bool fork)
        {
            if (next == null)
            {
                fork = true;
                return;
            }

            fork = !(current.ConditionType == Condition.If && next.ConditionType == Condition.If);
        }
    }
}