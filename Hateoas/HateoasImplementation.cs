using System.Collections.Generic;

namespace FinancesAPI.Hateoas
{
    public class HateoasImplementation
    {
        
        public string url;
        public string protocol = "https://";
        public List<Link> actions = new List<Link>();
        public HateoasImplementation(string url, string protocol)
        {
            this.url = url;
            this.protocol = protocol;
        }

        public HateoasImplementation(string url)
        {
            this.url = url;
        }       

        public void AddAction(string rel, string method)
        {
            actions.Add(new Link(this.protocol + this.url, rel, method));
        }

        public Link[] GetActions(string code)
        {
            Link[] tempLinks = new Link[actions.Count];
            for (int i = 0; i < tempLinks.Length; i++)
                tempLinks[i] = new Link(actions[i].href, actions[i].rel, actions[i].method);

            foreach (var link in tempLinks)
                link.href = link.href+code;
            return tempLinks;
        }
    }
}