using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media;

namespace EffectusReunion.MediaTransportSystem
{
    /// <summary>
    /// Global states for transport control.
    /// </summary>
    public class TransportControl
    {
        // TODO: Differentiate Preview/Rendering?
        public TimeSpan Time { get; set; } = TimeSpan.Zero;
    }
}
