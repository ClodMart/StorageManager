using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageManagerMobile.Resources
{
    public class PageLink
    {
        public string Label { get; set; }
        public string Page { get; set; }

        public PageLink(string label, string page)
        {
            Label = label; Page = page;
        }
    }
}
