using System;
using SimpleCqrs.Interface;

namespace SimpleCqrs.Implementation
{
    public static class Either
    {
        public static IEither<Tl, Tr> Create<Tl, Tr>(Tl value)
        {
            return new LeftOperand<Tl, Tr>(value);
        }

        public static IEither<Tl, Tr> Create<Tl, Tr>(Tr value)
        {
            return new RightOperand<Tl, Tr>(value);
        }

        private abstract class Operand<T>
        {
            protected readonly T value;

            protected Operand(T value) { this.value = value; }

            protected TU Case<TU>(Func<T, TU> func)
            {
                if (func == null) throw new ArgumentNullException(nameof(func));

                return func(this.value);
            }

            protected void Case(Action<T> action)
            {
                if (action == null) throw new ArgumentNullException(nameof(action));

                action(this.value);
            }
        }

        private sealed class LeftOperand<Tl, Tr> : Operand<Tl>, IEither<Tl, Tr>
        {
            public LeftOperand(Tl value) : base(value) { }

            public TU Case<TU>(Func<Tl, TU> ofLeft, Func<Tr, TU> ofRight)
                => base.Case(ofLeft);

            public IEither<Tl, TU2r> Right<TU2r>(Func<Tr, TU2r> ofRight)
                => Create<Tl, TU2r>(this.value);

            public IEither<Tl, TU2r> Right<TU2r>(Func<Tr, IEither<Tl, TU2r>> ofRight)
                => Create<Tl, TU2r>(this.value);
        }

        private sealed class RightOperand<Tl, Tr> : Operand<Tr>, IEither<Tl, Tr>
        {
            public RightOperand(Tr value) : base(value) { }

            public TU Case<TU>(Func<Tl, TU> ofLeft, Func<Tr, TU> ofRight)
                => base.Case(ofRight);

            public IEither<Tl, TU2r> Right<TU2r>(Func<Tr, TU2r> ofRight)
                => Create<Tl, TU2r>(ofRight(this.value));

            public IEither<Tl, TU2r> Right<TU2r>(Func<Tr, IEither<Tl, TU2r>> ofRight)
                => ofRight(this.value);
        }
    }
}