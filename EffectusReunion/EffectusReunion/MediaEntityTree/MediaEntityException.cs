using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectusReunion.MediaEntityTree
{

    [Serializable]
    public class MediaEntityException : Exception
    {
        public MediaEntityException()
        {
        }
        public MediaEntityException(string message) : base(message) { }
        public MediaEntityException(string message, Exception inner) : base(message, inner) { }
        protected MediaEntityException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
