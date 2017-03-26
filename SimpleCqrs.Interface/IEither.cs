using System;

namespace SimpleCqrs.Interface
{
    public interface IEither<Tl, Tr>
    {
        void Case(Action<Tl> ofLeft, Action<Tr> ofRight);

        TU Case<TU>(Func<Tl, TU> ofLeft, Func<Tr, TU> ofRight);

        IEither<TUl, TUr> Case<TUl, TUr>(Func<Tl, TUl> ofLeft, Func<Tr, TUr> ofRight);

        IEither<Tl, TU2r> Right<TU2r>(Func<Tr, IEither<Tl, TU2r>> ofRight);

        IEither<Tl, TU2r> Right<TU2r>(Func<Tr, TU2r> ofRight);
    }
}