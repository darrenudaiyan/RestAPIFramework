using System.Collections.Generic;

namespace Rest.API.Tests.ObjectModels.WhiskeyModel
{
    public class WhiskeyResponse
    {
        public string context { get; set; }
        public List<Whiskey> value { get; set; }
    }
}
