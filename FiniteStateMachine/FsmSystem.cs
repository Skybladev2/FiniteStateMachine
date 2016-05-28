using System;
using System.Collections.Generic;

namespace Skyblade
{
    public class FsmSystem<TState> where TState: class
    {
        private Dictionary<TState, Dictionary<object, TState>> routes = new Dictionary<TState, Dictionary<object, TState>>();
        private TState currentState;

        public TState GetCurrentState()
        {
            return currentState;
        }

        public void AddTransition<TEnum>(TState from, TEnum transition, TState to) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            CheckEnum<TEnum>();

            if (!routes.ContainsKey(from))
            {
                routes.Add(from, new Dictionary<object, TState>());
            }

            routes[from][transition] = to;
        }

        public void PerformTransition<TEnum>(TEnum transition) where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            CheckEnum<TEnum>();

            TState nextState = routes[currentState][transition];
            currentState = nextState;
        }

        public void SetInitialState(TState initialState)
        {
            if (!routes.ContainsKey(initialState))
            {
                throw new ArgumentException("State machine does not contain this state.", "initialState");
            }

            currentState = initialState;
        }

        private void CheckEnum<TEnum>() where TEnum : struct, IConvertible, IComparable, IFormattable
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enum.");
            }
        }
    }
}
