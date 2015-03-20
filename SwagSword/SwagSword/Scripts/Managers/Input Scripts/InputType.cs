using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

//Names: Danny Hawk

namespace SwagSword
{
    public abstract class InputType
    {
        abstract public bool IsDown();
        abstract public bool IsUp();
    }
}
