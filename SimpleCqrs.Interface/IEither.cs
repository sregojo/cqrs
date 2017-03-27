using System;

namespace SimpleCqrs.Interface
{
    public interface IEither<Tl, Tr>
    {
        TU Case<TU>(Func<Tl, TU> ofLeft, Func<Tr, TU> ofRight);

        IEither<Tl, TU2r> Right<TU2r>(Func<Tr, IEither<Tl, TU2r>> ofRight);

        IEither<Tl, TU2r> Right<TU2r>(Func<Tr, TU2r> ofRight);
    }
}