using EstudoCriptografia.Extensions;
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
        [EncryptedBuddy]
        public IEnumerable<InnerInnerViewModel> ListInnerInner { get; set; }
    }
}