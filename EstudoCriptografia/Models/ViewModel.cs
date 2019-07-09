using Cryptographer;
using EstudoCriptografia.Extensions;
using System.Collections.Generic;

namespace EstudoCriptografia.Models
{
    public class ViewModel
    {
        public ViewModel()
        {
            ListInnerViewModel = new List<InnerViewModel>();
        }

        [EncryptedOnView]
        public string Id { get; set; }
        public string Nome { get; set; }

        [EncryptedOnView]
        public IEnumerable<InnerViewModel> ListInnerViewModel { get; set; }
    }
}