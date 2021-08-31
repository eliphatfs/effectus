using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media;

namespace EffectusReunion.VirtualTransport
{
    /// <summary>
    /// Global states for transport control.
    /// </summary>
    public class VirtualTransportControl
    {
        public TimeSpan Time { get; set; } = TimeSpan.Zero;
        public MediaTimelineController MediaTimelineController
        {
            get; private set;
        } = new MediaTimelineController();
    }
}
