using System.Collections.Generic;

namespace Rest.API.Tests.ObjectModels.AssayModel
{
    public class AssayResponse
    {
        public string context { get; set; }
        public List<Assay> value { get; set; }
    }
}
