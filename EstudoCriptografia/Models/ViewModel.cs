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

        [EncryptedBuddy]
        public string Id { get; set; }
        public string Nome { get; set; }

        [EncryptedBuddy]
        public IEnumerable<InnerViewModel> ListInnerViewModel { get; set; }
    }
}