using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  TuneLab_Extensions.Utils;


namespace TuneLab_Extensions.Animation;

internal class AnimationColor : AnimationProperty<Color>
{
    protected override Color Lerp(Color t1, Color t2, double ratio)
    {
        return t1.Lerp(t2, ratio);
    }
}
