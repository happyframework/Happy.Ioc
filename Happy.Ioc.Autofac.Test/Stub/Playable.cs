using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Autofac.Test.Stub
{
    public sealed class Playable : IPlayable
    {
        public string Play()
        {
            return "Play";
        }
    }
}
