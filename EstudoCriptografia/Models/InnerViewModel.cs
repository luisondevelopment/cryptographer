using Cryptographer;
using System.Collections.Generic;

namespace EstudoCriptografia.Models
{
    public class InnerViewModel
    {
        public InnerViewModel()
        {
            ListInnerInner = new List<InnerInnerViewModel>();
        }

        public string Nome { get; set; }
        [EncryptedOnView]
        public IEnumerable<InnerInnerViewModel> ListInnerInner { get; set; }
    }
}